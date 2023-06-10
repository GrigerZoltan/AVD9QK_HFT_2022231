using AVD9QK_HFT_2022231.Endpoint.Services;
using AVD9QK_HFT_2022231.Logic;
using AVD9QK_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        IOperatorLogic logic;

        IHubContext<SignalRHub> hub;

        public OperatorController(IOperatorLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Operator> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Operator Get(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Operator value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("OperatorCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Operator value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("OperatorUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var operatorToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("OperatorDeleted", operatorToDelete);
        }
    }
}
