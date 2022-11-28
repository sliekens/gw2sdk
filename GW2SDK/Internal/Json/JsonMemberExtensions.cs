using System;
using System.Collections.Generic;

namespace GuildWars2.Json;

internal static class JsonMemberExtensions
{
    internal static int GetValue(this RequiredMember<Coin> instance) =>
        instance.Select(json => new Coin(json.GetInt32()));

    internal static int GetValue(this RequiredMember<int> instance) =>
        instance.Select(json => json.GetInt32());

    internal static int? GetValue(this NullableMember<int> instance) =>
        instance.Select(json => json.GetInt32());

    internal static long GetValue(this RequiredMember<long> instance) =>
        instance.Select(json => json.GetInt64());

    internal static double GetValue(this RequiredMember<double> instance) =>
        instance.Select(json => json.GetDouble());

    internal static string GetValue(this RequiredMember<string> instance) =>
        instance.Select(json => json.GetStringRequired());

    internal static string? GetValueOrNull(this OptionalMember<string> instance) =>
        instance.Select(json => json.GetString());

    internal static string GetValueOrEmpty(this OptionalMember<string> instance) =>
        instance.Select(json => json.GetString()) ?? "";

    internal static bool GetValue(this RequiredMember<bool> instance) =>
        instance.Select(json => json.GetBoolean());

    internal static bool GetValue(this OptionalMember<bool> instance) =>
        instance.Select(json => json.GetBoolean());

    internal static bool? GetValue(this NullableMember<bool> instance) =>
        instance.Select(json => json.GetBoolean());

    internal static DateTimeOffset GetValue(this RequiredMember<DateTimeOffset> instance) =>
        instance.Select(json => json.GetDateTimeOffset());

    internal static TEnum? GetValue<TEnum>(
        this NullableMember<TEnum> instance,
        MissingMemberBehavior missingMemberBehavior
    ) where TEnum : struct, Enum =>
        instance.Select(value => value.GetEnum<TEnum>(missingMemberBehavior));

    internal static TEnum GetValue<TEnum>(
        this RequiredMember<TEnum> instance,
        MissingMemberBehavior missingMemberBehavior
    ) where TEnum : struct, Enum =>
        instance.Select(value => value.GetEnum<TEnum>(missingMemberBehavior));

    internal static TEnum GetValue<TEnum>(
        this OptionalMember<TEnum> instance,
        MissingMemberBehavior missingMemberBehavior
    ) where TEnum : struct, Enum =>
        instance.Select(value => value.GetEnum<TEnum>(missingMemberBehavior));

    internal static IReadOnlyCollection<TEnum> GetValues<TEnum>(
        this RequiredMember<TEnum> instance,
        MissingMemberBehavior missingMemberBehavior
    ) where TEnum : struct, Enum =>
        instance.SelectMany(value => value.GetEnum<TEnum>(missingMemberBehavior));

    internal static IReadOnlyCollection<TEnum>? GetValues<TEnum>(
        this OptionalMember<TEnum> instance,
        MissingMemberBehavior missingMemberBehavior
    ) where TEnum : struct, Enum =>
        instance.SelectMany(value => value.GetEnum<TEnum>(missingMemberBehavior));
}
