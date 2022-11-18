using AVD9QK_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Repository
{
    public class WeaponRepository : Repository<Weapon>, IRepository<Weapon>
    {
        public WeaponRepository(OperatorDbContext ctx) : base(ctx)
        {

        }

        public override Weapon Read(int id)
        {
            return ctx.Weapons.FirstOrDefault(t => t.WeaponId == id);
        }

        public override void Update(Weapon item)
        {
            var old = Read(item.WeaponId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }

            ctx.SaveChanges();
        }
    }
}
