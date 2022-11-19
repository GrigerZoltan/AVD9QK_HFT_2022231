using AVD9QK_HFT_2022231.Models;
using AVD9QK_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Logic
{
    public class OperatorLogic : IOperatorLogic
    {
        IRepository<Operator> operatorRepo;
        IRepository<Faction> factionRepo;
        IRepository<Weapon> weaponRepo;

        public OperatorLogic(IRepository<Operator> operatorRepo, IRepository<Faction> factionRepo, IRepository<Weapon> weaponRepo)
        {
            this.operatorRepo = operatorRepo;
            this.factionRepo = factionRepo;
            this.weaponRepo = weaponRepo;
        }

        public void Create(Operator item)
        {
            if (item.Age < 18)
            {
                throw new ArgumentException("Ez a személy túl fiatal");
            }
            else if (item.Age > 70)
            {
                throw new ArgumentException("Ez a személy túl öreg");
            }

            this.operatorRepo.Create(item);
        }

        public void Delete(int id)
        {
            this.operatorRepo.Delete(id);
        }

        public Operator Read(int id)
        {
            return this.operatorRepo.Read(id);
        }

        public IQueryable<Operator> ReadAll()
        {
            return this.operatorRepo.ReadAll();
        }

        public void Update(Operator item)
        {
            this.operatorRepo.Update(item);
        }

        public IEnumerable<KeyValuePair<string, string>> FactionNamewithOperator()
        {
            return from x in factionRepo.ReadAll()
                   join y in operatorRepo.ReadAll()
                   on x.FactionId equals y.FactionId
                   select new KeyValuePair<string, string>(x.FactionName, y.Name);
        }

        public IEnumerable<KeyValuePair<string, string>> OperatorsPreferredWeapon()
        {
            return from x in operatorRepo.ReadAll()
                   join y in weaponRepo.ReadAll()
                   on x.WeaponId equals y.WeaponId
                   select new KeyValuePair<string, string>(x.Name, y.WeaponName);
        }

        public IEnumerable<KeyValuePair<string, int>> MaxAgePerFaction()
        {
            return from x in factionRepo.ReadAll()
                   join y in operatorRepo.ReadAll()
                   on x.FactionId equals y.FactionId
                   group y by x.FactionName into g
                   orderby g.Key
                   select new KeyValuePair<string, int>(g.Key, g.Max(op => op.Age));
        }

        public IEnumerable<KeyValuePair<string, int>> MinHeightPerFaction()
        {
            return from x in factionRepo.ReadAll()
                   join y in operatorRepo.ReadAll()
                   on x.FactionId equals y.FactionId
                   group y by x.FactionName into g
                   orderby g.Key
                   select new KeyValuePair<string, int>(g.Key, g.Min(op => op.Height));
        }

        public IEnumerable<KeyValuePair<string, int>> OperatorsPerFaction()
        {
            return from x in factionRepo.ReadAll()
                   join y in operatorRepo.ReadAll()
                   on x.FactionId equals y.FactionId
                   let joinedSet = new { y.FactionId, x.FactionName }
                   group joinedSet by joinedSet.FactionName into g
                   orderby g.Key
                   select new KeyValuePair<string, int>(g.Key, g.Count());
        }
    }
}
