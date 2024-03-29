﻿using System.Text.Json;
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

    public TValue Map<TValue>(Func<JsonElement, TValue> resultSelector)
    {
        if (member.Value.ValueKind == Undefined || member.Value.ValueKind == Null)
        {
            throw new InvalidOperationException($"Missing value for '{Name}'.");
        }

        try
        {
            return resultSelector(member.Value);
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
