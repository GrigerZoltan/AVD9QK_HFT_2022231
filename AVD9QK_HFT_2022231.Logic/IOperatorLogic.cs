using AVD9QK_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Logic
{
    public interface IOperatorLogic
    {
        void Create(Operator item);
        void Delete(int id);
        Operator Read(int id);
        IQueryable<Operator> ReadAll();
        void Update(Operator item);
        IEnumerable<KeyValuePair<string, string>> FactionNamewithOperator();
        IEnumerable<KeyValuePair<string, int>> MaxAgePerFaction();
        IEnumerable<KeyValuePair<string, int>> MinHeightPerFaction();
        IEnumerable<KeyValuePair<string, int>> OperatorsPerFaction();
        IEnumerable<KeyValuePair<string, string>> OperatorsPreferredWeapon();
    }
}
