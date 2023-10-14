using System.Text.Json;
using static System.Text.Json.JsonValueKind;
using Array = System.Array;

namespace GuildWars2.Json;

internal ref struct NullableMember<T> where T : struct
{
    public JsonElement Value = default;

    public readonly bool IsUndefined => Value.ValueKind == Undefined;

    public readonly bool IsMissing => Value.ValueKind is Null or Undefined;

#if !NET // Because there is no implicit cast from String to ReadOnlySpan
    internal NullableMember(string name)
    {
        Name = name.AsSpan();
    }
#endif

    internal NullableMember(ReadOnlySpan<char> name)
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

    internal IReadOnlyList<T?> SelectMany(Func<JsonElement, T?> resultSelector)
    {
        if (IsMissing)
        {
            return Array.Empty<T?>();
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
