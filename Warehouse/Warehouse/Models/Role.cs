namespace Warehouse.Models
{
    using System.Collections.Generic;
    
    public class Role
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}