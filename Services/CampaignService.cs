// Services/CampaignService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using APIAnallyzer_v2.Data;
using MongoDB.Driver;
using APIAnallyzer_v2.DTOs;
using APIAnallyzer_v2.Models;

namespace APIAnallyzer_v2.Services
{
    public class CampaignService
    {
        private readonly IMongoCollection<Campaign> _campaigns;
        private readonly ValidationService _validationService;

        public CampaignService(MongoDbService mongoDbService, ValidationService validationService)
        {
            _campaigns = mongoDbService.Database?.GetCollection<Campaign>("campaigns");
            _validationService = validationService;
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsAsync()
        {
            return await _campaigns.Find(FilterDefinition<Campaign>.Empty).ToListAsync();
        }

        public async Task<Campaign?> GetCampaignByIdAsync(string id)
        {
            var filter = Builders<Campaign>.Filter.Eq(x => x.Id, id);
            return await _campaigns.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Campaign?> GetCampaignByCNPJAsync(string cnpj)
        {
            var filter = Builders<Campaign>.Filter.Eq(x => x.CNPJ, cnpj);
            return await _campaigns.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Campaign?> UpdateCampaignAsync(string id, UpdateCampaignDTO campaignDto)
        {
            var campaign = await GetCampaignByIdAsync(id);
            if (campaign == null) return null;
            
            if (!string.IsNullOrEmpty(campaignDto.CNPJ) && !_validationService.IsValidCNPJ(campaignDto.CNPJ))
                throw new ArgumentException("O CNPJ informado não é válido.");
            
            var cnpjExists = await _campaigns.Find(Builders<Campaign>.Filter.Eq(c => c.CNPJ, campaignDto.CNPJ)).FirstOrDefaultAsync();
            if (cnpjExists != null) throw new InvalidOperationException("O CNPJ informado já está cadastrado. Por favor, use outro CNPJ.");
            
            if (!string.IsNullOrEmpty(campaignDto.Email))
            {
                if (!_validationService.IsValidEmail(campaignDto.Email))
                    throw new ArgumentException("O e-mail informado não é válido.");
        
                var emailExists = await _campaigns.Find(Builders<Campaign>.Filter.Eq(c => c.Email, campaignDto.Email)).FirstOrDefaultAsync();
                if (emailExists != null) throw new InvalidOperationException("O e-mail informado já está cadastrado. Por favor, use outro e-mail.");
                
                // Chama a validação assíncrona da API
                if (!await _validationService.IsEmailValidAsync(campaignDto.Email))
                    throw new ArgumentException("O e-mail informado é considerado inválido pela API.");
            }
            
            if (!string.IsNullOrEmpty(campaignDto.Name)) campaign.Name = campaignDto.Name;
            if (!string.IsNullOrEmpty(campaignDto.Description)) campaign.Description = campaignDto.Description;
            if (!string.IsNullOrEmpty(campaignDto.Company)) campaign.Company = campaignDto.Company;
            if (!string.IsNullOrEmpty(campaignDto.CNPJ)) campaign.CNPJ = campaignDto.CNPJ;
            if (!string.IsNullOrEmpty(campaignDto.Email)) campaign.Email = campaignDto.Email;
            if (campaignDto.StartDate.HasValue) campaign.StartDate = campaignDto.StartDate.Value;
            if (campaignDto.ForecastDate.HasValue) campaign.ForecastDate = campaignDto.ForecastDate.Value;
            if (!string.IsNullOrEmpty(campaignDto.Status)) campaign.Status = campaignDto.Status;

            await _campaigns.ReplaceOneAsync(c => c.Id == id, campaign);
            return campaign;
        }

        public async Task<Campaign> CreateCampaignAsync(CampaignDTO campaignDto)
        {
            if (!_validationService.IsValidCNPJ(campaignDto.CNPJ))
            {
                throw new ArgumentException("O CNPJ informado não é válido.");
            }
            
            var cnpjExists = await _campaigns
                .Find(Builders<Campaign>.Filter.Eq(c => c.CNPJ, campaignDto.CNPJ))
                .FirstOrDefaultAsync();
            if (cnpjExists != null) throw new InvalidOperationException("O CNPJ informado já está cadastrado. Por favor, use outro CNPJ.");
        
            if (!_validationService.IsValidEmail(campaignDto.Email))
            {
                throw new ArgumentException("O e-mail informado não é válido.");
            }
        
            var emailExists = await _campaigns
                .Find(Builders<Campaign>.Filter.Eq(c => c.Email, campaignDto.Email))
                .FirstOrDefaultAsync();
            if (emailExists != null) throw new InvalidOperationException("O e-mail informado já está cadastrado. Por favor, use outro e-mail.");
        
            if (!await _validationService.IsEmailValidAsync(campaignDto.Email))
                {
                    throw new ArgumentException("O e-mail informado é considerado inválido pela API.");
                }
                
            var campaign = new Campaign
            {
                Name = campaignDto.Name,
                Description = campaignDto.Description,
                Company = campaignDto.Company,
                CNPJ = campaignDto.CNPJ,
                Email = campaignDto.Email,
                StartDate = campaignDto.StartDate,
                ForecastDate = campaignDto.ForecastDate,
                Status = campaignDto.Status
            };
        
            await _campaigns.InsertOneAsync(campaign);
            return campaign;
        }


        public async Task<bool> DeleteCampaignAsync(string id)
        {
            var filter = Builders<Campaign>.Filter.Eq(x => x.Id, id);
            var result = await _campaigns.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
