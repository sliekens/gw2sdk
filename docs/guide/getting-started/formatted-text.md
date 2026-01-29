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

> Double-click to apply to an unused infusion slot. Adds a festive glow.<br>
<span style="color: #ff0000">Warning!</span><br><span style="color: #99dddd">Captain's
Council recommends avoiding direct contact with this substance.</span>

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

Output:

``` html
Double-click to apply to an unused infusion slot. Adds a festive glow.<br>
<span style="color: #ff0000">Warning!</span><br><span style="color: hotpink">
Captain's Council recommends avoiding direct contact with this substance.</span>
```

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


The markup language supports a few simple tags, summarized below:

| Tag      | Usage Example                        | Rendered Output                                         | Notes                                                      |
|----------|--------------------------------------|---------------------------------------------------------|------------------------------------------------------------|
| `<c>`    | `<c=@Warning>Warning!</c>`           | <span style="color: #ff0000">Warning!</span>            | Changes text color by name or hex value. E.g. `<c=#f8c56e>Text</c>`. |
| `<br>`   | `Line 1<br>Line 2`                   | Line 1<br>Line 2                                        | Inserts a line break.                                      |
| `\n`     | `Line 1\nLine 2`                     | Line 1<br>Line 2                                        | Sometimes used for line breaks.                            |

---

### Tag examples

`<c=@Warning>Warning!</c>` renders as:
> <span style="color: #ff0000">Warning!</span>

A hexadecimal RGB value can be used instead of a color name:
`<c=#f8c56e>Attention</c>` renders as:
> <span style="color: #f8c56e">Attention</span>

`Line 1<br>Line 2` renders as:
> Line 1  
> Line 2

`Line 1\nLine 2` renders as:
> Line 1  
> Line 2

---

### Differences compared to HTML

| Aspect            | HTML Example / Behavior                                                                 | GW2 Markup Example / Behavior                                                      |
|-------------------|----------------------------------------------------------------------------------------|------------------------------------------------------------------------------------|
| Tags & Attributes | `<span style="color: red">text</span>` (tags with multiple attributes)                | `<c=@warning>text</c>` (single tag, single attribute name and value)               |
| Whitespace       | Collapses whitespace and line breaks (`\n`) into a single space, unless in `<pre>`      | Preserves whitespace and line breaks (`\n`). The provided `MarkupHtmlConverter` replaces line breaks with `<br>` tags to replicate this behavior. |
