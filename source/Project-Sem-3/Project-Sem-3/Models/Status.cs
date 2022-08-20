using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Sem_3.Models
{
    public class Status
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public Status() { }
        public Status(int Number, string Name)
        {
            this.Number = Number;
            this.Name = Name;
        }

        public static List<Status> GetStatusList()
        {
            List<Status> statusList = new List<Status>();
            statusList.Add(new Status(0, "Cancel"));
            statusList.Add(new Status(1, "Awaiting"));
            statusList.Add(new Status(2, "Accepted"));
            statusList.Add(new Status(3, "Shipping"));
            statusList.Add(new Status(4, "Done"));
            return statusList;
        }
    }
}