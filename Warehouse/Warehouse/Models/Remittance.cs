namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;

    public class Remittance
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public RemittanceType RemittanceType { get; set; }
        
        public DateTime InDate { get; set; } = DateTime.Now;

        public byte StoreId { get; set; }

        public virtual StoreDto Store { get; set; }

        public long UserId { get; set; }

        public virtual UserDto User { get; set; }

        public string Description { get; set; }

        public virtual ICollection<RemittanceStuff> RemittanceStuffs { get; set; }

        public Guid InstanceId { get; set; }
    }
}