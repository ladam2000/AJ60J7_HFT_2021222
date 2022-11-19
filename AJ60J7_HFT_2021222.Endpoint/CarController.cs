using AJ60J7_HFT_2021222.Endpoint.Services;
using AJ60J7_HFT_2021222.Logic;
using AJ60J7_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
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
        IHubContext<SignalRHub> hub;
        public CarController(ICarLogic cl, IHubContext<SignalRHub> hub)
        {
            this.logic = cl;
            this.hub = hub; 
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
            this.hub.Clients.All.SendAsync("CarCreated", value);
        }
        [HttpPut]
        public void EditOne([FromBody] Car value)
        {
            logic.Update(value);
            this.hub.Clients.All.SendAsync("CarUpdated", value);
        }

        [HttpDelete("{id}")]
        public void DeleteOne([FromRoute] int carId)
        {
            var carToDelete = this.logic.ReadOne(carId);
            logic.Delete(carId);
            this.hub.Clients.All.SendAsync("CarDeleted", carToDelete);
        }
    }
}
