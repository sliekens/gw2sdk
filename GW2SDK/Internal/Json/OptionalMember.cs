using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct OptionalMember
{
    public readonly ReadOnlySpan<char> Name;

    public readonly JsonElement Value = default;

    public bool IsUndefined => Value.ValueKind == Undefined;

    public bool IsUndefinedOrNull => IsUndefined || Value.ValueKind == Null;

    private OptionalMember(ReadOnlySpan<char> name)
    {
        Name = name;
    }

    private OptionalMember(ReadOnlySpan<char> name, JsonElement value)
    {
        Name = name;
        Value = value;
    }

    public static implicit operator OptionalMember(string name) => new(name.AsSpan());

    public static implicit operator OptionalMember(JsonProperty member) =>
        new(member.Name.AsSpan(), member.Value);

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
}
