using System.Text.Json;

namespace GuildWars2.Json;

internal delegate T? JsonTransformNullable<T>(in JsonElement json);
