using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IDiaryService
    {
        
        public List<DiaryDTO> GetAllDiary(int farmid);
        public List<DiaryDTO> GetAllPlantDiary(int plantid);
        public List<DiaryDTO> GetAllPlantDiaryByType(int plantid,int type);
        public DiaryDTO GetDiaryById(int milestoneId);
        public Diary GetRawDiaryById(int milestoneId);
        public bool CreateDiary(Diary milestone);
        public bool UpdateDiary(Diary milestone);
        public bool DeleteDiary(int milestoneId);
       
    }
}
