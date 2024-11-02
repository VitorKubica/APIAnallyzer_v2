using APIAnallyzer_v2.Models;

namespace APIAnallyzer_v2.DTOs
{
    /// <summary>
    /// Data Transfer Object para atualização de campanhas.
    /// </summary>
    public class UpdateCampaignDTO
    {
        /// <summary>
        /// Nome da campanha.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descrição detalhada da campanha.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Nome da empresa responsável pela campanha.
        /// </summary>
        public string? Company { get; set; }

        /// <summary>
        /// CNPJ da empresa.
        /// </summary>
        public string? CNPJ { get; set; }

        /// <summary>
        /// Email da empresa. Deve conter o endereço de email "ex: @xxx.xxx".
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Data e hora de início da campanha.
        /// </summary>
        public DateTime? StartDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data e hora previstas para o término da campanha.
        /// </summary>
        public DateTime? ForecastDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Status atual da campanha.
        /// </summary>
        public string? Status { get; set; } = "Ativa";
    }
}