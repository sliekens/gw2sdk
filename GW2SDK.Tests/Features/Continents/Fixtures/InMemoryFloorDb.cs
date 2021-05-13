using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Continents.Fixtures
{
    public class InMemoryFloorDb
    {
        private readonly List<string> _db;

        public InMemoryFloorDb(IEnumerable<string> items)
        {
            _db = items.ToList();
        }

        public IReadOnlyCollection<string> Floors => _db.AsReadOnly();
    }
}
