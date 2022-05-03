using AJ60J7_HFT_2021222.Logic;
using AJ60J7_HFT_2021222.Models;
using AJ60J7_HFT_2021222.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJ60J7_HFT_2021222.Test
{
    class Tester
    {
        ICarLogic carLogic;
        IBrandLogic brandLogic;
        IEngineLogic engineLogic;
        public Tester()
        {
            var mockCarRepo = new Mock<IDefaultRepository<Car>>();
            var mockBrandRepo = new Mock<IDefaultRepository<Brand>>();
            var mockEngineRepo = new Mock<IDefaultRepository<Engine>>();

            Brand brand1 = new Brand() { Id = 1, Name = "BMW" };
            Brand brand2 = new Brand() { Id = 2, Name = "Audi" };

            Engine engine1 = new Engine() { Id = 1, Type = "Diesel" };
            Engine engine2 = new Engine() { Id = 2, Type = "Diesel" };

            Engine engine3 = new Engine() { Id = 3, Type = "Petrol" };
            Engine engine4 = new Engine() { Id = 4, Type = "Petrol" };

            Engine engine5 = new Engine() { Id = 5, Type = "Electric" };
            Engine engine6 = new Engine() { Id = 6, Type = "Hybrid" };


            mockCarRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Car>
                {
                    new Car() {
                        BasePrice = 3000,
                        Brand = brand1,
                        Id = 1,
                        Engine = engine1
                    },
                    new Car() {
                        BasePrice = 4000,
                        Brand = brand1,
                        Id = 2,
                        Engine = engine2
                    },
                    new Car() {
                        BasePrice = 7000,
                        Brand = brand2,
                        Id = 3,
                        Engine = engine3
                    },
                    new Car() {
                        BasePrice = 4000,
                        Brand = brand2,
                        Id = 4,
                        Engine = engine4
                    },
                    new Car() {
                        BasePrice = 4000,
                        Brand = brand2,
                        Id = 5,
                        Engine = engine5
                    },
                    new Car() {
                        BasePrice = 5000,
                        Brand = brand2,
                        Id = 6,
                        Engine = engine6
                    },
                }.AsQueryable());

            mockBrandRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Brand>
                {brand1, brand2 }
                .AsQueryable());

            mockEngineRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Engine>
                {engine1, engine2, engine3, engine4, engine5, engine6 }
                .AsQueryable());

            carLogic = new CarLogic(mockCarRepo.Object, mockBrandRepo.Object, mockEngineRepo.Object);
            brandLogic = new BrandLogic(mockBrandRepo.Object, mockCarRepo.Object);
            engineLogic = new EngineLogic(mockEngineRepo.Object, mockCarRepo.Object);
        }
        #region non-CRUD tests
        [Test]
        public void Engine_Type_Usage_Test1()
        {
            var result = carLogic.EngineTypeUsage().ToArray();

            Assert.That(result[0], Is.EqualTo(
                new KeyValuePair<string, double>("Diesel", 2)));
            Assert.That(result[1], Is.EqualTo(
                new KeyValuePair<string, double>("Petrol", 2)));
            Assert.That(result[2], Is.EqualTo(
                new KeyValuePair<string, double>("Electric", 1)));
            Assert.That(result[3], Is.EqualTo(
                new KeyValuePair<string, double>("Hybrid", 1)));

        }
        
        [Test]
        public void Avg_PBET_Test2()
        {
            var avg = carLogic.AveragePriceByEngineTypes().ToArray();
            Assert.That(avg[0], Is.EqualTo(
                new KeyValuePair<string, double>("Diesel", 3500)));
            Assert.That(avg[1], Is.EqualTo(
                new KeyValuePair<string, double>("Petrol", 5500)));
            Assert.That(avg[2], Is.EqualTo(
                new KeyValuePair<string, double>("Electric", 4000)));
            Assert.That(avg[3], Is.EqualTo(
                new KeyValuePair<string, double>("Hybrid", 5000)));
        }
        
        [Test]
        public void Brand_Pop_Test3()
        {
            var result = brandLogic.BrandPopularity().ToArray();
            Assert.That(result[0], Is.EqualTo(
                new KeyValuePair<string, double>("BMW", 2)));
            Assert.That(result[1], Is.EqualTo(
                new KeyValuePair<string, double>("Audi", 4)));
        }
        [Test]
        public void Diesel_CostHigherThank_Test4()
        {
            var result = engineLogic.DieselCostHigherThan4k().ToArray();
            Assert.That(result[0], Is.EqualTo(
                new KeyValuePair<string, int?>("Diesel", 4000)));
        }
        [Test]
        public void Avg_PBB_Test5()
        {
            var avg = carLogic.AveragePBB().ToArray();
            Assert.That(avg[0], Is.EqualTo(
                new KeyValuePair<string, double>("BMW", 3500)));
            Assert.That(avg[1], Is.EqualTo(
                new KeyValuePair<string, double>("Audi", 5000)));
        }
        #endregion
        #region Other tests
        [Test]
        public void Avg_Price_Test6()
        {
            var avg = carLogic.AveragePrice();
            Assert.That(avg, Is.EqualTo(4500));
        }
        [Test]
        public void Highest_Price_Test7()
        {
            var result = carLogic.HighestPrice();
            Assert.That(result, Is.EqualTo(7000));
        }
        #endregion
        #region Create tests
        [Test]
        public void Create_Car_Test8()
        {
            Assert.That(() => carLogic.Create(new Car()
            {
                Model = "Astra",
                BasePrice = -4000
            }), Throws.Exception);
        }
        [Test]
        public void Create_Brand_Test9()
        {
            Assert.That(() => brandLogic.Create(new Brand()
            {
                Name = "999"
            }), Throws.Exception);
        }
        [Test]
        public void Create_Engine_Test10()
        {
            Assert.That(() => engineLogic.Create(new Engine()
            {
                Type = "999"
            }), Throws.Exception);
        }
        #endregion
        
    }
}
