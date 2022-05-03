using AJ60J7_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Logic
{
    public interface ICarLogic
    {
        double HighestPrice();
        double AveragePrice();

        IEnumerable<KeyValuePair<string, double>> EngineTypeUsage();
        IEnumerable<KeyValuePair<string, double>> AveragePBB();
        IEnumerable<KeyValuePair<string, double>> AveragePriceByEngineTypes();

        void Update(Car car);
        void Create(Car car);
        void Delete(int carId);

        
        IEnumerable<Car> ReadAll();

        Car ReadOne(int id);
        
    }
}
