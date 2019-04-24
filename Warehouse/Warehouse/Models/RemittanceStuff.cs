namespace Warehouse.Models
{
    public class RemittanceStuff
    {
        public long Id { get; set; }

        public long RemittanceId { get; set; }

        public Remittance Remittance { get; set; }

        public long StuffId { get; set; }

        public Stuff Stuff { get; set; }

        public int Count { get; set; }
    }
}