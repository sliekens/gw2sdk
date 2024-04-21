using System.Collections;
using static System.Globalization.NumberFormatInfo;
using Argument = System.Collections.Generic.KeyValuePair<string, string>;

namespace GuildWars2.Http;

/// <summary>Used to build a query string, e.g. <c><![CDATA[ids=1,2,3&lang=fr&v=latest]]></c>. Not meant to be used
/// directly.</summary>
[PublicAPI]
public sealed class QueryBuilder : IEnumerable
{
    private readonly List<Argument> arguments = [];

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)arguments).GetEnumerator();

    /// <summary>Adds an argument with the specified key and value to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="value">The value of the argument.</param>
    public void Add(string key, int value) => arguments.Add(new Argument(key, ToString(value)));

    /// <summary>Adds an argument with the specified key and value to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="value">The value of the argument.</param>
    public void Add(string key, string value) => arguments.Add(new Argument(key, value));

    /// <summary>Adds an argument with the specified key and values to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="values">The values of the argument.</param>
    public void Add(string key, IEnumerable<string> values) =>
        arguments.Add(new Argument(key, values.Select(value => value).ToCsv()));

    /// <summary>Adds an argument with the specified key and values to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="values">The values of the argument.</param>
    public void Add(string key, IEnumerable<int> values) =>
        arguments.Add(new Argument(key, ToString(values).ToCsv()));

    /// <summary>Builds the query string from the arguments in the <see cref="QueryBuilder" />.</summary>
    /// <returns>The built query string.</returns>
    public string Build()
    {
        if (arguments.Count == 0)
        {
            return "";
        }

        var queryLength = 0;
        foreach (var (key, value) in arguments)
        {
            // Length of '?key=value' (or '&key=value')
            queryLength += key.Length + value.Length + 2;
        }

#if NET
        return string.Create(
            queryLength,
            arguments,
            (buffer, state) =>
            {
                var position = 0;
                foreach (var (key, value) in state)
                {
                    if (position == 0)
                    {
                        buffer[position++] = '?';
                    }
                    else
                    {
                        buffer[position++] = '&';
                    }

                    foreach (var c in key)
                    {
                        buffer[position++] = c;
                    }

                    buffer[position++] = '=';

                    foreach (var c in value)
                    {
                        buffer[position++] = c;
                    }
                }
            }
        );
#else
        Span<char> buffer = stackalloc char[queryLength];
        var position = 0;
        foreach (var (key, value) in arguments)
        {
            if (position == 0)
            {
                buffer[position++] = '?';
            }
            else
            {
                buffer[position++] = '&';
            }

            foreach (var c in key)
            {
                buffer[position++] = c;
            }

            buffer[position++] = '=';

            foreach (var c in value)
            {
                buffer[position++] = c;
            }
        }

        return buffer.ToString();
#endif
    }

    private static string ToString(int value) => value.ToString(InvariantInfo);

    private static IEnumerable<string> ToString(IEnumerable<int> values) => values.Select(ToString);

    /// <inheritdoc />
    public override string ToString() => Build();
}

#if !NET
internal static class QueryBuilderHelper
{
    internal static void Deconstruct(this Argument instance, out string key, out string value)
    {
        key = instance.Key;
        value = instance.Value;
    }
}
#endif
