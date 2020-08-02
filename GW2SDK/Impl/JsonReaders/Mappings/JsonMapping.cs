﻿namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public abstract class JsonMapping
    {
        public string Name { get; set; } = "";

        public abstract JsonMappingKind Kind { get; }

        public MappingSignificance Significance { get; set; } = MappingSignificance.Required;

        public abstract void Accept(IJsonMappingVisitor visitor);
    }
}