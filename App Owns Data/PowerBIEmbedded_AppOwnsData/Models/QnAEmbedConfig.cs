namespace PowerBIEmbedded_AppOwnsData.Models
{
    public class QnAEmbedConfig : EmbedConfig
    {
        public string datasetId { get; set; }
        public string viewMode { get; set; }
        public string question { get; set; }
    }
}