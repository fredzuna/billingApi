using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicBilling.API.Dto
{
    public class CreateBilling
    {
        public DateTime Period { get; set; }

        public int ServiceId { get; set; }

        public int Amount{ get; set; }

        public int ClientId { get; set; }

    }
}
