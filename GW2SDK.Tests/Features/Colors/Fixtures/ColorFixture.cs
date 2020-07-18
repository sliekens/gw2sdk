using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorFixture : IDisposable
    {
        public ColorFixture()
        {
            var reader = new STJsonFlatFileReader();
            Colors = reader.Read("Data/colors.json").ToList().AsReadOnly();
        }

        public IReadOnlyCollection<JsonDocument> Colors { get; }
        public void Dispose()
        {
            foreach (var jsonDocument in Colors)
            {
                jsonDocument.Dispose();
            }
        }
    }
}
