using AVD9QK_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Logic
{
    public interface IFactionLogic
    {
        void Create(Faction item);

        void Delete(int id);

        Faction Read(int id);

        IQueryable<Faction> ReadAll();

        void Update(Faction item);
    }
}
