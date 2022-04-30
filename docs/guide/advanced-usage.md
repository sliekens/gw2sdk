# Advanced usage

The `Gw2Client` class is only provided as a convenience to make features easier to discover. When you use `Gw2Client.Meta`, you are actually using a class named `MetaQuery`.

``` cs
public sealed class Gw2Client
{
    private readonly HttpClient httpClient;

    public Gw2Client(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public MetaQuery Meta => new(httpClient);
}
```

You can use the Query classes in a more direct manner. I recommend that you begin with the `Gw2Client` class to discover the features you need. Then switch to using the Query classes directly (Example 1). If you want to go even lower, use the Request classes directly, but most people shouldn't have to do that (Example 2).

<<< @/samples/AdvancedUsage/Program.cs