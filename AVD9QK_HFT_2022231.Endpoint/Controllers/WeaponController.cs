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
    public class WeaponController : ControllerBase
    {
        IWeaponLogic logic;

        IHubContext<SignalRHub> hub;

        public WeaponController(IWeaponLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Weapon> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Weapon Get(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Weapon value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("WeaponCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Weapon value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("WeaponUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var weaponToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("WeaponDeleted", weaponToDelete);
        }
    }
}
