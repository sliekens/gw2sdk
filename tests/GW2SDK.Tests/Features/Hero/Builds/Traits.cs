using GuildWars2.Hero.Builds;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Traits
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Trait> actual, MessageContext context) = await sut.Hero.Builds.GetTraits(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, trait =>
        {
            Assert.True(trait.Id >= 1);
            Assert.InRange(trait.Tier, 0, 4);
            Assert.True(trait.Order >= 0);
            Assert.NotEmpty(trait.Name);
            Assert.NotNull(trait.Description);
            MarkupSyntaxValidator.Validate(trait.Description);
            Assert.True(trait.Slot.IsDefined());
        });
    }
}
