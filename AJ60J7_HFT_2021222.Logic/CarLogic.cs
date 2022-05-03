using AJ60J7_HFT_2021222.Models;
using AJ60J7_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Logic
{
    public class CarLogic : ICarLogic
    {
        IDefaultRepository<Car> cRepo;
        IDefaultRepository<Brand> bRepo;
        IDefaultRepository<Engine> eRepo;
        public CarLogic(IDefaultRepository<Car> cRepo,
            IDefaultRepository<Brand> bRepo,
            IDefaultRepository<Engine> eRepo)
        {
            this.cRepo = cRepo;
            this.bRepo = bRepo;
            this.eRepo = eRepo;
        }
        public double AveragePrice()
        {
            return cRepo
                .ReadAll()
                .Average(c => c.BasePrice) ?? 0;
        }

        public double HighestPrice()
        {
            return cRepo
                .ReadAll()
                .Max(c => c.BasePrice) ?? 0;
        }

        #region non-CRUDs
        public IEnumerable<KeyValuePair<string, double>> AveragePBB()
        {
            return from c in cRepo.ReadAll().ToList()
                   join b in bRepo.ReadAll().ToList()
                   on c.Brand.Id equals b.Id
                   group c by b.Name into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(car => car.BasePrice) ?? 0);
        }
        public IEnumerable<KeyValuePair<string, double>> AveragePriceByEngineTypes()
        {
            return from c in cRepo.ReadAll().ToList()
                   join e in eRepo.ReadAll().ToList()
                   on c.Engine.Id equals e.Id
                   group c by e.Type into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(car => car.BasePrice) ?? 0);
        }

        public IEnumerable<KeyValuePair<string, double>> EngineTypeUsage()
        {
            return from c in cRepo.ReadAll().ToList()
                   join e in eRepo.ReadAll().ToList()
                   on c.Engine.Id equals e.Id
                   group c by e.Type into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Count());

        }
        #endregion
        public void Create(Car car)
        {
            if (car.BasePrice < 0)
                throw new ArgumentException("The car can't have a negative price!");
            cRepo.Create(car);
        }

        public IEnumerable<Car> ReadAll()
        {
            return cRepo.ReadAll();
        }

        public Car ReadOne(int id)
        {
            return cRepo.ReadOne(id);
        }

        public void Update(Car car)
        {
            cRepo.Update(car);
        }

        public void Delete(int carId)
        {
            cRepo.Delete(carId);
        }
    }
}
