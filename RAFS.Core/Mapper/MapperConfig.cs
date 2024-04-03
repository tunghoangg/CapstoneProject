using AutoMapper;
using Microsoft.Identity.Client.AppConfig;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RAFS.Core.Mapper
{
    public class MapperConfig : Profile
    {
        //DTO => Data Tranfer Object 
        public MapperConfig() {
            CreateMap<Farm, FarmDTO>();
            CreateMap<Farm, HomepageFarmDetailsDTO>();
            CreateMap<Farm, FarmAdminDTO>().ReverseMap();
            CreateMap<QuestionForm, QuestionFormDTO>();
            CreateMap<QuestionFormDTO, QuestionForm>();
            CreateMap<QuestionForm, ListFormDTO>();
            CreateMap<AspUser, RegistrationDTO>()
                .ForMember(c => c.Password, opt => opt.Ignore())
                .ForMember(c => c.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap();

        }

    }
}
