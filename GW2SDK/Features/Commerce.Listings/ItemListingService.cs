using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Commerce.Listings.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Listings
{
    [PublicAPI]
    public sealed class ItemListingService
    {
        private readonly HttpClient _http;

        private readonly IItemListingReader _itemListingReader;

        public ItemListingService(HttpClient http, IItemListingReader itemListingReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _itemListingReader = itemListingReader ?? throw new ArgumentNullException(nameof(itemListingReader));
        }

        public async Task<IDataTransferSet<int>> GetItemListingsIndex()
        {
            var request = new ItemListingsIndexRequest();
            return await _http.GetResourcesSet(request, json => _itemListingReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<ItemListing> GetItemListingById(int itemId)
        {
            var request = new ItemListingByIdRequest(itemId);
            return await _http.GetResource(request, json => _itemListingReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<ItemListing>> GetItemListingsByIds(IReadOnlyCollection<int> itemIds)
        {
            var request = new ItemListingsByIdsRequest(itemIds);
            return await _http.GetResourcesSet(request, json => _itemListingReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
