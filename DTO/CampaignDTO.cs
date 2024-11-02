using System;
using System.ComponentModel.DataAnnotations;

namespace APIAnallyzer_v2.DTOs
{
    /// <summary>
    /// Data Transfer Object para a criação e atualização de campanhas.
    /// </summary>
    public class CampaignDTO
    {
        /// <summary>
        /// Nome da campanha.
        /// </summary>
        [Required(ErrorMessage = "O nome da campanha é obrigatório.")]
        public string Name { get; set; }

        /// <summary>
        /// Descrição detalhada da campanha.
        /// </summary>
        [Required(ErrorMessage = "A descrição é obrigatória.")] 
        public string Description { get; set; }

        /// <summary>
        /// Nome da empresa responsável pela campanha.
        /// </summary>
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        public string Company { get; set; }
        
        /// <summary>
        /// CNPJ da empresa.
        /// </summary>
        [Required(ErrorMessage = "O CNPJ é obrigatório."), MinLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        public String CNPJ { get; set; }
        
        /// <summary>
        /// Email da empresa. Deve conter o endereçio de email "ex: @xxx.xxx"
        /// </summary>
        [Required(ErrorMessage = "O email é obrigatório.")]
        public String Email { get; set; }

        /// <summary>
        /// Data e hora de início da campanha.
        /// </summary>
        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data e hora previstas para o término da campanha.
        /// </summary>
        [Required(ErrorMessage = "A data prevista para término é obrigatória.")]
        public DateTime ForecastDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Status atual da campanha.
        /// </summary>
        [Required(ErrorMessage = "O status é obrigatório.")]
        public string Status { get; set; } = "Ativa";
    }
}