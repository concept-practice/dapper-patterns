using DapperPatterns.AircraftTypes;
using DapperPatterns.Airplanes;
using Microsoft.Extensions.DependencyInjection;

namespace DapperPatterns
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var collection = new ServiceCollection();

            collection.RegisterDependencies();

            var provider = collection.BuildServiceProvider();

            var repo = provider.GetRequiredService<IAircraftTypeRepository>();

            var planeRepo = provider.GetRequiredService<IAirplaneRepository>();

            //var aircraftType = new AircraftType(Guid.NewGuid(), "Boeing", "737-8", 168);

            //await repo.Add(aircraftType);

            //var results = await repo.GetAll();

            //var airplane = new Airplane("N734SN", results.First());

            //await planeRepo.AddEntity(airplane);

            //const string sql = "SELECT * from dbo.Airplane p"
            //                   + " inner join dbo.AircraftType t on p.AircraftType_Id = t.Id;";

            //var planes = await planeRepo.GetAll();

            //foreach (var plane in planes)
            //{
            //    Console.WriteLine($"Id: {plane.Id} Registration: {plane.Registration} Model: {plane.AircraftType.Model}");
            //}

            var newtype = new AircraftType("Airbus", "A321", 186);

            var a = new AircraftType("Boeing", "737-7", 150);
            var b = new AircraftType("Boeing", "737-8", 174);

            //await repo.AddEntity(a);

            var result = await repo.GetAll();

            //await repo.AddEntities(new[] {a, b});

            //var sql = new AircraftTypeSqlBuilder().InsertMultiple(new[] {a, b}).Query;

            //await repo.AddEntity(newtype);

            //var result = await repo.GetAll();

            foreach (var aircraftType in result)
            {
                Console.WriteLine($"Id: {aircraftType.Id} Model: {aircraftType.Model}");
            }

            var first = new Airplane(Guid.NewGuid(), "N211NA", result.First());
            var second = new Airplane("N721NA", result.First());

            //var rows = await planeRepo.AddEntities(new[] {first, second});

            //Console.WriteLine(rows);

            //await planeRepo.AddEntity(plane);

            var planes = await planeRepo.GetAll();

            foreach (var plane in planes)
            {
                Console.WriteLine($"Id: {plane.Id} Registration: {plane.Registration} Model: {plane.AircraftType.Model}");
            }

            //var aircraft = await repo.GetById(Guid.Parse("9EF0B50E-30E5-4F64-8EF5-D6A593E8D2F9"));

            //Console.WriteLine($"{aircraft.Model}");

            //foreach (var aircraftType in aircraft)
            //{
            //    Console.WriteLine($"Id: {aircraftType.Id} Model: {aircraftType.Model}");
            //}

            //var result = await planeRepo.GetById(Guid.Parse("CF2DE76E-069F-4BEB-91A9-6D27A33C169C"));

            //Console.WriteLine(result);

            // var first = planes.First();

            //await planeRepo.DeleteEntity(first);

            Console.ReadLine();
        }
    }
}
