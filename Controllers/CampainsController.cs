using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIAnallyzer_v2.DTOs;
using APIAnallyzer_v2.Models;
using APIAnallyzer_v2.Services;

namespace APIAnallyzer_v2.Controllers
{
    /// <summary>
    /// Controlador para gerenciar campanhas de marketing.
    /// Este controlador fornece endpoints para criar, ler, atualizar e deletar campanhas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly CampaignService _campaignService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CampaignsController"/>.
        /// </summary>
        /// <param name="campaignService">O serviço responsável pelas operações de campanha.</param>
        public CampaignsController(CampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        /// <summary>
        /// Obtém uma lista de todas as campanhas.
        /// </summary>
        /// <returns>Uma lista de campanhas.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaigns()
        {
            var campaigns = await _campaignService.GetCampaignsAsync();
            return Ok(campaigns);
        }

        /// <summary>
        /// Obtém uma campanha específica pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da campanha a ser obtida.</param>
        /// <returns>A campanha correspondente ao ID fornecido, ou um status 404 se não for encontrada.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign?>> GetCampaignById(string id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);
            return campaign is not null ? Ok(campaign) : NotFound();
        }

        /// <summary>
        /// Obtém uma campanha específica pelo seu CNPJ.
        /// </summary>
        /// <param name="cnpj">O CNPJ da campanha a ser obtida.</param>
        /// <returns>A campanha correspondente ao CNPJ fornecido, ou um status 404 se não for encontrada.</returns>
        [HttpGet("cnpj/{cnpj}")]
        public async Task<ActionResult<Campaign?>> GetCampaignByCNPJ(string cnpj)
        {
            var campaign = await _campaignService.GetCampaignByCNPJAsync(cnpj);
            return campaign is not null ? Ok(campaign) : NotFound();
        }

        /// <summary>
        /// Atualiza uma campanha existente.
        /// </summary>
        /// <param name="id">O ID da campanha a ser atualizada.</param>
        /// <param name="campaignDto">O objeto contendo os dados atualizados da campanha.</param>
        /// <returns>Um status 200 se a atualização for bem-sucedida, um status 404 se a campanha não for encontrada, ou um status 400/409 em caso de erro.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(string id, UpdateCampaignDTO campaignDto)
        {
            try
            {
                var updatedCampaign = await _campaignService.UpdateCampaignAsync(id, campaignDto);
                return updatedCampaign != null ? Ok(updatedCampaign) : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cria uma nova campanha.
        /// </summary>
        /// <param name="campaignDto">Os dados da nova campanha a ser criada.</param>
        /// <returns>Um status 201 se a campanha for criada com sucesso, ou um status 400/409 em caso de erro.</returns>
        [HttpPost]
        public async Task<ActionResult<Campaign>> PostCampaign(CampaignDTO campaignDto)
        {
            try
            {
                var campaign = await _campaignService.CreateCampaignAsync(campaignDto);
                return CreatedAtAction(nameof(GetCampaignById), new { id = campaign.Id }, campaign);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deleta uma campanha específica pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da campanha a ser deletada.</param>
        /// <returns>Um status 200 se a campanha for deletada com sucesso, ou um status 404 se a campanha não for encontrada.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(string id)
        {
            var deleted = await _campaignService.DeleteCampaignAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}
