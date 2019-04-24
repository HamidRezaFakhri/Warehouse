namespace Warehouse.Models
{
    using System;

    public class Stuff
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}