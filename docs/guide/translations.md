# Translations

Some data contains localized text. Pass a `Language` object when fetching data to choose the language.

Currently the supported languages are:

- English (default)
- Spanish
- German
- French
- Chinese

English is used if you don't specify any language, or if the specified language is not supported. Let's have a [discussion] if you think `CurrentUICulture` should always be used by default.

@[code](../../samples/Translations/Program.cs)

Output

``` text
Preferred language
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

Korean (unsupported)
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

[discussion]:https://github.com/sliekens/gw2sdk/discussions/new/choose