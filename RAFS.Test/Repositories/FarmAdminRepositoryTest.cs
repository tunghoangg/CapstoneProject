using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RAFS.Core.Context;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Test.Repositories
{
    public class FarmAdminRepositoryTest
    {

        [Fact]
        public void GetFarmByUserIdAsync_GetFarm_WhenCalled()
        {
            string userId = "9b949f70-b0c4-4b56-912a-b305543adc05";

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

            var service = new FarmAdminRepo(mockContext.Object);

            //act
            var result = service.GetFarmByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.IsType<Farm>(result);
        }

        [Fact]
        public void GetFarmAdminDTOsAsync_GetFarm_WhenCalled()
        {
            string userId = "9b949f70-b0c4-4b56-912a-b305543adc06";

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
                new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc06", FarmId = 1},
                new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc06", FarmId = 2},
                new UserFunctionFarm { AspUserId = "9b949f70-b0c4-4b56-912a-b305543adc05", FarmId = 2},
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

            var service = new FarmAdminRepo(mockContext.Object);

            //act
            var result = service.GetFarmAdminDTOsAsync(userId);

            Assert.NotNull(result);

        }

        [Fact]
        public async Task GetByIdAsync_ReturnFarm_WhenCalled()
        {
            int farmId = 1;

            var farmData = new List<Farm>() {
                 new Farm {Id = 1, Code = "FARM01"},
                 new Farm {Id = 2, Code = "FARM02"},
            }.AsQueryable();


            var mockDbSetFarm = new Mock<DbSet<Farm>>();
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmData.Provider);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmData.Expression);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmData.ElementType);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmData.GetEnumerator());

            var expectedFarm = new Farm { Id = 1, Code = "FARM01" };

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Farms).Returns(mockDbSetFarm.Object);
            mockContext.Setup(x => x.Set<Farm>().FindAsync(farmId)).ReturnsAsync(expectedFarm);


            //var repo = new GenericRepo<Farm>(mockContext.Object);

            var service = new FarmAdminRepo(mockContext.Object);

            //act
            Farm result = await service.GetByIdAsync(farmId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedFarm, result);
            Assert.Equal("FARM01", result.Code);
        }

        [Fact]
        public void GetAllFarmsAsync_ReturnListFarm_WhenCalled()
        {

            var farmData = new List<Farm>() {
                 new Farm {Id = 1, Code = "FARM01"},
                 new Farm {Id = 2, Code = "FARM02"},
            }.AsQueryable();


            var mockDbSetFarm = new Mock<DbSet<Farm>>();
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmData.Provider);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmData.Expression);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmData.ElementType);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Farms).Returns(mockDbSetFarm.Object);

            var service = new FarmAdminRepo(mockContext.Object);

            //act
            var result = service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddAsync_AddFarm_WhenCalled()
        {

            // Arrange
            var mockSet = new Mock<DbSet<Farm>>();
            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Set<Farm>()).Returns(mockSet.Object);

            var entityToAdd = new Farm
            {
                Id = 12,
                Code = "FWRM001",
                Name = "Sunrise Acres",
                Address = "456 Oak Avenue",
                Phone = "987-654-3210",
                Logo = "sunriseacres_logo.png",
                PageLink = "http://www.sunriseacres.com",
                Area = 75.8, // in acres
                EstablishedDate = new DateTime(1995, 10, 20),
                CreatedDate = DateTime.Now, // two years ago
                Status = true, // Active
                Description = "Sunrise Acres specializes in sustainable farming practices...",
                LastUpdate = DateTime.Now // last updated 45 days ago
            };
            var repository = new GenericRepo<Farm>(mockContext.Object);

            // Act
            await repository.AddAsync(entityToAdd);

            // Assert
            await Task.Delay(100); // Delay to ensure AddAsync completes (you can adjust the delay time as needed)
            mockSet.Verify(m => m.AddAsync(entityToAdd, It.IsAny<CancellationToken>()), Times.Once);
            //mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdateFarm_WhenCalled()
        {
            // Arrange
            var farmData = new List<Farm>() {
                new Farm { Id = 1, Code = "FARM01", Name = "Old" },
                new Farm { Id = 2, Code = "FARM02", Name = "Old2"  },
            }.AsQueryable();

            var mockDbSetFarm = new Mock<DbSet<Farm>>();
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmData.Provider);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmData.Expression);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmData.ElementType);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Farms).Returns(mockDbSetFarm.Object);

            // Create a new instance of GenericRepo<Farm> with the mock context
            var repository = new GenericRepo<Farm>(mockContext.Object);

            // Create a new instance of Farm to update
            var farmUpdate = new Farm()
            {
                Id = 1,
                Code = "FARM01",
                Name = "New"
            };

            // Act
            var result = repository.UpdateAsync(farmUpdate); // Await the UpdateAsync method

            // Assert
            await Task.Delay(100); // Delay for a short time to allow the asynchronous operation to complete
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteAsync_DeleteFarm_WhenCalled()
        {
            // Arrange
            var farmData = new List<Farm>() {
                new Farm { Id = 1, Code = "FARM01", Name = "Old" },
                new Farm { Id = 2, Code = "FARM02", Name = "Old2"  },
            }.AsQueryable();

            var mockDbSetFarm = new Mock<DbSet<Farm>>();
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmData.Provider);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmData.Expression);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmData.ElementType);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Farms).Returns(mockDbSetFarm.Object);

            //var repository = new GenericRepo<Farm>(mockContext.Object);
            var repository = new FarmAdminRepo(mockContext.Object);

            var farmToDelete = new Farm()
            {
                Id = 1,
                Code = "FARM01",
                Name = "Old"
            };

            // Act
            var result = repository.DeleteAsync(farmToDelete);
            Assert.NotNull(result);

        }

    }
}
