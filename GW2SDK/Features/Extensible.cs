using System.Diagnostics;

namespace GuildWars2;

/// <summary>Represents an enum that can be extended with additional values.</summary>
/// <typeparam name="TEnum">The type of the enum.</typeparam>
[PublicAPI]
[DebuggerDisplay("{ToString(),nq}")]
public struct Extensible<TEnum>(string Name) where TEnum : struct, Enum
{
    /// <summary>Determines whether the current name is defined in the enum.</summary>
    /// <returns><c>true</c> if the name is defined in the enum; otherwise, <c>false</c>.</returns>
    public bool IsDefined()
    {
        if (Name is null)
        {
            return Enum.IsDefined(typeof(TEnum), 0);
        }

        return Enum.TryParse<TEnum>(Name, true, out var _);
    }

    /// <summary>Converts the current name to the corresponding enum value.</summary>
    /// <value>The corresponding enum value if the conversion is successful; otherwise, <c>null</c>.</value>
    public TEnum? ToEnum()
    {
        if (Name is null)
        {
            return Enum.IsDefined(typeof(TEnum), 0) ? default(TEnum) : null;
        }

        if (Enum.TryParse<TEnum>(Name, true, out var value))
        {
            return value;
        }

        return null;
    }

    /// <summary>Determines whether the current instance of Extensible is equal to another Extensible object.</summary>
    /// <param name="other">The Extensible object to compare with the current instance.</param>
    /// <returns><c>true</c> if the current instance is equal to the other object; otherwise, <c>false</c>.</returns>
    public bool Equals(Extensible<TEnum> other) =>
        string.Equals(ToString(), other.ToString(), StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Extensible<TEnum> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(ToString());

    /// <inheritdoc />
    public override string ToString()
    {
        if (Name is not null)
        {
            return Name;
        }

        if (Enum.IsDefined(typeof(TEnum), 0))
        {
            return default(TEnum)!.ToString();
        }

        return "";
    }

    /// <summary>Determines whether two enums are equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Extensible<TEnum> left, Extensible<TEnum> right) =>
        left.Equals(right);

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Extensible<TEnum> left, Extensible<TEnum> right) =>
        !left.Equals(right);

    /// <summary>Determines whether two enums are equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Extensible<TEnum> left, TEnum right) => left.Equals(right);

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Extensible<TEnum> left, TEnum right) => !left.Equals(right);

    /// <summary>Determines whether two enums are equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Extensible<TEnum> left, string right) => left.Equals(right);

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Extensible<TEnum> left, string right) => !left.Equals(right);

    /// <summary>Implicitly converts a string to an instance of the Extensible enum class.</summary>
    /// <param name="name">The name of the enum value.</param>
    /// <returns>An instance of the Extensible enum class.</returns>
    public static implicit operator Extensible<TEnum>(string name) => new(name);

    /// <summary>Implicitly converts an enum to an instance of the Extensible enum class.</summary>
    /// <param name="name">The enum value.</param>
    /// <returns>An instance of the Extensible enum class.</returns>
    public static implicit operator Extensible<TEnum>(TEnum name) => new(name.ToString());
}
