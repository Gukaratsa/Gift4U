using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gift4U.Domain.Models
{
    public class GivenGift
    {
        public Guid Id { get; set; }
        public Guid GiftId { get; set; }
        public Gift Gift { get; set; }
        public int Amount { get; set; }
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
        public Guid GiverId { get; set; }
        public User Giver { get; set; }

        public ICollection<Request> Requests{ get; set; }
    }
}
