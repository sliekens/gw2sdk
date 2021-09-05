using JetBrains.Annotations;

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public interface IBackstoryReader
    {
        IBackstoryQuestionReader Question { get; }

        IBackstoryAnswerReader Answer { get; }
    }
}
