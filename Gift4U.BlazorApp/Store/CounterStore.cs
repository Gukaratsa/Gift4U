using System;
using System.Collections.Generic;

namespace Gift4U.BlazorApp.Store
{
    public class CounterState
    {
        public int CounterValue {get;}
        public CounterState(int counterValue)
        {
            CounterValue = counterValue;
        }
    }

    public class CounterStore : IObservable<CounterState>
    {
        private CounterState _counterState = new CounterState(0);
        private readonly List<IObserver<CounterState>> observers = new List<IObserver<CounterState>>();

        public CounterState GetState()
        {
            return _counterState;
        }

        public void Increment()
        {
           _counterState = new CounterState(_counterState.CounterValue + 1);
           Broadcast();
        }

        public void Decrement()
        {
           _counterState = new CounterState(_counterState.CounterValue + 1);
           Broadcast();
        }

        public IDisposable Subscribe(IObserver<CounterState> observer)
        {
            if (! observers.Contains(observer)) {
                observers.Add(observer);
                observer.OnNext(_counterState);
            }
            return new Unsubscriber<CounterState>(observers, observer);
        }

        private void Broadcast()
        {
            foreach(var observer in observers)
            observer.OnNext(_counterState);
        }
    }
}