namespace Warehouse.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        [Display(Name = "نام کاربری")]
        [Required]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "تعداد کاراکترها در محدوده مجاز نمیباشد!")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage ="تعداد کاراکترها در محدوده مجاز نمیباشد!")]
        public string Password { get; set; }

        [Display(Name = "نقش کاربری")]
        [Required]
        public long RoleId { get; set; }

        public virtual RoleDto Role { get; set; }

        public virtual ICollection<RemittanceDto> Remittances { get; set; }
    }
}