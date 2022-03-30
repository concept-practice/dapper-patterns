using DapperPatterns.AircraftTypes;
using DapperPatterns.Domain;

namespace DapperPatterns.Airplanes
{
    public class Airplane : Entity
    {
        public Airplane(Guid id, string registration, AircraftType aircraftType)
            : base(id)
        {
            Registration = registration;
            AircraftType = aircraftType;
        }

        public Airplane(string registration, AircraftType aircraftType)
            : this(Guid.NewGuid(), registration, aircraftType)
        {
            AddNotification(new AirplaneCreated());
        }

        public Airplane(Guid id, string registration)
            : base(id)
        {
            Registration = registration;
        }

        public string Registration { get; }

        public AircraftType AircraftType { get; private set; }

        public override string ToString()
        {
            return $"Id: {Id} Registration: {Registration}";
        }
    }
}
