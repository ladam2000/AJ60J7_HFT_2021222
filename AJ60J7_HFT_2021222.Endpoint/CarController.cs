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
    public class CarController : ControllerBase
    {
        ICarLogic logic;
        public CarController(ICarLogic cl)
        {
            this.logic = cl;
        }

        [HttpGet]
        public IEnumerable<Car> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Car GetOne(int id)
        {
            return logic.ReadOne(id);
        }

        [HttpPost]
        public void AddOne([FromBody] Car value)
        {
            logic.Create(value);
        }
        [HttpPut]
        public void EditOne([FromBody] Car value)
        {
            logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void DeleteOne([FromRoute] int carId)
        {
            logic.Delete(carId);
        }
    }
}
