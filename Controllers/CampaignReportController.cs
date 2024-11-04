using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIAnallyzer_v2.DTOs;
using APIAnallyzer_v2.Models;
using APIAnallyzer_v2.Services;

namespace APIAnallyzer_v2.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento de relatórios de campanhas.
    /// Oferece endpoints para criar, atualizar, buscar e deletar relatórios de campanhas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignReportController : ControllerBase
    {
        private readonly ICampaignReportService _campaignReportService;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="CampaignReportController"/>.
        /// </summary>
        /// <param name="campaignReportService">Serviço de gerenciamento de relatórios de campanhas.</param>
        public CampaignReportController(ICampaignReportService campaignReportService)
        {
            _campaignReportService = campaignReportService;
        }

        /// <summary>
        /// Cria novos relatórios de campanha em massa.
        /// </summary>
        /// <param name="campaignReportDtos">Lista de dados dos relatórios de campanha a serem criados.</param>
        /// <returns>Confirmação da criação dos relatórios de campanha.</returns>
        /// <response code="201">Relatórios de campanha criados com sucesso.</response>
        /// <response code="400">Dados inválidos para criação dos relatórios.</response>
        [HttpPost]
        [ProducesResponseType(typeof(List<CampaignReport>), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] List<CampaignReportDTO> campaignReportDtos)
        {
            var createdReports = new List<CampaignReport>();
            foreach (var campaignReportDto in campaignReportDtos)
            {
                var createdReport = await _campaignReportService.CreateAsync(campaignReportDto);
                createdReports.Add(createdReport);
            }
            return CreatedAtAction(nameof(GetAll), createdReports);
        }


        /// <summary>
        /// Busca todos os relatórios de campanhas.
        /// </summary>
        /// <returns>Lista de relatórios de campanhas existentes.</returns>
        /// <response code="200">Retorna a lista de relatórios de campanhas.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CampaignReport>), 200)]
        public async Task<ActionResult<IEnumerable<CampaignReport>>> GetAll()
        {
            var reports = await _campaignReportService.GetAllAsync();
            return Ok(reports);
        }

        /// <summary>
        /// Busca um relatório de campanha específico pelo ID.
        /// </summary>
        /// <param name="id">ID do relatório de campanha.</param>
        /// <returns>O relatório de campanha correspondente ao ID.</returns>
        /// <response code="200">Retorna o relatório de campanha com o ID especificado.</response>
        /// <response code="404">Relatório de campanha não encontrado para o ID especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CampaignReport), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CampaignReport>> GetById(string id)
        {
            var report = await _campaignReportService.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        /// <summary>
        /// Atualiza um relatório de campanha existente.
        /// </summary>
        /// <param name="id">ID do relatório de campanha a ser atualizado.</param>
        /// <param name="campaignReportDto">Dados atualizados do relatório de campanha.</param>
        /// <returns>Status da operação de atualização.</returns>
        /// <response code="204">Relatório de campanha atualizado com sucesso.</response>
        /// <response code="404">Relatório de campanha não encontrado para o ID especificado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string id, [FromBody] CampaignReportDTO campaignReportDto)
        {
            var updated = await _campaignReportService.UpdateAsync(id, campaignReportDto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Exclui um relatório de campanha pelo ID.
        /// </summary>
        /// <param name="id">ID do relatório de campanha a ser excluído.</param>
        /// <returns>Status da operação de exclusão.</returns>
        /// <response code="204">Relatório de campanha excluído com sucesso.</response>
        /// <response code="404">Relatório de campanha não encontrado para o ID especificado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
