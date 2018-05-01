using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.Api.V2
{
    public class Qnas : IServiceOperations<PowerBIClient>, IQnas
    {
        public PowerBIClient Client { get; private set; }
        private static readonly string EmbedUrlBase = ConfigurationManager.AppSettings["embedUrlBase"];


        public Qnas(PowerBIClient client)
        {
            if (client == null)
                throw new ArgumentNullException("client");
            this.Client = client;
        }


        public async Task<Qna> GetQnaInGroupAsync(string groupId, string datasetKey)
        {
            // harcoding for now, as powerbi clinet does not have option to generate QnA embed token
            string str1 = "/";
            string str2 = EmbedUrlBase.EndsWith(str1) ? "" : "/";
            string embedUrl = new Uri(new Uri(EmbedUrlBase + str2), $"qnaEmbed?groupId={groupId}").ToString();

            //TODO: dummy await. made this method async just in case if we need to fetch anything in future
            await Task.CompletedTask;
            return new Microsoft.PowerBI.Api.V2.Models.Qna(embedUrl);
        }
    }
}
