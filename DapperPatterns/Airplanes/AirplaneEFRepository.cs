using DapperPatterns.Dapper;
using Microsoft.EntityFrameworkCore;

namespace DapperPatterns.Airplanes
{
    public class AirplaneEFRepository : EFRepository<Airplane>, IAirplaneRepository
    {
        public AirplaneEFRepository(Context context) 
            : base(context, x => x.Include(y => y.AircraftType))
        {
        }
    }
}
