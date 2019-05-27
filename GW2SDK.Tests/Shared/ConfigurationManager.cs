using System;
using Microsoft.Extensions.Configuration;

namespace GW2SDK.Tests.Shared
{
    public class ConfigurationManager
    {
        public ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddUserSecrets<ConfigurationManager>().Build();

            // 2-phase build, because the next part requires configuration of its own
            Configuration = new ConfigurationBuilder().AddConfiguration(Configuration)
                                                      .AddAzureKeyVault($"https://{Configuration["KeyVaultName"]}.vault.azure.net/")
                                                      .Build();
        }

        public IConfigurationRoot Configuration { get; }

        public string ApiKeyBasic => Configuration["ApiKeyBasic"];

        public string ApiKeyFull => Configuration["ApiKeyFull"];

        public Uri BaseAddress => new Uri(Configuration["Authority"], UriKind.Absolute);

        public static ConfigurationManager Instance { get; } = new ConfigurationManager();
    }
}
