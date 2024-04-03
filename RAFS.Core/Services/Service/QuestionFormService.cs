using AutoMapper;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.Service
{
    public class QuestionFormService : IQuestionFormService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public QuestionFormService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public void ChangeFormStatus(int id, bool status)
        {
            var form = _uow.questionFormRepo.GetQuestionCotent(id);
            form.Status = status;
            _uow.questionFormRepo.Update(form);
            _uow.Save();
        }

        public List<ListFormDTO> GetForms(string? sort)
        {
            var listForm = _uow.questionFormRepo.GetQuestionFormList(sort);
            var fDTO = _mapper.Map<List<ListFormDTO>>(listForm);

            return fDTO;

        }

        public QuestionContentDTO GetQuestionCotent(int id)
        {
            var result = _uow.questionFormRepo.GetQuestionCotent(id);
            var content = new QuestionContentDTO
            {
                Id = id,
                Description = result.Description,
                Status = result.Status
            };
            return content;
        }

        public void SendQuestionForm(QuestionFormDTO form)
        {
            
            try
            {
                QuestionForm formCreated = new QuestionForm();
                formCreated.GuestName = form.GuestName;
                formCreated.Email = form.Email;
                formCreated.Title = form.Title;
                formCreated.Description = form.Description;
                formCreated.SendDate = DateTime.Now;
                formCreated.Status = false;

                _uow.questionFormRepo.Add(formCreated);
                _uow.Save();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi thêm dữ liệu vào cơ sở dữ liệu
                throw new Exception("Lỗi khi gửi form: " + ex.Message);
            }
        }
    }
}
