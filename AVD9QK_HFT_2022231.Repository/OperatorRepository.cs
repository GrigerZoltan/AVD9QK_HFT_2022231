using AVD9QK_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Repository
{
    public class OperatorRepository : Repository<Operator>, IRepository<Operator>
    {
        public OperatorRepository(OperatorDbContext ctx) : base(ctx)
        {

        }

        public override Operator Read(int id)
        {
            return ctx.Operators.FirstOrDefault(t => t.OperatorId == id);
        }

        public override void Update(Operator item)
        {
            var old = Read(item.OperatorId);
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
