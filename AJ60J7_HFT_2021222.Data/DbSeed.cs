using AJ60J7_HFT_2021222.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aj60J7_HFT_2021222.Data
{
    public class DbSeed : DbContext
    {

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Engine> Engines { get; set; }

        public DbSeed()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); ;
            }
        }
        public DbSeed(DbContextOptions<DbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf; Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Brand bmw = new Brand() { Id = 1, Name = "BMW" };
            Brand citroen = new Brand() { Id = 2, Name = "Citroen" };
            Brand audi = new Brand() { Id = 3, Name = "Audi" };

            Car bmw1 = new Car() { Id = 1, BrandId = bmw.Id, BasePrice = 20000, Model = "BMW 116d", }; //engineID???
            Car bmw2 = new Car() { Id = 2, BrandId = bmw.Id, BasePrice = 30000, Model = "BMW 510" };
            Car citroen1 = new Car() { Id = 3, BrandId = citroen.Id, BasePrice = 10000, Model = "Citroen C1" };
            Car citroen2 = new Car() { Id = 4, BrandId = citroen.Id, BasePrice = 15000, Model = "Citroen C3" };
            Car audi1 = new Car() { Id = 5, BrandId = audi.Id, BasePrice = 20000, Model = "Audi A3" };
            Car audi2 = new Car() { Id = 6, BrandId = audi.Id, BasePrice = 25000, Model = "Audi A4" };

            Engine diesel1 = new Engine() { Id = 1, CarId = bmw1.Id, Type = "Diesel", Horsepower = 200 };
            Engine petrol1 = new Engine() { Id = 2, CarId = citroen1.Id, Type = "Petrol", Horsepower = 190 };
            Engine petrol2 = new Engine() { Id = 3, CarId = audi1.Id, Type = "Petrol", Horsepower = 220 };
            Engine petrol3 = new Engine() { Id = 4, CarId = citroen2.Id, Type = "Petrol", Horsepower = 200 };
            Engine hybrid1 = new Engine() { Id = 5, CarId = audi2.Id, Type = "Hybrid", Horsepower = 400 };
            Engine electric1 = new Engine() { Id = 6, CarId = bmw2.Id, Type = "Electric", Horsepower = 136 };

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasOne(car => car.Brand)
                    .WithMany(brand => brand.Cars)
                    .HasForeignKey(car => car.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Engine>(entity =>
            {
                entity.HasOne(engine => engine.Car)
                    .WithOne(car => car.Engine)
                    .HasForeignKey<Engine>(engine => engine.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Brand>().HasData(bmw, citroen, audi);
            modelBuilder.Entity<Car>().HasData(bmw1, bmw2, citroen1, citroen2, audi1, audi2);
            modelBuilder.Entity<Engine>().HasData(diesel1, petrol1, petrol2, petrol3, hybrid1, electric1);
        }

    }
}
