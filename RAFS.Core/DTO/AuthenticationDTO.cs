using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class UserLoginDTO
    {
        //[EmailAddress]
        [Display(Name = "Nhập tên người dùng")]
        [Required(ErrorMessage = "Tên người dùng không được để trống.")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string Password { get; set; }

        [Display(Name = "Duy trì đăng nhập")]
        public bool RememberMe { get; set; }
    }

    public class RegistrationDTO {

        [Required(ErrorMessage = "Tên người dùng là bắt buộc")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Nhập lại mật khẩu là bắt buộc.")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không đúng.")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"(((\+|)84)|0)(3|5|7|8|9)+([0-9]{8})\b", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        public string Avatar = "https://lh3.googleusercontent.com/d/1LtjBZGYa-Mn6n1D7n2WwXwLrRpeUIUkY";
        
    }

    public class ExternalRegistrationDTO
    {
        [Required(ErrorMessage = "Tên người dùng là bắt buộc")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ.")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"(((\+|)84)|0)(3|5|7|8|9)+([0-9]{8})\b", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        public string Avatar = "https://lh3.googleusercontent.com/d/1LtjBZGYa-Mn6n1D7n2WwXwLrRpeUIUkY";
        
    }
}

