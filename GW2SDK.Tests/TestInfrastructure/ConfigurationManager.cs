using System;
using Microsoft.Extensions.Configuration;

namespace GW2SDK.Tests.TestInfrastructure
{
    public class ConfigurationManager
    {
        public ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<ConfigurationManager>()
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfigurationRoot Configuration { get; }

        public string ApiKeyBasic => Configuration["ApiKeyBasic"] ?? throw new InvalidOperationException("Missing ApiKeyBasic.");

        public string ApiKeyFull => Configuration["ApiKeyFull"] ?? throw new InvalidOperationException("Missing ApiKeyFull.");

        public Uri BaseAddress => new Uri(Configuration["Authority"], UriKind.Absolute);

        public static ConfigurationManager Instance { get; } = new ConfigurationManager();
    }
}
