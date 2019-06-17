using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.TestDataHelper
{
    public class Program
    {
        public static async Task Main(FeatureName feature, bool indented = false, string baseAddress = "https://api.guildwars2.com", string outFile = null)
        {
            if (outFile != null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outFile));
                Console.SetOut(File.CreateText(outFile));
            }

            using (Console.Out)
            using (var http = new HttpClient { BaseAddress = new Uri(baseAddress, UriKind.Absolute) })
            {
                if (feature == FeatureName.Build)
                {
                    var service = new JsonBuildService(http);
                    var json = await service.GetJsonBuild(indented);
                    Console.WriteLine(json);
                }

                if (feature == FeatureName.Achievements)
                {
                    var service = new JsonAchievementService(http);
                    var jsons = await service.GetAllJsonAchievements(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Colors)
                {
                    var service = new JsonColorService(http);
                    var jsons = await service.GetAllJsonColors(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Worlds)
                {
                    var service = new JsonWorldService(http);
                    var jsons = await service.GetAllJsonWorlds(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Items)
                {
                    var service = new JsonItemService(http);
                    var jsons = await service.GetAllJsonItems(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Recipes)
                {
                    var service = new JsonRecipeService(http);
                    var jsons = await service.GetAllJsonRecipes(indented);
                    foreach (var json in jsons)
                    {
                        Console.WriteLine(json);
                    }
                }

                if (feature == FeatureName.Skins)
                {
                    var service = new JsonSkinService(http);
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
