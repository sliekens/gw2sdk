using GuildWars2.Http;

namespace GuildWars2.Tests.Http;

public class HttpRequestMessageTemplateTest
{
    /// <summary>Needed because HttpRequestMessage does not implement equality members.</summary>
    private class HttpRequestMessageComparer : EqualityComparer<HttpRequestMessage>
    {
        public static HttpRequestMessageComparer Instance { get; } = new();

        public override bool Equals(HttpRequestMessage x, HttpRequestMessage y) =>
            string.Equals(x?.ToString(), y?.ToString(), StringComparison.Ordinal);

        public override int GetHashCode(HttpRequestMessage obj) => obj.ToString().GetHashCode();
    }

    [Fact]
    public void Compile_is_idempotent()
    {
        var sut = new HttpRequestMessageTemplate(HttpMethod.Get, "/stuff");

        var first = sut.Compile();
        var second = sut.Compile();

        Assert.Equal(first, second, HttpRequestMessageComparer.Instance);
    }
}
