using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace GuildWars2.TestDataHelper;

public class Program
{
    public static async Task Main(string outDir)
    {
        Directory.CreateDirectory(outDir);
        await using var services = new Container();

        try
        {
            await AnsiConsole.Progress()
                .StartAsync(
                    async ctx =>
                    {
                        var achievements = ctx.AddTask("Downloading achievements.");
                        await using (var file =
                            CreateTextCompressed(Path.Combine(outDir, "achievements.json.gz")))
                        {
                            var service = services.Resolve<JsonAchievementService>();
                            var documents =
                                await service.GetAllJsonAchievements(Update(achievements));
                            foreach (var document in documents)
                            {
                                await file.WriteLineAsync(document);
                            }
                        }

                        var items = ctx.AddTask("Downloading items.");
                        await using (var file =
                            CreateTextCompressed(Path.Combine(outDir, "items.json.gz")))
                        {
                            var service = services.Resolve<JsonItemService>();
                            var documents = await service.GetAllJsonItems(Update(items));
                            foreach (var document in documents)
                            {
                                await file.WriteLineAsync(document);
                            }
                        }

                        var recipes = ctx.AddTask("Downloading recipes.");
                        await using (var file =
                            CreateTextCompressed(Path.Combine(outDir, "recipes.json.gz")))
                        {
                            var service = services.Resolve<JsonRecipeService>();
                            var documents = await service.GetAllJsonRecipes(Update(recipes));
                            foreach (var document in documents)
                            {
                                await file.WriteLineAsync(document);
                            }
                        }

                        var skins = ctx.AddTask("Downloading skins.");
                        await using (var file =
                            CreateTextCompressed(Path.Combine(outDir, "skins.json.gz")))
                        {
                            var service = services.Resolve<JsonSkinService>();
                            var documents = await service.GetAllJsonSkins(Update(skins));
                            foreach (var document in documents)
                            {
                                await file.WriteLineAsync(document);
                            }
                        }

                        var prices = ctx.AddTask("Downloading prices.");
                        await using (var file =
                            CreateTextCompressed(Path.Combine(outDir, "prices.json.gz")))
                        {
                            var service = services.Resolve<JsonItemPriceService>();
                            var documents = await service.GetJsonItemPrices(Update(prices));
                            foreach (var document in documents)
                            {
                                await file.WriteLineAsync(document);
                            }
                        }

                        var orders = ctx.AddTask("Downloading orders.");
                        await using (var file =
                            CreateTextCompressed(Path.Combine(outDir, "listings.json.gz")))
                        {
                            var service = services.Resolve<JsonOrderBookService>();
                            var documents = await service.GetJsonOrderBooks(Update(orders));
                            foreach (var document in documents)
                            {
                                await file.WriteLineAsync(document);
                            }
                        }
                    }
                );
        }
        catch (Exception crash)
        {
            AnsiConsole.WriteException(crash);
        }
    }

    private static Progress<ICollectionContext> Update(ProgressTask progressTask) =>
        new(c => progressTask.MaxValue(c.ResultTotal).Value(c.ResultCount));

    private static StreamWriter CreateTextCompressed(string path) =>
        new(new GZipStream(File.OpenWrite(path), CompressionMode.Compress), Encoding.UTF8);
}
