using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicBilling.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BasicBilling.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ServiceController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Service>>> Get() 
        {
            return await context.Services.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<List<Service>>> Save()
        {
            var newServices = new List<Service>() {
                new Service(){ Name= "ELECTRICITY" , Description="Electricity elfec"},
                new Service(){ Name= "WATER" , Description="water service"},
                new Service(){ Name= "SEWER" , Description="Sewer service"}
            };

            context.Services.AddRange(newServices);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
