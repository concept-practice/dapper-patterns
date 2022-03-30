using DapperPatterns.Domain;

namespace DapperPatterns.AircraftTypes
{
    public class AircraftTypeSqlBuilder : SqlBuilder<AircraftType, Guid>
    {
        public AircraftTypeSqlBuilder()
        : base("dbo.AircraftTypes")
        {
            
        }

        protected override Func<AircraftType, IEnumerable<string>> EntityProperties { get; } = aircraftType => new List<string>
            { aircraftType.Id.ToString(), aircraftType.Manufacturer, aircraftType.Model, aircraftType.Seats.ToString() };
    }
}
