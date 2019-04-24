namespace Warehouse.Models
{
    using System;

    public class Remittance
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public RemittanceType RemittanceType { get; set; }
        
        public DateTime InDate { get; set; } = DateTime.Now;

        public byte StoreId { get; set; }

        public virtual Store Store { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }

        public string Description { get; set; }
    }
}