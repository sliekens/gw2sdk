using GuildWars2;
using GuildWars2.Quaggans;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuagganGalleria.Pages;

internal sealed class QuagganViewModel
{
    public bool Active { get; set; }

    public Uri ImageUrl { get; set; } = new("about:blank");

    public string Id { get; set; } = "";
}

internal sealed class IndexModel(ILogger<IndexModel> logger, QuaggansClient quaggans) : PageModel
{
    public DateTimeOffset Refreshed { get; set; }

    public IEnumerable<QuagganViewModel> Quaggans { get; set; } = [];

    public async Task OnGet()
    {
        logger.LogInformation("Retrieving the Quaggans.");
        (HashSet<Quaggan> found, MessageContext context) = await quaggans.GetQuaggans().ConfigureAwait(false);
        Refreshed = context.Date;
        Quaggans = found.Select((quaggan, index) => new QuagganViewModel
        {
            Active = index == 0,
            Id = quaggan.Id,
            ImageUrl = quaggan.ImageUrl
        }
        );
    }
}
