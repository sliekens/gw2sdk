using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class Specializations(Gw2Client sut)
{
    [Test]
    public async Task Specializations_can_be_listed()
    {
        (IImmutableValueSet<Specialization> actual, MessageContext context) = await sut.Hero.Builds.GetSpecializations(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        foreach (Specialization specialization in actual)
        {
            await Assert.That(specialization.Id).IsGreaterThanOrEqualTo(1);
            await Assert.That(specialization.Name).IsNotEmpty();
            await Assert.That(specialization.Profession.IsDefined()).IsTrue();
            await Assert.That(specialization.MinorTraitIds).IsNotEmpty();
            await Assert.That(specialization.MajorTraitIds).IsNotEmpty();
            await Assert.That(specialization.IconUrl.IsAbsoluteUri).IsTrue();
            await Assert.That(specialization.BackgroundUrl.IsAbsoluteUri).IsTrue();
            await Assert.That(specialization.ProfessionBigIconUrl == null || specialization.ProfessionBigIconUrl.IsAbsoluteUri).IsTrue();
            await Assert.That(specialization.ProfessionIconUrl == null || specialization.ProfessionIconUrl.IsAbsoluteUri).IsTrue();
        }
    }
}
