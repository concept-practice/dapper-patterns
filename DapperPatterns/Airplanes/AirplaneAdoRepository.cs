using System.Data.SqlClient;
using DapperPatterns.AircraftTypes;
using DapperPatterns.Dapper;
using DapperPatterns.Domain;

namespace DapperPatterns.Airplanes
{
    public class AirplaneAdoRepository : AdoRepository<Airplane>, IAirplaneRepository
    {
        private static readonly Func<IList<object>, Airplane> conversionFunc = objects =>
        {
            return new Airplane(Guid.Parse(objects[0].ToString()), objects[1].ToString(),
                new AircraftType(Guid.Parse(objects[3].ToString()), objects[4].ToString(), objects[5].ToString(),
                    int.Parse(objects[6].ToString())));
        };

        public AirplaneAdoRepository(SqlConnection sqlConnection, SqlBuilder<Airplane, Guid> sqlBuilder)
            : base(sqlConnection, sqlBuilder, conversionFunc)
        {
        }
    }
}
