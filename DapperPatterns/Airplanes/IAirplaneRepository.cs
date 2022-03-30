using DapperPatterns.Common;

namespace DapperPatterns.Airplanes
{
    public interface IAirplaneRepository : 
        IGetAll<Airplane>, 
        IAddEntity<Airplane>, 
        IGetById<Airplane>, 
        IDeleteEntity<Airplane>,
        IAddEntities<Airplane>
    {
        //Task<List<Airplane>> AllSevenThirtySevens();
    }
}
