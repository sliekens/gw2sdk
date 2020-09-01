using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        ///// <summary>Map a property of any type, using the specified <see cref="IJsonReader{T}"/>.</summary>
        ///// <typeparam name="TProperty">The type of the destination property or field.</typeparam>
        ///// <param name="propertyName">The name of the JSON property.</param>
        ///// <param name="anyType">An expression that returns the destination property, eg. order => order.Id.</param>
        ///// <param name="converter">The function that performs the actual conversion from JSON to the destination type.</param>
        ///// <param name="significance">Determines if the JSON property is required, optional or ignored.</param>
        //public void Map<TProperty>(
        //    string propertyName,
        //    Expression<SelectProperty<TObject, TProperty>> anyType,
        //    ConvertJsonElement<TProperty> converter,
        //    MappingSignificance significance = MappingSignificance.Required)
        //{
        //    Children.Add(
        //        new JsonPropertyMapping
        //        {
        //            Name = propertyName,
        //            Destination = ((MemberExpression) anyType.Body).Member,
        //            Significance = significance,
        //            ValueNode = new JsonValueMapping<TProperty>
        //            {
        //                Name = propertyName,
        //                ValueKind = JsonValueMappingKind.Any,
        //                JsonReader = new JsonObjectReader<TProperty>().Configure(m => m.Map()),
        //                Significance = significance
        //            }
        //        }
        //    );
        //}

        /// <summary>Map a property of any type, using the specified <see cref="IJsonReader{T}"/>.</summary>
        /// <typeparam name="TProperty">The type of the destination property or field.</typeparam>
        /// <param name="propertyName">The name of the JSON property.</param>
        /// <param name="anyType">An expression that returns the destination property, eg. order => order.Id.</param>
        /// <param name="converter">The function that performs the actual conversion from JSON to the destination type.</param>
        /// <param name="significance">Determines if the JSON property is required, optional or ignored.</param>
        public void Map<TProperty>(
            string propertyName,
            Expression<SelectProperty<TObject, TProperty>> anyType,
            ConvertJsonElement<TProperty> converter,
            MappingSignificance significance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) anyType.Body).Member,
                    Significance = significance,
                    ValueNode = new JsonValueMapping<TProperty>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Any,
                        JsonConverter = converter,
                        Significance = significance
                    }
                }
            );
        }

        public void Map<TProperty>(
            string propertyName,
            Expression<SelectProperty<TObject, IEnumerable<TProperty>?>> anyTypes,
            ConvertJsonElement<TProperty> converter,
            MappingSignificance significance = MappingSignificance.Required,
            MappingSignificance itemSignificance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) anyTypes.Body).Member,
                    Significance = significance,
                    ValueNode = new JsonArrayMapping<TProperty>
                    {
                        Name = propertyName,
                        ValueMapping = new JsonValueMapping<TProperty>
                        {
                            Name = propertyName,
                            ValueKind = JsonValueMappingKind.Any,
                            JsonConverter = converter,
                            Significance = itemSignificance
                        },
                        Significance = significance
                    }
                }
            );
        }
    }
}
