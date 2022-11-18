using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Models
{
    [Table("operators")]
    public class Operator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperatorId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Height { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Faction Faction { get; set; }

        [ForeignKey(nameof(Faction))]
        public int FactionId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Weapon Weapon { get; set; }

        [ForeignKey(nameof(Weapon))]
        public int WeaponId { get; set; }

        public Operator()
        {

        }

        public Operator(string line)
        {
            string[] split = line.Split('#');
            OperatorId = int.Parse(split[0]);
            Name = split[1];
            Age = int.Parse(split[2]);
            Height = int.Parse(split[3]);
            FactionId = int.Parse(split[4]);
            WeaponId = int.Parse(split[5]);
        }
    }
}
