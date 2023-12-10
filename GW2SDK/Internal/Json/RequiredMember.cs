using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct RequiredMember
{
    public readonly string Name;

    private readonly JsonElement value = default;

    private RequiredMember(string name)
    {
        Name = name;
    }

    private RequiredMember(string name, JsonElement value)
    {
        Name = name;
        this.value = value;
    }

    public static implicit operator RequiredMember(string name) => new(name);

    public static implicit operator RequiredMember(JsonProperty member) =>
        new(member.Name, member.Value);

    public TValue Map<TValue>(Func<JsonElement, TValue> resultSelector)
    {
        if (value.ValueKind == Undefined || value.ValueKind == Null)
        {
            throw new InvalidOperationException($"Missing value for '{Name}'.");
        }

        try
        {
            return resultSelector(value);
        }
        catch (Exception reason)
        {
            throw new InvalidOperationException($"Value for '{Name}' is incompatible.", reason)
            {
                Data =
                {
                    ["ValueKind"] = value.ValueKind.ToString(),
                    ["Value"] = value.GetRawText()
                }
            };
        }
    }
}
