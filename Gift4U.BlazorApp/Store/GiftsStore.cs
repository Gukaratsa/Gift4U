using Gift4U.BlazorApp.Data;
using System;
using System.Collections.Generic;

namespace Gift4U.BlazorApp.Store
{
    public class GiftState
    {
        public Guid ActivatorId { get; init; }
        public Guid ReceiverId { get; init; }
        public Guid GivenGiftId { get; init; }
        public RequestStateEnum NewState { get; init; }
        public GiftState(Guid activatorId, Guid receiverId, Guid givenGiftId, RequestStateEnum newState)
        {
            ActivatorId = activatorId;
            ReceiverId = receiverId;
            GivenGiftId = givenGiftId;
            NewState = newState;
        }
    }

    public class GiftStore : IObservable<GiftState>
    {
        private GiftState _giftState = new GiftState(new Guid(), new Guid(), new Guid(), RequestStateEnum.Pending);
        private readonly List<IObserver<GiftState>> observers = new List<IObserver<GiftState>>();

        public void Broadcast_Change(Guid activatorId, Guid receiverId, Guid givenGiftId, RequestStateEnum newState)
        {
            _giftState = new GiftState(activatorId, receiverId, givenGiftId, newState);
            Broadcast();
        }

        public IDisposable Subscribe(IObserver<GiftState> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                observer.OnNext(_giftState);
            }
            return new Unsubscriber<GiftState>(observers, observer);
        }

        private void Broadcast()
        {
            foreach (var observer in observers)
                observer.OnNext(_giftState);
        }
    }
}