using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct OptionalMember
{
    private readonly string? name;

    private readonly JsonProperty member;

    private OptionalMember(string name)
    {
        this.name = name;
    }
    private OptionalMember(JsonProperty member)
    {
        this.member = member;
    }

    public string Name => name ?? member.Name;

    public static implicit operator OptionalMember(string name) => new(name);

    public static implicit operator OptionalMember(JsonProperty member) => new(member);

    public bool Match(JsonProperty property) =>
        member.Value.ValueKind == Undefined && property.NameEquals(name);

    public TValue? Map<TValue>(Func<JsonElement, TValue> transform)
    {
        if (member.Value.ValueKind == Undefined || member.Value.ValueKind == Null)
        {
            return default;
        }

        try
        {
            return transform(member.Value);
        }
        catch (Exception reason)
        {
            throw new InvalidOperationException($"Value for '{Name}' is incompatible.", reason)
            {
                Data =
                {
                    ["ValueKind"] = member.Value.ValueKind.ToString(),
                    ["Value"] = member.Value.GetRawText()
                }
            };
        }
    }
}
