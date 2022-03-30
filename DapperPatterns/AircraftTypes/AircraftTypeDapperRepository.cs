using System.Data.SqlClient;
using DapperPatterns.Dapper;
using DapperPatterns.Domain;

namespace DapperPatterns.AircraftTypes
{
    public class AircraftTypeDapperRepository : DapperRepository<AircraftTypeRecord, AircraftType>, IAircraftTypeRepository
    {
        public AircraftTypeDapperRepository(SqlConnection connection, SqlBuilder<AircraftType, Guid> sqlBuilder) 
            : base(connection, sqlBuilder, record => new AircraftType(record.Id, record.Manufacturer, record.Model, record.Seats))
        {
        }
    }
}
