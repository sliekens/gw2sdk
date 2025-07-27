using System.Text.Json;

namespace GuildWars2.Json;

internal delegate T JsonTransform<T>(in JsonElement json);
