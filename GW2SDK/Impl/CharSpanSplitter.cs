using System;

namespace GW2SDK.Impl
{
    internal ref struct CharSpanSplitter
    {
        private ReadOnlySpan<char> _value;

        private readonly char _separator;

        public CharSpanSplitter(ReadOnlySpan<char> value, char separator)
        {
            _value = value;
            _separator = separator;
            Current = ReadOnlySpan<char>.Empty;
        }

        public ReadOnlySpan<char> Current { get; private set; }

        public bool MoveNext()
        {
            if (_value == ReadOnlySpan<char>.Empty)
            {
                return false;
            }

            var index = _value.IndexOf(_separator);
            if (index == -1)
            {
                Current = _value;
                _value = ReadOnlySpan<char>.Empty;
                return true;
            }

            Current = _value.Slice(0, index);
            _value = _value.Slice(index + 1);
            return true;
        }
    }
}