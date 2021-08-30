﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Masteries.Http;
using JetBrains.Annotations;

namespace GW2SDK.Masteries
{
    [PublicAPI]
    public sealed class MasteryService
    {
        private readonly HttpClient http;

        private readonly IMasteryReader masteryReader;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public MasteryService(
            HttpClient http,
            IMasteryReader masteryReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.masteryReader = masteryReader ?? throw new ArgumentNullException(nameof(masteryReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Mastery>> GetMasteries(Language? language = default)
        {
            var request = new MasteriesRequest(language);
            return await http.GetResourcesSet(request, json => masteryReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetMasteriesIndex()
        {
            var request = new MasteriesIndexRequest();
            return await http
                .GetResourcesSet(request, json => masteryReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Mastery>> GetMasteryById(int masteryId, Language? language = default)
        {
            var request = new MasteryByIdRequest(masteryId, language);
            return await http.GetResource(request, json => masteryReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Mastery>> GetMasteriesByIds(
            IReadOnlyCollection<int> masteryIds,
            Language? language = default
        )
        {
            var request = new MasteriesByIdsRequest(masteryIds, language);
            return await http.GetResourcesSet(request, json => masteryReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
