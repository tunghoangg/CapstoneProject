using System.ComponentModel.DataAnnotations;

namespace RAFS.Web.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ.")]
        public string Email { get; set; }
    }
}
