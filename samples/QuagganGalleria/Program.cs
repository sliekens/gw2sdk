using GW2SDK;
using GW2SDK.Http;
using GW2SDK.Quaggans;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient("api.guildwars2.com",
    http =>
    {
        http.UseGuildWars2();
        http.UseLanguage(Language.English);
        http.UseSchemaVersion(SchemaVersion.Recommended);
    }).AddTypedClient<QuagganQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
