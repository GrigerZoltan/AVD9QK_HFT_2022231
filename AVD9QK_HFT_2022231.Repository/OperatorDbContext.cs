using AVD9QK_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVD9QK_HFT_2022231.Repository
{
    public partial class OperatorDbContext : DbContext
    {
        public virtual DbSet<Faction> Factions { get; set; }

        public virtual DbSet<Operator> Operators { get; set; }

        public virtual DbSet<Weapon> Weapons { get; set; }

        public OperatorDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("operatordb").UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operator>(entity =>
            {
                entity.HasOne(Operator => Operator.Faction)
                .WithMany(Faction => Faction.Operators)
                .HasForeignKey(Operator => Operator.FactionId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.HasOne(Weapon => Weapon.Operator)
                  .WithOne(Operator => Operator.Weapon)
                  .HasForeignKey<Weapon>(Weapon => Weapon.OperatorId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            //id,name,age,height,factionid,weaponid
            modelBuilder.Entity<Operator>().HasData(new Operator[]
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

            });

            //Id,name,country

            modelBuilder.Entity<Faction>().HasData(new Faction[]
            {
                new Faction("1#Special Air Services#United Kingdom"),
                new Faction("2#Spetsnaz GRU#Russian Federation"),
                new Faction("3#Federal Bureau of Investigation#United States of America"),
                new Faction("4#National Gendarmerie Intervention Group#France"),
                new Faction("5#Border Protection Group 9#Germany"),
                new Faction("6#Joint Task Force 2#Canada")
            });

            //WeaponId,Name,calibre,facturer,operatorid

            modelBuilder.Entity<Weapon>().HasData(new Weapon[]
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
            });
        }
    }
}
