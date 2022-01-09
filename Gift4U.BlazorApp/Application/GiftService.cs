using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            giftDBContext?.Database.Migrate();
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

        public IEnumerable<Gift> GetGifts()
        {
            return _giftDBContext.Gifts;
        }

        public IEnumerable<GivenGift> GetReceivedGifts(User receiver)
        {
            return _giftDBContext.GivenGifts
                .Where(gg => gg.ReceiverId == receiver.Id);
        }

        public void RequestGiftActivation(Gift gift)
        {

        }

        public void ResponseToGiftActivation()
        {

        }

        public void ActivateGift()
        {

        }
    }
}
