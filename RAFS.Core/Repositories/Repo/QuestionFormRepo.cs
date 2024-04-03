using RAFS.Core.Context;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Repo
{
    public class QuestionFormRepo : GenericRepo<QuestionForm>, IQuestionFormRepo
    {
        public QuestionFormRepo(RAFSContext context) : base(context)
        {

        }

        public QuestionForm GetQuestionCotent(int id)
        {
            QuestionForm form = new QuestionForm();
            form = _context.QuestionForms.SingleOrDefault(x => x.Id == id);
            return form;
        }

        public List<QuestionForm> GetQuestionFormList(string? sort)
        {
            var listForm = _context.QuestionForms.AsQueryable();

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "not read":
                        listForm = listForm.OrderByDescending(f => f.SendDate).Where(f => f.Status == false);
                        break;
                    case "read":
                        listForm = listForm.OrderByDescending(f => f.SendDate).Where(f => f.Status == true);
                        break;
                    case "none":
                        listForm = listForm.OrderByDescending(f => f.SendDate);
                        break;
                }
            }
            return listForm.ToList();
        }
    }
}
