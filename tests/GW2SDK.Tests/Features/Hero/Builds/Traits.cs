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
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        foreach (Trait trait in actual)
        {
            await Assert.That(trait.Id).IsGreaterThanOrEqualTo(1);
            await Assert.That(trait.Tier).IsGreaterThanOrEqualTo(0).And.IsLessThanOrEqualTo(4);
            await Assert.That(trait.Order).IsGreaterThanOrEqualTo(0);
            await Assert.That(trait.Name).IsNotEmpty();
            await Assert.That(trait.Description).IsNotNull();
            MarkupSyntaxValidator.Validate(trait.Description);
            await Assert.That(trait.Slot.IsDefined()).IsTrue();
        }
    }
}
