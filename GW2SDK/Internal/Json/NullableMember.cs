using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct NullableMember
{
    public readonly ReadOnlySpan<char> Name;

    private readonly JsonElement value = default;

    private NullableMember(ReadOnlySpan<char> name)
    {
        Name = name;
    }

    private NullableMember(ReadOnlySpan<char> name, JsonElement value)
    {
        Name = name;
        this.value = value;
    }

    public static implicit operator NullableMember(string name) => new(name.AsSpan());

    public static implicit operator NullableMember(JsonProperty member) =>
        new(member.Name.AsSpan(), member.Value);

    public TValue? Map<TValue>(Func<JsonElement, TValue> resultSelector) where TValue : struct
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
