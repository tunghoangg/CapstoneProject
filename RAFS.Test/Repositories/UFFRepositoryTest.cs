using Microsoft.EntityFrameworkCore;
using Moq;
using RAFS.Core.Context;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Test.Repositories
{
    public class UFFRepositoryTest
    {
        [Fact]
        public async Task GetUFFsByFarmId_ReturnUFF_WhenCalled()
        {
            string userId = "9b949f70-b0c4-4b56-912a-b305543adc05";
            var farmId =  1;

            var farmData = new List<Farm>() {
                 new Farm {Id = 1, Code = "FARM01"},
                 new Farm {Id = 2, Code = "FARM02"},
            }.AsQueryable();

            var userData = new List<AspUser>() {
                new AspUser {Id = "9b949f70-b0c4-4b56-912a-b305543adc06", FullName = "Test" },
                new AspUser {Id = "9b949f70-b0c4-4b56-912a-b305543adc05", FullName = "Test" },
            }.AsQueryable();

            var functionData = new List<Function>() {
                new Function { Id = 1, Name = "Khác"},
                new Function { Id = 2, Name = "Sổ nhật ký"},
            }.AsQueryable();

            var userFunctionFarmData = new List<UserFunctionFarm> {
                new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc06", FarmId = 1, FunctionId = 1 },
                new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc05", FarmId = 1, FunctionId = 1 }
            }.AsQueryable();

            var mockDbSetFarm = new Mock<DbSet<Farm>>();
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmData.Provider);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmData.Expression);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmData.ElementType);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmData.GetEnumerator());

            var mockDbSetUser = new Mock<DbSet<AspUser>>();
            mockDbSetUser.As<IQueryable<AspUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            mockDbSetUser.As<IQueryable<AspUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            mockDbSetUser.As<IQueryable<AspUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            mockDbSetUser.As<IQueryable<AspUser>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());

            var mockDbSetUFF = new Mock<DbSet<UserFunctionFarm>>();
            mockDbSetUFF.As<IQueryable<UserFunctionFarm>>().Setup(m => m.Provider).Returns(userFunctionFarmData.Provider);
            mockDbSetUFF.As<IQueryable<UserFunctionFarm>>().Setup(m => m.Expression).Returns(userFunctionFarmData.Expression);
            mockDbSetUFF.As<IQueryable<UserFunctionFarm>>().Setup(m => m.ElementType).Returns(userFunctionFarmData.ElementType);
            mockDbSetUFF.As<IQueryable<UserFunctionFarm>>().Setup(m => m.GetEnumerator()).Returns(() => userFunctionFarmData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Farms).Returns(mockDbSetFarm.Object);
            mockContext.Setup(c => c.AspUsers).Returns(mockDbSetUser.Object);
            mockContext.Setup(c => c.UserFunctionFarms).Returns(mockDbSetUFF.Object);

            var service = new UFFRepo(mockContext.Object);

            //act
            var result = service.GetUFFsByFarmId(userId, farmId);

            Assert.NotNull(result);
            //Assert.IsType<List<UserFunctionFarm>>(result);
        }

        [Fact]
        public void GetAllUFFsAsync_ReturnListUFF_WhenCalled()
        {

            var data = new List<UserFunctionFarm>() {
                 new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc06", FarmId = 1, FunctionId = 1 },
                 new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc05", FarmId = 1, FunctionId = 1 }
            }.AsQueryable();


            var mockDbSet = new Mock<DbSet<UserFunctionFarm>>();
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.UserFunctionFarms).Returns(mockDbSet.Object);
            var service = new UFFRepo(mockContext.Object);

            //act
            var result = service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddAsync_AddUFF_WhenCalled()
        {

            // Arrange
            var mockSet = new Mock<DbSet<UserFunctionFarm>>();
            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Set<UserFunctionFarm>()).Returns(mockSet.Object);

            var entityToAdd = new UserFunctionFarm() { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc05", FarmId = 1, FunctionId = 1, CreatedDate = DateTime.Now };
            var repository = new UFFRepo(mockContext.Object);

            // Act
            await repository.AddAsync(entityToAdd);

            // Assert
            await Task.Delay(100); // Delay to ensure AddAsync completes (you can adjust the delay time as needed)
            mockSet.Verify(m => m.AddAsync(entityToAdd, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdateUFF_WhenCalled()
        {
            var data = new List<UserFunctionFarm>() {
                 new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc06", FarmId = 1, FunctionId = 3, CreatedDate = DateTime.Now },
                 new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc05", FarmId = 1, FunctionId = 1, CreatedDate = DateTime.Now }
            }.AsQueryable();


            var mockDbSet = new Mock<DbSet<UserFunctionFarm>>();
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.UserFunctionFarms).Returns(mockDbSet.Object);
            var service = new UFFRepo(mockContext.Object);

            // Create a new instance of Farm to update
            var itemUpdate = new UserFunctionFarm() { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc06", FarmId = 1, FunctionId = 2, CreatedDate = DateTime.UtcNow };

            // Act
            var result = service.UpdateAsync(itemUpdate); // Await the UpdateAsync method

            // Assert
            await Task.Delay(100); // Delay for a short time to allow the asynchronous operation to complete
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteAsync_DeleteUFF_WhenCalled()
        {
            var data = new List<UserFunctionFarm>() {
                 new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc06", FarmId = 1, FunctionId = 3 },
                 new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc05", FarmId = 1, FunctionId = 1 }
            }.AsQueryable();


            var mockDbSet = new Mock<DbSet<UserFunctionFarm>>();
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<UserFunctionFarm>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.UserFunctionFarms).Returns(mockDbSet.Object);
            var service = new UFFRepo(mockContext.Object);

            // Create a new instance of Farm to update
            var itemDelete = new UserFunctionFarm() { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc06", FarmId = 1, FunctionId = 2};

            // Act
            var result = service.DeleteAsync(itemDelete); // Await the UpdateAsync method

            // Assert
            await Task.Delay(100); // Delay for a short time to allow the asynchronous operation to complete
            Assert.NotNull(result);

        }
    }
}
