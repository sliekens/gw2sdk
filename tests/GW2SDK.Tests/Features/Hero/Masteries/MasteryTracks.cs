using GuildWars2.Hero.Masteries;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Masteries;

[ServiceDataSource]
public class MasteryTracks(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<MasteryTrack> actual, MessageContext context) = await sut.Hero.Masteries.GetMasteryTracks(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        foreach (MasteryTrack mastery in actual)
        {
            await Assert.That(mastery.Id).IsGreaterThan(0);
            await Assert.That(mastery.Name).IsNotEmpty();
            await Assert.That(mastery.Requirement).IsNotNull();
            await Assert.That(mastery.Order).IsGreaterThanOrEqualTo(0);
            await Assert.That(mastery.BackgroundHref).IsNotEmpty();
            await Assert.That(mastery.Region.IsDefined()).IsTrue();
            await Assert.That(mastery.Region).IsNotEqualTo(MasteryRegionName.Unknown);
            foreach (Mastery level in mastery.Masteries)
            {
                await Assert.That(level.Name).IsNotEmpty();
                await Assert.That(level.Description).IsNotEmpty();
                MarkupSyntaxValidator.Validate(level.Description);
                await Assert.That(level.Instruction).IsNotEmpty();
                MarkupSyntaxValidator.Validate(level.Instruction);
                await Assert.That(level.IconUrl is null || level.IconUrl.IsAbsoluteUri).IsTrue();
                await Assert.That(level.PointCost).IsGreaterThan(0);
                await Assert.That(level.ExperienceCost).IsGreaterThan(0);
            }
        }
    }
}
