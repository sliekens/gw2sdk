using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct OptionalMember
{
    public readonly ReadOnlySpan<char> Name;

    public readonly JsonElement Value = default;

    public bool IsUndefined => Value.ValueKind == Undefined;

    public bool IsUndefinedOrNull => IsUndefined || Value.ValueKind == Null;

    public OptionalMember(string name)
    {
        Name = name.AsSpan();
    }

    private OptionalMember(string name, JsonElement value)
    {
        Name = name.AsSpan();
        Value = value;
    }

    public static implicit operator OptionalMember(JsonProperty member) =>
        new(member.Name, member.Value);

    public TValue? Select<TValue>(Func<JsonElement, TValue> resultSelector)
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

    public IReadOnlyList<TValue>? SelectMany<TValue>(Func<JsonElement, TValue> resultSelector)
    {
        if (IsUndefinedOrNull)
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
