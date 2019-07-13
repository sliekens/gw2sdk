using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class InMemoryWorldDb
    {
        public InMemoryWorldDb(IEnumerable<string> objects)
        {
            Worlds = objects.ToList().AsReadOnly();
        }

        public IReadOnlyList<string> Worlds { get; }
    }
}
