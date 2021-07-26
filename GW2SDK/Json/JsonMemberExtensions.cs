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
            return instance.Select(value => Parse<TEnum>(value.GetStringRequired()));
        }

        internal static TEnum? GetValue<TEnum>(
            this NullableMember<TEnum> instance,
            MissingMemberBehavior missingMemberBehavior
        ) where TEnum : struct, Enum
        {
            return instance.Select(value => missingMemberBehavior == MissingMemberBehavior.Error
                ? Parse<TEnum>(value.GetStringRequired())
                : TryHardParse<TEnum>(value.GetStringRequired()));
        }

        internal static TEnum GetValue<TEnum>(
            this RequiredMember<TEnum> instance,
            MissingMemberBehavior missingMemberBehavior
        ) where TEnum : struct, Enum
        {
            return instance.Select(value => missingMemberBehavior == MissingMemberBehavior.Error
                ? Parse<TEnum>(value.GetStringRequired())
                : TryHardParse<TEnum>(value.GetStringRequired()));
        }

        internal static TEnum GetValue<TEnum>(
            this OptionalMember<TEnum> instance,
            MissingMemberBehavior missingMemberBehavior
        ) where TEnum : struct, Enum
        {
            return instance.Select(value => missingMemberBehavior == MissingMemberBehavior.Error
                ? Parse<TEnum>(value.GetStringRequired())
                : TryHardParse<TEnum>(value.GetStringRequired()));
        }

        internal static TEnum[] GetValue<TEnum>(
            this RequiredMember<TEnum[]> instance,
            MissingMemberBehavior missingMemberBehavior
        ) where TEnum : struct, Enum
        {
            return instance.Select(value => value.GetArray(item => missingMemberBehavior == MissingMemberBehavior.Error
                ? Parse<TEnum>(item.GetStringRequired())
                : TryHardParse<TEnum>(item.GetStringRequired())));
        }

        internal static TEnum[]? GetValue<TEnum>(
            this OptionalMember<TEnum[]> instance,
            MissingMemberBehavior missingMemberBehavior
        ) where TEnum : struct, Enum
        {
            return instance.Select(value => value.GetArray(item => missingMemberBehavior == MissingMemberBehavior.Error
                ? Parse<TEnum>(item.GetStringRequired())
                : TryHardParse<TEnum>(item.GetStringRequired())));
        }

        private static TEnum Parse<TEnum>(string name) where TEnum : struct, Enum
        {
            if (!Enum.TryParse(name, true, out TEnum value))
            {
               throw new InvalidOperationException(Strings.UnexpectedMember(name));
            }

            return value;
        }

        /// <summary>A variation on Enum.TryParse() that tries harder.</summary>
        /// <typeparam name="TEnum">The type of Enum to parse.</typeparam>
        /// <param name="name">The name of a member of the Enum.</param>
        /// <returns>The Enum value of the member with the specified name.</returns>
        private static TEnum TryHardParse<TEnum>(string name) where TEnum : struct, Enum
        {
            if (Enum.TryParse(name, true, out TEnum result))
            {
                return result;
            }

            // When parsing fails, treat the value as a constant where the name is unknown
            // i.e. unique strings receive a unique value
            //      and duplicate strings receive the same value
            // (Using hash code is not perfect because of collissions, but it's good enough.)
            // The actual value should be treated as an opaque value
            return (TEnum) Enum.ToObject(typeof(TEnum), name.GetDeterministicHashCode());
        }
    }
}
