using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Commerce.Exchange.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Exchange
{
    [PublicAPI]
    public sealed class ExchangeService
    {
        private readonly IExchangeReader _exchangeReader;
        private readonly HttpClient _http;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ExchangeService(HttpClient http, IExchangeReader exchangeReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _exchangeReader = exchangeReader ?? throw new ArgumentNullException(nameof(exchangeReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<GemsForGoldExchange>> ExchangeGemsForGold(int gemsCount)
        {
            var request = new ExchangeGemsForGoldRequest(gemsCount);
            return await _http.GetResource(request, json => _exchangeReader.GemsForGold.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<GoldForGemsExchange>> ExchangeGoldForGems(Coin coinsCount)
        {
            var request = new ExchangeGoldForGemsRequest(coinsCount);
            return await _http.GetResource(request, json => _exchangeReader.GoldForGems.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
