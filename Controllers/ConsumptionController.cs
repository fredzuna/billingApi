using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicBilling.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BasicBilling.API.Dto;
using Microsoft.AspNetCore.Cors;

namespace BasicBilling.API.Controllers
{
    [ApiController]
    [Route("api/Consumptions")]
    public class ConsumptionController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ConsumptionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Consumption>>> Get() 
        {
            return await context.Consumptions
                .Include(item => item.Client)
                .Include(item => item.Service)
                .AsNoTracking()
                .ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Consumption>> GetById(int id)
        {
            return await context.Consumptions
                .Include(item => item.Client)
                .Include(item => item.Service)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        
        [HttpGet(template: "User/{userId}", Name = "GetByUserId")]
        public async Task<ActionResult<Consumption>> GetByUserId(int userId)
        {
            return await context.Consumptions
                .Where(c => c.ClientId == userId)
                .Include(item => item.Client)
                .Include(item => item.Service)
                .AsNoTracking().FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult<List<Consumption>>> CreateBilling(CreateBilling billing)
        {
            var service = context.Services.SingleOrDefault((item) => item.Id == billing.ServiceId);
            var client = context.Clients.SingleOrDefault((item) => item.Id == billing.ClientId);

            if (service == null)
            {
                return NotFound("The service has not been found");
            }

            if (client == null) {
                return NotFound("The client has not been found");
            }

            var newConsumption = new Consumption()
            {
                Period = billing.Period,
                Status = StatusEnum.Pending,
                Amount = billing.Amount
            };
            newConsumption.Service = service;
            newConsumption.Client = client;

            context.Consumptions.Add(newConsumption);
            var res = await context.SaveChangesAsync();

            return Ok(res);
        }

        [HttpPost(template: "PayBilling", Name = "PayBilling")]
        public async Task<ActionResult> PayBilling([FromBody] PayBilling payment)
        {   
            var updateConsumption = context.Consumptions.SingleOrDefault((item) => item.Id == payment.ConsumptionId);

            if (updateConsumption == null)
            {
                return NotFound("The consumption has not been found");
            }

            if (updateConsumption.Status == StatusEnum.Paid)
            {
                return NotFound("The consumption has been paid already");
            }

            var newPayment= new Payment()
            {
                Amount = updateConsumption.Amount,
                PaymentDate = DateTime.Now
            };

            updateConsumption.PaymentDetail = newPayment;
            updateConsumption.Status = StatusEnum.Paid;

            context.Consumptions.Update(updateConsumption);
            var res = await context.SaveChangesAsync();

            return Ok(res);
        }
    }
}
