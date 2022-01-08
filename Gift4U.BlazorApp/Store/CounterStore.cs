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

    internal class Unsubscriber<T> : IDisposable
    {
        private List<IObserver<T>> _observers;
        private IObserver<T> _observer;

        internal Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }

    public class Observer<T> : IObserver<T>
    {
        private readonly Action<T> _OnNext;
        private readonly Action<Exception> _OnError;
        private readonly Action _OnCompleted;

        public Observer(Action<T> OnNext, Action<Exception> OnError, Action OnCompleted)
        {
            _OnNext = OnNext;
            _OnError = OnError;
            _OnCompleted = OnCompleted;
        }

        public void OnCompleted()
        {
            _OnCompleted.Invoke();
        }

        public void OnError(Exception error)
        {
            _OnError.Invoke(error);
        }

        public void OnNext(T value)
        {
            _OnNext.Invoke(value);
        }
    }
}