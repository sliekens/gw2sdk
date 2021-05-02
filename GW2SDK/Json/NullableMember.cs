using System;
using System.Text.Json;

namespace GW2SDK.Json
{
    internal readonly ref struct NullableMember<T>
        where T : struct
    {
        private readonly JsonMember _member;
        
        internal NullableMember(ReadOnlySpan<char> name)
        {
            Name = name;
            _member = default;
        }

        private NullableMember(ReadOnlySpan<char> name, JsonMember member)
        {
            Name = name;
            _member = member;
        }

        internal ReadOnlySpan<char> Name { get; }

        internal T? Select(Func<JsonElement, T?> selector)
        {
            if (_member.IsMissing)
            {
                return default;
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

        internal NullableMember<T> From(JsonElement member) => new(Name, new JsonMember(member));
    }
}
