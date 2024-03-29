﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Specializations
{
    [Fact]
    public async Task Specializations_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Builds.GetSpecializations();

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            specialization =>
            {
                specialization.Id_is_positive();
                specialization.Name_is_not_empty();
                specialization.It_has_minor_traits();
                specialization.It_has_major_traits();
                specialization.Icon_is_not_empty();
                specialization.Background_is_not_empty();
                specialization.Big_profession_icon_is_not_null();
                specialization.Profession_icon_is_not_null();
            }
        );
    }
}
