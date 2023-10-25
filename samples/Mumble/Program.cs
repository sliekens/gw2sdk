using System;
using GuildWars2;

if (!GameLink.IsSupported())
{
    throw new PlatformNotSupportedException();
}

var refreshInterval = TimeSpan.FromSeconds(1);
using var gameLink = GameLink.Open(refreshInterval);

gameLink.Subscribe(
    gameTick =>
    {
        var player = gameTick.GetIdentity();
        if (player is not null)
        {
            Console.WriteLine($"{player.Name} is ready to go!");
        }
    }
);
