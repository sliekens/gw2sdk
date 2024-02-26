using System.Collections;
using static System.Globalization.NumberFormatInfo;
using Argument = System.Collections.Generic.KeyValuePair<string, string>;

namespace GuildWars2.Http;

/// <summary>Used to build a query string, e.g. <c><![CDATA[ids=1,2,3&lang=fr&v=latest]]></c>. Not meant to be used directly.</summary>
[PublicAPI]
public sealed class QueryBuilder : IEnumerable
{
    /// <summary>Represents an empty <see cref="QueryBuilder" /> instance.</summary>
    public static readonly QueryBuilder Empty = new() { frozen = true };

    private readonly List<Argument> arguments;

    private bool frozen;

    /// <summary>Initializes a new instance of the <see cref="QueryBuilder" /> class.</summary>
    public QueryBuilder()
    {
        arguments = [];
    }

    /// <summary>Initializes a new instance of the <see cref="QueryBuilder" /> class with the specified arguments.</summary>
    /// <param name="arguments">The arguments to initialize the <see cref="QueryBuilder" /> with.</param>
    public QueryBuilder(IEnumerable<Argument> arguments)
    {
        this.arguments = arguments.ToList();
    }

    /// <summary>Gets the number of arguments in the <see cref="QueryBuilder" />.</summary>
    public int Count => arguments.Count;

    /// <summary>Returns an enumerator that iterates through the <see cref="QueryBuilder" /> arguments.</summary>
    /// <returns>An enumerator that can be used to iterate through the <see cref="QueryBuilder" /> arguments.</returns>
    public IEnumerator GetEnumerator() => ((IEnumerable)arguments).GetEnumerator();

    /// <summary>Creates a new instance of the <see cref="QueryBuilder" /> with the same arguments as the current instance.</summary>
    /// <returns>A new instance of the <see cref="QueryBuilder" /> with the same arguments as the current instance.</returns>
    public QueryBuilder Clone() => new(arguments);

    /// <summary>Adds an argument with the specified key and value to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="value">The value of the argument.</param>
    public void Add(string key, int value)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, ToString(value)));
    }

    /// <summary>Adds an argument with the specified key and value to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="value">The value of the argument.</param>
    public void Add(string key, string value)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, value));
    }

    /// <summary>Adds an argument with the specified key and values to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="values">The values of the argument.</param>
    public void Add(string key, IEnumerable<string> values)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, values.Select(value => value).ToCsv()));
    }

    /// <summary>Adds an argument with the specified key and values to the <see cref="QueryBuilder" />.</summary>
    /// <param name="key">The key of the argument.</param>
    /// <param name="values">The values of the argument.</param>
    public void Add(string key, IEnumerable<int> values)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, ToString(values).ToCsv()));
    }

    /// <summary>Freezes the <see cref="QueryBuilder" />, preventing further modifications.</summary>
    public void Freeze() => frozen = true;

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

    private void EnsureMutable()
    {
        if (frozen)
        {
            throw new InvalidOperationException(
                "Invalid attempt to mutate a frozen query builder."
            );
        }
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
