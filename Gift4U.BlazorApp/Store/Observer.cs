using System;

namespace Gift4U.BlazorApp.Store
{
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