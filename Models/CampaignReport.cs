using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace APIAnallyzer_v2.Models
{
    public class CampaignReport
    {
        /// <summary>
        /// Identificador único da empresa.
        /// </summary>
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public String Id { get; set; }

        /// <summary>
        /// ID da campanha específica à qual o relatório está associado.
        /// </summary>
        [BsonRequired]
        public string CampaignId { get; set; }

        /// <summary>
        /// Número total de cliques na campanha nos últimos 7 dias.
        /// </summary>
        public int Clicks_7d { get; set; }

        /// <summary>
        /// Número total de aberturas de emails da campanha nos últimos 7 dias.
        /// </summary>
        public int Opens_7d { get; set; }

        /// <summary>
        /// Número total de emails enviados da campanha nos últimos 7 dias.
        /// </summary>
        public int Sends_7d { get; set; }

        /// <summary>
        /// Número total de leads gerados pela campanha nos últimos 7 dias.
        /// </summary>
        public int Leads_7d { get; set; }

        /// <summary>
        /// Número total de cliques na campanha nos últimos 30 dias.
        /// </summary>
        public int Clicks_30d { get; set; }

        /// <summary>
        /// Número total de aberturas de emails da campanha nos últimos 30 dias.
        /// </summary>
        public int Opens_30d { get; set; }

        /// <summary>
        /// Número total de emails enviados da campanha nos últimos 30 dias.
        /// </summary>
        public int Sends_30d { get; set; }

        /// <summary>
        /// Número total de leads gerados pela campanha nos últimos 30 dias.
        /// </summary>
        public int Leads_30d { get; set; }

        // Construtor padrão
        public CampaignReport() { }

        /// <summary>
        /// Construtor para facilitar a criação de instâncias de CampaignReport.
        /// </summary>
        /// <param name="campaignId">ID da campanha associada.</param>
        /// <param name="clicks7d">Cliques nos últimos 7 dias.</param>
        /// <param name="opens7d">Aberturas nos últimos 7 dias.</param>
        /// <param name="sends7d">Envios nos últimos 7 dias.</param>
        /// <param name="leads7d">Leads nos últimos 7 dias.</param>
        /// <param name="clicks30d">Cliques nos últimos 30 dias.</param>
        /// <param name="opens30d">Aberturas nos últimos 30 dias.</param>
        /// <param name="sends30d">Envios nos últimos 30 dias.</param>
        /// <param name="leads30d">Leads nos últimos 30 dias.</param>
        public CampaignReport(string campaignId, int clicks7d, int opens7d, int sends7d, int leads7d,
                              int clicks30d, int opens30d, int sends30d, int leads30d)
        {
            CampaignId = campaignId;
            Clicks_7d = clicks7d;
            Opens_7d = opens7d;
            Sends_7d = sends7d;
            Leads_7d = leads7d;
            Clicks_30d = clicks30d;
            Opens_30d = opens30d;
            Sends_30d = sends30d;
            Leads_30d = leads30d;
        }
    }
}
