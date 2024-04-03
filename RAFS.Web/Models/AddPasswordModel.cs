using System.ComponentModel.DataAnnotations;

namespace RAFS.Web.Models
{
    public class AddPasswordModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập lại mật khẩu không được để trống.")]
        [Compare("newPassword", ErrorMessage = "Nhập lại mật khẩu không đúng")]
        public string newPasswordConfirm { get; set; }
    }
}
