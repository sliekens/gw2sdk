namespace GuildWars2.Tests.Features;

public class HyperlinkTest
{
    [Fact]
    public void Null_hyperlink_is_empty()
    {
        Hyperlink sut = new((Uri?)null);
        Assert.True(sut.IsEmpty, "sut.IsEmpty");
    }

    [Fact]
    public void None_hyperlink_is_empty()
    {
        Hyperlink sut = Hyperlink.None;
        Assert.True(sut.IsEmpty, "sut.IsEmpty");
    }

    [Fact]
    public void An_actual_hyperlink_is_not_empty()
    {
        Hyperlink sut = new(BaseAddress.DefaultUri);
        Assert.False(sut.IsEmpty, "sut.IsEmpty");
    }

    [Fact]
    public void Identitcal_hyperlinks_are_equal_by_contents()
    {
        Hyperlink left = new(BaseAddress.DefaultUri);
        Hyperlink right = new(BaseAddress.DefaultUri);
        Assert.Equal(left, right);
    }

    [Fact]
    public void Identitcal_hyperlinks_are_equal_by_identity()
    {
        Hyperlink left = new(BaseAddress.DefaultUri);
        Hyperlink right = new(BaseAddress.DefaultUri);
        Assert.True(left == right, "left == right");
    }
}
