using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using static System.Globalization.NumberFormatInfo;
using Argument = System.Collections.Generic.KeyValuePair<string, string>;

namespace GW2SDK.Http
{
    /// <summary>Used to build a query string, e.g. <c>"id=123&amp;lang=fr"</c>. Not meant to be used directly.</summary>
    [PublicAPI]
    public sealed class QueryBuilder
    {
        private readonly List<Argument> _arguments = new();

        public int Count => _arguments.Count;

        public void Add(string key, int value) => _arguments.Add(new Argument(key, ToString(value)));

        public void Add(string key, string value) => _arguments.Add(new Argument(key, value));

        public void Add(string key, IEnumerable<string> values) => _arguments.Add(new Argument(key, ToCsv(values)));

        public void Add(string key, IEnumerable<int> values) => _arguments.Add(new Argument(key, ToCsv(ToString(values))));

        public string Build()
        {
            var query = new StringBuilder();
            foreach (var (key, value) in _arguments)
            {
                if (query.Length != 0)
                {
                    query.Append("&");
                }

                query.AppendJoin('=', key, value);
            }

            return query.ToString();
        }

        private static string ToString(int value) => value.ToString(InvariantInfo);

        private static IEnumerable<string> ToString(IEnumerable<int> values) => values.Select(ToString);

        private static string ToCsv(IEnumerable<string> values) => string.Join(',', values);

        public override string ToString() => Build();
    }
}
