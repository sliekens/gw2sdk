using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal ref struct RequiredMember<T>
{
    public JsonElement Value = default;

    public readonly bool IsUndefined => Value.ValueKind == Undefined;

    public readonly bool IsMissing => Value.ValueKind is Null or Undefined;

#if !NET // Because there is no implicit cast from String to ReadOnlySpan
    internal RequiredMember(string name)
    {
        Name = name.AsSpan();
    }
#endif

    internal RequiredMember(ReadOnlySpan<char> name)
    {
        Name = name;
    }

    internal ReadOnlySpan<char> Name { get; }

    internal T Select(Func<JsonElement, T> resultSelector)
    {
        if (IsMissing)
        {
            throw new InvalidOperationException($"Missing value for '{Name.ToString()}'.");
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

    internal IReadOnlyList<T> SelectMany(Func<JsonElement, T> resultSelector)
    {
        if (IsMissing)
        {
            throw new InvalidOperationException($"Missing value for '{Name.ToString()}'.");
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
