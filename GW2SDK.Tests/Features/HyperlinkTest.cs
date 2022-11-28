using Xunit;

namespace GuildWars2.Tests.Features;

public class HyperlinkTest
{
    [Fact]
    public void Null_hyperlink_is_empty()
    {
        var sut = new Hyperlink(null!);
        Assert.True(sut.IsEmpty, "sut.IsEmpty");
    }

    [Fact]
    public void Empty_string_hyperlink_is_empty()
    {
        var sut = new Hyperlink("");
        Assert.True(sut.IsEmpty, "sut.IsEmpty");
    }

    [Fact]
    public void None_hyperlink_is_empty()
    {
        var sut = Hyperlink.None;
        Assert.True(sut.IsEmpty, "sut.IsEmpty");
    }

    [Fact]
    public void An_actual_hyperlink_is_not_empty()
    {
        var sut = new Hyperlink(BaseAddress.Default);
        Assert.False(sut.IsEmpty, "sut.IsEmpty");
    }

    [Fact]
    public void Identitcal_hyperlinks_are_equal_by_contents()
    {
        var left = new Hyperlink(BaseAddress.Default);
        var right = new Hyperlink(BaseAddress.Default);

        Assert.Equal(left, right);
    }

    [Fact]
    public void Identitcal_hyperlinks_are_equal_by_identity()
    {
        var left = new Hyperlink(BaseAddress.Default);
        var right = new Hyperlink(BaseAddress.Default);

        Assert.True(left == right, "left == right");
    }
}
