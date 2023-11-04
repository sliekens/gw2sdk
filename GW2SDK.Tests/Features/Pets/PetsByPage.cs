using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pets;

public class PetsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.Pets.GetPetsByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Value.Count);
        Assert.NotNull(actual.Context.PageContext);
        Assert.Equal(pageSize, actual.Context.PageContext.PageSize);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(pageSize, actual.Context.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_description();
                entry.Has_icon();
                entry.Has_skills();
            }
        );
    }
}
