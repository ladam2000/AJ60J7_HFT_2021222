using AJ60J7_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Logic
{
    public interface IEngineLogic
    {
        IEnumerable<Engine> ReadAll();
        IEnumerable<KeyValuePair<string, int?>> DieselCostHigherThan4k();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        void Create(Engine engine);
        void Delete(int engineId);
        
        Engine ReadOne(int id);

        void Update(Engine engine);
    }
}
