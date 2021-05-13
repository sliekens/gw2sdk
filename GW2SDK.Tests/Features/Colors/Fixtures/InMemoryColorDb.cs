using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class InMemoryColorDb
    {
        public InMemoryColorDb(IEnumerable<string> objects)
        {
            Colors = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Colors { get; }
    }
}
