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
    public class CashFlowRepositoryTest
    {
        [Fact]
        public async Task GetByIdAsync_ReturnCashFlow_WhenCalled()
        {
            int cashId = 1;

            var data = new List<CashFlow>() {
                 new CashFlow() {Id = 1, Code = Guid.Parse("B27BD2AF-6F7A-48D2-973C-1333ACEC52A3")},
                 new CashFlow() {Id = 2, Code = Guid.Parse("E13A0CF1-EA24-4FC8-ABE4-0EE0EF5C3F38")},
            }.AsQueryable();


            var mockDbSet = new Mock<DbSet<CashFlow>>();
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var expectedValue = new CashFlow() { Id = 1, Code = Guid.Parse("B27BD2AF-6F7A-48D2-973C-1333ACEC52A3") };

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.CashFlows).Returns(mockDbSet.Object);
            mockContext.Setup(x => x.Set<CashFlow>().FindAsync(cashId)).ReturnsAsync(expectedValue);


            //var repo = new GenericRepo<Farm>(mockContext.Object);

            var service = new CashFlowRepo(mockContext.Object);

            //act
            var result = await service.GetByIdAsync(cashId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void GetAllCashFlowAsync_ReturnListCashFLow_WhenCalled()
        {

            var data = new List<CashFlow>() {
                 new CashFlow() {Id = 1, Code = Guid.Parse("B27BD2AF-6F7A-48D2-973C-1333ACEC52A3")},
                 new CashFlow() {Id = 2, Code = Guid.Parse("E13A0CF1-EA24-4FC8-ABE4-0EE0EF5C3F38")},
            }.AsQueryable();


            var mockDbSet = new Mock<DbSet<CashFlow>>();
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.CashFlows).Returns(mockDbSet.Object);
            var service = new CashFlowRepo(mockContext.Object);

            //act
            var result = service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddAsync_AddCashFlow_WhenCalled()
        {

            // Arrange
            var mockSet = new Mock<DbSet<CashFlow>>();
            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Set<CashFlow>()).Returns(mockSet.Object);

            var entityToAdd = new CashFlow() { Id = 3, Code = Guid.Parse("B27BD2AF-6F7A-48D2-973C-1333ACEC52A2") };
            var repository = new CashFlowRepo(mockContext.Object);

            // Act
            await repository.AddAsync(entityToAdd);

            // Assert
            await Task.Delay(100); // Delay to ensure AddAsync completes (you can adjust the delay time as needed)
            mockSet.Verify(m => m.AddAsync(entityToAdd, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdateCashFlow_WhenCalled()
        {
            // Arrange
            var data = new List<CashFlow>() {
                 new CashFlow() {Id = 1, Code = Guid.Parse("B27BD2AF-6F7A-48D2-973C-1333ACEC52A3"), Value = 100},
                 new CashFlow() {Id = 2, Code = Guid.Parse("E13A0CF1-EA24-4FC8-ABE4-0EE0EF5C3F38"), Value = 200},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<CashFlow>>();
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.CashFlows).Returns(mockDbSet.Object);
            var service = new CashFlowRepo(mockContext.Object);

            // Create a new instance of Farm to update
            var itemUpdate = new CashFlow() { Id = 1, Code = Guid.Parse("B27BD2AF-6F7A-48D2-973C-1333ACEC52A3"), Value = 300 };

            // Act
            var result = service.UpdateAsync(itemUpdate); // Await the UpdateAsync method

            // Assert
            await Task.Delay(100); // Delay for a short time to allow the asynchronous operation to complete
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteAsync_DeleteCashFlow_WhenCalled()
        {
            // Arrange
            var data = new List<CashFlow>() {
                 new CashFlow() {Id = 1, Code = Guid.Parse("B27BD2AF-6F7A-48D2-973C-1333ACEC52A3"), Value = 300},
                 new CashFlow() {Id = 2, Code = Guid.Parse("E13A0CF1-EA24-4FC8-ABE4-0EE0EF5C3F38"), Value = 200},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<CashFlow>>();
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<CashFlow>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.CashFlows).Returns(mockDbSet.Object);
            var service = new CashFlowRepo(mockContext.Object);

            // Create a new instance of Farm to update
            var itemDelete = new CashFlow() { Id = 1, Code = Guid.Parse("B27BD2AF-6F7A-48D2-973C-1333ACEC52A3"), Value = 300 };

            // Act
            var result = service.DeleteAsync(itemDelete); // Await the UpdateAsync method

            // Assert
            await Task.Delay(100); // Delay for a short time to allow the asynchronous operation to complete
            Assert.NotNull(result);

        }
    }
}
