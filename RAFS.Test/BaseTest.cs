using Moq;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Test
{
    public class BaseTest
    {
        protected IUnitOfWork uow;
        protected IFarmRepo farmRepo;
        protected IMilestonRepo milestoneRepo;
        protected IPlantRepo plantRepo;

        protected Mock<IUnitOfWork> uowMock;
        protected Mock<IFarmRepo> farmRepoMock;
        protected Mock<IMilestonRepo> milestoneRepoMock;
        protected Mock<IPlantRepo> plantRepoMock;

        protected Mock<IFarmAdminRepo> farmAdminRepoMock;
        protected Mock<IItemRepo> itemRepoMock;
        protected Mock<ICashFlowRepo> cashFlowRepoMock;
        public BaseTest() 
        {
            uowMock = new Mock<IUnitOfWork>();
            farmRepoMock = new Mock<IFarmRepo>();
            milestoneRepoMock = new Mock<IMilestonRepo>();
            plantRepoMock = new Mock<IPlantRepo>();
            farmAdminRepoMock = new Mock<IFarmAdminRepo>();
            itemRepoMock = new Mock<IItemRepo>();
            cashFlowRepoMock = new Mock<ICashFlowRepo>();
        }

    }
}
