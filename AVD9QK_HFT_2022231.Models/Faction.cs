using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Models
{
    public class Faction
    {
        public int FactionId { get; set; }

        public string FactionName { get; set; }

        public string Nation { get; set; }

        public virtual ICollection<Operator> Operators { get; set; }

        public Faction()
        {
            this.Operators = new HashSet<Operator>();
        }

        public Faction(string line)
        {
            string[] split = line.Split('#');
            FactionId = int.Parse(split[0]);
            FactionName = split[1];
            Nation = split[2];
            Operators = new HashSet<Operator>();
        }
    }
}
