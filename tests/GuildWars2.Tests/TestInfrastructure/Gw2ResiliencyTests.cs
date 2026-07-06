namespace GuildWars2.Tests.TestInfrastructure;

public class Gw2ResiliencyTests
{
    [Arguments("API not active", false)]
    [Arguments(" API not active ", false)]
    [Arguments("api not active", false)]
    [Arguments("unknown error", true)]
    [Arguments(" Unknown Error ", true)]
    [Arguments("endpoint requires authentication", true)]
    [Arguments("ErrBadData", true)]
    [Arguments("ErrTimeout", true)]
    [Arguments(null, false)]
    [Arguments("", false)]
    [Arguments("   ", false)]
    [Test]
    public async Task Known_error_text_is_treated_consistently(string? text, bool shouldRetry)
    {
        bool actual = Gw2Resiliency.IsKnownTransientErrorText(text);
        await Assert.That(actual).IsEqualTo(shouldRetry);
    }
}
