﻿namespace GuildWars2.Http.WebLinks;

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

#pragma warning disable CS0618 // Suppress obsolete warning
        Target = target;
#pragma warning restore CS0618
        TargetUrl = new Uri(target, UriKind.RelativeOrAbsolute);
        RelationType = relationType;
    }

    /// <summary>The target URI as a string. Use TargetUrl instead.</summary>
    [Obsolete("Use TargetUrl instead.")]
    public string Target { get; }

    /// <summary>The target URI.</summary>
    public Uri TargetUrl { get; }

    /// <summary>The link's relation type.</summary>
    public string RelationType { get; }

    /// <summary>Returns the link value as a string.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"<{TargetUrl}>; rel={RelationType}";
    }
}
