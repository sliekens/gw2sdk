using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Colors.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public sealed class ColorService
    {
        private readonly IColorReader colorReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public ColorService(
            HttpClient http,
            IColorReader colorReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.colorReader = colorReader ?? throw new ArgumentNullException(nameof(colorReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Color>> GetColors(Language? language = default)
        {
            var request = new ColorsRequest(language);
            return await http.GetResourcesSet(request, json => colorReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetColorsIndex()
        {
            var request = new ColorsIndexRequest();
            return await http.GetResourcesSet(request, json => colorReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Color>> GetColorById(int colorId, Language? language = default)
        {
            var request = new ColorByIdRequest(colorId, language);
            return await http.GetResource(request, json => colorReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Color>> GetColorsByIds(
            IReadOnlyCollection<int> colorIds,
            Language? language = default
        )
        {
            var request = new ColorsByIdsRequest(colorIds, language);
            return await http.GetResourcesSet(request, json => colorReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Color>> GetColorsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new ColorsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request, json => colorReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
