namespace GuildWars2.Http;

internal static class QueryBuilderExtensions
{
    public static void AddLanguage(this QueryBuilder query, Language? language)
    {
        if (language is not null)
        {
            query.Add("lang", language.Alpha2Code);
        }
    }

    public static void AddSchemaVersion(this QueryBuilder query, SchemaVersion version) =>
        query.Add("v", version);

    public static void AddId(this QueryBuilder query, string id) => query.Add("id", id);

    public static void AddId(this QueryBuilder query, int id) => query.Add("id", id);

    public static void AddIds(this QueryBuilder query, IEnumerable<int> ids) =>
        query.Add("ids", ids);

    public static void AddIds(this QueryBuilder query, IEnumerable<string> ids) =>
        query.Add("ids", ids);

    public static void AddAllIds(this QueryBuilder query) => query.Add("ids", "all");

    public static void AddPage(this QueryBuilder query, int pageIndex, int? pageSize)
    {
        query.Add("page", pageIndex);
        if (pageSize.HasValue)
        {
            query.Add("page_size", pageSize.Value);
        }
    }
}
