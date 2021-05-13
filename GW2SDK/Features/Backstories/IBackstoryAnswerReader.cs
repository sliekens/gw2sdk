using JetBrains.Annotations;
using GW2SDK.Backstories.Answers;
using GW2SDK.Json;

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public interface IBackstoryAnswerReader : IJsonReader<BackstoryAnswer>
    {
        IJsonReader<string> Id { get; }
    }
}