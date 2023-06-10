using AVD9QK_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Repository
{
    public class FactionRepository : Repository<Faction>, IRepository<Faction>
    {
        public FactionRepository(OperatorDbContext ctx) : base(ctx)
        {

        }

        public override Faction Read(int id)
        {
            return ctx.Factions.FirstOrDefault(t => t.FactionId == id);
        }

        public override void Update(Faction item)
        {
            var old = Read(item.FactionId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old,prop.GetValue(item));
                }
            }

            ctx.SaveChanges();
        }
    }
}
