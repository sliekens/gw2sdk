using static System.Globalization.NumberFormatInfo;

namespace GuildWars2.Http;

internal static class QueryBuilderExtensions
{
    internal static void AddLanguage(this QueryBuilder query, Language? language)
    {
        if (language is not null)
        {
            query.Add("lang", language.Alpha2Code);
        }
    }

    internal static void AddId(this QueryBuilder query, string id) => query.Add("id", id);

    internal static void AddId(this QueryBuilder query, int id) => query.Add("id", id);

    internal static void AddIds(this QueryBuilder query, IEnumerable<int> ids) =>
        query.Add("ids", string.Join(",", ids));

    internal static void AddIds(this QueryBuilder query, IEnumerable<string> ids) =>
        query.Add("ids", ids.ToCsv());

    internal static void AddAllIds(this QueryBuilder query) => query.Add("ids", "all");

    internal static void AddPage(this QueryBuilder query, int pageIndex, int? pageSize)
    {
        query.Add("page", pageIndex);
        if (pageSize.HasValue)
        {
            query.Add("page_size", pageSize.Value);
        }
    }

    internal static void Add(this QueryBuilder query, string key, int value) =>
        query.Add(key, value.ToString(InvariantInfo));
}
