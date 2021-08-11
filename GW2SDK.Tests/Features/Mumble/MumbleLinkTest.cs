using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Json;
using GW2SDK.Mumble;
using Xunit;

#pragma warning disable CA1416
namespace GW2SDK.Tests.Features.Mumble
{
    public class MumbleLinkTestObserver : IObserver<Snapshot>
    {
        private readonly TaskCompletionSource<bool> tcs;

        public Task<bool> Handle => tcs.Task;

        public bool HasFirst { get; private set; }
        public Snapshot First { get; private set; }

        public Snapshot Last { get; private set; }

        public MumbleLinkTestObserver(CancellationToken ct)
        {
            tcs = new TaskCompletionSource<bool>();
            ct.Register(tcs.SetCanceled);
        }

        public void OnCompleted()
        {
            tcs.SetResult(HasFirst);
        }

        public void OnError(Exception error)
        {
            tcs.SetException(error);
        }

        public void OnNext(Snapshot value)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (!HasFirst)
            {
                HasFirst = true;
                First = value;
            }
            else
            {
                Last = value;
                tcs.TrySetResult(true);
            }
        }
    }

    public class MumbleLinkTest
    {
        [MumbleLinkFact]
        public void Name_can_be_read_from_Mumble_link()
        {
            using var sut = MumbleLink.Open();

            var actual = sut.GetSnapshot();

            Assert.Equal("Guild Wars 2", actual.Name);
        }

        [MumbleLinkFact]
        public async Task The_link_is_self_updating()
        {
            using var sut = MumbleLink.Open();

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            var actual = new MumbleLinkTestObserver(cts.Token);

            sut.Subscribe(actual);

            try
            {
                await actual.Handle;
            }
            catch (TaskCanceledException)
            {
                Assert.True(actual.Last.UiTick > actual.First.UiTick, "This test only works if you are in a map, not in a loading screen etc.");
            }

            Assert.True(actual.Last.UiTick > actual.First.UiTick, "MumbleLink should be self-updating");
        }
        
        [MumbleLinkFact]
        public void The_link_provides_context()
        {
            using var sut = MumbleLink.Open();

            var snapshot = sut.GetSnapshot();
            Assert.True(snapshot.TryGetContext(out var actual));
            Assert.True(actual.BuildId > 100_000, "Game build should be over 100,000");
        }

        [MumbleLinkFact]
        public void The_link_provides_identity()
        {
            using var sut = MumbleLink.Open();

            var snapshot = sut.GetSnapshot();
            Assert.True(snapshot.TryGetIdentity(out var actual, MissingMemberBehavior.Error));
        }
    }

    public class MumbleLinkFact : FactAttribute
    {
        public MumbleLinkFact()
        {
            if (!MumbleLink.IsSupported())
            {
                Skip = "MumbleLink is not supported on the current platform.";
                return;
            }

            try
            {
                using var file = MemoryMappedFile.OpenExisting("MumbleLink", MemoryMappedFileRights.Read);
            }
            catch (FileNotFoundException)
            {
                Skip = "The MumbleLink is not initialized. Start Mumble -and- Guild Wars 2 before running this test.";
            }
        }
    }
}
