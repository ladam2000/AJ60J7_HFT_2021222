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
        public class BrandController : ControllerBase
        {

            IBrandLogic logic;
            public BrandController(IBrandLogic lc)
            {
                this.logic = lc;
            }
            // GET: api/<BrandController>
            [HttpGet]
            public IEnumerable<Brand> GetAll()
            {
                return logic.ReadAll();
            }

            [HttpGet("{id}")]
            public Brand GetOne(int id)
            {
                return logic.ReadOne(id);
            }

            // POST api/<BrandController>
            [HttpPost]
            public void AddOne([FromBody] Brand value)
            {
                logic.Create(value);
            }

            // PUT api/<BrandController>/5
            [HttpPut]
            public void UpdateOne([FromBody] Brand value)
            {
                logic.Update(value);
            }

            // DELETE api/<BrandController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
                logic.Delete(id);
            }
        }
    
}
