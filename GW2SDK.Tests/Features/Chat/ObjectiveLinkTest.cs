using GuildWars2.Chat;

namespace GuildWars2.Tests.Features.Chat;

public class ObjectiveLinkTest
{
    [Theory]
    [InlineData("[&DGIAAADIAwAA]", 968, 98)]
    [InlineData("[&DFIAAADIAwAA]", 968, 82)]
    [InlineData("[&DGcAAABgAAAA]", 96, 103)]
    public void Can_marshal_objective_links(string chatLink, int mapId, int objectiveId)
    {
        ObjectiveLink sut = ObjectiveLink.Parse(chatLink);

        var actual = sut.ToString();

        Assert.Equal(chatLink, actual);
        Assert.Equal(mapId, sut.MapId);
        Assert.Equal(objectiveId, sut.ObjectiveId);
    }
}
