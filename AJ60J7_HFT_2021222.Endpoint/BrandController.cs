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
    public class BrandController : ControllerBase
    {

        IBrandLogic logic;

        IHubContext<SignalRHub> hub;
        public BrandController(IBrandLogic lc, IHubContext<SignalRHub> hub)
        {
            this.logic = lc;
            this.hub = hub; 
        }
      
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

        // POST BrandController
        [HttpPost]
        public void AddOne([FromBody] Brand value)
        {
            logic.Create(value);
            this.hub.Clients.All.SendAsync("BrandCreated", value);
        }

     
        [HttpPut]
        public void UpdateOne([FromBody] Brand value)
        {
            logic.Update(value);
            this.hub.Clients.All.SendAsync("BrandUpdated", value);
        }

        // DELETE BrandController
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var brandToDelete = this.logic.ReadOne(id);
            logic.Delete(id);
            this.hub.Clients.All.SendAsync("BrandDeleted", brandToDelete);
        }
    }
}
