namespace GuildWars2.Authorization;

/// <summary>Builds options for creating a subtoken.</summary>
public sealed class SubtokenOptionsBuilder
{
    private static readonly Permission[] EmptyPermissions = [];
    private static readonly Uri[] EmptyAllowedUrls = [];

    private List<Permission>? permissions;
    private List<Uri>? allowedUrls;

    /// <summary>Gets the requested absolute expiration date.</summary>
    public DateTimeOffset? AbsoluteExpirationDate { get; private set; }

    /// <summary>Gets the requested permissions.</summary>
    public IReadOnlyList<Permission> Permissions
    {
        get
        {
            IReadOnlyList<Permission>? currentPermissions = permissions;
            return currentPermissions ?? EmptyPermissions;
        }
    }

    /// <summary>Gets the requested allowlisted URLs.</summary>
    public IReadOnlyList<Uri> AllowedUrls
    {
        get
        {
            IReadOnlyList<Uri>? currentAllowedUrls = allowedUrls;
            return currentAllowedUrls ?? EmptyAllowedUrls;
        }
    }

    /// <summary>Clears any requested permissions.</summary>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder ClearPermissions()
    {
        permissions?.Clear();
        return this;
    }

    /// <summary>Adds a permission to the request.</summary>
    /// <param name="permission">The permission to request.</param>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder AddPermission(Permission permission)
    {
        List<Permission> permissionList = EnsurePermissions();
        permissionList.Add(permission);
        return this;
    }

    /// <summary>Replaces the requested permissions.</summary>
    /// <param name="values">The permissions to request.</param>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder WithPermissions(IEnumerable<Permission> values)
    {
        ThrowHelper.ThrowIfNull(values);
        List<Permission> permissionList = EnsurePermissions();
        permissionList.Clear();
        permissionList.AddRange(values);
        return this;
    }

    /// <summary>Replaces the requested permissions.</summary>
    /// <param name="values">The permissions to request.</param>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder WithPermissions(IEnumerable<Extensible<Permission>> values)
    {
        ThrowHelper.ThrowIfNull(values);
        List<Permission> permissionList = EnsurePermissions();
        permissionList.Clear();
        permissionList.AddRange(values.Select(v => v.ToEnum()).Where(p => p.HasValue).Select(p => p!.Value));
        return this;
    }

    /// <summary>Clears any requested allowed URLs.</summary>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder ClearAllowedUrls()
    {
        allowedUrls?.Clear();
        return this;
    }

    /// <summary>Adds an allowed URL.</summary>
    /// <param name="url">The URL to allow.</param>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder AddAllowedUrl(Uri url)
    {
        ThrowHelper.ThrowIfNull(url);
        List<Uri> urlList = EnsureAllowedUrls();
        urlList.Add(url);
        return this;
    }

    /// <summary>Replaces the allowed URLs.</summary>
    /// <param name="values">The URLs to allow.</param>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder WithAllowedUrls(IEnumerable<Uri> values)
    {
        ThrowHelper.ThrowIfNull(values);
        List<Uri> urlList = EnsureAllowedUrls();
        urlList.Clear();
        foreach (Uri url in values)
        {
            ThrowHelper.ThrowIfNull(url);
            urlList.Add(url);
        }

        return this;
    }

    /// <summary>Adds an allowed URL.</summary>
    /// <param name="url">The URL to allow.</param>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder AddAllowedUrl(string url)
    {
        ThrowHelper.ThrowIfNull(url);
        return AddAllowedUrl(new Uri(url, UriKind.RelativeOrAbsolute));
    }

    /// <summary>Replaces the allowed URLs.</summary>
    /// <param name="values">The URLs to allow.</param>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder WithAllowedUrlStrings(IEnumerable<string> values)
    {
        ThrowHelper.ThrowIfNull(values);
        List<Uri> urlList = EnsureAllowedUrls();
        urlList.Clear();
        foreach (string value in values)
        {
            ThrowHelper.ThrowIfNull(value);
            urlList.Add(new Uri(value, UriKind.RelativeOrAbsolute));
        }

        return this;
    }

    /// <summary>Sets the absolute expiration date.</summary>
    /// <param name="absoluteExpirationDate">The expiration date to request.</param>
    /// <returns>The current builder.</returns>
    public SubtokenOptionsBuilder WithAbsoluteExpiration(in DateTimeOffset? absoluteExpirationDate)
    {
        AbsoluteExpirationDate = absoluteExpirationDate;
        return this;
    }

    private List<Permission> EnsurePermissions()
    {
        permissions ??= [];
        return permissions;
    }

    private List<Uri> EnsureAllowedUrls()
    {
        allowedUrls ??= [];
        return allowedUrls;
    }
}
