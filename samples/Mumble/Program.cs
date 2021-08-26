﻿using System;
using System.Threading;
using GW2SDK.Json;
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

        private void CallBack(object state)
        {
            var snapshot = (Snapshot) state;
            var pos = snapshot.AvatarPosition;

            if (!snapshot.TryGetIdentity(out var identity, MissingMemberBehavior.Error))
            {
                return;
            }

            if (!snapshot.TryGetContext(out var context))
            {
                return;
            }

            Console.WriteLine("Update {0}: {1}, the {2} {3} is {4} on {5} in Map: {6}, Position: {{ Right = {7}, Up = {8}, Front = {9} }}",
                snapshot.UiTick,
                identity.Name,
                identity.Race,
                identity.Profession,
                context.UiState.HasFlag(UiState.IsInCombat) ? "fighting" : "traveling",
                context.IsMounted ? context.GetMount() : "foot",
                identity.MapId,
                pos[0],
                pos[1],
                pos[2]);
        }
    }
}