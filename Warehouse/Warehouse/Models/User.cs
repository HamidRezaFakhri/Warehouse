namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;
    using Warehouse.Entities;

    public class User : EntityBase
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public long RoleId { get; set; }

        public virtual Role Role { get; set; }

        public Guid InstanceId { get; set; }

        public virtual ICollection<Remittance> Remittances { get; set; }
    }
}