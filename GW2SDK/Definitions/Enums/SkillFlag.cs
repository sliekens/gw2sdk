namespace GuildWars2;

/// <summary>Modifiers for skills.</summary>
[PublicAPI]
public enum SkillFlag
{
    /// <summary>The skill is a ground targeting skill with a range indicator.</summary>
    GroundTargeted = 1,

    /// <summary>The skill can't be used while swimming.</summary>
    NoUnderwater
}
