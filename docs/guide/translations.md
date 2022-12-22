# Translations

Some data contains localized text. Pass a `Language` object when fetching data to choose the language.

Currently the supported languages are:

- English (default)
- Spanish
- German
- French
- Chinese

If you don't specify any language, or if the language is not supported, English is used.

**There is no automatic language selection.** If your application uses `CultureInfo.CurrentUICulture` for translations, that won't automatically work for `Gw2Client`. You must explicitly opt-in to translations by using `Language.CurrentUICulture`. Let's have a discussion if you think `CurrentUICulture` should be used by default.

<<< @/samples/Translations/Program.cs

Output

> English
>
> - Griffon
> - Jackal
> - Raptor
> - Roller Beetle
> - Skimmer
> - Skyscale
> - Springer
> - Turtle
> - Warclaw
>
> German
>
> - Greif
> - Schakal
> - Raptor
> - Rollkäfer
> - Schweberochen
> - Himmelsschuppe
> - Springer
> - Schildkröte
> - Kriegsklaue
>
> French
>
> - Griffon
> - Chacal
> - Raptor
> - Scaraboule
> - Voldécume
> - Dracaille
> - Frappesol
> - Tortue
> - Razziafelis
>
> Spanish
>
> - Grifo
> - Chacal
> - Raptor
> - Escarabajo
> - Mantarraya
> - Escamaceleste
> - Saltarín
> - Tortuga
> - Garrabélica
>
> Chinese
>
> - 狮鹫
> - 胡狼
> - 肉食鸟
> - 翻滚甲虫
> - 飞鱼
> - 飞天鳞龙
> - 弹跳兔
> - 乌龟
> - 战爪
>
> Korean
>
> - Griffon
> - Jackal
> - Raptor
> - Roller Beetle
> - Skimmer
> - Skyscale
> - Springer
> - Turtle
> - Warclaw
