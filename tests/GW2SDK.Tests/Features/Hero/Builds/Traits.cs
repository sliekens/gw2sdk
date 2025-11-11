using GuildWars2.Hero.Builds;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class Traits(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Trait> actual, MessageContext context) = await sut.Hero.Builds.GetTraits(cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
