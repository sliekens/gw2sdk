using GuildWars2.Chat;

namespace GuildWars2.Hero.Builds;

/// <summary>The base type for skills. Cast objects of this type to a more specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Skill
{
    /// <summary>The skill ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the skill.</summary>
    public required string Name { get; init; }

    /// <summary>The list of skill behaviors. For example, if the current skill is a ranged attack, this list will contain a
    /// <see cref="Facts.Range" /> to indicate the maximum range. The list type is abstract, the derived types are documented
    /// in the Facts namespace.</summary>
    public required IReadOnlyList<Fact>? Facts { get; init; }

    /// <summary>Some specialization traits can alter this skill's <see cref="Facts" />, modifying their behavior or adding new
    /// behaviors. This list contains the overrides that apply when a certain trait is equipped.</summary>
    public required IReadOnlyList<TraitedFact>? TraitedFacts { get; init; }

    /// <summary>The description as it appears in the tooltip of the skill.</summary>
    public required string Description { get; init; }

    /// <summary>The URL of the skill icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the skill icon.</summary>
    public required Uri? IconUrl { get; init; }

#pragma warning disable CA1721 // Property names should not match get methods
    /// <summary>The chat code of the skill. This can be used to link the skill in the chat, but also in guild or squad
    /// messages.</summary>
    public required string ChatLink { get; init; }
#pragma warning restore CA1721 // Property names should not match get methods

    /// <summary>The skill category as displayed in the tooltip.</summary>
    public required IReadOnlyList<Extensible<SkillCategoryName>> Categories { get; init; }

    /// <summary>Contains various modifiers that affect how skills behave.</summary>
    public required SkillFlags SkillFlags { get; init; }

#pragma warning disable CA1024 // Use properties where appropriate
    /// <summary>Gets a chat link object for this skill.</summary>
    /// <returns>The chat link as an object.</returns>
    public SkillLink GetChatLink()
    {
        return new() { SkillId = Id };
    }
#pragma warning restore CA1024 // Use properties where appropriate
}
