# Translations

Many API methods return localized text. Pass a `Language` object to control the response language.

---

## 🌐 Supported Languages

| Language | Code |
|----------|------|
| English | `en` (default) |
| Spanish | `es` |
| German | `de` |
| French | `fr` |
| Chinese | `zh` |

> [!NOTE]
> GW2SDK does **not** use `CultureInfo.CurrentUICulture`. You must pass `Language` explicitly.

---

## 📝 Example

Use `Language.CurrentUICulture` to match the user's system language:

[!code-csharp[](~/samples/Translations/Program.cs)]

### Output

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
