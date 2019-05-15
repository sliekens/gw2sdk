using System;
using Microsoft.Extensions.Configuration;

namespace GW2SDK.Tests
{
    public class ConfigurationFixture
    {
        public ConfigurationFixture()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public IConfigurationRoot Configuration { get; }

        public Uri BaseAddress => new Uri(Configuration["Authority"], UriKind.Absolute);
    }
}