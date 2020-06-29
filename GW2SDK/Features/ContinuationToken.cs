using System;

namespace GW2SDK
{
    public sealed class ContinuationToken
    {
        private readonly string _token;

        internal ContinuationToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(token));
            _token = token;
        }

        public override bool Equals(object? obj) =>
            obj is ContinuationToken token &&
            _token == token._token;

        public override int GetHashCode() => HashCode.Combine(_token);

        public override string ToString() => _token;

        public static implicit operator string(ContinuationToken token) => token._token;
    }
}
