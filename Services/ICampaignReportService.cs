using System.Collections.Generic;
using System.Threading.Tasks;
using APIAnallyzer_v2.Models;

namespace APIAnallyzer_v2.Services
{
    public interface ICampaignReportService
    {
        Task<CampaignReport> CreateAsync(CampaignReport campaignReport);
        Task<CampaignReport> GetByIdAsync(string id);
        Task<IEnumerable<CampaignReport>> GetAllAsync();
        Task<bool> UpdateAsync(string id, CampaignReport campaignReport);
        Task<bool> DeleteAsync(string id);
    }
}