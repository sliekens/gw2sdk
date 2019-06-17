using System;
using System.IO;
using System.Threading.Tasks;

namespace GW2SDK.TestDataHelper
{
    public class Program
    {
        public static async Task Main(FeatureName feature, bool indented = false, string outFile = null)
        {
            if (outFile != null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outFile));
                Console.SetOut(File.CreateText(outFile));
            }

            var services = new Container();

            using (Console.Out)
            {
                if (feature == FeatureName.Build)
                {
                    var service = services.Resolve<JsonBuildService>();
                    var json = await service.GetJsonBuild(indented);
                    Console.WriteLine(json);
                }

                if (feature == FeatureName.Achievements)
                {
                    var service = services.Resolve<JsonAchievementService>();
                    var jsons = await service.GetAllJsonAchievements(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.AchievementCategories)
                {
                    var service = services.Resolve<JsonAchievementCategoriesService>();
                    var jsons = await service.GetAllJsonAchievementCategories(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Colors)
                {
                    var service = services.Resolve<JsonColorService>();
                    var jsons = await service.GetAllJsonColors(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Worlds)
                {
                    var service = services.Resolve<JsonWorldService>();
                    var jsons = await service.GetAllJsonWorlds(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Items)
                {
                    var service = services.Resolve<JsonItemService>();
                    var jsons = await service.GetAllJsonItems(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Recipes)
                {
                    var service = services.Resolve<JsonRecipeService>();
                    var jsons = await service.GetAllJsonRecipes(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Skins)
                {
                    var service = services.Resolve<JsonSkinService>();
                    var jsons = await service.GetAllJsonSkins(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }
            }
        }
    }
}
