using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct OptionalMember
{
    public readonly ReadOnlySpan<char> Name;

    private readonly JsonElement value = default;

    private OptionalMember(ReadOnlySpan<char> name)
    {
        Name = name;
    }

    private OptionalMember(ReadOnlySpan<char> name, JsonElement value)
    {
        Name = name;
        this.value = value;
    }

    public static implicit operator OptionalMember(string name) => new(name.AsSpan());

    public static implicit operator OptionalMember(JsonProperty member) =>
        new(member.Name.AsSpan(), member.Value);

    public TValue? Map<TValue>(Func<JsonElement, TValue> resultSelector)
    {
        if (value.ValueKind == Undefined || value.ValueKind == Null)
        {
            return default;
        }

        try
        {
            return resultSelector(value);
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
