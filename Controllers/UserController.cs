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
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get() 
        {
            return await context.Clients.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            return await context.Clients
                .Include(item => item.Consumptions)
                .ThenInclude(item => item.Service)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> Save()
        {
            var newClients = new List<Client>() {
                new Client(){ FirstName= "Jhon" , LastName="Smith"},
                new Client(){ FirstName= "Peter" , LastName="Fox"},
                new Client(){ FirstName= "Juan" , LastName="Rodriguez"},
                new Client(){ FirstName= "Maria" , LastName="Barrio"}
            };

            context.Clients.AddRange(newClients);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
