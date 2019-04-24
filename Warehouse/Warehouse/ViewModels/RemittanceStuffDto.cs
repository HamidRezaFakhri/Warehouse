using System.ComponentModel.DataAnnotations;

namespace Warehouse.ViewModels
{
    public class RemittanceStuffDto
    {
        [Display(Name = "شماره حواله")]
        [Required]
        public long RemittanceId { get; set; }

        public RemittanceDto Remittance { get; set; }

        [Display(Name = "کالا")]
        [Required]
        public long StuffId { get; set; }

        public StuffDto Stuff { get; set; }

        [Display(Name = "تعداد")]
        [Required]
        public int Count { get; set; }
    }
}