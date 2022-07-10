using DapperPatterns.Dapper;
using Microsoft.EntityFrameworkCore;

namespace DapperPatterns.AircraftTypes
{
    public class AircraftTypeEFRepository : EFRepository<AircraftType>, IAircraftTypeRepository
    {
        public AircraftTypeEFRepository(DbContext context) : base(context)
        {
        }
    }
}
