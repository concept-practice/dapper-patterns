using System.Data.SqlClient;
using DapperPatterns.AircraftTypes;
using DapperPatterns.Dapper;
using DapperPatterns.Domain;

namespace DapperPatterns.Airplanes
{
    public class AirplaneRepository : DapperRepository<AirplaneRecord, AircraftTypeRecord, Airplane>, IAirplaneRepository
    {
        private static readonly Func<AirplaneRecord, AircraftTypeRecord, Airplane> Map = (airplaneRecord, aircraftTypeRecord) => new Airplane(airplaneRecord.Id,
            airplaneRecord.Registration, new AircraftType(aircraftTypeRecord.Id, aircraftTypeRecord.Manufacturer, aircraftTypeRecord.Model, aircraftTypeRecord.Seats));

        public AirplaneRepository(SqlConnection connection, SqlBuilder<Airplane, Guid> sqlBuilder) 
            : base(connection, sqlBuilder, Map)
        {
        }
    }
}
