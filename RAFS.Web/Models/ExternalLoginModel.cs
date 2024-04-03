using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RAFS.Web.Models
{
    public class ExternalLoginModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
    public class InputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
    }
}
