using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Models
{
    public class Weapon
    {
        public int WeaponId { get; set; }

        public string WeaponName { get; set; }

        public string Caliber { get; set; }

        public string Facturer { get; set; }

        public virtual Operator Operator { get; set; }

        public int OperatorId { get; set; }

        public Weapon()
        {

        }

        public Weapon(string line)
        {
            string[] split = line.Split('#');
            WeaponId = int.Parse(split[0]);
            WeaponName = split[1];
            Caliber = split[2];
            Facturer = split[3];
            OperatorId = int.Parse(split[4]);
        }
    }
}
