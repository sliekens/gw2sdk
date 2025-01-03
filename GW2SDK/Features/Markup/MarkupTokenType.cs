﻿using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Markup;

/// <summary>Represents the different types of tokens that can be encountered in the markup processing.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(MarkupTokenTypeJsonConverter))]
public enum MarkupTokenType
{
    /// <summary>No token type.</summary>
    None,

    /// <summary>Represents a text token.</summary>
    Text,

    /// <summary>Represents a line break token.</summary>
    LineBreak,

    /// <summary>Represents the start of a tag.</summary>
    TagStart,

    /// <summary>Represents the value within a tag.</summary>
    TagValue,

    /// <summary>Represents a closing tag.</summary>
    TagClose,

    /// <summary>Represents a void tag, which is a self-closing tag.</summary>
    TagVoid,

    /// <summary>Represents the end of the file/input.</summary>
    End
}
