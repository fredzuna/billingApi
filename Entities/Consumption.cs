using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicBilling.API.Entities
{
    public enum StatusEnum
    {
        Pending,
        Paid,
    }

    public class Consumption
    {
        public int Id { get; set; }

        public DateTime Period { get; set; }

        public StatusEnum Status { get; set; }

        public int Amount{ get; set; }

        public Payment? PaymentDetail { get; set; }


        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public Client Client { get; set; }
    }
}
