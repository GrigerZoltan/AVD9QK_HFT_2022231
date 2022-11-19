using AVD9QK_HFT_2022231.Logic;
using AVD9QK_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FactionController : ControllerBase
    {
        IFactionLogic logic;

        public FactionController(IFactionLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Faction> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Faction Get(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Faction value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Faction value)
        {
            this.logic.Update(value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
