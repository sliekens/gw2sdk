#if !NET
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Http.Caching
{
    /// <summary>Polyfill for older .NET versions where empty content does not exist.</summary>
    internal class EmptyContent : HttpContent
    {
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) => Task.CompletedTask;

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return true;
        }
    }
}
#endif
