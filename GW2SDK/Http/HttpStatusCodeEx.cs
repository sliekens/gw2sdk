﻿using System.Net;
using JetBrains.Annotations;

namespace GuildWars2.Http;

[PublicAPI]
public static class HttpStatusCodeEx
{
    public const HttpStatusCode TooManyRequests = (HttpStatusCode)429;
}
