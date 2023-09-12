using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasicBilling.API.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "Date")]
        public DateTime PaymentDate { get; set; }

        public int ConsumptionId { get; set; } // Required foreign key property
        public Consumption Consumption { get; set; } = null!; // Required reference navigation to principal
    }
}
