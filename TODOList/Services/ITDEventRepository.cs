﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOList.Services
{
    // todo-list events repository pattern interface
    public interface ITDEventRepository
    {
        // Getting list of all events as IEnumerable<Entities.TDEvent>
        IEnumerable<Entities.TDEvent> GetTDList();
        
        // Getting exact event with ID == TDEventID as parameter
        Entities.TDEvent GetTDEvent(int TDEventID);

        // Addition new event == tdEventToAdd as parameter
        void AddTDEvent(Entities.TDEvent tdEventToAdd);

        // Removing exact event with ID == TDEventID as parameter
        void RemoveTDEvent(int TDEventID);

        // Checking if exact event with ID == TDEventID as parameter is exist
        bool IsExist(int TDEventID);

        // Checking if event is saved
        bool IsSaved();
    }
}
