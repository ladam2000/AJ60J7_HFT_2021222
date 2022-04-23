using AJ60J7_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Logic
{
    public interface IBrandLogic
    {
        IEnumerable<KeyValuePair<string, double>> BrandPopularity();
        void Create(Brand brand);
        void Delete(int brandId);
        IEnumerable<Brand> ReadAll();
        Brand ReadOne(int id);
        void Update(Brand brand);
    }
}
