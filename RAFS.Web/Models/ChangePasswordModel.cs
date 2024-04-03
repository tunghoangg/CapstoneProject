using System.ComponentModel.DataAnnotations;

namespace RAFS.Web.Models
{
    public class ChangePasswordModel
    {
        public string userId { get; set; }
        [Required(ErrorMessage = "Mật khẩu hiện tại không được để trống.")]
        [DataType(DataType.Password)]
        public string currentPassword { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không được để trống.")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Nhập lại mật khẩu không đúng")]
        public string newPasswordConfirm { get; set; }
    }
}
