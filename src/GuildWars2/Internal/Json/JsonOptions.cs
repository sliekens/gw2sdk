namespace GuildWars2.Json;

internal static class JsonOptions
{
    private static readonly AsyncLocal<MissingMemberBehavior> MissingMemberBehaviorContext = new();

    internal static MissingMemberBehavior MissingMemberBehavior
    {
        get => MissingMemberBehaviorContext.Value;
        set => MissingMemberBehaviorContext.Value = value;
    }
}
