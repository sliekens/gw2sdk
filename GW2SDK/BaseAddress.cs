﻿namespace GuildWars2;

[PublicAPI]
public static class BaseAddress
{
    /// <summary>"https://api.guildwars2.com"</summary>
    public static readonly string Default = "https://api.guildwars2.com";

    /// <summary>"https://api.guildwars2.com"</summary>
    public static readonly Uri DefaultUri = new(Default, UriKind.Absolute);
}
