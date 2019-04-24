namespace Warehouse.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StoreDto
    {
        [Display(Name = "نام/عنوان")]
        [Required]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "تعداد کاراکترها در محدوده مجاز نمیباشد!")]
        public string Name { get; set; }

        public virtual ICollection<RemittanceDto> Remittances { get; set; }
    }
}