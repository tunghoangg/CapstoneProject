using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    public interface IQuestionFormRepo : IGenericRepo<QuestionForm>
    {
        List<QuestionForm> GetQuestionFormList(string? sort);
        QuestionForm GetQuestionCotent(int id);
    }
}
