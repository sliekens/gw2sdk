using System.Collections.Generic;
using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Json
{
    [PublicAPI]
    public interface IJsonReader<out T>
    {
        public T Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior = default);

        T Read(
            JsonDocument json,
            MissingMemberBehavior missingMemberBehavior = default) =>
            Read(json.RootElement, missingMemberBehavior);

        IEnumerable<T> ReadArray(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior = default)
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var item in json.EnumerateArray())
            {
                yield return Read(item, missingMemberBehavior);
            }
        }

        IEnumerable<T> ReadArray(
            JsonDocument json,
            MissingMemberBehavior missingMemberBehavior = default) =>
            ReadArray(json.RootElement, missingMemberBehavior);
    }
}
