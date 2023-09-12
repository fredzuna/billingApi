using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicBilling.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BasicBilling.API.Dto;

namespace BasicBilling.API.Controllers
{
    [ApiController]
    [Route("api/consumptions")]
    public class ConsumptionController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ConsumptionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        /*[HttpGet]
        public async Task<ActionResult<List<Service>>> Get() 
        {
            return await context.Consumptions.ToListAsync();
        }*/

        [HttpPost]
        public async Task<ActionResult<List<Consumption>>> CreateBilling(CreateBilling billing)
        {
            var service = context.Services.SingleOrDefault((item) => item.Id == billing.ServiceId);

            if (service == null) {
                return NotFound();
            }

            var newBilling = new Consumption()
            {
                Period = createBilling.Period,
                Status = StatusEnum.Pending,
                Amount = 150
            };



            context.Services.AddRange(newServices);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
