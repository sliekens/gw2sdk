using System;
using Microsoft.Extensions.Configuration;

namespace GW2SDK.Tests.TestInfrastructure
{
    public class ConfigurationManager
    {
        public ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .AddUserSecrets<ConfigurationManager>()
                .AddEnvironmentVariables()
                .Build();
        }

        private IConfigurationRoot Configuration { get; }

        public string ApiKeyBasic =>
            Configuration["ApiKeyBasic"] ?? throw new InvalidOperationException("Missing ApiKeyBasic.");

        public string ApiKeyFull =>
            Configuration["ApiKeyFull"] ?? throw new InvalidOperationException("Missing ApiKeyFull.");

        public string CharacterName =>
            Configuration["CharacterName"] ?? throw new InvalidOperationException("Missing CharacterName.");

        public Uri BaseAddress => new(Configuration["Authority"], UriKind.Absolute);

        public static ConfigurationManager Instance { get; } = new();
    }

    public sealed class TestCharacterName
    {
        public TestCharacterName(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public sealed class ApiKeyBasic
    {
        public ApiKeyBasic(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }

    public sealed class ApiKeyFull
    {
        public ApiKeyFull(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }
}
