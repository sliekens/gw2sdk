global using JetBrains.Annotations;
global using GuildWars2.Annotations;
#if NETFRAMEWORK
global using System.Net.Http;
#endif
global using static System.Net.Http.HttpMethod;
global using static System.Net.HttpStatusCode;
#if NETSTANDARD || NETFRAMEWORK
global using static GuildWars2.Http.HttpStatusCodeEx;
#endif
