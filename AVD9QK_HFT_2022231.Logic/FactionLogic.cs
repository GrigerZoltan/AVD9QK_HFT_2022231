using AVD9QK_HFT_2022231.Models;
using AVD9QK_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Logic
{
    public class FactionLogic : IFactionLogic
    {
        IRepository<Faction> repo;

        public FactionLogic(IRepository<Faction> repo)
        {
            this.repo = repo;
        }

        public void Create(Faction item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Faction Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Faction> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Faction item)
        {
            this.repo.Update(item);
        }
    }
}
