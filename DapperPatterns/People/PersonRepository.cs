using System.Data.SqlClient;
using DapperPatterns.Dapper;
using DapperPatterns.Domain;

namespace DapperPatterns.People
{
    public class PersonRepository : DapperRepository<PersonRecord, Person>, IPersonRepository //: SimpleAdoRepository<Person>, IPersonRepository
    {
        //public PersonRepository(SqlConnection sqlConnection, SqlBuilder<Person, Guid> sqlBuilder)
        //    : base(sqlConnection, sqlBuilder, builder => new Person(builder.GetGuid(0), new Name(builder.GetString(1), builder.GetString(2)), builder.GetInt32(3)))
        //{
        //}
        public PersonRepository(SqlConnection connection, SqlBuilder<Person, Guid> sqlBuilder) 
            : base(connection, sqlBuilder, record => new Person(record.Id, new Name(record.FirstName, record.LastName), record.Age))
        {
        }
    }
}
