﻿using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct NullableMember
{
    private readonly string? name;

    private readonly JsonProperty member;

    private NullableMember(string name)
    {
        this.name = name;
    }
    private NullableMember(JsonProperty member)
    {
        this.member = member;
    }

    public string Name => name ?? member.Name;

    public static implicit operator NullableMember(string name) => new(name);

    public static implicit operator NullableMember(JsonProperty member) => new(member);

    public bool Match(JsonProperty property) =>
        member.Value.ValueKind == Undefined && property.NameEquals(name);

    public TValue? Map<TValue>(Func<JsonElement, TValue> transform) where TValue : struct
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
