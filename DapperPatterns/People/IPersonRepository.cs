using DapperPatterns.Common;

namespace DapperPatterns.People
{
    public interface IPersonRepository :
        IAddEntity<Person>,
        IGetAll<Person>,
        IAddEntities<Person>
    {
    }
}
