using System;
using System.Threading;
using GW2SDK.Mumble;

namespace Mumble
{
    internal class Program : IObserver<Snapshot>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Goodbye.");
        }

        public void OnError(Exception error)
        {
            Console.Error.WriteLine(error.ToString());
        }

        public void OnNext(Snapshot snapshot)
        {
            ThreadPool.QueueUserWorkItem(CallBack, snapshot);
        }

        private static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (_, _) =>
            {
                cts.Cancel();
            };

            new Program().RealMain(cts.Token);
        }

        public void RealMain(CancellationToken cancellationToken)
        {
            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine("This sample is only supported on Windows!");
                return;
            }

            using var mumble = MumbleLink.Open();

            using var subscription = mumble.Subscribe(this);

            WaitHandle.WaitAll(new[] { cancellationToken.WaitHandle });
        }

        private void CallBack(object? state)
        {
            var snapshot = (Snapshot) state;
            var pos = snapshot.AvatarPositionMeters;
            Console.WriteLine("Update: {0}, Right: {1}, Up: {2}, Front: {3}", snapshot.UiTick, pos[0], pos[1], pos[2]);
        }
    }
}
