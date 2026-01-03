using GuildWars2.Chat;

using Assert = TUnit.Assertions.Assert;

namespace GuildWars2.Tests.Features.Chat;

public class ObjectiveLinkTest
{
    [Test]
    [Arguments("[&DGIAAADIAwAA]", 968, 98)]
    [Arguments("[&DFIAAADIAwAA]", 968, 82)]
    [Arguments("[&DGcAAABgAAAA]", 96, 103)]
    public async Task Can_marshal_objective_links(string chatLink, int mapId, int objectiveId)
    {
        ObjectiveLink sut = ObjectiveLink.Parse(chatLink);
        string actual = sut.ToString();
        await Assert.That(actual).IsEqualTo(chatLink);
        await Assert.That(sut.MapId).IsEqualTo(mapId);
        await Assert.That(sut.ObjectiveId).IsEqualTo(objectiveId);
    }
}
