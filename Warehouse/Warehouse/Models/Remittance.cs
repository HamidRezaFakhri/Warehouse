namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;
    using Warehouse.Entities;

    public class Remittance : EntityBase
    {
        public string Code { get; set; }

        public RemittanceType RemittanceType { get; set; }
        
        public DateTime InDate { get; set; } = DateTime.Now;

        public long StoreId { get; set; }

        public virtual Store Store { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }

        public string Description { get; set; }

        public virtual ICollection<RemittanceStuff> RemittanceStuffs { get; set; }

        public Guid InstanceId { get; set; }
    }
}