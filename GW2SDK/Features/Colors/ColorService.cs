using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        private readonly IDyeReader dyeReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public ColorService(
            HttpClient http,
            IDyeReader dyeReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.dyeReader = dyeReader ?? throw new ArgumentNullException(nameof(dyeReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Dye>> GetColors(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ColorsRequest(language);
            return await http.GetResourcesSet(request, json => dyeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetColorsIndex(CancellationToken cancellationToken = default)
        {
            var request = new ColorsIndexRequest();
            return await http.GetResourcesSet(request, json => dyeReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Dye>> GetColorById(
            int colorId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ColorByIdRequest(colorId, language);
            return await http.GetResource(request, json => dyeReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Dye>> GetColorsByIds(
#if NET
            IReadOnlySet<int> colorIds,
#else
            IReadOnlyCollection<int> colorIds,
#endif
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ColorsByIdsRequest(colorIds, language);
            return await http.GetResourcesSet(request, json => dyeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Dye>> GetColorsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ColorsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request, json => dyeReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
