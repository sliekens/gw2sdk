using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.MailCarriers
{
    [PublicAPI]
    public interface IMailCarrierReader : IJsonReader<MailCarrier>
    {
        IJsonReader<int> Id { get; }
    }
}