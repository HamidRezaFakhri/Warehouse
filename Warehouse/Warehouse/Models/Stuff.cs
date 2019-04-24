namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;

    public class Stuff
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public long UserId { get; set; }

        public virtual UserDto User { get; set; }

        public virtual ICollection<RemittanceStuff> RemittanceStuffs { get; set; }

        public Guid InstanceId { get; set; }
    }
}