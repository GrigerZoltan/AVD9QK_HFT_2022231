using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Models
{
    public class Operator
    {
        public int OperatorId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Height { get; set; }

        public virtual Faction Faction { get; set; }

        public int FactionId { get; set; }

        public virtual Weapon Weapon { get; set; }

        public int WeaponId { get; set; }

        public Operator()
        {

        }
    }
}
