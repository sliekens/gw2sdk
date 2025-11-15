using System.Text.Json.Serialization;

using GuildWars2;
using GuildWars2.Hero.Accounts;

namespace GW2SDK.Tests.Common;


[JsonSerializable(typeof(Extensible<ProductName>))]
[JsonSerializable(typeof(ProductName))]
public partial class TestJsonContext : JsonSerializerContext
{
}
