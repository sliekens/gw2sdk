using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Specializations
{
    [Fact]
    public async Task Specializations_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Builds.GetSpecializations(
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            specialization =>
            {
                Assert.True(specialization.Id >= 1);
                Assert.NotEmpty(specialization.Name);
                Assert.True(specialization.Profession.IsDefined());
                Assert.NotEmpty(specialization.MinorTraitIds);
                Assert.NotEmpty(specialization.MajorTraitIds);
                Assert.True(specialization.IconUrl.IsAbsoluteUri);
                Assert.True(specialization.BackgroundUrl.IsAbsoluteUri);
                Assert.True(specialization.ProfessionBigIconUrl == null || specialization.ProfessionBigIconUrl.IsAbsoluteUri);
                Assert.True(specialization.ProfessionIconUrl == null || specialization.ProfessionIconUrl.IsAbsoluteUri);
            }
        );
    }
}
