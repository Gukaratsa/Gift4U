using Gift4U.BlazorApp.Data;
using System;
using System.Collections.Generic;

namespace Gift4U.BlazorApp.Store
{
    public class TimerState
    {
        public Guid GiverId { get; init; }
        public Guid ReceiverId { get; init; }
        public Guid GivenGiftId { get; init; }
        public bool Paused { get; init; }
        public bool Finished { get; init; }
        public TimeSpan CurrentTime { get; init; }
        public TimeSpan TotalTime { get; init; }

        public TimerState(Guid giverId, Guid receiverId, Guid givenGiftId, bool paused, bool finished, TimeSpan currentTime, TimeSpan totalTime)
        {
            GiverId = giverId;
            ReceiverId = receiverId;
            GivenGiftId = givenGiftId;
            Paused = paused;
            Finished = finished;
            CurrentTime = currentTime;
            TotalTime = totalTime;
        }
    }

    public class TimerStore : IObservable<TimerState>
    {
        private TimerState _timerState = new TimerState(new Guid(), new Guid(), new Guid(), true, false, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(0));
        private readonly List<IObserver<TimerState>> observers = new List<IObserver<TimerState>>();

        public TimerState GetState()
        {
            return _timerState;
        }

        public void Broadcast_Change(Guid giverId, Guid receiverId, Guid givenGiftId, bool paused, bool finished, TimeSpan currentTime, TimeSpan totalTime)
        {
            _timerState = new TimerState(giverId, receiverId, givenGiftId, paused, finished, currentTime, totalTime);
            Broadcast();
        }

        public IDisposable Subscribe(IObserver<TimerState> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                observer.OnNext(_timerState);
            }
            return new Unsubscriber<TimerState>(observers, observer);
        }

        private void Broadcast()
        {
            foreach (var observer in observers)
                observer.OnNext(_timerState);
        }
    }
}