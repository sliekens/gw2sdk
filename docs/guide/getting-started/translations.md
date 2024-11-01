# Translations

Some API methods return localized data, and you can pass a `Language` object to
control the language of the text returned by the API. English is used if you don't
specify any language, or if the specified language is not supported.

Currently the supported languages are:

- English (default)
- Spanish
- German
- French
- Chinese

In .NET, you typically set `CultureInfo.CurrentUICulture` to the user's preferred
language. However, GW2SDK does not use this setting. Instead, you should pass a
`Language` object to the API methods that support it. You can use the
`Language.CurrentUICulture` static property to get the current UI culture as a
`Language` object.

[!code-csharp[](~/samples/Translations/Program.cs)]

Output

``` text
CurrentUICulture (German)
* Greif
* Schakal
* Raptor
* Rollkäfer
* Schweberochen
* Himmelsschuppe
* Springer
* Schildkröte
* Kriegsklaue

English
* Griffon
* Jackal
* Raptor
* Roller Beetle
* Skimmer
* Skyscale
* Springer
* Turtle
* Warclaw

German
* Greif
* Schakal
* Raptor
* Rollkäfer
* Schweberochen
* Himmelsschuppe
* Springer
* Schildkröte
* Kriegsklaue

French
* Griffon
* Chacal
* Raptor
* Scaraboule
* Voldécume
* Dracaille
* Frappesol
* Tortue
* Razziafelis

Spanish
* Grifo
* Chacal
* Raptor
* Escarabajo
* Mantarraya
* Escamaceleste
* Saltarín
* Tortuga
* Garrabélica

Chinese
* 狮鹫
* 胡狼
* 肉食鸟
* 翻滚甲虫
* 飞鱼
* 飞天鳞龙
* 弹跳兔
* 乌龟
* 战爪

Japanese
* Griffon
* Jackal
* Raptor
* Roller Beetle
* Skimmer
* Skyscale
* Springer
* Turtle
* Warclaw

```
