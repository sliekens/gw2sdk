using System;
using System.Collections.Generic;

namespace GuildWars2;

/// <summary>Contains commonly used precondition checks.</summary>
internal static class Check
{
    internal static void String(string text, string parameterName)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse // don't trust NRT
        if (text is null)
        {
            throw new ArgumentNullException(parameterName, "The string must not be empty.");
        }

        if (text.Length == 0)
        {
            throw new ArgumentException("The string must not be empty.", parameterName);
        }
    }

    internal static void Constant<TEnum>(TEnum value, string parameterName)
        where TEnum : struct, Enum
    {
#if NET
        if (!Enum.IsDefined(value))
#else
        if (!Enum.IsDefined(typeof(TEnum), value))
#endif
        {
            throw new ArgumentException("The value must be a named constant.", parameterName);
        }
    }

    internal static void Collection(IReadOnlyCollection<int> collection, string parameterName) =>
        EnsureCollectionNotNullOrEmpty(collection, parameterName);

    internal static void Collection<TEnum>(
        IReadOnlyCollection<TEnum> collection,
        string parameterName
    ) where TEnum : struct, Enum
    {
        EnsureCollectionNotNullOrEmpty(collection, parameterName);

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var text in collection)
        {
#if NET
            if (!Enum.IsDefined(text))
#else
            if (!Enum.IsDefined(typeof(TEnum), text))
#endif
            {
                throw new ArgumentException(
                    "The collection must contain only named constants.",
                    parameterName
                );
            }
        }
    }

    internal static void Collection(IReadOnlyCollection<string> collection, string parameterName)
    {
        EnsureCollectionNotNullOrEmpty(collection, parameterName);

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var text in collection)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(
                    "The collection must not contain empty strings.",
                    parameterName
                );
            }
        }
    }

    private static void EnsureCollectionNotNullOrEmpty<T>(
        IReadOnlyCollection<T> collection,
        string parameterName
    )
    {
        if (collection is null)
        {
            throw new ArgumentNullException(parameterName, "The collection must not be null.");
        }

        if (collection.Count == 0)
        {
            throw new ArgumentException("The collection must contain one or more values.");
        }
    }
}
