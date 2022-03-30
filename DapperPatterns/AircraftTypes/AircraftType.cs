using DapperPatterns.Domain;

namespace DapperPatterns.AircraftTypes
{
    public class AircraftType : Entity
    {
        public AircraftType(Guid id, string manufacturer, string model, int seats)
            : base(id)
        {
            Manufacturer = manufacturer;
            Model = model;
            Seats = seats;
        }

        public AircraftType(string manufacturer, string model, int seats)
            :this(Guid.NewGuid(), manufacturer, model, seats)
        {
        }

        public string Manufacturer { get; }

        public string Model { get; }

        public int Seats { get; }
    }
}
