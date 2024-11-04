using System.Collections.Generic;
using System.Threading.Tasks;
using APIAnallyzer_v2.Models;
using APIAnallyzer_v2.DTOs;

namespace APIAnallyzer_v2.Services
{
    public interface ICampaignReportService
    {
        Task<CampaignReport> CreateAsync(CampaignReportDTO campaignReportDto);
        Task<CampaignReport> GetByIdAsync(string id);
        Task<IEnumerable<CampaignReport>> GetAllAsync();
        Task<bool> UpdateAsync(string id, CampaignReportDTO campaignReportDto);
        Task<bool> DeleteAsync(string id);
    }
}