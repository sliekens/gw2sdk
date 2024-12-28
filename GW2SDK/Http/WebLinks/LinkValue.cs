namespace GuildWars2.Http.WebLinks;

/// <summary>Represents a web link.</summary>
[PublicAPI]
public sealed class LinkValue
{
    /// <summary>Creates an instance of <see cref="LinkValue" />.</summary>
    /// <param name="target">The link target.</param>
    /// <param name="relationType"></param>
    /// <exception cref="ArgumentException"></exception>
    public LinkValue(string target, string relationType)
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            ThrowHelper.ThrowBadArgument("Value cannot be null or whitespace.", nameof(target));
        }

        if (string.IsNullOrWhiteSpace(relationType))
        {
            ThrowHelper.ThrowBadArgument(
                "Value cannot be null or whitespace.",
                nameof(relationType)
            );
        }

        Target = target;
        RelationType = relationType;
    }

    /// <summary>The target URI.</summary>
    public string Target { get; }

    /// <summary>The link's relation type.</summary>
    public string RelationType { get; }

    /// <summary>Returns the link value as a string.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"<{Target}>; rel={RelationType}";
    }
}
