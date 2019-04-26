namespace Warehouse.Models
{
    using System;
    using Warehouse.Entities;

    public class WSLog : EntityBase
    {
        public string Message { get; set; }

        public string MessageTemplate { get; set; }

        public string Level { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Exception { get; set; }

        public string Properties { get; set; }

        public string LogEvent { get; set; }

        public string OtherData { get; set; }
    }
}