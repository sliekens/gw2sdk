using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct RequiredMember
{
    private readonly string? name;

    private readonly JsonProperty member;

    private RequiredMember(string name)
    {
        this.name = name;
    }

    private RequiredMember(JsonProperty member)
    {
        this.member = member;
    }

    public string Name => name ?? member.Name;

    public static implicit operator RequiredMember(string name) => new(name);

    public static implicit operator RequiredMember(JsonProperty member) => new(member);

    public bool Match(JsonProperty property) =>
        member.Value.ValueKind == Undefined && property.NameEquals(name);

    public TValue Map<TValue>(Func<JsonElement, TValue> transform)
    {
        if (member.Value.ValueKind == Undefined || member.Value.ValueKind == Null)
        {
            JsonThrowHelper.ThrowMissingValue(Name);
        }

        try
        {
            return transform(member.Value);
        }
        catch (Exception reason)
        {
            JsonThrowHelper.ThrowIncompatibleValue(Name, reason, member);
        }

        // Fix CS0161, a return is needed even though this code is unreachable
        return default;
    }
}
