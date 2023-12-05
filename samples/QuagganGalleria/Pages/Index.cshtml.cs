using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Quaggans;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace QuagganGalleria.Pages;

public class QuagganViewModel
{
    public bool Active { get; set; }

    public string PictureHref { get; set; } = "";

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
        Quaggans = found.Select(
            (quaggan, index) => new QuagganViewModel
            {
                Active = index == 0,
                Id = quaggan.Id,
                PictureHref = quaggan.PictureHref
            }
        );
    }
}
