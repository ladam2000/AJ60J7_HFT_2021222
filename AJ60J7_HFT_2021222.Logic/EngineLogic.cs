using AJ60J7_HFT_2021222.Models;
using AJ60J7_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Logic
{
    public class EngineLogic : IEngineLogic
    {
        IDefaultRepository<Car> cRepo;
        IDefaultRepository<Engine> eRepo;

        public EngineLogic(IDefaultRepository<Engine> eRepo,
            IDefaultRepository<Car> cRepo)
        {
            this.eRepo = eRepo;
            this.cRepo = cRepo;

        }

        public IEnumerable<KeyValuePair<string, int?>> DieselCostHigherThan4k()
        {
            return from c in cRepo.ReadAll().ToList()
                   join e in eRepo.ReadAll().ToList()
                   on c.Engine.Id equals e.Id
                   where c.BasePrice >= 3500 && e.Type == "Diesel"
                   select new KeyValuePair<string, int?>
                   (e.Type, c.BasePrice);

        }

        public void Create(Engine engine)
        {
            long number = 0;
            bool isNumber = long.TryParse(engine.Type, out number);
            if (isNumber)
                throw new Exception("The type cannot be just numbers!");
            eRepo.Create(engine);
        }

        public IEnumerable<Engine> ReadAll()
        {
            return eRepo.ReadAll();
        }

        public Engine ReadOne(int id)
        {
            return eRepo.ReadOne(id);
        }
        public void Update(Engine engine)
        {
            eRepo.Update(engine);
        }

        public void Delete(int engineId)
        {
            eRepo.Delete(engineId);
        }
    }
}

