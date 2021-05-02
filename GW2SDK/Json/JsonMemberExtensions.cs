using System;

namespace GW2SDK.Json
{
    internal static class JsonMemberExtensions
    {
        internal static int GetValue(this RequiredMember<int> instance)
        {
            return instance.Select(json => json.GetInt32());
        }

        internal static int? GetValue(this NullableMember<int> instance)
        {
            return instance.Select(json => json.GetInt32());
        }

        internal static double GetValue(this RequiredMember<double> instance)
        {
            return instance.Select(json => json.GetDouble());
        }

        internal static string GetValue(this RequiredMember<string> instance)
        {
            return instance.Select(json => json.GetStringRequired());
        }

        internal static string? GetValueOrNull(this OptionalMember<string> instance)
        {
            return instance.Select(json => json.GetString());
        }

        internal static string GetValueOrEmpty(this OptionalMember<string> instance)
        {
            return instance.Select(json => json.GetString()) ?? "";
        }

        internal static bool GetValue(this RequiredMember<bool> instance)
        {
            return instance.Select(json => json.GetBoolean());
        }

        internal static bool? GetValue(this NullableMember<bool> instance)
        {
            return instance.Select(json => json.GetBoolean());
        }

        internal static DateTimeOffset GetValue(this RequiredMember<DateTimeOffset> instance)
        {
            return instance.Select(json => json.GetDateTimeOffset());
        }

        internal static TEnum GetValue<TEnum>(this OptionalMember<TEnum> instance) where TEnum : struct, Enum
        {
            return instance.Select(value => Enum.Parse<TEnum>(value.GetStringRequired(), true));
        }

        internal static TEnum[]? GetValue<TEnum>(this OptionalMember<TEnum[]> instance) where TEnum : struct, Enum
        {
            return instance.Select(value => value.GetArray(item => Enum.Parse<TEnum>(item.GetStringRequired(), true)));
        }

        internal static TEnum GetValue<TEnum>(this RequiredMember<TEnum> instance) where TEnum : struct, Enum
        {
            return instance.Select(value => Enum.Parse<TEnum>(value.GetStringRequired(), true));
        }

        internal static TEnum[] GetValue<TEnum>(this RequiredMember<TEnum[]> instance) where TEnum : struct, Enum
        {
            return instance.Select(value => value.GetArray(item => Enum.Parse<TEnum>(item.GetStringRequired(), true)));
        }
    }
}
