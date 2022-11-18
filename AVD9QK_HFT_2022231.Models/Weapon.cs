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
    [Table("weapons")]
    public class Weapon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WeaponId { get; set; }

        public string WeaponName { get; set; }

        public string Caliber { get; set; }

        public string Facturer { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Operator Operator { get; set; }

        [ForeignKey(nameof(Operator))]
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
