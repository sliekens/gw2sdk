using System;
using System.Globalization;

namespace GW2SDK.Impl.JsonReaders
{
    public readonly ref struct JsonPath
    {
        private readonly ReadOnlySpan<char> _path;

        public JsonPath(in ReadOnlySpan<char> path)
        {
            _path = path;
        }

        public static JsonPath Root => new JsonPath("$");

        public JsonPath AccessProperty(in string propertyName)
        {
            // TODO: use String.Concat when .NET Standard gets support for Concat(ReadOnlySpan<Char>, ...)
            return new JsonPath(Concat(_path, ".", propertyName));
        }

        public JsonPath AccessArrayIndex(in int index)
        {
            // TODO: use String.Concat when .NET Standard gets support for Concat(ReadOnlySpan<Char>, ...)
            return new JsonPath(Concat(_path, "[", index.ToString(NumberFormatInfo.InvariantInfo), "]"));
        }

        public override string ToString() => _path.ToString();

        private static ReadOnlySpan<char> Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1, ReadOnlySpan<char> str2)
        {
            var length = checked(str0.Length + str1.Length + str2.Length);
            if (length == 0)
            {
                return string.Empty;
            }

            var result = new char[length];
            var resultSpan = new Span<char>(result);

            str0.CopyTo(resultSpan);
            resultSpan = resultSpan.Slice(str0.Length);

            str1.CopyTo(resultSpan);
            resultSpan = resultSpan.Slice(str1.Length);

            str2.CopyTo(resultSpan);
            
            return result;
        }

        private static ReadOnlySpan<char> Concat(in ReadOnlySpan<char> str0, in ReadOnlySpan<char> str1, in ReadOnlySpan<char> str2, in ReadOnlySpan<char> str3)
        {
            var length = checked(str0.Length + str1.Length + str2.Length + str3.Length);
            if (length == 0)
            {
                return string.Empty;
            }

            var result = new char[length];
            var resultSpan = new Span<char>(result);

            str0.CopyTo(resultSpan);
            resultSpan = resultSpan.Slice(str0.Length);

            str1.CopyTo(resultSpan);
            resultSpan = resultSpan.Slice(str1.Length);

            str2.CopyTo(resultSpan);
            resultSpan = resultSpan.Slice(str2.Length);

            str3.CopyTo(resultSpan);

            return result;
        }

    }
}
