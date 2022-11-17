using AJ60J7_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Repository
{
    public class CarRepository : IDefaultRepository<Car>
    {
        CarShopContext context;
        public CarRepository(CarShopContext ctx)
        {
            context = ctx;
        }
        

        public Car ReadOne(int id)
        {
            ;
            return context
                .Cars
                .FirstOrDefault(c => c.Id == id);
        }
        public IQueryable<Car> ReadAll()
        {
            return context.Cars;
        }

        public void Update(Car car)
        {
            Car old = ReadOne(car.Id);
            
            old.BasePrice = car.BasePrice;
            old.BrandId = car.BrandId;
            old.Model = car.Model;
            context.SaveChanges();
        }
        public void Create(Car car)
        {
            context.Cars.Add(car);
            context.SaveChanges();
        }
        public void Delete(int carId)
        {
            
            context.Cars.Remove(ReadOne(carId));
            context.SaveChanges();
        }
    }
}
