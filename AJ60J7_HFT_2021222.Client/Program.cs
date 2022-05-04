using AJ60J7_HFT_2021222.Models;
using System;
using System.Collections.Generic;

namespace AJ60J7_HFT_2021222.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);

            //RestService rest = new RestService("http://localhost:44728");

            Menu();
            string navigator = Console.ReadLine();
            while (navigator.ToUpper() != "X")
            {
                if (navigator.Equals("1"))
                    Create();
                else if (navigator.Equals("2"))
                    Read();
                else if (navigator.Equals("3"))
                    Update();
                else if (navigator.Equals("4"))
                    Delete();
                else if (navigator.Equals("5"))
                    AveragePriceByBrands();
                else if (navigator.Equals("6"))
                    AveragePriceByEngineTypes();
                else if (navigator.Equals("7"))
                    EngineTypeUsage();
                else if (navigator.Equals("8"))
                    BrandPopularity();
                else if (navigator.Equals("9"))
                    DieselCostHigherThan4k();

                Console.Clear();
                Menu();
                navigator = Console.ReadLine();
            }
        }
        private static void Menu()
        {
            Console.WriteLine("************************************");
            Console.WriteLine("*                                  *");
            Console.WriteLine("*       Car Database Menu          *");
            Console.WriteLine("*                                  *");
            Console.WriteLine("*             CRUDs                *");
            Console.WriteLine("* [1] Create  Brand/Car/Engine     *");
            Console.WriteLine("* [2] Read    Brand/Car/Engine     *");
            Console.WriteLine("* [3] Update  Brand/Car/Engine     *");
            Console.WriteLine("* [4] Delete  Brand/Car/Engine     *");
            Console.WriteLine("*                                  *");
            Console.WriteLine("*           non CRUDs              *");
            Console.WriteLine("* [5] AveragePriceByBrands         *");
            Console.WriteLine("* [6] AveragePriceByEngineTypes    *");
            Console.WriteLine("* [7] EngineTypeUsage              *");
            Console.WriteLine("* [8] BrandPopularity              *");
            Console.WriteLine("* [9] DieselCostHigherThan4k       *");
            Console.WriteLine("*                                  *");
            Console.WriteLine("* [X] Exit                         *");
            Console.WriteLine("*                                  *");
            Console.WriteLine("************************************");
        }
        private static void Create()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");
            Console.WriteLine("Choose what you want to CREATE: Brand/Car/Engine");
            string selection = Console.ReadLine();

            if (selection.ToUpper() == "BRAND")
            {
                Console.WriteLine("The new Brand's desired name:");
                string name = Console.ReadLine();

                rest.Post(new Brand() { Name = name }, "brand");
            }
            else if (selection.ToUpper() == "CAR")
            {
                Console.WriteLine("The new Car's desired brand ID:");
                int brandID = int.Parse(Console.ReadLine());

                Console.WriteLine("The new Car's desired model name:");
                string model = Console.ReadLine();

                Console.WriteLine("The new Car's desired baseprice:");
                int? baseprice = int.Parse(Console.ReadLine());

                rest.Post(new Car() { BrandId = brandID, BasePrice = baseprice, Model = model }, "car");
            }
            else if (selection.ToUpper() == "ENGINE")
            {
                Console.WriteLine("The new Engine's desired car ID:");
                int carID = int.Parse(Console.ReadLine());

                Console.WriteLine("The new Engine's desired type:");
                string eType = Console.ReadLine();

                Console.WriteLine("The new Engine's desired horsepower:");
                int horsepower = int.Parse(Console.ReadLine());

                rest.Post(new Engine() { CarId = carID, Type = eType, Horsepower = horsepower }, "engine");
            }
            Console.WriteLine($"The {selection.ToUpper()} has been CREATED!");
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
        private static void Read()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");
            Console.WriteLine("Select what to READ: Brand/Car/Engine");
            string selection = Console.ReadLine();

            if (selection.ToUpper().Equals("BRAND"))
            {
                Console.WriteLine("The desired Brand's ID:");
                int bId = int.Parse(Console.ReadLine());

                Brand brand = rest.Get<Brand>(bId, "brand");
                Console.WriteLine($"\n {nameof(brand.Id)}: {brand.Id} " +
                    $"\n {nameof(brand.Name)}: {brand.Name}");
            }
            else if (selection.ToUpper().Equals("CAR"))
            {
                Console.WriteLine("The desired Car's ID:");
                int cId = int.Parse(Console.ReadLine());

                Car car = rest.Get<Car>(cId, "car");
                Console.WriteLine($"\n {nameof(car.Id)}: {car.Id} " +
                    $"\n {nameof(car.Model)}: {car.Model}" +
                    $"\n {nameof(car.BasePrice)}: {car.BasePrice}");
            }
            else if (selection.ToUpper().Equals("ENGINE"))
            {
                Console.WriteLine("The desired Engine's ID:");
                int eId = int.Parse(Console.ReadLine());

                Engine engine = rest.Get<Engine>(eId, "engine");
                Console.WriteLine($"\n {nameof(engine.Id)}: {engine.Id} " +
                    $"\n {nameof(engine.Type)}: {engine.Type}" +
                    $"\n {nameof(engine.Horsepower)}: {engine.Horsepower}");
            }
            Console.WriteLine($"The {selection.ToUpper()} has been DISPLAYED!");
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
        private static void Update()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");
            Console.WriteLine("Select what to UPDATE: Brand/Car/Engine");
            string selection = Console.ReadLine();

            if (selection.ToUpper().Equals("BRAND"))
            {
                Console.WriteLine("The Brand's ID that needs to be updated is:");
                int brandID = int.Parse(Console.ReadLine());

                Console.WriteLine("The Brand's new name:");
                string newName = Console.ReadLine();

                rest.Put(new Brand() { Id = brandID, Name = newName }, "brand");
            }
            else if (selection.ToUpper().Equals("CAR"))
            {
                Console.WriteLine("The Car's ID that needs to be updated is:");
                int carID = int.Parse(Console.ReadLine());

                Console.WriteLine("The Car's new model:");
                string newModel = Console.ReadLine();

                Console.WriteLine("The Car's new base price:");
                int newBasePrice = int.Parse(Console.ReadLine());

                rest.Put(new Car() { Id = carID, Model = newModel, BasePrice = newBasePrice }, "car");
            }
            else if (selection.ToUpper().Equals("ENGINE"))
            {
                Console.WriteLine("The Engine's ID that needs to be updated is:");
                int engineID = int.Parse(Console.ReadLine());

                Console.WriteLine("The Engine's new model:");
                string newType = Console.ReadLine();

                Console.WriteLine("The Engine's new base price:");
                int newHorsepower = int.Parse(Console.ReadLine());

                rest.Put(new Engine() { Id = engineID, Type = newType, Horsepower = newHorsepower }, "engine");
            }
            Console.WriteLine($"The {selection.ToUpper()} has been UPDATED!");
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
        private static void Delete()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");
            Console.WriteLine("Select what to DELETE: Brand/Car/Engine");
            string selection = Console.ReadLine();

            if (selection.ToUpper().Equals("BRAND"))
            {
                Console.WriteLine("The Brand's ID that needs to be deleted is:");
                int brandID = int.Parse(Console.ReadLine());

                rest.Delete(brandID, "brand");
            }
            else if (selection.ToUpper().Equals("CAR"))
            {
                Console.WriteLine("The CAR's ID that needs to be deleted is:");
                int carID = int.Parse(Console.ReadLine());

                rest.Delete(carID, "car");
            }
            else if (selection.ToUpper().Equals("ENGINE"))
            {
                Console.WriteLine("The ENGINE's ID that needs to be deleted is:");
                int engineID = int.Parse(Console.ReadLine());

                rest.Delete(engineID, "engine");
            }
            Console.WriteLine($"The {selection.ToUpper()} has been UPDATED!");
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
        public static void AveragePriceByBrands()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");

            var rslt = rest.Get<KeyValuePair<string, double>>("stat/averagePBB");
            Console.WriteLine("Average Price By Brands:");
            foreach (KeyValuePair<string, double> pair in rslt)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
        public static void AveragePriceByEngineTypes()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");

            var rslt = rest.Get<KeyValuePair<string, double>>("stat/AveragePriceByEngineTypes");
            Console.WriteLine("Average Price By Engine Types:");
            foreach (KeyValuePair<string, double> pair in rslt)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
        public static void EngineTypeUsage()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");

            var rslt = rest.Get<KeyValuePair<string, double>>("stat/EngineTypeUsage");
            Console.WriteLine("Usage of each Engine Type:");
            foreach (KeyValuePair<string, double> pair in rslt)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
        public static void BrandPopularity()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");

            var rslt = rest.Get<KeyValuePair<string, double>>("stat/BrandPopularity");
            Console.WriteLine("Popularity of each Brand:");
            foreach (KeyValuePair<string, double> pair in rslt)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
        public static void DieselCostHigherThan4k()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:44728");

            var rslt = rest.Get<KeyValuePair<string, double>>("stat/DieselCostHigherThan4k");
            Console.WriteLine("Cars with diesel type engines that cost more than 4000:");
            foreach (KeyValuePair<string, double> pair in rslt)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.WriteLine("Press a key to exit!");
            Console.ReadLine();
        }
    }
}

