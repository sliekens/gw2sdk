using System.Collections;
using System.Diagnostics;

namespace GuildWars2.Http;

/// <summary>Used to build a query string, e.g. <c><![CDATA[ids=1,2,3&lang=fr&v=latest]]></c>. Not meant to be used
/// directly.</summary>
[PublicAPI]
[DebuggerDisplay("{Build()}")]
public sealed class QueryBuilder : IEnumerable<KeyValuePair<string, string>>
{
    private readonly List<KeyValuePair<string, string>> arguments = [];

    private int queryLength;

    /// <summary>Returns an enumerator that iterates through the collection of query arguments.</summary>
    /// <returns>An enumerator for the query arguments.</returns>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return arguments.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Adds an argument with the specified key and value to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="value">The value of the argument.</param>
    public void Add(string key, string value)
    {
        ThrowHelper.ThrowIfNull(key);
        ThrowHelper.ThrowIfNull(value);

        arguments.Add(new KeyValuePair<string, string>(key, value));

        // Length of '?key=value' (or '&key=value')
        queryLength += key.Length + value.Length + 2;
    }

    /// <summary>Builds the query string from the arguments in the <see cref="QueryBuilder" />.</summary>
    /// <returns>The built query string.</returns>
    public string Build()
    {
        if (queryLength == 0)
        {
            return "";
        }

#if NET
        return string.Create(
            queryLength,
            arguments,
            static (buffer, state) =>
            {
                int position = 0;
                foreach ((string key, string value) in state)
                {
                    buffer[position] = position == 0 ? '?' : '&';
                    position++;

                    key.CopyTo(buffer[position..]);
                    position += key.Length;

                    buffer[position++] = '=';

                    value.CopyTo(buffer[position..]);
                    position += value.Length;
                }
            }
        );
#else
        Span<char> buffer = stackalloc char[queryLength];
        int position = 0;
        foreach ((string key, string value) in arguments)
        {
            buffer[position] = position == 0 ? '?' : '&';
            position++;

            key.AsSpan().CopyTo(buffer[position..]);
            position += key.Length;

            buffer[position++] = '=';

            value.AsSpan().CopyTo(buffer[position..]);
            position += value.Length;
        }

        return buffer.ToString();
#endif
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Build();
    }
}
