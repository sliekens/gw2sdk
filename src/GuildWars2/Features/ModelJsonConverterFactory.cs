using System.Text.Json.Serialization;

namespace GuildWars2;

/// <summary>Creates the JSON converters for SDK models.</summary>
/// <remarks>The converter implementations stay internal. This factory is public so a consumer's source-generated
/// <see cref="JsonSerializerContext" /> can construct the converter named by <see cref="JsonConverterAttribute" />.
/// See https://github.com/sliekens/gw2sdk/issues/520.</remarks>
public sealed partial class ModelJsonConverterFactory : JsonConverterFactory;
