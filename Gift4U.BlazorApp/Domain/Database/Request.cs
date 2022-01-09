using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gift4U.Domain.Models
{
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Requested { get; set; }
        public DateTime? Responded { get; set; }
        public DateTime? Started { get; set; }

        public Guid RequestStateId{get; set;}
        public RequestState RequestState {get; set;}
        public Guid GivenInRequestId { get; set;}
        public GivenGift GivenInRequest { get; set;}
        
    }
}