using AVD9QK_HFT_2022231.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;

namespace AVD9QK_HFT_2022231.Client
{
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            if (entity == "Operator")
            {
                Console.WriteLine("Enter Operator Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Operator Age: ");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Operator Height: ");
                int height = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Operator FactionId: ");
                int fid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Operator WeaponId: ");
                int wid = Convert.ToInt32(Console.ReadLine());
                rest.Post(new Operator() { Name = name, Age = age, Height = height, FactionId = fid, WeaponId = wid }, "operator");
            }
            else if (entity == "Faction")
            {
                Console.WriteLine("Enter Faction Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Faction Nation: ");
                string nation = Console.ReadLine();

                rest.Post(new Faction() { FactionName = name, Nation = nation }, "faction");
            }
            else if (entity == "Weapon")
            {
                Console.WriteLine("Enter Weapon Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Weapon Caliber: ");
                string caliber = Console.ReadLine();
                Console.WriteLine("Enter Weapon Facturer: ");
                string facturer = Console.ReadLine();

                rest.Post(new Weapon() { WeaponName = name, Caliber = caliber, Facturer = facturer }, "weapon");
            }
        }

        static void List(string entity)
        {
            if (entity == "Operator")
            {
                List<Operator> operators = rest.Get<Operator>("operator");
                foreach (var item in operators)
                {
                    Console.WriteLine(item.OperatorId + ": " + item.Name);
                }
            }
            else if (entity == "Faction")
            {
                List<Faction> factions = rest.Get<Faction>("faction");
                foreach (var item in factions)
                {
                    Console.WriteLine(item.FactionId + ": " + item.FactionName);
                }
            }
            else if (entity == "Weapon")
            {
                List<Weapon> weapons = rest.Get<Weapon>("weapon");
                foreach (var item in weapons)
                {
                    Console.WriteLine(item.WeaponId + ": " + item.WeaponName);
                }
            }
            Console.ReadLine();
        }

        static void Update(string entity)
        {
            if (entity == "Operator")
            {
                Console.Write("Enter Operator's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Operator one = rest.Get<Operator>(id, "operator");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                Console.Write($"New age [old: {one.Age}]: ");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.Write($"New Height [old: {one.Height}]: ");
                int height = Convert.ToInt32(Console.ReadLine());
                Console.Write($"New FactionId [old: {one.FactionId}]: ");
                int fid = Convert.ToInt32(Console.ReadLine());
                Console.Write($"New WeaponId [old: {one.WeaponId}]: ");
                int wid = Convert.ToInt32(Console.ReadLine());
                one.Name = name;
                one.Age = age;
                one.Height = height;
                one.WeaponId = wid;
                one.FactionId = fid;
                rest.Put(one, "operator");
            }
            else if (entity == "Faction")
            {
                Console.Write("Enter Faction's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Faction one = rest.Get<Faction>(id, "faction");
                Console.Write($"New name [old: {one.FactionName}]: ");
                string name = Console.ReadLine();
                Console.Write($"New age [old: {one.Nation}]: ");
                string nation = Console.ReadLine();

                one.FactionName = name;
                one.Nation = nation;

                rest.Put(one, "faction");
            }
            else if (entity == "Weapon")
            {
                Console.Write("Enter Weapon's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Weapon one = rest.Get<Weapon>(id, "weapon");
                Console.Write($"New name [old: {one.WeaponName}]: ");
                string name = Console.ReadLine();
                Console.Write($"New facturer [old: {one.Facturer}]: ");
                string facturer = Console.ReadLine();
                Console.Write($"New Caliber [old: {one.Caliber}]: ");
                string caliber = Console.ReadLine();
                Console.Write($"New OperatorId [old: {one.OperatorId}]: ");
                int oid = Convert.ToInt32(Console.ReadLine());

                one.WeaponName = name;
                one.Facturer = facturer;
                one.Caliber = caliber;
                one.OperatorId = oid;
                rest.Put(one, "weapon");
            }
        }

        static void Delete(string entity)
        {
            if (entity == "Operator")
            {
                Console.Write("Enter Operator's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "operator");
            }
            else if (entity == "Faction")
            {
                Console.Write("Enter Factions's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "faction");
            }
            else if (entity == "Weapon")
            {
                Console.Write("Enter Weapon's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "weapon");
            }
        }

        static void FactionNameWithOperator(RestService rest)
        {
            var result = rest.Get<KeyValuePair<string, string>>("stat/factionnamewithoperators");
            Console.WriteLine("\nFaction names and operators:\n");
            if (result != null)
            {
                foreach (var item in result)
                {
                    Console.WriteLine("Faction: {0}, Operator: {1}", item.Key, item.Value);
                }
            }
            else { Console.WriteLine("There is no data"); }

            Console.ReadLine();
        }

        static void MaxAgePerFaction(RestService rest)
        {
            var result = rest.Get<KeyValuePair<string, int>>("stat/maxageperfaction");
            Console.WriteLine("\nMaximum age in every faction\n");
            if (result != null)
            {
                foreach (var item in result)
                {
                    Console.WriteLine("Faction: {0}, Age: {1}", item.Key, item.Value);
                }
            }
            else { Console.WriteLine("There is no data"); }

            Console.ReadLine();
        }

        static void MinHeightPerFaction(RestService rest)
        {
            var result = rest.Get<KeyValuePair<string, int>>("stat/minheightperfaction");
            Console.WriteLine("\nMinimum height in every faction\n");
            if (result != null)
            {
                foreach (var item in result)
                {
                    Console.WriteLine("Faction: {0}, Height: {1}", item.Key, item.Value);
                }
            }
            else { Console.WriteLine("There is no data"); }

            Console.ReadLine();
        }

        static void OperatorsPerFaction(RestService rest)
        {
            var result = rest.Get<KeyValuePair<string, int>>("stat/operatorsperfaction");
            Console.WriteLine("\nNumber of operators in every faction\n");
            if (result != null)
            {
                foreach (var item in result)
                {
                    Console.WriteLine("Faction: {0}, Number of operators: {1}", item.Key, item.Value);
                }
            }
            else { Console.WriteLine("There is no data"); }

            Console.ReadLine();
        }

        static void OperatorsPreferredWeapon(RestService rest)
        {
            var result = rest.Get<KeyValuePair<string, string>>("stat/operatorspreferredweapon");
            Console.WriteLine("\nOperators and their preferred weapon\n");
            if (result != null)
            {
                foreach (var item in result)
                {
                    Console.WriteLine("Operator: {0}, Weapon: {1}", item.Key, item.Value);
                }
            }
            else { Console.WriteLine("There is no data"); }

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:55349/", "operator");

            var operatorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Operator"))
                .Add("Create", () => Create("Operator"))
                .Add("Delete", () => Delete("Operator"))
                .Add("Update", () => Update("Operator"))
                .Add("Exit", ConsoleMenu.Close);

            var factionSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Faction"))
                .Add("Create", () => Create("Faction"))
                .Add("Delete", () => Delete("Faction"))
                .Add("Update", () => Update("Faction"))
                .Add("Exit", ConsoleMenu.Close);

            var weaponSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Weapon"))
                .Add("Create", () => Create("Weapon"))
                .Add("Delete", () => Delete("Weapon"))
                .Add("Update", () => Update("Weapon"))
                .Add("Exit", ConsoleMenu.Close);

            var nonCRUDSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Faction name with operator", () => FactionNameWithOperator(rest))
                .Add("Maximum age per faction", () => MaxAgePerFaction(rest))
                .Add("Minimum height per faction", () => MinHeightPerFaction(rest))
                .Add("Operators per faction", () => OperatorsPerFaction(rest))
                .Add("Operators preferred weapon", () => OperatorsPreferredWeapon(rest))
                .Add("Exit", ConsoleMenu.Close); 

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Operators", () => operatorSubMenu.Show())
                .Add("Factions", () => factionSubMenu.Show())
                .Add("Weapons", () => weaponSubMenu.Show())
                .Add("non-CRUD", () => nonCRUDSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}

