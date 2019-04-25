namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;
    using Warehouse.Entities;

    public class Role : EntityBase
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Guid InstanceId { get; set; }
    }
}