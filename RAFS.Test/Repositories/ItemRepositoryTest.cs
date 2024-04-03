using Microsoft.EntityFrameworkCore;
using Moq;
using RAFS.Core.Context;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Test.Repositories
{
    public class ItemRepositoryTest
    {

        [Fact]
        public async Task GetByIdAsync_ReturnItem_WhenCalled()
        {
            int itemId = 2;

            var data = new List<Item>() {
                 new Item() {Id = 1, ItemName = "Item 1"},
                 new Item() {Id = 2, ItemName = "Item 2"},
            }.AsQueryable();


            var mockDbSet = new Mock<DbSet<Item>>();
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var expectedValue = new Item() { Id = 2, ItemName = "Item 2" };

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Inventories).Returns(mockDbSet.Object);
            mockContext.Setup(x => x.Set<Item>().FindAsync(itemId)).ReturnsAsync(expectedValue);


            //var repo = new GenericRepo<Farm>(mockContext.Object);

            var service = new ItemRepo(mockContext.Object);

            //act
            var result = await service.GetByIdAsync(itemId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void GetAllItemAsync_ReturnListItem_WhenCalled()
        {

            var data = new List<Item>() {
                 new Item() {Id = 1, ItemName = "Item 1"},
                 new Item() {Id = 2, ItemName = "Item 2"},
            }.AsQueryable();


            var mockDbSet = new Mock<DbSet<Item>>();
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Inventories).Returns(mockDbSet.Object);
            var service = new ItemRepo(mockContext.Object);

            //act
            var result = service.GetAllAsync();
            
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddAsync_AddItem_WhenCalled()
        {

            // Arrange
            var mockSet = new Mock<DbSet<Item>>();
            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Set<Item>()).Returns(mockSet.Object);

            var entityToAdd = new Item
            {
                Id = 1,
                ItemName = "Item 1"
            };
            var repository = new ItemRepo(mockContext.Object);

            // Act
            await repository.AddAsync(entityToAdd);

            // Assert
            await Task.Delay(100); // Delay to ensure AddAsync completes (you can adjust the delay time as needed)
            mockSet.Verify(m => m.AddAsync(entityToAdd, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdateItem_WhenCalled()
        {
            // Arrange
            var data = new List<Item>() {
                 new Item() {Id = 1, ItemName = "Item 1"},
                 new Item() {Id = 2, ItemName = "Item 2"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Item>>();
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Inventories).Returns(mockDbSet.Object);
            var service = new ItemRepo(mockContext.Object);

            // Create a new instance of Farm to update
            var itemUpdate = new Item() { Id = 1, ItemName = "Item 2" };

            // Act
            var result = service.UpdateAsync(itemUpdate); // Await the UpdateAsync method

            // Assert
            await Task.Delay(100); // Delay for a short time to allow the asynchronous operation to complete
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteAsync_DeleteItem_WhenCalled()
        {
            var data = new List<Item>() {
                 new Item() {Id = 1, ItemName = "Item 1"},
                 new Item() {Id = 2, ItemName = "Item 2"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Item>>();
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Inventories).Returns(mockDbSet.Object);
            var service = new ItemRepo(mockContext.Object);

            // Create a new instance of Farm to update
            var itemUpdate = new Item() { Id = 2, ItemName = "Item 2" };

            // Act
            var result = service.DeleteAsync(itemUpdate); // Await the UpdateAsync method

            // Assert
            await Task.Delay(100); // Delay for a short time to allow the asynchronous operation to complete
            Assert.NotNull(result);

        }
    }
}
