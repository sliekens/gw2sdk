# Formatted text

The game client uses a custom markup language to format text. The language is similar
to HTML, but with a few differences. The game client uses this language to format
text in various tooltips, player titles, and other user interfaces.

For example:

```text
Double-click to apply to an unused infusion slot. Adds a festive glow.
<c=@Warning>Warning!</c>
<c=@Flavor>Captain's Council recommends avoiding direct contact with this substance.</c>
```

Which is rendered as:

> Double-click to apply to an unused infusion slot. Adds a festive glow.  
> <span style="color: #ff0000">Warning!</span>  
> <span style="color: #99dddd">Captain's Council recommends avoiding direct
contact with this substance.</span>

## Converting formatted text to other formats

GW2SDK provides a parser for the game's markup format, and formatters to convert
it to other formats:

- `MarkupTextConverter` converts formatted text to plain text, stripping all formatting.
- `MarkupHtmlConverter` converts formatted text to HTML, preserving the formatting.

A convenient `MarkupConverter` class is provided to work with both formatters.

Alternatively, you can build a custom formatter for your UI framework using the
`MarkupLexer` and `MarkupParser` classes.

### Converting formatted text to plain text

```csharp
using GuildWars2.Markup;

var input = "Double-click to apply to an unused infusion slot. Adds a festive glow."
    + "\n<c=@Warning>Warning!</c>"
    + "\n<c=@Flavor>Captain's Council recommends avoiding direct contact with this"
    + " substance.</c>";

var plainText = MarkupConverter.ToPlainText(input);
Console.WriteLine(plainText);
```

Output:

```text
Double-click to apply to an unused infusion slot. Adds a festive glow.
Warning!
Captain's Council recommends avoiding direct contact with this substance
```

### Converting formatted text to HTML

```csharp
var input = "Double-click to apply to an unused infusion slot. Adds a festive glow."
    + "\n<c=@Warning>Warning!</c>"
    + "\n<c=@Flavor>Captain's Council recommends avoiding direct contact with this"
    + " substance.</c>";

var html = MarkupConverter.ToHtml(input);
Console.WriteLine(html);
```

Output:

```html
Double-click to apply to an unused infusion slot. Adds a festive glow.<br>
<span style="color: #ff0000">Warning!</span><br><span style="color: #99dddd">Captain's
Council recommends avoiding direct contact with this substance.</span>
```

#### Overriding default colors

Optionally, you can override the default colors. Start by cloning the default
color map, and then modify the values:

```csharp
var input = "Double-click to apply to an unused infusion slot. Adds a festive glow."
    + "\n<c=@Warning>Warning!</c>"
    + "\n<c=@Flavor>Captain's Council recommends avoiding direct contact with this"
    + " substance.</c>";

var colorMap = new Dictionary<string, string>(MarkupColorName.DefaultColorMap)
{
    [MarkupColorName.Flavor] = "hotpink"
};

var html = MarkupConverter.ToHtml(input, colorMap);
Console.WriteLine(html);
```

Output (rendered in HTML):

> Double-click to apply to an unused infusion slot. Adds a festive glow.<br>
<span style="color: #ff0000">Warning!</span><br><span style="color: hotpink">
Captain's Council recommends avoiding direct contact with this substance.</span>

### Building a custom formatter

You may want to build a custom formatter for your UI framework if it can't render
HTML. You can use the `MarkupLexer` and `MarkupParser` to tokenize and parse the
markup language, and then convert the syntax tree to the desired format.

For example, to convert the markup language to the markup language used by
[Spectre.Console](https://spectreconsole.net/markup):

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
        // Use a switch statement to convert each type of node to the desired format.
        // You could also use pattern matching with C# 9.
        switch (node.Type)
        {
            // TextNode is just a plain text node, no formatting.
            case MarkupNodeType.Text:
                var text = (TextNode)node;
                return Markup.Escape(text.Text);

            // LineBreakNode represents a line break, covers both \n and <br>
            case MarkupNodeType.LineBreak:
                return Environment.NewLine;
            
            // ColoredTextNode represents text with a color like <c=#ff000>text</c>
            // or <c=@warning>text</c>
            case MarkupNodeType.ColoredText:
                var coloredText = (ColoredTextNode)node;
                var builder = new StringBuilder();
                foreach (var child in coloredText.Children)
                {
                    builder.Append(ConvertNode(child));
                }

                var content = builder.ToString();
                if (coloredText.Color.StartsWith("#", StringComparison.Ordinal))
                {
                    var colorCode = coloredText.Color;
                    return $"[{colorCode}]{content}[/]";
                }
                else if (ColorMap.TryGetValue(coloredText.Color, out var colorCode))
                {
                    return $"[{colorCode}]{content}[/]";
                }
                else
                {
                    return content;
                }

            default:
                return "";
        }
    }

    // A map of color names to hexadecimal RGB values.
    // Note that the color names are case-insensitive.
    private static readonly IReadOnlyDictionary<string, string> ColorMap
        = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [MarkupColorName.Flavor] = "#99dddd",
            [MarkupColorName.Reminder] = "#aaaaaa",
            [MarkupColorName.AbilityType] = "#ffee88",
            [MarkupColorName.Warning] = "#ff0000",
            [MarkupColorName.Task] = "#ffcc55",
        };
}

```

Usage:

```csharp
// Set up the lexer, parser, and converter.
var lexer = new MarkupLexer();
var parser = new MarkupParser();
var converter = new SpectreMarkupConverter();

// Tokenize the input
var input = "... (markup text)";
var tokens = lexer.Tokenize(input);

// Convert the tokens to a syntax tree
var syntax = parser.Parse(tokens);

// Convert the syntax tree to the desired format
var output = converter.Convert(syntax);
```

## Language reference

The markup language is quite simple, it only supports a few tags:

- `c`: Changes the color of the text. The color is specified by a color name or
  a hexadecimal RGB value.
  For example, `<c=@Warning>Warning!</c>` renders as:
  > <span style="color: #ff0000">Warning!</span>
  >
  A hexadecimal RGB value can be used instead of a color name. For example,
  `<c=#f8c56e>Attention</c>` renders as:
  > <span style="color: #f8c56e">Attention</span>
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
  For example, `<c=@warning>text</c>`.

Whitespace

- HTML collapses whitespace, including line breaks `\n`, into a single space, unless
  a `<pre>` tag is used.
- GW2 preserves whitespace, including line breaks `\n`. The provided `MarkupHtmlConverter`
  replace line breaks with `<br>` tags to replicate this behavior.
