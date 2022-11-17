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
        IDefaultRepository<Car> C_rep;
        IDefaultRepository<Engine> E_rep;

        public EngineLogic(IDefaultRepository<Engine> eRepo,IDefaultRepository<Car> cRepo)
        {
            this.E_rep = eRepo;
            this.C_rep = cRepo;

        }

        public IEnumerable<KeyValuePair<string, int?>> DieselCostHigherThan4k()
        {
            return from c in C_rep.ReadAll().ToList()
                   join e in E_rep.ReadAll().ToList()
                   on c.Engine.Id equals e.Id
                   where c.BasePrice >= 3500 && e.Type == "Diesel"
                   select new KeyValuePair<string, int?>
                   (e.Type, c.BasePrice);

        }
        public IEnumerable<Engine> ReadAll()
        {
            return E_rep.ReadAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public void Create(Engine engine)
        {
            long number = 0;
            bool isNumber = long.TryParse(engine.Type, out number);
            if (isNumber)
                throw new Exception("Type can't be just a number!");
            E_rep.Create(engine);
        }
        public void Update(Engine engine)
        {
            E_rep.Update(engine);
        }

        public Engine ReadOne(int id)
        {
            return E_rep.ReadOne(id);
        }
        
        public void Delete(int engineId)
        {
            ;
            E_rep.Delete(engineId);
        }
    }
}

