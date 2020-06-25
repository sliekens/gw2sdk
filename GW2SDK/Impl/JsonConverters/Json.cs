using System.Collections.Generic;
using GW2SDK.Achievements.Impl;
using GW2SDK.Continents.Impl;
using GW2SDK.Items.Impl;
using GW2SDK.Recipes.Impl;
using GW2SDK.Skins.Impl;
using GW2SDK.Tokens.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GW2SDK.Impl.JsonConverters
{
    internal static class Json
    {
        internal static JsonSerializerSettings DefaultJsonSerializerSettings =>
            new JsonSerializerSettings
            {
                Converters = GetJsonConverters(), ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
            };

        internal static List<JsonConverter> GetJsonConverters() =>
            new List<JsonConverter>
            {
                new DiscriminatedJsonConverter(new AchievementDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new AchievementBitDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new AchievementRewardDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new PointOfInterestDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new ArmorDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new ConsumableDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new ContainerDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new GatheringToolSkinDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new GizmoDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new ItemDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new GatheringToolDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new ToolDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new TrinketDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new UnlockerDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new UpgradeComponentDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new WeaponDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new RecipeDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new SkinDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new ArmorSkinDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new GatheringToolSkinDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new WeaponSkinDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new TokenDiscriminatorOptions()),
                new DiscriminatedJsonConverter(new SkinDiscriminatorOptions())
            };
    }
}
