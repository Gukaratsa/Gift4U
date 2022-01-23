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

        public void GiveGift(Guid fromId, Guid toId, Guid giftId, int amount)
        {
            var from = _giftDBContext.Users.First(u => u.Id == fromId);
            var to = _giftDBContext.Users.First(u => u.Id == toId);
            var gift = _giftDBContext.Gifts.First(u => u.Id == giftId);

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

        public void AddGift(string Name, string ImageBase64, double Duration)
        {
            _giftDBContext.Gifts.Add(new Gift()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Image = ImageBase64,
                Duration = TimeSpan.FromMinutes(Duration)
            });
            _giftDBContext.SaveChanges();
        }

        public IEnumerable<Gift> GetGifts()
        {
            return _giftDBContext.Gifts;
        }

        public IEnumerable<GivenGift> GetGifts(Guid userId)
        {
            // Why are these laods needed?
            _giftDBContext.Gifts.Load();
            _giftDBContext.Users.Load();
            _giftDBContext.GivenGifts.Load();
            _giftDBContext.RequestStates.Load();
            _giftDBContext.Requests.Load();
            var result = _giftDBContext.GivenGifts.Where(gg => gg.ReceiverId == userId);
            return result;
        }

        public GivenGift GetGivenGift(Guid GivenGiftId)
        {
            // Why are these laods needed?
            _giftDBContext.Gifts.Load();
            _giftDBContext.Users.Load();
            _giftDBContext.GivenGifts.Load();
            var result = _giftDBContext.GivenGifts.First(g => g.Id == GivenGiftId);
            return result;
        }

        public void DeleteGift(Gift gift)
        {
            _giftDBContext.Gifts.Remove(gift);
            _giftDBContext.SaveChanges();
        }

        public IEnumerable<GivenGift> GetReceivedGifts(User receiver)
        {
            // TODO This is the correct interface to request reeceived gifts
            return _giftDBContext.GivenGifts
                .Where(gg => gg.ReceiverId == receiver.Id);
        }

        public IEnumerable<Request> PendingRequests(Guid giftGiverId)
        {
            _giftDBContext.Gifts.Load();
            _giftDBContext.GivenGifts.Load();
            _giftDBContext.RequestStates.Load();
            _giftDBContext.Requests.Load();
            var pendingState = _giftDBContext.RequestStates.First(rs => rs.Name == nameof(RequestStateEnum.Pending));
            var givenGifts = _giftDBContext.Requests.Where(r => r.GivenInRequest.GiverId == giftGiverId);
            var pendingGifts = givenGifts.Where(gg => gg.RequestStateId == pendingState.Id);
            return pendingGifts;
        }

        public IEnumerable<Request> RequestsWaiting(User giftReceiver)
        {
            return _giftDBContext.Requests.Where(r =>
                r.GivenInRequest.ReceiverId == giftReceiver.Id &&
                r.RequestState.Name != nameof(RequestStateEnum.Pending));
        }

        public Request RequestGiftActivation(Guid givenGiftId)
        {
            var gift = _giftDBContext.GivenGifts.First(gg => gg.Id == givenGiftId);
            var pendingStatus = _giftDBContext.RequestStates.First(rs => rs.Name == nameof(RequestStateEnum.Pending));
            var giftPending = _giftDBContext.Requests.FirstOrDefault(r => r.GivenInRequestId == givenGiftId && r.RequestStateId == pendingStatus.Id);
            if (giftPending != null)
            {
                // Already pending
                return null;
            }

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
            return _giftDBContext.Requests.First(r => r.GivenInRequestId == givenGiftId && r.RequestStateId == pendingStatus.Id);
        }

        public void ResponseToGiftActivation(Request request, RequestStateEnum requestResponse)
        {
            var requestName = Enum.GetName(typeof(RequestStateEnum), requestResponse);
            var pendingState = _giftDBContext.RequestStates
                .First(rs => rs.Name == requestName);
            request.RequestState = pendingState;
            request.Responded = DateTime.Now;
            _giftDBContext.SaveChanges();
        }

        public IEnumerable<Request> RequestsAccepted(Guid giftReceiverId)
        {
            var approvedState = _giftDBContext.RequestStates
             .Where(rs => rs.Name == nameof(RequestStateEnum.RequestApproved))
             .First();
            var result = _giftDBContext.Requests
                .Where(r =>
                    r.GivenInRequest.ReceiverId == giftReceiverId &&
                    r.RequestStateId == approvedState.Id);
            return result;
        }

        public void ActivateGift(Request request)
        {
            var pendingState = _giftDBContext.RequestStates
                .Where(rs => rs.Name == nameof(RequestStateEnum.RequestStarted))
                .First();

            request.RequestState = pendingState;
            request.GivenInRequest.Used += 1;
            request.Started = DateTime.Now;
            _giftDBContext.SaveChanges();
        }

        public TimeSpan? GetGiftDuration(Guid giftId)
        {
            var gift = _giftDBContext.Gifts.FirstOrDefault(g => g.Id == giftId);
            if (gift == null)
                return null;
            return gift.Duration;
        }
    }
}
