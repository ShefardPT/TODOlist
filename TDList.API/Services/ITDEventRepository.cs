using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDList.API.Services
{
    public interface ITDEventRepository
    {
        IEnumerable<Entities.TDEvent> GetTDList();

        Entities.TDEvent GetTDEvent(int TDEventID);

        void PostTDEvent([FromBody] Models.TDEventToAdd tdEventToAdd);
        
        //void PutTDEvent(int TDEventID);

        //void PatchTDEvent(int TDEventID);
        
        //void DeleteTDEvent(int TDEventID);
    }
}
