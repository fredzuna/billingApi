using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicBilling.API.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Consumption> Consumptions { get; set; }

        // public virtual ICollection<Service> Services { get; set; }
    }
}
