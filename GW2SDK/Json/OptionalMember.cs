using System;
using System.Text.Json;

namespace GW2SDK.Json
{
    internal readonly ref struct OptionalMember<T>
    {
        private readonly JsonMember member;

#if !NET // Because there is no implicit cast from String to ReadOnlySpan
        internal OptionalMember(string name)
        {
            Name = name.AsSpan();
            member = default;
        }
#endif

        internal OptionalMember(ReadOnlySpan<char> name)
        {
            Name = name;
            member = default;
        }

        private OptionalMember(ReadOnlySpan<char> name, JsonMember member)
        {
            Name = name;
            this.member = member;
        }

        internal ReadOnlySpan<char> Name { get; }

        internal T? Select(Func<JsonElement, T?> selector)
        {
            if (member.IsMissing)
            {
                return default;
            }

            try
            {
                return selector(member.Value);
            }
            catch (Exception reason)
            {
                throw new InvalidOperationException($"Value for '{Name.ToString()}' is incompatible.", reason);
            }
        }

        internal OptionalMember<T> From(JsonElement member) => new(Name, new JsonMember(member));
    }
}
