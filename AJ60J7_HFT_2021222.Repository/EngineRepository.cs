using AJ60J7_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Repository
{
    public class EngineRepository : IDefaultRepository<Engine>
    {
        CarShopContext context;
        public EngineRepository(CarShopContext ctx)
        {
            context = ctx;
        }
        public void Create(Engine engine)
        {
            context.Add(engine);
            context.SaveChanges();
        }
        public Engine ReadOne(int id)
        {
            return context
                .Engines
                .FirstOrDefault(e => e.Id == id);
        }
        public IQueryable<Engine> ReadAll()
        {
            return context.Engines;
        }
        public void Update(Engine engine)
        {
            Engine old = ReadOne(engine.Id); 
            old.Type = engine.Type;
            old.Horsepower = engine.Horsepower;
            old.CarId = engine.CarId;
            context.SaveChanges();
        }
        public void Delete(int engineId)
        {
            context.Engines.Remove(ReadOne(engineId));
            context.SaveChanges();
        }
    }
}
