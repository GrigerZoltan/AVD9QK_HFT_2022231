using AVD9QK_HFT_2022231.Logic;
using AVD9QK_HFT_2022231.Models;
using AVD9QK_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Test
{
    [TestFixture]
    public class OperatorLogicTester
    {
        OperatorLogic logic;

        Mock<IRepository<Operator>> mockOpRepo;

        Mock<IRepository<Faction>> mockFactionRepo;

        Mock<IRepository<Weapon>> mockWeaponRepo;

        [SetUp]
        public void Init()
        {
            mockOpRepo = new Mock<IRepository<Operator>>();

            mockOpRepo.Setup(m => m.ReadAll()).Returns(new List<Operator>()
            {
                new Operator("1#Seamus Cowden#40#193#1#1"),
                new Operator("2#Mike Baker#54#180#1#2"),
                new Operator("3#James Porter#39#178#1#3"),
                new Operator("4#Jordan Trace#35#179#3#4"),
                new Operator("5#Miles Campbell#41#185#3#5"),
                new Operator("6#Shuhrat Kessikbayev#39#180#2#6"),
                new Operator("7#Emmanuelle Pichon#33#168#4#7"),
                new Operator("8#Gustave Kateb#44#177#4#8"),
                new Operator("9#Dominic Brunsmeier#33#180#5#9"),
                new Operator("10#Sebastien Cote#37#178#6#10")

            }.AsQueryable());

            mockFactionRepo = new Mock<IRepository<Faction>>();

            mockFactionRepo.Setup(m => m.ReadAll()).Returns(new List<Faction>()
            {
                new Faction("1#Special Air Services#United Kingdom"),
                new Faction("2#Spetsnaz GRU#Russian Federation"),
                new Faction("3#Federal Bureau of Investigation#United States of America"),
                new Faction("4#National Gendarmerie Intervention Group#France"),
                new Faction("5#Border Protection Group 9#Germany"),
                new Faction("6#Joint Task Force 2#Canada")
            }.AsQueryable());

            mockWeaponRepo = new Mock<IRepository<Weapon>>();

            mockWeaponRepo.Setup(m => m.ReadAll()).Returns(new List<Weapon>()
            {
                new Weapon("1#L85A2#5.56 NATO#RSAF Enfield#1"),
                new Weapon("2#HK33#5.56 NATO#Heckler and Koch#2"),
                new Weapon("3#ARES FMG#9mm Parabellum#ARES#3"),
                new Weapon("4#M1014#12-Gauge#Benelli Armi SPA#4"),
                new Weapon("5#UMP45#.45 ACP#Heckler and Koch#5"),
                new Weapon("6#AK-12#7.62#Kalashnikov Concern#6"),
                new Weapon("7#FAMAS F1#5.56 NATO#GIAT Industries#7"),
                new Weapon("8#MP5#9mm Parabellum#Heckler and Koch#8"),
                new Weapon("9#MP7#4.6x30mm#Heckler and Koch#9"),
                new Weapon("10#C7#5.56 NATO#Colt Canada#10"),
            }.AsQueryable());

            logic = new OperatorLogic(mockOpRepo.Object, mockFactionRepo.Object, mockWeaponRepo.Object);
        }

        [Test]
        public void OperatorsPreferredWeaponTester()
        {
            var result = logic.OperatorsPreferredWeapon();
            Assert.That(result, Is.EqualTo(new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Seamus Cowden","L85A2"),
                    new KeyValuePair<string, string>("Mike Baker","HK33"),
                    new KeyValuePair<string, string>("James Porter","ARES FMG"),
                    new KeyValuePair<string, string>("Jordan Trace","M1014"),
                    new KeyValuePair<string, string>("Miles Campbell","UMP45"),
                    new KeyValuePair<string, string>("Shuhrat Kessikbayev","AK-12"),
                    new KeyValuePair<string, string>("Emmanuelle Pichon","FAMAS F1"),
                    new KeyValuePair<string, string>("Gustave Kateb","MP5"),
                    new KeyValuePair<string, string>("Dominic Brunsmeier","MP7"),
                    new KeyValuePair<string, string>("Sebastien Cote","C7"),
                }
            ));
        }

        [Test]
        public void FactionNameWithOperatorTester()
        {
            var result = logic.FactionNamewithOperator();
            Assert.That(result, Is.EqualTo(new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Special Air Services","Seamus Cowden"),
                    new KeyValuePair<string, string>("Special Air Services","Mike Baker"),
                    new KeyValuePair<string, string>("Special Air Services","James Porter"),
                    new KeyValuePair<string, string>("Spetsnaz GRU","Shuhrat Kessikbayev"),
                    new KeyValuePair<string, string>("Federal Bureau of Investigation","Jordan Trace"),
                    new KeyValuePair<string, string>("Federal Bureau of Investigation","Miles Campbell"),
                    new KeyValuePair<string, string>("National Gendarmerie Intervention Group","Emmanuelle Pichon"),
                    new KeyValuePair<string, string>("National Gendarmerie Intervention Group","Gustave Kateb"),
                    new KeyValuePair<string, string>("Border Protection Group 9","Dominic Brunsmeier"),
                    new KeyValuePair<string, string>("Joint Task Force 2","Sebastien Cote"),
                }
            ));
        }

        [Test]
        public void MaxAgePerFactionTester()
        {
            var result = logic.MaxAgePerFaction();
            Assert.That(result, Is.EqualTo(new List<KeyValuePair<string, int>>()
                {
                   new KeyValuePair<string, int>("Border Protection Group 9",33),
                   new KeyValuePair<string, int>("Federal Bureau of Investigation",41),
                   new KeyValuePair<string, int>("Joint Task Force 2",37),
                   new KeyValuePair<string, int>("National Gendarmerie Intervention Group",44),
                   new KeyValuePair<string, int>("Special Air Services",54),
                   new KeyValuePair<string, int>("Spetsnaz GRU",39),
                }
            ));
        }

        [Test]
        public void MinHeightPerFactionTester()
        {
            var result = logic.MinHeightPerFaction();
            Assert.That(result, Is.EqualTo(new List<KeyValuePair<string, int>>()
                {
                   new KeyValuePair<string, int>("Border Protection Group 9",180),
                   new KeyValuePair<string, int>("Federal Bureau of Investigation",179),
                   new KeyValuePair<string, int>("Joint Task Force 2",178),
                   new KeyValuePair<string, int>("National Gendarmerie Intervention Group",168),
                   new KeyValuePair<string, int>("Special Air Services",178),
                   new KeyValuePair<string, int>("Spetsnaz GRU",180),
                }
            ));
        }

        [Test]
        public void OperatorsPerFactionTester()
        {
            var result = logic.OperatorsPerFaction();
            Assert.That(result, Is.EqualTo(new List<KeyValuePair<string, int>>()
                {
                   new KeyValuePair<string, int>("Border Protection Group 9",1),
                   new KeyValuePair<string, int>("Federal Bureau of Investigation",2),
                   new KeyValuePair<string, int>("Joint Task Force 2",1),
                   new KeyValuePair<string, int>("National Gendarmerie Intervention Group",2),
                   new KeyValuePair<string, int>("Special Air Services",3),
                   new KeyValuePair<string, int>("Spetsnaz GRU",1),
                }
            ));
        }
    }
}
