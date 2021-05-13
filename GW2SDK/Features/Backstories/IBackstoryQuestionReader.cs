using JetBrains.Annotations;
using GW2SDK.Backstories.Questions;
using GW2SDK.Json;

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public interface IBackstoryQuestionReader : IJsonReader<BackstoryQuestion>
    {
        IJsonReader<int> Id { get; }
    }
}