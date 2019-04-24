namespace Warehouse.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StuffDto
    {
        [Display(Name = "نام/عنوان")]
        [Required]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "تعداد کاراکترها در محدوده مجاز نمیباشد!")]
        public string Name { get; set; }

        [Display(Name = "کد")]
        [Required]
        [StringLength(50, MinimumLength = 1,
            ErrorMessage = "تعداد کاراکترها در محدوده مجاز نمیباشد!")]
        public string Code { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, MinimumLength = 5,
            ErrorMessage = "تعداد کاراکترها در محدوده مجاز نمیباشد!")]
        public string Description { get; set; }

        public virtual ICollection<RemittanceStuffDto> RemittanceStuffs { get; set; }
    }
}