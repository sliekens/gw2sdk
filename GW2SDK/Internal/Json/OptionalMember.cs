using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GW2SDK.Json;

internal ref struct OptionalMember<T>
{
    public JsonElement Value = default;

    public bool IsMissing => Value.ValueKind is Null or Undefined;

#if !NET // Because there is no implicit cast from String to ReadOnlySpan
    internal OptionalMember(string name)
    {
        Name = name.AsSpan();
    }
#endif

    internal OptionalMember(ReadOnlySpan<char> name)
    {
        Name = name;
    }

    internal ReadOnlySpan<char> Name { get; }

    internal T? Select(Func<JsonElement, T?> resultSelector)
    {
        if (IsMissing)
        {
            return default;
        }

        try
        {
            return resultSelector(Value);
        }
        catch (Exception reason)
        {
            throw new InvalidOperationException(
                $"Value for '{Name.ToString()}' is incompatible.",
                reason
            );
        }
    }

    internal IReadOnlyCollection<T>? SelectMany(Func<JsonElement, T> resultSelector)
    {
        if (IsMissing)
        {
            return null;
        }

        try
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            return Value.EnumerateArray()
                .Select(item => resultSelector(item))
                .ToList()
                .AsReadOnly();
        }
        catch (Exception reason)
        {
            throw new InvalidOperationException(
                $"Value for '{Name.ToString()}' is incompatible.",
                reason
            );
        }
    }
}
