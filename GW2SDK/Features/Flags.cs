using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace GuildWars2;

/// <summary>The base type for flags objects.</summary>
[PublicAPI]
[DataTransferObject]
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public abstract record Flags
{
    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyList<string> Other { get; init; }

    /// <inheritdoc />
    public virtual bool Equals(Flags? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is null)
        {
            return false;
        }

        if (GetType() != other.GetType())
        {
            return false;
        }

        var flags = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(property => property.PropertyType == typeof(bool))
            .ToList();

        var left = flags.Select(property => (bool?)property.GetValue(this));
        var right = flags.Select(property => (bool?)property.GetValue(other));
        return left.SequenceEqual(right) && Other.SequenceEqual(other.Other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        HashCode hash = new();
        var flags = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(property => property.PropertyType == typeof(bool))
            .Select(property => property.GetValue(this));

        foreach (var flag in flags)
        {
            hash.Add(flag);
        }

        foreach (var flag in Other)
        {
            hash.Add(flag);
        }

        return hash.ToHashCode();
    }

    /// <summary>Returns a string that represents the enabled flags.</summary>
    /// <returns>A string that represents the enabled flags.</returns>
    public sealed override string ToString()
    {
        var flags = string.Join(
            ", ",
            GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.PropertyType == typeof(bool) && (bool?)property.GetValue(this) == true)
                .Select(property => property.Name)
                .Concat(Other)
                .ToList()
        );
        return $"[{flags}]";
    }
}
