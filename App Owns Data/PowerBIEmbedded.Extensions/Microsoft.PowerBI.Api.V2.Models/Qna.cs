using Newtonsoft.Json;

namespace Microsoft.PowerBI.Api.V2.Models
{
    public class Qna
    {
        [JsonProperty(PropertyName = "embedUrl")]
        public string EmbedUrl { get; set; }

        public Qna()
        {
        }

        public Qna(string embedUrl = null)
        {            
            this.EmbedUrl = embedUrl;
        }
    }
}