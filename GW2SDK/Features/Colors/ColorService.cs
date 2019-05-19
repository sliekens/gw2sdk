﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Colors.Infrastructure;
using GW2SDK.Features.Common;
using GW2SDK.Features.Common.Infrastructure;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Features.Colors
{
    public sealed class ColorService
    {
        private readonly IJsonColorService _api;

        public ColorService([NotNull] IJsonColorService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<IDataTransferList<int>> GetColorIds([CanBeNull] JsonSerializerSettings settings = null)
        {
            var list = new List<int>();
            var (json, metaData) = await _api.GetColorIds().ConfigureAwait(false);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<int>(list)
            {
                MetaData = metaData.GetListMetaData()
            };
        }

        public async Task<Color> GetColorById(int colorId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            var (json, _) = await _api.GetColorById(colorId);
            return JsonConvert.DeserializeObject<Color>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferList<Color>> GetColorsById([NotNull] IReadOnlyList<int> colorIds,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (colorIds == null) throw new ArgumentNullException(nameof(colorIds));
            var list = new List<Color>();
            var (json, metaData) = await _api.GetColorsById(colorIds).ConfigureAwait(false);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<Color>(list)
            {
                MetaData = metaData.GetListMetaData()
            };
        }

        public async Task<IDataTransferList<Color>> GetAllColors([CanBeNull] JsonSerializerSettings settings = null)
        {
            var list = new List<Color>();
            var (json, metaData) = await _api.GetAllColors().ConfigureAwait(false);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<Color>(list)
            {
                MetaData = metaData.GetListMetaData()
            };
        }

        public async Task<IDataTransferPagedList<Color>> GetColorsPage(int page, int? pageSize,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            
            var list = new List<Color>();
            var (json, metaData) = await _api.GetColorsPage(page, pageSize).ConfigureAwait(false);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPagedList<Color>(list)
            {
                MetaData = metaData.GetPagedListMetaData()
            };
        }
    }
}
