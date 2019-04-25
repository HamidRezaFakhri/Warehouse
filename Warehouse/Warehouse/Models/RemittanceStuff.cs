namespace Warehouse.Models
{
    using System;
    using Warehouse.Entities;

    public class RemittanceStuff : EntityBase
    {
        public long RemittanceId { get; set; }

        public Remittance Remittance { get; set; }

        public long StuffId { get; set; }

        public Stuff Stuff { get; set; }

        public int Count { get; set; }

        public Guid InstanceId { get; set; }
    }
}