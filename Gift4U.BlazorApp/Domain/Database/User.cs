using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gift4U.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        // TODO this should be hashed
        public string Password { get; set; }

        public ICollection<GivenGift> GivenGifts { get; set; }
        public ICollection<GivenGift> ReceivedGifts { get; set; }
    }
}
