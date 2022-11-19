using AVD9QK_HFT_2022231.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IOperatorLogic logic;

        public StatController(IOperatorLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> OperatorsPerFaction()
        {
            return this.logic.OperatorsPerFaction();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, string>> OperatorsPreferredWeapon()
        {
            return this.logic.OperatorsPreferredWeapon();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> MinHeightPerFaction()
        {
            return this.logic.MinHeightPerFaction();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> MaxAgePerFaction()
        {
            return this.logic.MaxAgePerFaction();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, string>> FactionNameWithOperators()
        {
            return this.logic.FactionNamewithOperator();
        }
    }
}
