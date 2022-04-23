using AJ60J7_HFT_2021222.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aj60J7_HFT_2021222.Data
{
    public class CarShopContext : DbContext
    {

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Engine> Engines { get; set; }

        public CarShopContext()
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
        public CarShopContext(DbContextOptions<DbContext> options)
            : base(options)
        {

        }

        string coonStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(coonStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Brands
            Brand tesla = new Brand() { Id = 1, Name = "Tesla" };
            Brand volkswagen = new Brand() { Id = 2, Name = "Volkswagen" };
            Brand audi = new Brand() { Id = 3, Name = "Audi" };
            
            //Cars
            Car tesla1 = new Car() { Id = 1, BrandId = tesla.Id, BasePrice = 48190, Model = "Tesla Model 3", }; 
            Car tesla2 = new Car() { Id = 2, BrandId = tesla.Id, BasePrice = 116440, Model = "Tesla Model X" };

            Car volkswagen1 = new Car() { Id = 3, BrandId = volkswagen.Id, BasePrice = 15000, Model = "Volkswagen Up" };
            Car volkswagen2 = new Car() { Id = 4, BrandId = volkswagen.Id, BasePrice = 35000, Model = "volkswagen ID. 4" };

            Car audi1 = new Car() { Id = 5, BrandId = audi.Id, BasePrice = 117595, Model = "Audi RS6" };
            Car audi2 = new Car() { Id = 6, BrandId = audi.Id, BasePrice = 25000, Model = "Audi A6" };

            //Engine 
            Engine petrol1 = new Engine() { Id = 3, CarId = volkswagen1.Id, Type = "Petrol", Horsepower = 90 };
            Engine petrol2 = new Engine() { Id = 5, CarId = audi1.Id, Type = "Petrol", Horsepower = 450 };
            

            Engine diesel1 = new Engine() { Id = 6, CarId = audi2.Id, Type = "Disel", Horsepower = 204 };

            Engine electric1 = new Engine() { Id = 1, CarId = tesla1.Id, Type = "Electric", Horsepower = 221 };
            Engine electric2 = new Engine() { Id = 2, CarId = tesla2.Id, Type = "Electric", Horsepower = 670 };
            Engine electric3 = new Engine() { Id = 4, CarId = volkswagen2.Id, Type = "Electric", Horsepower = 201 };

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

            modelBuilder.Entity<Brand>().HasData(tesla, volkswagen, audi);
            modelBuilder.Entity<Car>().HasData(tesla1, tesla2, volkswagen1, volkswagen2, audi1, audi2);
            modelBuilder.Entity<Engine>().HasData(electric2, petrol1, petrol2, electric3, electric2, electric1);
        }

    }
}
