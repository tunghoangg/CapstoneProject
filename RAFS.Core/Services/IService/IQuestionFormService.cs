using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IQuestionFormService
    {
        public void SendQuestionForm(QuestionFormDTO form);
        List<ListFormDTO> GetForms(string? sort);
        QuestionContentDTO GetQuestionCotent(int id);
        public void ChangeFormStatus(int id, bool status);
    }
}
