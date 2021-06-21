using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace GW2SDK.TestDataHelper
{
    public class Program
    {
        public static async Task Main(string outDir)
        {
            Directory.CreateDirectory(outDir);
            await using var services = new Container();
            await using (var file = File.CreateText(Path.Combine(outDir, "v2.json")))
            {
                Console.WriteLine("Getting API info.");
                var service = services.Resolve<JsonApiInfoService>();
                var json = await service.GetJsonApiInfo();
                await file.WriteLineAsync(json);
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "build.json")))
            {
                Console.WriteLine("Getting build number.");
                var service = services.Resolve<JsonBuildService>();
                var json = await service.GetJsonBuild();
                await file.WriteLineAsync(json);
            }

            // Compress everything else to save disk space

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "achievements.json.gz")))
            {
                Console.WriteLine("Getting achievements.");
                var service = services.Resolve<JsonAchievementService>();
                var jsons = await service.GetAllJsonAchievements();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "worlds.json.gz")))
            {
                Console.WriteLine("Getting worlds.");
                var service = services.Resolve<JsonWorldService>();
                var jsons = await service.GetAllJsonWorlds();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "items.json.gz")))
            {
                Console.WriteLine("Getting items.");
                var service = services.Resolve<JsonItemService>();
                var jsons = await service.GetAllJsonItems();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "mailCarriers.json.gz")))
            {
                Console.WriteLine("Getting mail carriers.");
                var service = services.Resolve<JsonMailCarriersService>();
                var jsons = await service.GetAllJsonMailCarriers();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "recipes.json.gz")))
            {
                Console.WriteLine("Getting recipes.");
                var service = services.Resolve<JsonRecipeService>();
                var jsons = await service.GetAllJsonRecipes();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "skins.json.gz")))
            {
                Console.WriteLine("Getting skins.");
                var service = services.Resolve<JsonSkinService>();
                var jsons = await service.GetAllJsonSkins();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "titles.json.gz")))
            {
                Console.WriteLine("Getting titles.");
                var service = services.Resolve<JsonTitlesService>();
                var jsons = await service.GetAllJsonTitles();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "traits.json.gz")))
            {
                Console.WriteLine("Getting traits.");
                var service = services.Resolve<JsonTraitsService>();
                var jsons = await service.GetAllJsonTraits();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "continents.json.gz")))
            {
                Console.WriteLine("Getting continents.");
                var service = services.Resolve<JsonContinentService>();
                var jsons = await service.GetAllJsonContinents();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "continents_1_floors.json.gz")))
            {
                Console.WriteLine("Getting floors 1/2.");
                var service = services.Resolve<JsonFloorService>();
                var jsons = await service.GetAllJsonFloors(1);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "continents_2_floors.json.gz")))
            {
                Console.WriteLine("Getting floors 2/2.");
                var service = services.Resolve<JsonFloorService>();
                var jsons = await service.GetAllJsonFloors(2);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "prices.json.gz")))
            {
                Console.WriteLine("Getting item prices.");
                var service = services.Resolve<JsonItemPriceService>();
                var jsons = await service.GetJsonItemPrices();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = CreateTextCompressed(Path.Combine(outDir, "listings.json.gz")))
            {
                Console.WriteLine("Getting item listings.");
                var service = services.Resolve<JsonItemListingService>();
                var jsons = await service.GetJsonItemListing();
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }
        }

        private static StreamWriter CreateTextCompressed(string path) => new StreamWriter(new GZipStream(File.OpenWrite(path), CompressionMode.Compress), Encoding.UTF8);
    }
}
