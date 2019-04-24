namespace Warehouse.Models
{
    using System;

    public class RemittanceStuff
    {
        public long Id { get; set; }

        public long RemittanceId { get; set; }

        public Remittance Remittance { get; set; }

        public long StuffId { get; set; }

        public StuffDto Stuff { get; set; }

        public int Count { get; set; }

        public Guid InstanceId { get; set; }
    }
}