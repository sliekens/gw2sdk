using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed record MaterialCategory
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public IEnumerable<int> Items { get; init; } = Enumerable.Empty<int>();

        public int Order { get; init; }
    }
}
