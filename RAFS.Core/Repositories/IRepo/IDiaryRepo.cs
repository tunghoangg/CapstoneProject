using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    public interface IDiaryRepo : IGenericRepo<Diary>
    {
        List<DiaryDTO> GetDiaries(int farmid);
        List<DiaryDTO> GetPlantDiaries(int plantid);
        List<DiaryDTO> GetPlantDiariesByType(int plantid, int type);
        Diary GetDiaryById(int milestoneid);
        public bool CreateDiary(Diary milestoneinfor);
        public bool UpdateDiary(Diary milestoneinfor);
        public bool DeleteDiary(int milestoneidfarmid);

    }
}
