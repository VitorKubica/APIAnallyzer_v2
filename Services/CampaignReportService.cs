using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIAnallyzer_v2.Models;
using APIAnallyzer_v2.DTOs;

namespace APIAnallyzer_v2.Services
{
    public class CampaignReportService : ICampaignReportService
    {
        private readonly IMongoCollection<CampaignReport> _collection;

        public CampaignReportService(IMongoDatabase database)
        {
            _collection = database.GetCollection<CampaignReport>("campaignReports");
        }

        public async Task<CampaignReport> CreateAsync(CampaignReportDTO campaignReportDto)
        {
            var campaignReport = MapToEntity(campaignReportDto);
            await _collection.InsertOneAsync(campaignReport);
            return campaignReport;
        }

        public async Task<CampaignReport> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CampaignReport>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(string id, CampaignReportDTO campaignReportDto)
        {
            var campaignReport = MapToEntity(campaignReportDto);
            campaignReport.Id = id; // Define o ID para atualização

            var result = await _collection.ReplaceOneAsync(x => x.Id == id, campaignReport);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        // Mapeamento entre DTO e Entidade
        private CampaignReport MapToEntity(CampaignReportDTO dto)
        {
            return new CampaignReport
            {
                CampaignId = dto.CampaignId,
                Clicks_7d = dto.Clicks_7d,
                Opens_7d = dto.Opens_7d,
                Sends_7d = dto.Sends_7d,
                Leads_7d = dto.Leads_7d,
                Clicks_30d = dto.Clicks_30d,
                Opens_30d = dto.Opens_30d,
                Sends_30d = dto.Sends_30d,
                Leads_30d = dto.Leads_30d
            };
        }
    }
}
