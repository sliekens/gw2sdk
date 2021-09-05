using GW2SDK.Backstories.Questions;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public interface IBackstoryQuestionReader : IJsonReader<BackstoryQuestion>
    {
        IJsonReader<int> Id { get; }
    }
}
