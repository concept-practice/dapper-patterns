using DapperPatterns.Domain;

namespace DapperPatterns.People
{
    public class Person : Entity
    {
        public Person(Guid id, Name name, int age) 
            : base(id)
        {
            Name = name;
        }

        public Person(Name name, int age)
            : this(Guid.NewGuid(), name, age)
        {
        }

        public Name Name { get; }

        public int Age { get; }
    }
}
