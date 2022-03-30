using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperPatterns.Dapper;
using Microsoft.EntityFrameworkCore;

namespace DapperPatterns.AircraftTypes
{
    public class AircraftTypeEFRepository : EFRepository<AircraftType>, IAircraftTypeRepository
    {
        public AircraftTypeEFRepository(Context context) : base(context)
        {
        }
    }
}
