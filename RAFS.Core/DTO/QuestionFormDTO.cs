using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class QuestionFormDTO
    {
        public string GuestName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class ListFormDTO
    {
        public int Id { get; set; }
        public string GuestName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime SendDate { get; set; }
    }

    public class PaginateFormListDTO
    {
        public List<ListFormDTO> Forms { get; set; } = new List<ListFormDTO>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class QuestionContentDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

    }
}
 