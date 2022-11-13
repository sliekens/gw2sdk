using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace GW2SDK.Tests.TestInfrastructure;

public static class AssertEx
{
    /// <summary>Verifies that all items in the collection pass when executed against action. Unlike
    /// <see cref="Assert.All{T}(IEnumerable{T}, Action{T})" />, ForEach will stop execution on first assertion error.</summary>
    /// <typeparam name="T">The type of the object to be verified</typeparam>
    /// <param name="collection">The collection</param>
    /// <param name="action">The action to test each item against</param>
    /// <exception cref="AllException">Thrown when the collection contains at least one non-matching element</exception>
    public static void ForEach<T>(IEnumerable<T> collection, Action<T> action)
    {
        var errors = new Stack<Tuple<int, object, Exception>>();
        var array = collection.ToArray();

        for (var idx = 0; idx < array.Length; ++idx)
        {
            try
            {
                action(array[idx]);
            }
            catch (Exception ex)
            {
                errors.Push(new Tuple<int, object, Exception>(idx, array[idx], ex));
                throw new AllException(array.Length, errors.ToArray());
            }
        }
    }

    public static void Equal(DateTimeOffset expected, DateTimeOffset actual, TimeSpan precision) =>
        Assert.Equal(expected.LocalDateTime, actual.LocalDateTime, precision);
}
