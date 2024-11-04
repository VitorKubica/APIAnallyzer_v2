using Microsoft.ML;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIAnallyzer_v2.DTOs;
using APIAnallyzer_v2.ML;
using APIAnallyzer_v2.Models;

namespace APIAnallyzer_v2.Services
{
    public class CampaignPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly Lazy<ITransformer> _lazyModel;
        private readonly IMongoDatabase _database;

        public CampaignPredictionService(IMongoDatabase database)
        {
            _mlContext = new MLContext();
            _database = database;
            _lazyModel = new Lazy<ITransformer>(() => TrainModel());
        }

        private ITransformer TrainModel()
        {
            // Carregar dados da coleção CampaignReports
            var campaignCollection = _database.GetCollection<CampaignReport>("campaignReports");
            var campaignData = campaignCollection.Find(_ => true).ToList();

            // Verifica se há dados suficientes para treinamento
            if (campaignData.Count < 10)
            {
                throw new InvalidOperationException("Dados insuficientes para treinamento do modelo.");
            }

            // Converte os dados para o tipo de entrada necessário
            var trainingData = campaignData.Select(data => new CampaignData
            {
                Clicks_7d = data.Clicks_7d,
                Opens_7d = data.Opens_7d,
                Sends_7d = data.Sends_7d,
                Leads_7d = data.Leads_7d,
                Clicks_30d = data.Clicks_30d,
                Opens_30d = data.Opens_30d,
                Sends_30d = data.Sends_30d,
                Leads_30d = data.Leads_30d,
                Label = DetermineSuccessLabel(data) // Define a variável-alvo para treinamento
            }).ToList();

            // Carregar os dados em um IDataView do ML.NET
            IDataView dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            // Definir e treinar o pipeline de classificação binária sem MapValueToKey para booleano
            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(CampaignData.Clicks_7d), nameof(CampaignData.Opens_7d),
                    nameof(CampaignData.Sends_7d), nameof(CampaignData.Leads_7d), nameof(CampaignData.Clicks_30d), nameof(CampaignData.Opens_30d),
                    nameof(CampaignData.Sends_30d), nameof(CampaignData.Leads_30d))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            return pipeline.Fit(dataView);
        }

        // Função auxiliar para determinar o sucesso da campanha
        private bool DetermineSuccessLabel(CampaignReport data)
        {
            // Defina critérios para considerar uma campanha como "boa"
            return data.Clicks_7d >= 400 && data.Leads_7d >= 50; // Exemplo de critério para campanha "boa"
        }

        public bool Predict(CampaignData campaignData)
        {
            var model = _lazyModel.Value;
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<CampaignData, CampaignPrediction>(model);
            var prediction = predictionEngine.Predict(campaignData);
            return prediction.PredictedLabel;
        }
    }
}
