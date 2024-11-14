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

    /// <summary>Returns a string that represents the enabled flags.</summary>
    /// <returns>A string that represents the enabled flags.</returns>
    public sealed override string ToString()
    {
        var flags = string.Join(
            ", ",
            GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.PropertyType == typeof(bool))
                .Where(property => (bool?)property.GetValue(this) == true)
                .Select(property => property.Name)
                .Concat(Other)
                .ToList()
        );
        return $"[{flags}]";
    }
}
