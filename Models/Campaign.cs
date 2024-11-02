using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIAnallyzer_v2.Models
{
    /// <summary>
    /// Cadastro de uma campanha de marketing.
    /// </summary>
    public class Campaign
    {
        /// <summary>
        /// Identificador único da empresa.
        /// </summary>
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public String Id { get; set; }
        
        /// <summary>
        /// Nome da campanha.
        /// </summary>
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O nome da campanha é obrigatório.")]
        public string Name { get; set; }

        /// <summary>
        /// Descrição detalhada da campanha.
        /// </summary>
        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Description { get; set; }

        /// <summary>
        /// Nome da empresa responsável pela campanha.
        /// </summary>
        [BsonElement("company"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        public string Company { get; set; }
        
        /// <summary>
        /// CNPJ da empresa.
        /// </summary>
        [BsonElement("cnpj"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O CNPJ é obrigatório."), MinLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        public String CNPJ { get; set; }
        
        /// <summary>
        /// Email da empresa. Deve conter o endereçio de email "ex: @xxx.xxx"
        /// </summary>
        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve estar em um formato válido.")]
        public String Email { get; set; }

        /// <summary>
        /// Data e hora de início da campanha.
        /// </summary>
        [BsonElement("start_date"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data e hora previstas para o término da campanha.
        /// </summary>
        [BsonElement("forecast_date"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A data prevista para término é obrigatória.")]
        public DateTime ForecastDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Status atual da campanha.
        /// </summary>
        [BsonElement("status"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "O status é obrigatório.")]
        public string Status { get; set; } = "Ativa";

        /// <summary>
        /// Data e hora de registro da campanha.
        /// </summary>
        [BsonElement("registration_date"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A data de registro é obrigatória.")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
