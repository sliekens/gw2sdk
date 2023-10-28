using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct OptionalMember
{
    public readonly string Name;

    private readonly JsonElement value = default;

    private OptionalMember(string name)
    {
        Name = name;
    }

    private OptionalMember(string name, JsonElement value)
    {
        Name = name;
        this.value = value;
    }

    public static implicit operator OptionalMember(string name) => new(name);

    public static implicit operator OptionalMember(JsonProperty member) =>
        new(member.Name, member.Value);

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
            throw new InvalidOperationException($"Value for '{Name}' is incompatible.", reason);
        }
    }
}
