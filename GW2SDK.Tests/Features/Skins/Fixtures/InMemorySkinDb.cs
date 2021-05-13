using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Skins.Fixtures
{
    public class InMemorySkinDb
    {
        public InMemorySkinDb(IEnumerable<string> objects)
        {
            Skins = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Skins { get; }
    }
}
