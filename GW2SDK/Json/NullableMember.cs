using System;
using System.Text.Json;

namespace GW2SDK.Json
{
    internal readonly ref struct NullableMember<T> where T : struct
    {
        private readonly JsonMember member;

#if !NET // Because there is no implicit cast from String to ReadOnlySpan
        internal NullableMember(string name)
        {
            Name = name.AsSpan();
            member = default;
        }
#endif

        internal NullableMember(ReadOnlySpan<char> name)
        {
            Name = name;
            member = default;
        }

        private NullableMember(ReadOnlySpan<char> name, JsonMember member)
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

        internal NullableMember<T> From(JsonElement member) => new(Name, new JsonMember(member));
    }
}
