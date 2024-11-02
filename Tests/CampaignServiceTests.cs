using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using APIAnallyzer_v2.Data;
using APIAnallyzer_v2.DTOs;
using APIAnallyzer_v2.Models;
using APIAnallyzer_v2.Services;
using Moq;
using MongoDB.Driver;
using Xunit;

namespace APIAnallyzer_v2.Tests
{
    public class CampaignServiceTests
    {
        private readonly Mock<IMongoCollection<Campaign>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly Mock<MongoDbService> _mockMongoDbService;
        private readonly Mock<ValidationService> _mockValidationService;
        private readonly CampaignService _campaignService;

        public CampaignServiceTests()
        {
            _mockCollection = new Mock<IMongoCollection<Campaign>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockMongoDbService = new Mock<MongoDbService>();
            _mockValidationService = new Mock<ValidationService>(Mock.Of<HttpClient>());

            // Configurando o mock do MongoDbService
            _mockMongoDbService.Setup(x => x.Database).Returns(_mockDatabase.Object);
            _mockDatabase.Setup(x => x.GetCollection<Campaign>("campaigns", null)).Returns(_mockCollection.Object);

            _campaignService = new CampaignService(_mockMongoDbService.Object, _mockValidationService.Object);
        }

        [Fact]
        public async Task GetCampaignsAsync_ReturnsAllCampaigns()
        {
            // Arrange
            var campaigns = new List<Campaign>
            {
                new Campaign { Id = "1", Name = "Campanha 1", Description = "Descrição 1", Company = "Empresa 1" },
                new Campaign { Id = "2", Name = "Campanha 2", Description = "Descrição 2", Company = "Empresa 2" }
            };

            _mockCollection.Setup(x => x.Find(FilterDefinition<Campaign>.Empty, null).ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(campaigns);

            // Act
            var result = await _campaignService.GetCampaignsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateCampaignAsync_ValidCampaign_CreatesCampaign()
        {
            // Arrange
            var campaignDto = new CampaignDTO
            {
                Name = "Nova Campanha",
                Description = "Descrição da nova campanha",
                Company = "Empresa Nova",
                CNPJ = "12345678000195",
                Email = "email@empresa.com",
                StartDate = DateTime.UtcNow,
                ForecastDate = DateTime.UtcNow.AddDays(10),
                Status = "Ativa"
            };

            _mockValidationService.Setup(x => x.IsValidCNPJ(campaignDto.CNPJ)).Returns(true);
            _mockValidationService.Setup(x => x.IsValidEmail(campaignDto.Email)).Returns(true);

            // Act
            var result = await _campaignService.CreateCampaignAsync(campaignDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(campaignDto.Name, result.Name);
            _mockCollection.Verify(x => x.InsertOneAsync(It.IsAny<Campaign>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCampaignAsync_ExistingCampaign_UpdatesCampaign()
        {
            // Arrange
            var existingCampaign = new Campaign
            {
                Id = "1",
                Name = "Campanha Original",
                Description = "Descrição original",
                Company = "Empresa Original",
                CNPJ = "12345678000195",
                Email = "email@empresa.com",
                StartDate = DateTime.UtcNow,
                ForecastDate = DateTime.UtcNow.AddDays(10),
                Status = "Ativa"
            };

            _mockCollection.Setup(x => x.Find(It.IsAny<FilterDefinition<Campaign>>(), null).FirstOrDefaultAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingCampaign);

            var updateDto = new UpdateCampaignDTO
            {
                Name = "Campanha Atualizada",
                Description = "Descrição atualizada"
            };

            // Act
            var result = await _campaignService.UpdateCampaignAsync("1", updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Campanha Atualizada", result.Name);
            _mockCollection.Verify(x => x.ReplaceOneAsync(
                It.IsAny<FilterDefinition<Campaign>>(),
                It.IsAny<Campaign>(),
                It.IsAny<ReplaceOptions>(), // Especificar ReplaceOptions
                It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        [Fact]
        public async Task DeleteCampaignAsync_ExistingCampaign_DeletesCampaign()
        {
            // Arrange
            var campaignId = "1";
            _mockCollection.Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Campaign>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeleteResult.Acknowledged(1));

            // Act
            var result = await _campaignService.DeleteCampaignAsync(campaignId);

            // Assert
            Assert.True(result);
            _mockCollection.Verify(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Campaign>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
