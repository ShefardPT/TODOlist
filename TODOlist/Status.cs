using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOlist.API
{
    public class Status
    {
        public enum Importance
        {
            INCONSIDERABLE,
            MEDIUM,
            IMPORTANT
        };
        public enum Urgency
        {
            FARTHER,
            MEDIUM,
            URGENT
        };

    } 
}
