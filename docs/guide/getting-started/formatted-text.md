# Formatted text

The game client uses a custom markup language to format text. The language is similar
to HTML, but with a few differences. The game client uses this language to format
text in various tooltips, player titles, and other user interfaces.

For example:

```text
Double-click to apply to an unused infusion slot. Adds a festive glow.
<c=@Warning>Warning!</c>
<c=@Flavor>Captain's Council recommends avoiding direct contact with this substance.</c>"
```

Which is rendered as:

> Double-click to apply to an unused infusion slot. Adds a festive glow.  
> <span style="color: red">Warning!</span>  
> <span style="color: lightblue">Captain's Council recommends avoiding direct
contact with this substance.</span>

## Converting formatted text to other formats

GW2SDK provides a parser for the game's markup format, and formatters to convert
it to other formats:

- `MarkupTextConverter` converts formatted text to plain text, stripping all formatting.
- `MarkupHtmlConverter` converts formatted text to HTML, preserving the formatting.

### Converting formatted text to plain text

```csharp
var lexer = new MarkupLexer();
var parser = new MarkupParser();
var converter = new MarkupTextConverter();

var input = "Double-click to apply to an unused infusion slot. Adds a festive glow."
    + "\n<c=@Warning>Warning!</c>"
    + "\n<c=@Flavor>Captain's Council recommends avoiding direct contact with this"
    + " substance.</c>";
var tokens = lexer.Tokenize(input);
var syntax = parser.Parse(tokens);
var actual = converter.Convert(syntax);
Console.WriteLine(actual);
```

Output:

```text
Double-click to apply to an unused infusion slot. Adds a festive glow
Warning!
Captain's Council recommends avoiding direct contact with this substance.
```

### Converting formatted text to HTML

```csharp
var lexer = new MarkupLexer();
var parser = new MarkupParser();
var converter = new MarkupHtmlConverter();

var input = "Double-click to apply to an unused infusion slot. Adds a festive glow."
    + "\n<c=@Warning>Warning!</c>"
    + "\n<c=@Flavor>Captain's Council recommends avoiding direct contact with this"
    + " substance.</c>";
var tokens = lexer.Tokenize(input);
var syntax = parser.Parse(tokens);
var actual = converter.Convert(syntax);
Console.WriteLine(actual);
```

Output:

```html
Double-click to apply to an unused infusion slot. Adds a festive glow.<br>
<span style="color: #ED0002">Warning!</span><br><span style="color: #9BE8E4">Captain's
Council recommends avoiding direct contact with this substance.</span>
```

### Building a custom formatter

Depending on your UI framework, you may want to build a custom formatter to convert
the game's markup language to your UI framework's text formatting. You can use the
`MarkupLexer` and `MarkupParser` to tokenize and parse the markup language, and then
convert the syntax tree to your desired format.

For example, to convert the markup language to the markup language used by
[Spectre.Console](https://spectreconsole.net/):

```csharp
using System.Text;
using GuildWars2.Markup;
using Spectre.Console;

public class SpectreMarkupConverter
{
    // This is the entry point for the conversion.
    // Input: The ROOT node of the syntax tree.
    // Output: the formatted text that can be understood by your UI framework.
    //   In this Spectre.Console example, the return type is a string,
    //   but it could also be an object like System.Windows.Media.FormattedText.
    public string Convert(RootNode root)
    {
        var builder = new StringBuilder();
        foreach (var node in root.Children)
        {
            builder.Append(ConvertNode(node));
        }

        return builder.ToString();
    }

    // This method converts a single node in the syntax tree to the desired format.
    // Input: the CURRENT node encountered while traversing the syntax tree.
    // Output: the formatted text that can be understood by your UI framework.
    //   In this Spectre.Console example, the return type is a string,
    //   but it could also be an object like System.Windows.Media.FormattedText.
    private string ConvertNode(MarkupNode node)
    {
        // MarkupNode is the base type for all nodes in the syntax tree.
        // Use pattern matching to convert each type of node to the desired format.
        switch (node)
        {
            // TextNode is just a plain text node, no formatting.
            case TextNode text:
                return text.Text;

            // LineBreakNode represents a line break, covers both \n and <br>
            case LineBreakNode:
                return Environment.NewLine;
            
            // ColoredTextNode represents text with a color like <c=#FF000>text</c>
            // or <c=@Warning>text</c>
            case ColoredTextNode coloredText:
                var content = string.Concat(coloredText.Children.Select(ConvertNode)).EscapeMarkup();
                if (coloredText.Color.StartsWith("#"))
                {
                    return $"[${coloredText.Color}]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Flavor,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#9BE8E4]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Reminder,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#B0B0B0]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.AbilityType,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#FFEC8C]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Warning,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#ED0002]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Task,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#FFC957]{content}[/]";
                }
                else
                {
                    return content;
                }
            default:
                return "";
        }
    }
}

```

## Language reference

The markup language is quite simple, it only supports a few tags:

- `c`: Changes the color of the text. The color is specified by a color name or
  a hexadecimal RGB value.
  For example, `<c=@Warning>Warning!</c>` renders as:
  > <span style="color: red">Warning!</span>
  >
  A hexadecimal RGB value can be used instead of a color name. For example,
  `<c=#F8C56E>Attention</c>` renders as:
  > <span style="color: #F8C56E">Attention</span>
  >
- `br`: Inserts a line break. For example, `Line 1<br>Line 2` renders as:
  > Line 1  
  > Line 2
- `\n`: is sometimes used instead of `br` to insert a line break. For example,
  `Line 1\nLine 2` renders as:
  > Line 1  
  > Line 2

Differences compared to HTML:

Tags and attributes

- HTML supports tags with attributes like `<span style="color: red">text</span>`.
- GW2 uses a simplified tag system with a single attribute name and value.
  For example, `<c=@Warning>text</c>`.

Whitespace

- HTML collapses whitespace, including line breaks `\n`, into a single space, unless
  a `<pre>` tag is used.
- GW2 preserves whitespace, including line breaks `\n`. The provided `MarkupHtmlConverter`
  replace line breaks with `<br>` tags to replicate this behavior.
