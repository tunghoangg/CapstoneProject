using System.ComponentModel.DataAnnotations;

namespace RAFS.Web.Models
{
    public class UpdateInfoModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ.")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
