using System;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitDiscriminator<TObject>(IJsonDiscriminatorMapping mapping)
        {
            throw new NotImplementedException();
        }
    }
}
