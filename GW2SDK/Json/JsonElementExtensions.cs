using System;
using System.Text.Json;

namespace GW2SDK.Json
{
    internal static class JsonElementExtensions
    {
        /// <summary>
        /// Returns a string, or throws if the element is null or not a string.
        /// </summary>
        /// <param name="json">A String value.</param>
        /// <returns>The value of the JSON element as a non-null string (can be empty).</returns>
        internal static string GetStringRequired(this JsonElement json)
        {
            var value = json.GetString();
            if (value is null)
            {
                throw new InvalidOperationException(
                    $"The requested operation requires an element of type 'String', but the target element has type '{json.ValueKind}'.");
            }

            return value;
        }

        /// <summary>
        /// Returns the value of the element as an array, or throws if the element is null or not an array.
        /// </summary>
        /// <typeparam name="T">The type of array elements.</typeparam>
        /// <param name="json">An array value.</param>
        /// <param name="convert">The function used to convert each array item.</param>
        /// <returns></returns>
        internal static T[] GetArray<T>(this JsonElement json, Func<JsonElement, T> convert)
        {
            var values = new T[json.GetArrayLength()];
            for (var i = 0; i < values.Length; i++)
            {
                var item = json[i];
                values[i] = convert(item);
            }

            return values;
        }
    }
}