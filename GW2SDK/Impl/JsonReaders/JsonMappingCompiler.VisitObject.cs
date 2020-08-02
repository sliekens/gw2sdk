﻿using System.Collections.Generic;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TObject>
    {
        public void VisitObject<TValue>(JsonObjectMapping<TValue> mapping)
        {
            var nodes = new List<PropertyNode>();
            foreach (var child in mapping.Children)
            {
                child.Accept(this);
                var propertyNode = (PropertyNode) Nodes.Pop();
                nodes.Add(propertyNode);
            }

            Nodes.Push(
                new ObjectNode
                {
                    Mapping = mapping,
                    ObjectType = typeof(TValue),
                    Children = nodes,
                    UnexpectedPropertyBehavior = mapping.UnexpectedPropertyBehavior,
                    ObjectSeenExpr = Variable(typeof(bool), $"{mapping.Name}_object_seen")
                }
            );
        }
    }
}