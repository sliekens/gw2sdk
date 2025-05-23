namespace GuildWars2.Tests.Features;

public class HyperlinkTest
{
    [Fact]
    public void Null_hyperlink_is_empty()
    {
        var sut = new Hyperlink((Uri?)null);
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
        var sut = new Hyperlink(BaseAddress.DefaultUri);
        Assert.False(sut.IsEmpty, "sut.IsEmpty");
    }

    [Fact]
    public void Identitcal_hyperlinks_are_equal_by_contents()
    {
        var left = new Hyperlink(BaseAddress.DefaultUri);
        var right = new Hyperlink(BaseAddress.DefaultUri);
        Assert.Equal(left, right);
    }

    [Fact]
    public void Identitcal_hyperlinks_are_equal_by_identity()
    {
        var left = new Hyperlink(BaseAddress.DefaultUri);
        var right = new Hyperlink(BaseAddress.DefaultUri);
        Assert.True(left == right, "left == right");
    }
}
