using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static System.Globalization.NumberFormatInfo;
using Argument = System.Collections.Generic.KeyValuePair<string, string>;

namespace GuildWars2.Http;

/// <summary>Used to build a query string, e.g. <c>"id=123&amp;lang=fr"</c>. Not meant to be used directly.</summary>
[PublicAPI]
public sealed class QueryBuilder : IEnumerable
{
    public static readonly QueryBuilder Empty = new() { frozen = true };

    private readonly List<Argument> arguments;

    private bool frozen;

    public QueryBuilder()
    {
        arguments = new List<Argument>();
    }

    public QueryBuilder(IEnumerable<Argument> arguments)
    {
        this.arguments = arguments.ToList();
    }

    public int Count => arguments.Count;

    public IEnumerator GetEnumerator() => ((IEnumerable)arguments).GetEnumerator();

    public QueryBuilder Clone() => new(arguments);

    public void Add(string key, int value)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, ToString(value)));
    }

    public void Add(string key, string value)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, value.UrlEncoded()));
    }

    public void Add(string key, IEnumerable<string> values)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, values.Select(value => value.UrlEncoded()).ToCsv()));
    }

    public void Add(string key, IEnumerable<int> values)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, ToString(values).ToCsv()));
    }

    public void Freeze() => frozen = true;

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
