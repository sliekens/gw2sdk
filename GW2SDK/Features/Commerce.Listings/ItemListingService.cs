using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Commerce.Listings.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    public sealed class ItemListingService
    {
        private readonly HttpClient _http;

        private readonly IItemListingReader _itemListingReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ItemListingService(HttpClient http, IItemListingReader itemListingReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _itemListingReader = itemListingReader ?? throw new ArgumentNullException(nameof(itemListingReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetItemListingsIndex()
        {
            var request = new ItemListingsIndexRequest();
            return await _http.GetResourcesSet(request, json => _itemListingReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<ItemListing>> GetItemListingById(int itemId)
        {
            var request = new ItemListingByIdRequest(itemId);
            return await _http.GetResource(request, json => _itemListingReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ItemListing>> GetItemListingsByIds(IReadOnlyCollection<int> itemIds)
        {
            var request = new ItemListingsByIdsRequest(itemIds);
            return await _http.GetResourcesSet(request, json => _itemListingReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
