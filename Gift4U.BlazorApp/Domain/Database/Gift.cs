using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gift4U.Domain.Models
{
    public class Gift
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Image { get; set; }
        public TimeSpan? Duration { get; set; }

        public ICollection<GivenGift> GivenGifts { get; set; }
    }
}
