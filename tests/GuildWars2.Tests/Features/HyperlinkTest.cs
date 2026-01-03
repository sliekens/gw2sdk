namespace GuildWars2.Tests.Features;

public class HyperlinkTest
{
    [Test]
    public async Task Null_hyperlink_is_empty()
    {
        Hyperlink sut = new((Uri?)null);
        await Assert.That(sut.IsEmpty).IsTrue().Because("sut.IsEmpty");
    }

    [Test]
    public async Task None_hyperlink_is_empty()
    {
        Hyperlink sut = Hyperlink.None;
        await Assert.That(sut.IsEmpty).IsTrue().Because("sut.IsEmpty");
    }

    [Test]
    public async Task An_actual_hyperlink_is_not_empty()
    {
        Hyperlink sut = new(BaseAddress.DefaultUri);
        await Assert.That(sut.IsEmpty).IsFalse().Because("sut.IsEmpty");
    }

    [Test]
    public async Task Identitcal_hyperlinks_are_equal_by_contents()
    {
        Hyperlink left = new(BaseAddress.DefaultUri);
        Hyperlink right = new(BaseAddress.DefaultUri);
        await Assert.That(left).IsEqualTo(right);
    }

    [Test]
    public async Task Identitcal_hyperlinks_are_equal_by_identity()
    {
        Hyperlink left = new(BaseAddress.DefaultUri);
        Hyperlink right = new(BaseAddress.DefaultUri);
        await Assert.That(left == right).IsTrue().Because("left == right");
    }
}
