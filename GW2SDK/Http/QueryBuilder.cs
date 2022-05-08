using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using static System.Globalization.NumberFormatInfo;
using Argument = System.Collections.Generic.KeyValuePair<string, string>;

namespace GW2SDK.Http;

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
        arguments.Add(new Argument(key, value));
    }

    public void Add(string key, IEnumerable<string> values)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, ToCsv(values)));
    }

    public void Add(string key, IEnumerable<int> values)
    {
        EnsureMutable();
        arguments.Add(new Argument(key, ToCsv(ToString(values))));
    }

    public void Freeze() => frozen = true;

    public string Build()
    {
        var query = new StringBuilder();
        foreach (var (key, value) in arguments)
        {
            query.Append(query.Length == 0 ? "?" : "&");
            query.AppendJoin("=", key, value);
        }

        return query.ToString();
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

    private static string ToCsv(IEnumerable<string> values) => string.Join(",", values);

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

    internal static void AppendJoin(
        this StringBuilder instance,
        string separator,
        string first,
        string second
    ) =>
        instance.Append(string.Join(separator, first, second));
}
#endif
