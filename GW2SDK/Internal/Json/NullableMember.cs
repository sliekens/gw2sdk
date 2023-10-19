using System.Text.Json;
using static System.Text.Json.JsonValueKind;
using Array = System.Array;

namespace GuildWars2.Json;

internal readonly ref struct NullableMember
{
    public readonly ReadOnlySpan<char> Name;

    public readonly JsonElement Value = default;

    public bool IsUndefined => Value.ValueKind == Undefined;

    public bool IsUndefinedOrNull => IsUndefined || Value.ValueKind == Null;

    public NullableMember(string name)
    {
        Name = name.AsSpan();
    }

    private NullableMember(string name, JsonElement value)
    {
        Name = name.AsSpan();
        Value = value;
    }

    public static implicit operator NullableMember(JsonProperty member) =>
        new(member.Name, member.Value);

    public TValue? Select<TValue>(Func<JsonElement, TValue> resultSelector) where TValue : struct
    {
        if (IsUndefinedOrNull)
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

    public IReadOnlyList<TValue?> SelectMany<TValue>(Func<JsonElement, TValue?> resultSelector)
        where TValue : struct
    {
        if (IsUndefinedOrNull)
        {
            return Array.Empty<TValue?>();
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
