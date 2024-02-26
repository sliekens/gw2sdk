using static System.String;

namespace GuildWars2;

/// <summary>Represents a schema version of the Guild Wars 2 API.</summary>
[PublicAPI]
public sealed class SchemaVersion
{
    /// <summary>Represents the schema version "2019-02-21T00:00:00.000Z".</summary>
    /// <remarks><list type="bullet"><item>Adds <c>last_modified</c> field to account and character records.</item></list>
    /// </remarks>
    public static readonly SchemaVersion V20190221 = new("2019-02-21T00:00:00.000Z");

    /// <summary>Represents the schema version "2019-03-22T00:00:00.000Z".</summary>
    /// <remarks><list type="bullet"><item>Changes <c>/v2/account/home/cats</c> to just list cat ids.</item></list></remarks>
    public static readonly SchemaVersion V20190322 = new("2019-03-22T00:00:00.000Z");

    /// <summary>Represents the schema version "2019-05-16T00:00:00.000Z".</summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>Changes the <c>access_required</c> field in <c>v2/achievements/daily</c> to show product conditions.</item>
    ///     </list>
    /// </remarks>
    public static readonly SchemaVersion V20190516 = new("2019-05-16T00:00:00.000Z");

    /// <summary>Represents the schema version "2019-05-21T23:00:00.000Z".</summary>
    /// <remarks><list type="bullet"><item>Adds <c>schema_versions</c> to <c>/v2.json</c>.</item></list></remarks>
    public static readonly SchemaVersion V20190521 = new("2019-05-21T23:00:00.000Z");

    /// <summary>Represents the schema version "2019-05-22T00:00:00.000Z".</summary>
    /// <remarks>
    ///     <list type="bullet"><item>Changes <c>/v2/tokeninfo</c> to show subtoken information when one is provided.</item>
    ///     </list>
    /// </remarks>
    public static readonly SchemaVersion V20190522 = new("2019-05-22T00:00:00.000Z");

    /// <summary>Represents the schema version "2019-12-19T00:00:00.000Z".</summary>
    /// <remarks>
    ///     <list type="bullet"><item>Adds <c>code</c> and <c>skills_by_palette</c> to <c>/v2/professions</c>.</item>
    ///         <item>Adds <c>code</c> to <c>/v2/legends</c>.</item>
    ///         <item>Adds <c>build_storage_slots</c> to <c>/v2/account</c>.</item>
    ///         <item>Adds <c>build_tabs_unlocked</c>, <c>active_build_tab</c>, <c>build_tabs</c>,
    /// <c>equipment_tabs_unlocked</c>, <c>active_equipment_tab</c>, and <c>equipment_tabs</c> to <c>/v2/characters/:id</c>.</item>
    ///         <item>Adds <c>equipment[i].location</c> and <c>equipment[i].tabs</c> to <c>/v2/characters/:id</c>.</item>
    ///         <item>Removes <c>skills</c> and <c>specializations</c> from <c>/v2/characters/:id</c>.</item></list>
    /// </remarks>
    public static readonly SchemaVersion V20191219 = new("2019-12-19T00:00:00.000Z");

    /// <summary>Represents the schema version "2020-11-17T00:30:00.000Z".</summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>Changes the type of <c>details.secondary_suffix_item_id</c> from <c>/v2/items</c> to be an optional int.</item>
    ///     </list>
    /// </remarks>
    public static readonly SchemaVersion V20201117 = new("2020-11-17T00:30:00.000Z");

    /// <summary>Represents the schema version "2021-04-06T21:00:00.000Z".</summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>Moves <c>equipment_pvp</c> under <c>equipment_tabs</c> in <c>/v2/characters/:id</c>.</item></list>
    /// </remarks>
    public static readonly SchemaVersion V20210406 = new("2021-04-06T21:00:00.000Z");

    /// <summary>Represents the schema version "2021-07-15T13:00:00.000Z".</summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>Adds <c>EquippedFromLegendaryArmory</c> and <c>LegendaryArmory</c> values to <c>equipment[i].location</c>
    /// field.</item>
    ///     </list>
    /// </remarks>
    public static readonly SchemaVersion V20210715 = new("2021-07-15T13:00:00.000Z");

    /// <summary>Represents the schema version "2022-03-09T02:00:00.000Z".</summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>Changes <c>ingredients</c> to <c>item_ingredients</c> and adds <c>currency_ingredients</c> to
    /// <c>/v2/recipes</c>.</item>
    ///     </list>
    /// </remarks>
    public static readonly SchemaVersion V20220309 = new("2022-03-09T02:00:00.000Z");

    /// <summary>Represents the schema version "2022-03-23T19:00:00.000Z".</summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>Changes <c>/v2/achievements/categories</c> to show tomorrow's dailies and show access requirements for
    /// relevant achievements.</item>
    ///     </list>
    /// </remarks>
    public static readonly SchemaVersion V20220323 = new("2022-03-23T19:00:00.000Z");

    /// <summary>The schema version that GW2SDK is optimized for.</summary>
    public static readonly SchemaVersion Recommended = V20220323;

    private SchemaVersion(string version)
    {
        if (IsNullOrWhiteSpace(version))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(version));
        }

        Version = version;
    }

    /// <summary>Gets the version string of the schema.</summary>
    public string Version { get; }

    /// <summary>Returns the string representation of the schema version.</summary>
    /// <returns>The schema version string.</returns>
    public override string ToString() => Version;

    /// <summary>Implicitly converts a SchemaVersion instance to a string.</summary>
    /// <param name="instance">The SchemaVersion instance.</param>
    /// <returns>The schema version string.</returns>
    public static implicit operator string(SchemaVersion instance) => instance.Version;
}
