using AVD9QK_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Logic
{
    public interface IWeaponLogic
    {
        void Create(Weapon item);
        void Delete(int id);
        Weapon Read(int id);
        IQueryable<Weapon> ReadAll();
        void Update(Weapon item);
    }
}
