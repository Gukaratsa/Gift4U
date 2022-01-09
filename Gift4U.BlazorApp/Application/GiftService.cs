using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gift4U.BlazorApp.Data;
using Gift4U.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gift4U.Application.Services
{
    public class GiftService
    {
        private readonly GiftDBContext _giftDBContext;

        public GiftService(GiftDBContext giftDBContext)
        {
            this._giftDBContext = giftDBContext;
            //giftDBContext?.Database.Migrate();
        }

        public void GiveGift(User from, User to, Gift gift, int amount)
        {
            _giftDBContext.GivenGifts.Add(new GivenGift()
            {
                Gift = gift,
                Amount = amount,
                Giver = from,
                Receiver = to
            });
            _giftDBContext.SaveChanges();
        }

        public void AddImage()
        {

        }

        public void AddGift(string Name, string ImageBase64)
        {
            _giftDBContext.Gifts.Add(new Gift() { 
                Id = Guid.NewGuid(),
                Name = Name,
                Image = ImageBase64
            });
            _giftDBContext.SaveChanges();
        }

        public IEnumerable<Gift> GetGifts()
        {
            return _giftDBContext.Gifts;
        }

        public void DeleteGift(Gift gift)
        {
            _giftDBContext.Gifts.Remove(gift);
            _giftDBContext.SaveChanges();
        }

        public IEnumerable<GivenGift> GetReceivedGifts(User receiver)
        {
            return _giftDBContext.GivenGifts
                .Where(gg => gg.ReceiverId == receiver.Id);
        }

        public IEnumerable<Request> PendingRequests(User giftGiver)
        {
            return _giftDBContext.Requests.Where(r =>
                r.GivenInRequest.GiverId == giftGiver.Id &&
                r.RequestState.Name != nameof(RequestStateEnum.Pending));
        }

        public IEnumerable<Request> RequestsWaiting(User giftReceiver)
        {
            return _giftDBContext.Requests.Where(r =>
                r.GivenInRequest.ReceiverId == giftReceiver.Id &&
                r.RequestState.Name != nameof(RequestStateEnum.Pending));
        }

        public void RequestGiftActivation(GivenGift gift)
        {
            var pendingState = _giftDBContext.RequestStates
                .Where(rs => rs.Name == nameof(RequestStateEnum.Pending))
                .First();

            _giftDBContext.Requests.Add(new Request()
            {
                Id = Guid.NewGuid(),
                GivenInRequest = gift,
                Requested = DateTime.Now,
                RequestState = pendingState,                
            });
            _giftDBContext.SaveChanges();
        }

        public void ResponseToGiftActivation(Request request, RequestStateEnum requestResponse)
        {
            var pendingState = _giftDBContext.RequestStates
                .Where(rs => rs.Name == nameof(requestResponse))
                .First();

            request.RequestState = pendingState;
            request.Responded = DateTime.Now;
            _giftDBContext.SaveChanges();
        }

        public void ActivateGift(Request request)
        {
            var pendingState = _giftDBContext.RequestStates
                .Where(rs => rs.Name == nameof(RequestStateEnum.RequestStarted))
                .First();

            request.RequestState = pendingState;
            request.Started = DateTime.Now;
            _giftDBContext.SaveChanges();
        }
    }
}
