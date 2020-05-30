using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Continents.Fixtures
{
    public class InMemoryContinentDb
    {
        private readonly List<string> _db;

        public InMemoryContinentDb(IEnumerable<string> items)
        {
            _db = items.ToList();
        }

        public IReadOnlyCollection<string> Continents => _db.AsReadOnly();
    }
}
