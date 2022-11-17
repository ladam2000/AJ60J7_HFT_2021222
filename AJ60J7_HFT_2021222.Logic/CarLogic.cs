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
        IDefaultRepository<Car> C_rep;

        IDefaultRepository<Engine> E_rep;

        IDefaultRepository<Brand> B_rep;
        
        public CarLogic(IDefaultRepository<Car> cRep,IDefaultRepository<Brand> bRep,IDefaultRepository<Engine> eRep)
        {
            this.C_rep = cRep;
            this.B_rep = bRep;
            this.E_rep = eRep;
        }
        public double AveragePrice()
        {
            return C_rep
                .ReadAll()
                .Average(c => c.BasePrice) ?? 0;
        }

        public double HighestPrice()
        {
            return C_rep
                .ReadAll()
                .Max(c => c.BasePrice) ?? 0;
        }

        #region non-CRUDs
        public IEnumerable<KeyValuePair<string, double>> AveragePBB()
        {
            return from c in C_rep.ReadAll().ToList()
                   join b in B_rep.ReadAll().ToList()
                   on c.Brand.Id equals b.Id
                   group c by b.Name into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(car => car.BasePrice) ?? 0);
        }
        public IEnumerable<KeyValuePair<string, double>> AveragePriceByEngineTypes()
        {
            return from c in C_rep.ReadAll().ToList()
                   join e in E_rep.ReadAll().ToList()
                   on c.Engine.Id equals e.Id
                   group c by e.Type into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(car => car.BasePrice) ?? 0);
        }

        public IEnumerable<KeyValuePair<string, double>> EngineTypeUsage()
        {
            return from c in C_rep.ReadAll().ToList()
                   join e in E_rep.ReadAll().ToList()
                   on c.Engine.Id equals e.Id
                   group c by e.Type into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Count());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="car"></param>
        #endregion
        public void Create(Car car)
        {
            if (car.BasePrice < 0)
                throw new ArgumentException("The car cannot have a negative price!");
            C_rep.Create(car);
        }
        public Car ReadOne(int id)
        {
            return C_rep.ReadOne(id);
        }

        public IEnumerable<Car> ReadAll()
        {
            return C_rep.ReadAll();
        }

        

        public void Update(Car car)
        {
            C_rep.Update(car);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="carId"></param>
        public void Delete(int carId)
        {

            C_rep.Delete(carId);
        }
    }
}
