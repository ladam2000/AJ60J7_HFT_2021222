using AJ60J7_HFT_2021222.Models;
using AJ60J7_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Logic
{
    public class BrandLogic : IBrandLogic
    {
        IDefaultRepository<Car> cRepo;
        IDefaultRepository<Brand> bRepo;
        public BrandLogic(IDefaultRepository<Brand> bRepo,
            IDefaultRepository<Car> cRepo)
        {
            this.bRepo = bRepo;
            this.cRepo = cRepo;
        }

        public IEnumerable<KeyValuePair<string, double>> BrandPopularity()
        {
            return from c in cRepo.ReadAll().ToList()
                   join b in bRepo.ReadAll().ToList()
                   on c.Brand.Id equals b.Id
                   group c by b.Name into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Count());
        }

        public void Create(Brand brand)
        {
            long number = 0;
            bool isNumber = long.TryParse(brand.Name, out number);
            if (isNumber)
                throw new Exception("The brand name cannot be just numbers!");
            bRepo.Create(brand);
        }

        public IEnumerable<Brand> ReadAll()
        {
            return bRepo.ReadAll();
        }

        public Brand ReadOne(int id)
        {
            return bRepo.ReadOne(id);
        }

        public void Update(Brand brand)
        {
            bRepo.Update(brand);
        }

        public void Delete(int brandId)
        {
            bRepo.Delete(brandId);
        }
    }
}
