using System;
using GW2SDK.Quaggans;
using Xunit;

namespace GW2SDK.Tests.Features.Quaggans;

internal static class Invariants
{
    internal static void Id_is_not_empty(this Quaggan actual) => Assert.NotEmpty(actual.Id);

    internal static void Quaggan_has_picture(this Quaggan actual) =>
        Assert.True(Uri.IsWellFormedUriString(actual.PictureHref, UriKind.Absolute));
}
