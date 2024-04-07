using RAFS.Core.Context;
using RAFS.Core.Repositories.IRepo;
using RAFS.Core.Repositories.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Generics
{
    public class UnitOfWork : IUnitOfWork
    {
        //Nơi gọi và khởi tạo chung các repo cũng như chứa các phương thức dispose và savechange
        private readonly RAFSContext _context;

        //khởi tạo
        //public IUserRepo userRepo { get; }
        public IFarmRepo farmRepo { get; }
        public IBlogRepo blogRepo { get; }
        public ITagRepo tagRepo { get; }
        public IBlogTagRepo blogTagRepo { get; }
        public IBlogImageRepo blogImageRepo { get; }
        public IPlantRepo plantRepo { get; }
        public IMilestonRepo milestonRepo { get; }
        public IPlantMaterialHistoryRepo plantMaterialHistoryRepo { get; }
        public IDiaryRepo diaryRepo { get; }
        public IFarmAdminRepo farmAdminRepo { get; }
        public IUFFRepo uffRepo { get; }
        public IAspUserRepo aspUserRepo { get; }
        public IQuestionFormRepo questionFormRepo { get; }
        public ICashFlowRepo cashFlowRepo  { get; }
        public IItemRepo itemRepo { get; }
        public IImageRepo imageRepo { get; }

        public UnitOfWork(RAFSContext context) {  
            _context = context;
            farmRepo = new FarmRepo(_context);
            blogRepo = new BlogRepo(_context);
            tagRepo = new TagRepo(_context);
            blogTagRepo= new BlogTagRepo(_context);
            blogImageRepo=  new BlogImageRepo(_context);
            milestonRepo = new MilestoneRepo(_context);
            farmAdminRepo = new FarmAdminRepo(_context);
            plantRepo = new PlantRepo(_context);
            uffRepo = new UFFRepo(_context);
            diaryRepo = new DiaryRepo(_context);
            questionFormRepo = new QuestionFormRepo(_context);
            aspUserRepo = new AspUserRepo(_context);
            plantMaterialHistoryRepo = new PlantMaterialHistoryRepo(_context);
            cashFlowRepo = new CashFlowRepo(_context);
            itemRepo = new ItemRepo(_context);  
            imageRepo = new ImageRepo(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
            // Giải phóng vùng nhớ
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
