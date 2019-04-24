namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;

    public class StoreDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Remittance> Remittances { get; set; }

        public Guid InstanceId { get; set; }
    }
}