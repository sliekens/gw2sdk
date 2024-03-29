﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

// ReSharper disable once ClassNeverInstantiated.Global
public class AchievementFixture
{
    public IReadOnlyCollection<string> Achievements { get; } =
        FlatFileReader.Read("Data/achievements.json.gz").ToList().AsReadOnly();
}
