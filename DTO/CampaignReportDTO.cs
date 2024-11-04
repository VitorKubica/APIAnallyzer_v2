namespace APIAnallyzer_v2.DTOs
{
    public class CampaignReportDTO
    {
        /// <summary>
        /// ID da campanha associada. Este campo é obrigatório.
        /// </summary>
        public string? CampaignId { get; set; }

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
    }
}