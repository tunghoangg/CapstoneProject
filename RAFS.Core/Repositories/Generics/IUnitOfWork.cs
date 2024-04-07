using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Generics
{
    public interface IUnitOfWork : IDisposable
    {
        //Nơi gọi và khởi tạo chung các repo cũng như chứa các phương thức dispose và savechange

        //interface repo
        //IUserRepo userRepo { get; }
        IFarmRepo farmRepo { get; }
        IMilestonRepo milestonRepo { get; }
        IPlantRepo plantRepo { get; }
        IDiaryRepo diaryRepo { get; }
        IPlantMaterialHistoryRepo plantMaterialHistoryRepo { get; }
        IQuestionFormRepo questionFormRepo { get; }
        IBlogRepo blogRepo { get; }
        ITagRepo tagRepo { get; }
        IBlogTagRepo blogTagRepo { get; }
        public IBlogImageRepo blogImageRepo { get; }
        IFarmAdminRepo farmAdminRepo { get; }
        IUFFRepo uffRepo { get; }
        IAspUserRepo aspUserRepo { get; }
        ICashFlowRepo cashFlowRepo { get; }
        IItemRepo itemRepo { get; }
        IImageRepo imageRepo { get; }
        void Save();
        Task SaveAsync();
    }
}
