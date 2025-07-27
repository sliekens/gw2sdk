using GuildWars2.Quaggans;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuagganGalleria.Pages;

public class QuagganViewModel
{
    public bool Active { get; set; }

    public Uri ImageUrl { get; set; } = new Uri("about:blank");

    public string Id { get; set; } = "";
}

public class IndexModel(ILogger<IndexModel> logger, QuaggansClient quaggans) : PageModel
{
    public DateTimeOffset Refreshed { get; set; }

    public IEnumerable<QuagganViewModel> Quaggans { get; set; } =
        Enumerable.Empty<QuagganViewModel>();

    public async Task OnGet()
    {
        logger.LogInformation("Retrieving the Quaggans.");
        var (found, context) = await quaggans.GetQuaggans();
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
