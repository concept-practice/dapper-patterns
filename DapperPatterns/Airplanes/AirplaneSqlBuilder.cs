using DapperPatterns.Domain;

namespace DapperPatterns.Airplanes
{
    public class AirplaneSqlBuilder : GuidSqlBuilder<Airplane>
    {
        public AirplaneSqlBuilder()
        : base("dbo.Airplanes")
        {
            
        }

        protected override Func<Airplane, IEnumerable<string>> EntityProperties { get; } = airplane => new List<string> 
            { airplane.Id.ToString(), airplane.Registration, airplane.AircraftType.Id.ToString() };

        protected override string DefaultInclude => "INNER JOIN dbo.AircraftTypes ON dbo.Airplanes.AircraftTypeId = dbo.AircraftTypes.Id";
    }
}
