using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public class DiscriminatorDescriptor : JsonDescriptor
    {
        public DiscriminatorDescriptor(IJsonDiscriminatorMapping mapping)
        {
            Mapping = mapping;
        }

        public override JsonDescriptorType DescriptorType => JsonDescriptorType.Discriminator;

        public IJsonDiscriminatorMapping Mapping { get; }

        public override IEnumerable<ParameterExpression> GetVariables() => throw new System.NotImplementedException();

        public override Expression MapElement(Expression jsonElementExpr, Expression jsonPathExpr) => throw new System.NotImplementedException();

        public override Expression GetResult() => throw new System.NotImplementedException();
    }

    public class ObjectDescriptor : JsonDescriptor
    {
        public ObjectDescriptor(IJsonObjectMapping mapping)
        {
            Mapping = mapping;
            ObjectSeenExpr = Variable(typeof(bool), $"{mapping.Name}_object_seen");
        }

        public override JsonDescriptorType DescriptorType => JsonDescriptorType.Object;

        public IJsonObjectMapping Mapping { get; }

        public ParameterExpression ObjectSeenExpr { get; }

        public List<PropertyDescriptor> Properties { get; set; } = new List<PropertyDescriptor>();

        public DiscriminatorDescriptor? Discriminator { get; set; }

        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior =>
            Mapping.UnexpectedPropertyBehavior;

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ObjectSeenExpr;
                if (Discriminator is object)
                {
                    foreach (var variable in Discriminator.GetVariables())
                    {
                        yield return variable;
                    }
                }

                foreach (var variable in Properties.SelectMany(property => property.GetVariables()))
                {
                    yield return variable;
                }
            }
        }

        public override Expression MapElement(Expression jsonElementExpr, Expression jsonPathExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonElement), jsonElementExpr);
            ExpressionDebug.AssertType(typeof(JsonPath), jsonPathExpr);
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            var source = new List<Expression>();

            // Important: ensure all variables are initialized to their default before mapping anything
            foreach (var variable in GetVariables())
            {
                source.Add(Assign(variable, Default(variable.Type)));
            }

            source.Add
            (
                IfThen
                (
                    Equal
                    (
                        JsonElementExpr.GetValueKind(jsonElementExpr),
                        Constant(JsonValueKind.Object)
                    ),
                    Block
                    (
                        MapObjectExpr(jsonElementExpr, jsonPathExpr)
                    )
                )
            );

            var propertyValidations = Properties.SelectMany
            (
                property =>
                {
                    var propertyNameExpr = Constant(property.Mapping.Name, typeof(string));
                    var propertyPathExpr = JsonPathExpr.AccessProperty
                        (jsonPathExpr, propertyNameExpr);
                    return property.GetValidations(Mapping.ObjectType, propertyPathExpr);
                }
            );

            switch (Mapping.Significance)
            {
                case MappingSignificance.Required:
                {
                    source.Add
                    (
                        IfThen
                        (
                            IsFalse(ObjectSeenExpr),
                            Throw
                            (
                                JsonExceptionExpr.Create
                                (
                                    Constant
                                    (
                                        $"Missing required value for object of type '{Mapping.ObjectType.Name}'."
                                    ),
                                    JsonPathExpr.ToString(jsonPathExpr)
                                )
                            )
                        )
                    );
                    source.AddRange(propertyValidations);

                    break;
                }
                case MappingSignificance.Optional:
                    // Only validate properties when the object is found
                    // because when the object is missing, it doesn't make sense to validate its mapped properties.
                    // This means that an object can be optional and still have required properties, but they will only be validated if the object is found.
                    source.Add
                    (
                        IfThen
                        (
                            IsTrue(ObjectSeenExpr),
                            Block(propertyValidations.DefaultIfEmpty(Empty()))
                        )
                    );
                    break;
            }

            return Block(source);
        }

        private IEnumerable<Expression> MapObjectExpr(Expression jsonElementExpr, Expression jsonPathExpr)
        {
            yield return Assign(ObjectSeenExpr, Constant(true));

            yield return JsonElementExpr.ForEachProperty
            (
                jsonElementExpr,
                (
                    jsonPropertyExpr,
                    @continue
                ) => MapPropertyExpression
                    (jsonPropertyExpr, @continue, jsonPathExpr)
            );
        }

        private Expression MapPropertyExpression
        (
            Expression jsonPropertyExpr,
            LabelTarget @continue,
            Expression pathExpr,
            int index = 0
        )
        {
            var propertyNameExpr = JsonPropertyExpr.GetName(jsonPropertyExpr);
            var propertyPathExpr = JsonPathExpr.AccessProperty(pathExpr, propertyNameExpr);
            if (Properties.Count == 0)
            {
                return UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                    ? JsonExceptionExpr.ThrowJsonException
                    (
                        Constant
                        (
                            $"Unexpected property for object of type '{Mapping.ObjectType.Name}'.",
                            typeof(string)
                        ),
                        JsonPathExpr.ToString(propertyPathExpr)
                    )
                    : Continue(@continue);
            }

            var propertyNode = Properties[index];
            return IfThenElse
            (
                propertyNode.TestExpr(jsonPropertyExpr),
                propertyNode.MapElement(jsonPropertyExpr, propertyPathExpr),
                index + 1 < Properties.Count
                    ? MapPropertyExpression
                    (
                        jsonPropertyExpr,
                        @continue,
                        pathExpr,
                        index + 1
                    )
                    :
                    UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                        ?
                        JsonExceptionExpr.ThrowJsonException
                        (
                            Constant
                            (
                                $"Unexpected property for object of type '{Mapping.ObjectType.Name}'.",
                                typeof(string)
                            ),
                            JsonPathExpr.ToString(propertyPathExpr)
                        )
                        : Continue(@continue)
            );
        }

        public override Expression GetResult() =>
            MemberInit
            (
                New(Mapping.ObjectType),
                Properties.SelectMany(property => property.GetBindings())
            );
    }
}
