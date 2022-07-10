using DapperPatterns.Common;

namespace DapperPatterns.AircraftTypes
{
    public interface IAircraftTypeRepository :
        IAddEntity<AircraftType>,
        IGetAll<AircraftType>,
        IGetById<AircraftType>,
        IAddEntities<AircraftType>
    {
        
    }
}
