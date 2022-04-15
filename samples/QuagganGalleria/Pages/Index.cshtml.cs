using GW2SDK.Quaggans;
using GW2SDK.Quaggans.Models;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuagganGalleria.Pages;

public class QuagganViewModel
{
    public bool Active { get; set; }

    public string PictureHref { get; set; }

    public string Id { get; set; }
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    private readonly QuagganQuery quaggans;

    public IndexModel(ILogger<IndexModel> logger, QuagganQuery quaggans)
    {
        this.logger = logger;
        this.quaggans = quaggans;
    }

    public DateTimeOffset Refreshed { get; set; }

    public IEnumerable<QuagganViewModel> Quaggans { get; set; }

    public async Task OnGet()
    {
        logger.LogInformation("Retrieving the Quaggans.");
        var result = await quaggans.GetQuaggans();
        Refreshed = result.Date;
        Quaggans = result.Values.Select((quaggan, index) => new QuagganViewModel
        {
            Active = index == 0,
            Id = quaggan.Id,
            PictureHref = quaggan.PictureHref
        });
    }
}