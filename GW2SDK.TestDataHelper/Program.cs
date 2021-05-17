using System;
using System.IO;
using System.Threading.Tasks;

namespace GW2SDK.TestDataHelper
{
    public class Program
    {
        public static async Task Main(string outDir, bool indented = false)
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
                var json = await service.GetJsonBuild(indented);
                await file.WriteLineAsync(json);
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "achievements.json")))
            {
                Console.WriteLine("Getting achievements.");
                var service = services.Resolve<JsonAchievementService>();
                var jsons = await service.GetAllJsonAchievements(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "achievementCategories.json")))
            {
                Console.WriteLine("Getting achievement categories.");
                var service = services.Resolve<JsonAchievementCategoriesService>();
                var jsons = await service.GetAllJsonAchievementCategories(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "achievementGroups.json")))
            {
                Console.WriteLine("Getting achievement groups.");
                var service = services.Resolve<JsonAchievementGroupsService>();
                var jsons = await service.GetAllJsonAchievementGroups(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "colors.json")))
            {
                Console.WriteLine("Getting dyes.");
                var service = services.Resolve<JsonColorService>();
                var jsons = await service.GetAllJsonColors(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "currencies.json")))
            {
                Console.WriteLine("Getting currencies.");
                var service = services.Resolve<JsonCurrencyService>();
                var jsons = await service.GetAllJsonCurrencies(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "worlds.json")))
            {
                Console.WriteLine("Getting worlds.");
                var service = services.Resolve<JsonWorldService>();
                var jsons = await service.GetAllJsonWorlds(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "items.json")))
            {
                Console.WriteLine("Getting items.");
                var service = services.Resolve<JsonItemService>();
                var jsons = await service.GetAllJsonItems(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "mailCarriers.json")))
            {
                Console.WriteLine("Getting mail carriers.");
                var service = services.Resolve<JsonMailCarriersService>();
                var jsons = await service.GetAllJsonMailCarriers(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "recipes.json")))
            {
                Console.WriteLine("Getting recipes.");
                var service = services.Resolve<JsonRecipeService>();
                var jsons = await service.GetAllJsonRecipes(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "skins.json")))
            {
                Console.WriteLine("Getting skins.");
                var service = services.Resolve<JsonSkinService>();
                var jsons = await service.GetAllJsonSkins(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "titles.json")))
            {
                Console.WriteLine("Getting titles.");
                var service = services.Resolve<JsonTitlesService>();
                var jsons = await service.GetAllJsonTitles(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "traits.json")))
            {
                Console.WriteLine("Getting traits.");
                var service = services.Resolve<JsonTraitsService>();
                var jsons = await service.GetAllJsonTraits(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "continents.json")))
            {
                Console.WriteLine("Getting continents.");
                var service = services.Resolve<JsonContinentService>();
                var jsons = await service.GetAllJsonContinents(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "continents_1_floors.json")))
            {
                Console.WriteLine("Getting floors 1/2.");
                var service = services.Resolve<JsonFloorService>();
                var jsons = await service.GetAllJsonFloors(1, indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "continents_2_floors.json")))
            {
                Console.WriteLine("Getting floors 2/2.");
                var service = services.Resolve<JsonFloorService>();
                var jsons = await service.GetAllJsonFloors(2, indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "prices.json")))
            {
                Console.WriteLine("Getting item prices.");
                var service = services.Resolve<JsonItemPriceService>();
                var jsons = await service.GetAllJsonItemPrices(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "listings.json")))
            {
                Console.WriteLine("Getting item listings.");
                var service = services.Resolve<JsonItemListingService>();
                var jsons = await service.GetAllJsonItemListing(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }
        }
    }
}
