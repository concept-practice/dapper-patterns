using System.Data.SqlClient;
using DapperPatterns.Dapper;
using DapperPatterns.Domain;

namespace DapperPatterns.AircraftTypes
{
    public class SimpleAircraftTypeRepository : SimpleAdoRepository<AircraftType>, IAircraftTypeRepository
    {
        public SimpleAircraftTypeRepository(SqlConnection sqlConnection, SqlBuilder<AircraftType, Guid> sqlBuilder)
            : base(sqlConnection, sqlBuilder,
                reader => new AircraftType(reader.GetGuid(0), reader.GetString(1), reader.GetString(2),
                    reader.GetInt32(3)))
        {
        }
    }
}
