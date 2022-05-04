using AJ60J7_HFT_2021222.Logic;
using AJ60J7_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Endpoint
{
    [Route("[controller]")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        IEngineLogic logic;

        public EngineController(IEngineLogic lc)
        {
            this.logic = lc;
        }
        [HttpGet]
        public IEnumerable<Engine> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Engine GetOne(int id)
        {
            return logic.ReadOne(id);
        }


        [HttpPost]
        public void CreateOne([FromBody] Engine value)
        {
            logic.Create(value);
        }

        [HttpPut]
        public void UpdateOne([FromBody] Engine value)
        {
            logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void DeleteOne(int id)
        {
            logic.Delete(id);
        }
    }
}
