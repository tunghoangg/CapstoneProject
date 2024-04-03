using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Body { get; set; }
        public List<String> Tag { get; set; }
        public List<String>? ImageURL { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Status { get; set; }
    }
    public class CreateBlogDTO
    {
        [Required(ErrorMessage = "Tiêu đề không được để  trống")]
        [StringLength(60, ErrorMessage = "Tiêu đề không được vượt quá 60 kí tự.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Nội dung không được để  trống")]
        public string Body { get; set; }
        public List<string> ListTag { get; set; }
        
        public List<string> UrlImage { get; set; }
        public string AuthorId { get; set; }
    }
    public class BlogResponses()
    {
        public List<BlogDTO> blogs { get; set; } = new List<BlogDTO>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
