using Microsoft.AspNetCore.Mvc;
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

        void AddTDEvent(Entities.TDEvent tdEventToAdd);

        void RemoveTDEvent(int TDEventID);

        bool IsExist(int TDEventID);

        bool IsSaved();
    }
}
