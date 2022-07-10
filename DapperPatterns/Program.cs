using DapperPatterns.People;
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

            var personRepo = provider.GetRequiredService<IPersonRepository>();

            var p1 = new Person(new Name("Bob", "Bill"), 23);

            //await personRepo.AddEntity(p1);

            var allPeeps = await personRepo.GetAll();

            //await personRepo.AddEntities(new []{ p1, new Person()})

            foreach (var allPeep in allPeeps)
            {
                Console.WriteLine(allPeep);
            }

            Console.ReadLine();
        }
    }
}
