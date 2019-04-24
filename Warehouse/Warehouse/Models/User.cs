namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;

    public class UserDto
    {
        public long Id { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }

        public long RoleId { get; set; }

        public virtual Role Role { get; set; }

        public Guid InstanceId { get; set; }

        public virtual ICollection<Remittance> Remittances { get; set; }

        public virtual ICollection<Stuff> Stuffs { get; set; }
    }
}