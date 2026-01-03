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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1231:Make parameter ref read-only", Justification = "Makes this code unusable")]
    private RequiredMember(JsonProperty member)
    {
        this.member = member;
    }

    public string Name => name ?? member.Name;

    public static implicit operator RequiredMember(string name)
    {
        return new(name);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1231:Make parameter ref read-only", Justification = "Makes this code unusable")]
    public static implicit operator RequiredMember(JsonProperty member)
    {
        return new(member);
    }

    public bool Match(in JsonProperty property)
    {
        return member.Value.ValueKind == Undefined && property.NameEquals(name);
    }

    public TValue Map<TValue>(JsonTransform<TValue> transform)
    {
        if (member.Value.ValueKind is Null or Undefined)
        {
            JsonThrowHelper.ThrowMissingValue(Name);
        }

#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            return transform(member.Value);
        }
        catch (Exception reason)
        {
            JsonThrowHelper.ThrowIncompatibleValue(Name, reason, member);
        }
#pragma warning restore CA1031 // Do not catch general exception types

        // Fix CS0161, a return is needed even though this code is unreachable
        return default;
    }
}
