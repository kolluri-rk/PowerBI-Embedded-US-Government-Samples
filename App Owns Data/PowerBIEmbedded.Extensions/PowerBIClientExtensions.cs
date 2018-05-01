using Microsoft.PowerBI.Api.V2.Models;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.Api.V2
{
    public static class PowerBIClientExtensions
    {
        public static IQnas Qnas(this PowerBIClient client)
        {
            return new Microsoft.PowerBI.Api.V2.Qnas(client);
        }
    }
}