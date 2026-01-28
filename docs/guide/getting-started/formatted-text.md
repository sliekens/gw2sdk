# Formatted Text

Guild Wars 2 uses a custom markup language for tooltips, titles, and other UI text.

## 📝 Example

**Input:**
```text
Double-click to apply to an unused infusion slot. Adds a festive glow.
<c=@Warning>Warning!</c>
<c=@Flavor>Captain's Council recommends avoiding direct contact with this substance.</c>
```

**Rendered:**
> Double-click to apply to an unused infusion slot. Adds a festive glow.  
> <span style="color: #ff0000">Warning!</span>  
> <span style="color: #99dddd">Captain's Council recommends avoiding direct contact with this substance.</span>

---

## 🔄 Converting Markup

GW2SDK provides converters for the game's markup format:

| Converter | Output |
|-----------|--------|
| `MarkupTextConverter` | Plain text (strips formatting) |
| `MarkupHtmlConverter` | HTML with inline styles |
| `MarkupConverter` | Convenience wrapper for both |

> [!TIP]
> Build custom formatters with `MarkupLexer` and `MarkupParser` for your UI framework.

---

### To Plain Text

```csharp
using GuildWars2.Markup;

string input = """
    Double-click to apply to an unused infusion slot. Adds a festive glow.
    <c=@Warning>Warning!</c>
    <c=@Flavor>Captain's Council recommends avoiding direct contact with this substance.</c>
    """;

string plainText = MarkupConverter.ToPlainText(input);
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
string input = """
    Double-click to apply to an unused infusion slot. Adds a festive glow.
    <c=@Warning>Warning!</c>
    <c=@Flavor>Captain's Council recommends avoiding direct contact with this substance.</c>
    """;

string html = MarkupConverter.ToHtml(input);
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
string input = """
    Double-click to apply to an unused infusion slot. Adds a festive glow.
    <c=@Warning>Warning!</c>
    <c=@Flavor>Captain's Council recommends avoiding direct contact with this substance.</c>
    """;

Dictionary<string, string> colorMap = new(MarkupColorName.DefaultColorMap)
{
    [MarkupColorName.Flavor] = "hotpink"
};

string html = MarkupConverter.ToHtml(input, colorMap);
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

[!code-csharp[](~/samples/SpectreMarkup/SpectreMarkupConverter.cs)]

Usage:

[!code-csharp[](~/samples/SpectreMarkup/Program.cs)]

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
