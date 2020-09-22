﻿using GW2SDK.Impl.JsonReaders.Linq;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitValue(IJsonValueMapping mapping)
        {
            Nodes.Push(new ValueDescriptor(mapping));
        }
    }
}
