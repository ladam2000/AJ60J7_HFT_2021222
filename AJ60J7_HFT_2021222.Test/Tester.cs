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

            Brand b1 = new Brand() { Id = 1, Name = "BMW" };
            Brand b2 = new Brand() { Id = 2, Name = "Audi" };

            Engine e1 = new Engine() { Id = 1, Type = "Diesel" };
            Engine e2 = new Engine() { Id = 2, Type = "Diesel" };
            Engine e3 = new Engine() { Id = 3, Type = "Petrol" };
            Engine e4 = new Engine() { Id = 4, Type = "Petrol" };
            Engine e5 = new Engine() { Id = 5, Type = "Electric" };
            Engine e6 = new Engine() { Id = 6, Type = "Hybrid" };

            mockCarRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Car>
                {
                    new Car() {
                        BasePrice = 3000,
                        Brand = b1,
                        Id = 1,
                        Engine = e1
                    },
                    new Car() {
                        BasePrice = 4000,
                        Brand = b1,
                        Id = 2,
                        Engine = e2
                    },
                    new Car() {
                        BasePrice = 7000,
                        Brand = b2,
                        Id = 3,
                        Engine = e3
                    },
                    new Car() {
                        BasePrice = 4000,
                        Brand = b2,
                        Id = 4,
                        Engine = e4
                    },
                    new Car() {
                        BasePrice = 4000,
                        Brand = b2,
                        Id = 5,
                        Engine = e5
                    },
                    new Car() {
                        BasePrice = 5000,
                        Brand = b2,
                        Id = 6,
                        Engine = e6
                    },
                }.AsQueryable());

            mockBrandRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Brand>
                {b1, b2 }
                .AsQueryable());

            mockEngineRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Engine>
                {e1, e2, e3, e4, e5, e6 }
                .AsQueryable());

            carLogic = new CarLogic(mockCarRepo.Object, mockBrandRepo.Object, mockEngineRepo.Object);
            brandLogic = new BrandLogic(mockBrandRepo.Object, mockCarRepo.Object);
            engineLogic = new EngineLogic(mockEngineRepo.Object, mockCarRepo.Object);
        }
        #region non-CRUD tests
        [Test]
        public void AveragePBB_Test()
        {
            var avg = carLogic.AveragePBB().ToArray();

            //BMW = 3500
            //Audi = 5000

            Assert.That(avg[0], Is.EqualTo(
                new KeyValuePair<string, double>("BMW", 3500)));

            Assert.That(avg[1], Is.EqualTo(
                new KeyValuePair<string, double>("Audi", 5000)));
        }
        [Test]
        public void AveragePBET_Test()
        {
            var avg = carLogic.AveragePriceByEngineTypes().ToArray();

            //Diesel = 3500
            //Petrol = 5500
            //Electric = 4000
            //Hybrid = 5000

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
        public void EngineTypeUsage_Test()
        {
            var result = carLogic.EngineTypeUsage().ToArray();

            //Diesel = 2
            //Petrol = 2
            //Electric, Hybrid = 1

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
        public void BrandPopularity_Test()
        {
            var result = brandLogic.BrandPopularity().ToArray();

            //bmw = 2
            //audi = 4

            Assert.That(result[0], Is.EqualTo(
                new KeyValuePair<string, double>("BMW", 2)));
            Assert.That(result[1], Is.EqualTo(
                new KeyValuePair<string, double>("Audi", 4)));
        }
        [Test]
        public void DieselCostHigherThan4k_Test()
        {
            var result = engineLogic.DieselCostHigherThan4k().ToArray();

            //Diesel = 4000

            Assert.That(result[0], Is.EqualTo(
                new KeyValuePair<string, int?>("Diesel", 4000)));

        }
        #endregion
        #region Create tests
        [Test]
        public void CarCreate_Test()
        {
            Assert.That(() => carLogic.Create(new Car()
            {
                Model = "Astra",
                BasePrice = -4000
            }), Throws.Exception);
        }
        [Test]
        public void BrandCreate_Test()
        {
            Assert.That(() => brandLogic.Create(new Brand()
            {
                Name = "999"
            }), Throws.Exception);
        }
        [Test]
        public void EngineCreate_Test()
        {
            Assert.That(() => engineLogic.Create(new Engine()
            {
                Type = "999"
            }), Throws.Exception);
        }
        #endregion
        #region Other tests
        [Test]
        public void AveragePrice_Test()
        {
            var avg = carLogic.AveragePrice();

            //avg = 4500

            Assert.That(avg, Is.EqualTo(4500));
        }
        [Test]
        public void HighestPrice_Test()
        {
            var result = carLogic.HighestPrice();

            //result = 7000

            Assert.That(result, Is.EqualTo(7000));
        }
        #endregion
    }
}
