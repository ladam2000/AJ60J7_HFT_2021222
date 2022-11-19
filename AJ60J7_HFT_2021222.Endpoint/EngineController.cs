using AJ60J7_HFT_2021222.Endpoint.Services;
using AJ60J7_HFT_2021222.Logic;
using AJ60J7_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        IHubContext<SignalRHub> hub;
        public EngineController(IEngineLogic lc, IHubContext<SignalRHub> hub)
        {
            this.logic = lc;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("EngineCreated", value);
        }

        [HttpPut]
        public void UpdateOne([FromBody] Engine value)
        {
            logic.Update(value);
            this.hub.Clients.All.SendAsync("EngineUpdated", value);
        }

        [HttpDelete("{id}")]
        public void DeleteOne(int id)
        {
            var engineToDelete = this.logic.ReadOne(id);
            logic.Delete(id);
            this.hub.Clients.All.SendAsync("EngineDeleted", engineToDelete);
        }
    }
}
