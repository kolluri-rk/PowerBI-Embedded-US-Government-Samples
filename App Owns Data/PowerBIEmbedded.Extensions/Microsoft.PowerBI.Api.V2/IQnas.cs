using Microsoft.PowerBI.Api.V2.Models;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.Api.V2
{
    public interface IQnas
    {
        Task<Qna> GetQnaInGroupAsync(string groupId, string datasetKey);
    }
}
