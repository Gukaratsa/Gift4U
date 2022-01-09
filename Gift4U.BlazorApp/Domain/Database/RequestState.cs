using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gift4U.Domain.Models
{
    public class RequestState
    {
        public Guid Id { get; set; }
        public string Name {get; set;}

        public ICollection<Request> Requests{ get; set;}
    }
}