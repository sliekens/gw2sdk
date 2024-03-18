﻿namespace GuildWars2.Items;

/// <summary>Information about a consumable that unlocks a miniature.</summary>
/// <remarks>These items differ from <see cref="Miniature" /> items. They can only be used to add the miniature to your
/// collection, not to summon the miniature directly. They also can't replace the actual miniature in Mystic Forge recipes.</remarks>
[PublicAPI]
public sealed record MiniatureUnlocker : Unlocker;
