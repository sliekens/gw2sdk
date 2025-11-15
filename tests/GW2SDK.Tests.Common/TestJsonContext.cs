using System.Text.Json.Serialization;

using GuildWars2.Hero.Accounts;

namespace GuildWars2.Tests.Common;

[JsonSerializable(typeof(Extensible<ProductName>))]
[JsonSerializable(typeof(ProductName))]
public partial class TestJsonContext : JsonSerializerContext
{
}
