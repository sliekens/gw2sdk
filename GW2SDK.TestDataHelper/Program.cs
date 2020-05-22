using System.IO;
using System.Threading.Tasks;

namespace GW2SDK.TestDataHelper
{
    public class Program
    {
        public static async Task Main(string outDir, bool indented = false)
        {
            Directory.CreateDirectory(outDir);
            var services = new Container();
            await using (var file = File.CreateText(Path.Combine(outDir, "build.json")))
            {
                var service = services.Resolve<JsonBuildService>();
                var json = await service.GetJsonBuild(indented);
                await file.WriteLineAsync(json);
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "achievements.json")))
            {
                var service = services.Resolve<JsonAchievementService>();
                var jsons = await service.GetAllJsonAchievements(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "achievementCategories.json")))
            {
                var service = services.Resolve<JsonAchievementCategoriesService>();
                var jsons = await service.GetAllJsonAchievementCategories(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "achievementGroups.json")))
            {
                var service = services.Resolve<JsonAchievementGroupsService>();
                var jsons = await service.GetAllJsonAchievementGroups(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "colors.json")))
            {
                var service = services.Resolve<JsonColorService>();
                var jsons = await service.GetAllJsonColors(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "worlds.json")))
            {
                var service = services.Resolve<JsonWorldService>();
                var jsons = await service.GetAllJsonWorlds(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "items.json")))
            {
                var service = services.Resolve<JsonItemService>();
                var jsons = await service.GetAllJsonItems(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "recipes.json")))
            {
                var service = services.Resolve<JsonRecipeService>();
                var jsons = await service.GetAllJsonRecipes(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "skins.json")))
            {
                var service = services.Resolve<JsonSkinService>();
                var jsons = await service.GetAllJsonSkins(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "continents.json")))
            {
                var service = services.Resolve<JsonContinentService>();
                var jsons = await service.GetAllJsonContinents(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "continents_1_floors.json")))
            {
                var service = services.Resolve<JsonFloorService>();
                var jsons = await service.GetAllJsonFloors(1, indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "continents_2_floors.json")))
            {
                var service = services.Resolve<JsonFloorService>();
                var jsons = await service.GetAllJsonFloors(2, indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }

            await using (var file = File.CreateText(Path.Combine(outDir, "prices.json")))
            {
                var service = services.Resolve<JsonItemPriceService>();
                var jsons = await service.GetAllJsonItemPrices(indented);
                foreach (var json in jsons)
                {
                    await file.WriteLineAsync(json);
                }
            }
        }
    }
}
