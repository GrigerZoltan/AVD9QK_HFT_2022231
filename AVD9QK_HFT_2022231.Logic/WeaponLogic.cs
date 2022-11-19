using AVD9QK_HFT_2022231.Models;
using AVD9QK_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Logic
{
    public class WeaponLogic : IWeaponLogic
    {
        IRepository<Weapon> repo;

        public WeaponLogic(IRepository<Weapon> repo)
        {
            this.repo = repo;
        }

        public void Create(Weapon item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Weapon Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Weapon> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Weapon item)
        {
            this.repo.Update(item);
        }
    }
}
