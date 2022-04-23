using AJ60J7_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Logic
{
    public class CarLogic : ICarLogic
    {
        public IEnumerable<KeyValuePair<string, double>> AveragePBB()
        {
            throw new NotImplementedException();
        }

        public double AveragePrice()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, double>> AveragePriceByEngineTypes()
        {
            throw new NotImplementedException();
        }

        public void Create(Car car)
        {
            throw new NotImplementedException();
        }

        public void Delete(int carId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, double>> EngineTypeUsage()
        {
            throw new NotImplementedException();
        }

        public double HighestPrice()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Car ReadOne(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
