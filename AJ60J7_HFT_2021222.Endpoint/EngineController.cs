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

        // GET: api/<EngineController>
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

        // POST api/<EngineController>
        [HttpPost]
        public void CreateOne([FromBody] Engine value)
        {
            logic.Create(value);
        }

        // PUT api/<EngineController>/5
        [HttpPut]
        public void UpdateOne([FromBody] Engine value)
        {
            logic.Update(value);
        }

        // DELETE api/<EngineController>/5
        [HttpDelete("{id}")]
        public void DeleteOne(int id)
        {
            logic.Delete(id);
        }
    }
}
