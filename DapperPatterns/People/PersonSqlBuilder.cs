using DapperPatterns.Domain;

namespace DapperPatterns.People
{
    public class PersonSqlBuilder : SqlBuilder<Person, Guid>
    {

        protected override Func<Person, IEnumerable<string>> EntityProperties { get; } = person => new[]
        {
            person.Id.ToString(), person.Name.First, person.Name.Last, person.Age.ToString()
        };
    }
}
