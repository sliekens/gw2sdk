using System.Collections.Generic;
using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Json
{
    [PublicAPI]
    public interface IJsonReader<out T>
    {
        public T Read(JsonElement json, MissingMemberBehavior missingMemberBehavior);

#if NET
        T Read(JsonDocument json, MissingMemberBehavior missingMemberBehavior) =>
            Read(json.RootElement, missingMemberBehavior);

        IEnumerable<T> ReadArray(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var item in json.EnumerateArray())
            {
                yield return Read(item, missingMemberBehavior);
            }
        }

        IEnumerable<T> ReadArray(JsonDocument json, MissingMemberBehavior missingMemberBehavior) =>
            ReadArray(json.RootElement, missingMemberBehavior);
#endif
    }

#if !NET
    [PublicAPI]
    public static class JsonReaderExtensions
    {
        public static T Read<T>(
            this IJsonReader<T> instance,
            JsonDocument json,
            MissingMemberBehavior missingMemberBehavior
        ) =>
            instance.Read(json.RootElement, missingMemberBehavior);

        public static IEnumerable<T> ReadArray<T>(
            this IJsonReader<T> instance,
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var item in json.EnumerateArray())
            {
                yield return instance.Read(item, missingMemberBehavior);
            }
        }

        public static IEnumerable<T> ReadArray<T>(
            this IJsonReader<T> instance,
            JsonDocument json,
            MissingMemberBehavior missingMemberBehavior
        ) =>
            instance.ReadArray(json.RootElement, missingMemberBehavior);
    }
#endif
}
