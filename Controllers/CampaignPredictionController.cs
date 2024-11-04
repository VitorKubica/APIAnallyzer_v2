using Microsoft.AspNetCore.Mvc;
using APIAnallyzer_v2.DTOs;
using APIAnallyzer_v2.Services;

namespace APIAnallyzer_v2.Controllers
{
    /// <summary>
    /// Controlador para previsão de sucesso de campanhas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignPredictionController : ControllerBase
    {
        private readonly CampaignPredictionService _predictionService;

        public CampaignPredictionController(CampaignPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        /// <summary>
        /// Prever o sucesso de uma campanha com base nas métricas fornecidas.
        /// </summary>
        /// <param name="campaignReportDto">Dados do relatório da campanha.</param>
        /// <returns>Boolean indicando o sucesso ou fracasso da campanha.</returns>
        [HttpPost("predict")]
        public ActionResult<bool> PredictSuccess([FromBody] CampaignReportDTO campaignReportDto)
        {
            var campaignData = new CampaignData
            {
                Clicks_7d = campaignReportDto.Clicks_7d,
                Opens_7d = campaignReportDto.Opens_7d,
                Sends_7d = campaignReportDto.Sends_7d,
                Leads_7d = campaignReportDto.Leads_7d,
                Clicks_30d = campaignReportDto.Clicks_30d,
                Opens_30d = campaignReportDto.Opens_30d,
                Sends_30d = campaignReportDto.Sends_30d,
                Leads_30d = campaignReportDto.Leads_30d
            };

            bool isSuccess = _predictionService.Predict(campaignData);
            return Ok(isSuccess);
        }
    }
}