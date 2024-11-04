using Microsoft.ML.Data;

namespace APIAnallyzer_v2.ML;

public class CampaignPrediction
{
    [ColumnName("PredictedLabel")]
    public bool PredictedLabel { get; set; }
}