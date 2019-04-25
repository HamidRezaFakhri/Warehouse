namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;
    using Warehouse.Entities;

    public class Stuff : EntityBase
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public virtual ICollection<RemittanceStuff> RemittanceStuffs { get; set; }

        public Guid InstanceId { get; set; }
    }
}