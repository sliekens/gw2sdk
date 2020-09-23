using System;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public partial class JsonExpressionCompiler
    {
        public void VisitObject(IJsonObjectMapping mapping)
        {
            if (mapping.Significance == MappingSignificance.Ignored)
            {
                return;
            }

            var ctx = Context.Peek();
            throw new NotImplementedException();
        }
    }
}
