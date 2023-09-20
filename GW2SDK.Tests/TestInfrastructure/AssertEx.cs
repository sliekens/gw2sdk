namespace GuildWars2.Tests.TestInfrastructure;

public static class AssertEx
{
    public static void Equal(DateTimeOffset expected, DateTimeOffset actual, TimeSpan precision) =>
        Assert.Equal(expected.LocalDateTime, actual.LocalDateTime, precision);
}
