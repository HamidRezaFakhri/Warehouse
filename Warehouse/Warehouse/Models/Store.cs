namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;
    using Warehouse.Entities;

    public class Store : EntityBase
    {
        public string Name { get; set; }

        public virtual ICollection<Remittance> Remittances { get; set; }

        public Guid InstanceId { get; set; }
    }
}