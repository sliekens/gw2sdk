using System;
using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Titles
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Title
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = "";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [Obsolete("This property should not be used because some titles can be unlocked by more than one achievement. Use Achievements instead.")]
        [JsonProperty("achievement", Required = Required.DisallowNull)]
        public int? Achievement { get; set; }

        [JsonProperty("achievements", Required = Required.DisallowNull)]
        public int[]? Achievements { get; set; }

        [JsonProperty("ap_required", Required = Required.DisallowNull)]
        public int? AchievementPointsRequired { get; set; }
    }
}
