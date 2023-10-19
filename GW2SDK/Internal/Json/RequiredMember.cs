using System.Text.Json;
using static System.Text.Json.JsonValueKind;

namespace GuildWars2.Json;

internal readonly ref struct RequiredMember
{
    public readonly ReadOnlySpan<char> Name;

    public readonly JsonElement Value = default;

    public bool IsUndefined => Value.ValueKind == Undefined;

    public bool IsUndefinedOrNull => IsUndefined || Value.ValueKind == Null;

    private RequiredMember(ReadOnlySpan<char> name)
    {
        Name = name;
    }

    private RequiredMember(ReadOnlySpan<char> name, JsonElement value)
    {
        Name = name;
        Value = value;
    }

    public static implicit operator RequiredMember(string name) => new(name.AsSpan());

    public static implicit operator RequiredMember(JsonProperty member) =>
        new(member.Name.AsSpan(), member.Value);

    public TValue Select<TValue>(Func<JsonElement, TValue> resultSelector)
    {
        if (IsUndefinedOrNull)
        {
            throw new InvalidOperationException($"Missing value for '{Name.ToString()}'.");
        }

        try
        {
            return resultSelector(Value);
        }
        catch (Exception reason)
        {
            throw new InvalidOperationException(
                $"Value for '{Name.ToString()}' is incompatible.",
                reason
            );
        }
    }

    public IReadOnlyList<TValue> SelectMany<TValue>(Func<JsonElement, TValue> resultSelector)
    {
        if (IsUndefinedOrNull)
        {
            throw new InvalidOperationException($"Missing value for '{Name.ToString()}'.");
        }

        try
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            return Value.EnumerateArray()
                .Select(item => resultSelector(item))
                .ToList()
                .AsReadOnly();
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
