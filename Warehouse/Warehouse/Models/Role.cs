namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Role
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }

        public Guid InstanceId { get; set; }
    }
}