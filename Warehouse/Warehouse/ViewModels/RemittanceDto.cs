namespace Warehouse.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Warehouse.Models;

    public class RemittanceDto
    {
        public long Id { get; set; }

        public string Code { get; set; }

        [Display(Name = "نوع حواله")]
        [Required]
        public RemittanceType RemittanceType { get; set; }

        [Display(Name = "تاریخ حواله")]
        [Required]
        public DateTime InDate { get; set; } = DateTime.Now;

        [Display(Name = "انبار")]
        [Required]
        public byte StoreId { get; set; }

        public virtual StoreDto Store { get; set; }

        public long UserId { get; set; }

        public virtual UserDto User { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, MinimumLength = 5,
            ErrorMessage = "تعداد کاراکترها در محدوده مجاز نمیباشد!")]
        public string Description { get; set; }

        public virtual ICollection<RemittanceStuffDto> RemittanceStuffs { get; set; }
    }
}