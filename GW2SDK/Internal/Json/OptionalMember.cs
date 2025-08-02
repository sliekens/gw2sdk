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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1231:Make parameter ref read-only", Justification = "Makes this code unusable")]
    private OptionalMember(JsonProperty member)
    {
        this.member = member;
    }

    public string Name => name ?? member.Name;

    public static implicit operator OptionalMember(string name)
    {
        return new(name);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1231:Make parameter ref read-only", Justification = "Makes this code unusable")]
    public static implicit operator OptionalMember(JsonProperty member)
    {
        return new(member);
    }

    public bool Match(in JsonProperty property)
    {
        return member.Value.ValueKind == Undefined && property.NameEquals(name);
    }

    public TValue? Map<TValue>(JsonTransform<TValue> transform)
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
            JsonThrowHelper.ThrowIncompatibleValue(Name, reason, member);
        }

        // Fix CS0161, a return is needed even though this code is unreachable
        return default;
    }
}
