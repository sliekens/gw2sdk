using System;
using System.Text.Json;

namespace GW2SDK.Json
{
    internal readonly ref struct RequiredMember<T>
    {
        private readonly JsonMember _member;

        internal RequiredMember(ReadOnlySpan<char> name)
        {
            Name = name;
            _member = default;
        }

        private RequiredMember(ReadOnlySpan<char> name, JsonMember member)
        {
            Name = name;
            _member = member;
        }

        internal ReadOnlySpan<char> Name { get; }

        internal T Select(Func<JsonElement, T> selector)
        {
            if (_member.IsMissing)
            {
                throw new InvalidOperationException($"Missing value for '{Name.ToString()}'.");
            }

            try
            {
                return selector(_member.Value);
            }
            catch (Exception reason)
            {
                throw new InvalidOperationException($"Value for '{Name.ToString()}' is incompatible.", reason);
            }
        }

        internal RequiredMember<T> From(JsonElement member) => new(Name, new JsonMember(member));
    }
}
