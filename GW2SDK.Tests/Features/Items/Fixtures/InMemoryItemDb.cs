using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Items.Fixtures
{
    public class InMemoryItemDb
    {
        public InMemoryItemDb(IEnumerable<string> objects)
        {
            Items = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Items { get; }
    }
}
