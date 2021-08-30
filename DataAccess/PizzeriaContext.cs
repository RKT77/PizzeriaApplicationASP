using DataAccess.Configurations;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class PizzeriaContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PizzeriaHall> PizzeriaHalls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Attendant> Attendants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=PC;Initial Catalog=TestDB;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PizzeriaHallConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());
            modelBuilder.ApplyConfiguration(new AttendantConfiguration());

            modelBuilder.Entity<ItemType>().HasData(new ItemType
            {
                Id=1,
                Name="Pica"
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "Menadzer"
            });
            modelBuilder.Entity<PizzeriaHall>().HasData(new PizzeriaHall
            {
                Id=1,
                Name="Basta"
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 1,
                IdItemType = 1,
                Name = "Cezar",
                Price = 500
            });
            modelBuilder.Entity<Table>().HasData(new Table
            {
                Id = 1,
                Name = "Sto1",
                IdPizzeriaHall = 1
            });
            modelBuilder.Entity<Attendant>().HasData(new Attendant
            {
                Id = 1,
                IdRole = 1,
                Email = "petar_petrovic@gmail.com",
                FirstName = "Petar",
                LastName = "Petrovic",
                Password = "attendant01!"
            });
        }
    }
}
