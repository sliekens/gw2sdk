using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct NullableMember
{
    public readonly ReadOnlySpan<char> Name;

    public readonly JsonElement Value = default;

    public bool IsUndefined => Value.ValueKind == Undefined;

    public bool IsUndefinedOrNull => IsUndefined || Value.ValueKind == Null;

    private NullableMember(ReadOnlySpan<char> name)
    {
        Name = name;
    }

    private NullableMember(ReadOnlySpan<char> name, JsonElement value)
    {
        Name = name;
        Value = value;
    }

    public static implicit operator NullableMember(string name) => new(name.AsSpan());

    public static implicit operator NullableMember(JsonProperty member) =>
        new(member.Name.AsSpan(), member.Value);

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
}
