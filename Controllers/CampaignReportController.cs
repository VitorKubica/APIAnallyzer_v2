using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIAnallyzer_v2.DTOs;
using APIAnallyzer_v2.Models;
using APIAnallyzer_v2.Services;

namespace APIAnallyzer_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignReportController : ControllerBase
    {
        private readonly ICampaignReportService _campaignReportService;

        public CampaignReportController(ICampaignReportService campaignReportService)
        {
            _campaignReportService = campaignReportService;
        }

        [HttpPost]
        public async Task<ActionResult<CampaignReportDTO>> Create([FromBody] CampaignReport campaignReportDto)
        {
            var createdReport = await _campaignReportService.CreateAsync(campaignReportDto);
            return CreatedAtAction(nameof(GetById), new { id = createdReport.CampaignId }, createdReport);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignReportDTO>>> GetAll()
        {
            var reports = await _campaignReportService.GetAllAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignReportDTO>> GetById(string id)
        {
            var report = await _campaignReportService.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] CampaignReport campaignReportDto)
        {
            var updated = await _campaignReportService.UpdateAsync(id, campaignReportDto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await _campaignReportService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
