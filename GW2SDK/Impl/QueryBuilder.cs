using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Globalization.NumberFormatInfo;

namespace GW2SDK.Impl
{
    public sealed class QueryBuilder
    {
        private readonly List<KeyValuePair<string, string>> _arguments = new List<KeyValuePair<string, string>>();

        public void WithId(int id) => Add("id", id.ToString(InvariantInfo));

        public void WithIds(IReadOnlyCollection<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            if (ids.Count == 0)
            {
                throw new ArgumentException("IDs cannot be an empty collection.", nameof(ids));
            }

            Add("ids", ToCsv(ToString(ids)));
        }

        public void WithIdsAll() => Add("ids", "all");

        public void WithPageIndex(int pageIndex) => Add("page", ToString(pageIndex));

        public void WithPageSize(int pageSize) => Add("page_size", ToString(pageSize));

        private void Add(string key, string value)
        {
            _arguments.Add(new KeyValuePair<string, string>(key, value));
        }

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
    }
}
