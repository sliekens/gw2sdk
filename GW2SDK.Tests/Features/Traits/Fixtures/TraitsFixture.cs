using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Traits.Fixtures
{
    public class TraitsFixture : IDisposable
    {
        public TraitsFixture()
        {
            var reader = new STJsonFlatFileReader();
            Traits = reader.Read("Data/traits.json").ToList().AsReadOnly();
        }

        public IReadOnlyCollection<JsonDocument> Traits { get; }

        public void Dispose() => Traits.ForEach(json => json.Dispose());
    }
}
