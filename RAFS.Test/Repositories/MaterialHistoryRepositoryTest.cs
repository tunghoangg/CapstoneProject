using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RAFS.Core.Context;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Test.Repositories
{

    public class MaterialHistoryRepositoryTest
    {
        public static DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
      

        [Fact]
        public void MaterialHistoryCheck_CreatePlantMaterialRecord()
        {
            var milestoneName = true;
            var milestoneData = new List<TakeAndSendMaterial>
        {
            new TakeAndSendMaterial { Id = 1,InventoryId=1,PlantId=1,Quality="MS8",Status=true}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<TakeAndSendMaterial>>();
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.TakeAndSendMaterials).Returns(mockDbSet.Object);

            var service = new PlantMaterialHistoryRepo(mockContext.Object);

            TakeAndSendMaterial testData = new TakeAndSendMaterial();
          
            testData.InventoryId = 1;
            testData.PlantId = 1;
            testData.Quality = "1";
            testData.Status = true;

            // Act
            var result = service.CreatePlantMaterialRecord(testData);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void MaterialHistoryCheck_UpdatePlantMaterialRecord()
        {
            var milestoneName = true;
            var milestoneData = new List<TakeAndSendMaterial>
        {
            new TakeAndSendMaterial { Id = 1,InventoryId=1,PlantId=1,Quality="MS8",Status=true}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<TakeAndSendMaterial>>();
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.TakeAndSendMaterials).Returns(mockDbSet.Object);

            var service = new PlantMaterialHistoryRepo(mockContext.Object);

            TakeAndSendMaterial testData = new TakeAndSendMaterial();
            testData.Id =1;
            testData.InventoryId = 1;
            testData.PlantId = 1;
            testData.Quality = "1";
            testData.Status = true;

            // Act
            var result = service.UpdatePlantMaterialRecord(testData);


            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void MaterialHistoryCheck_DeletePlantMaterialRecord()
        {
            var milestoneName = true;
            var milestoneData = new List<TakeAndSendMaterial>
        {
            new TakeAndSendMaterial { Id = 1,InventoryId=1,PlantId=1,Quality="MS8",Status=true}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<TakeAndSendMaterial>>();
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.TakeAndSendMaterials).Returns(mockDbSet.Object);

            var service = new PlantMaterialHistoryRepo(mockContext.Object);

            // Act
            var result = service.DeletePlantMaterialRecord(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
    

        [Fact]
        public void GetPlantMaterialHistory_Returns_CorrectMaterialHistory()
        {
            // Arrange
            int plantId = 1; // Specify the plantId for testing

            // Mock items and plants data
            var itemData = new List<Item>
            {
                new Item { Id = 1, ItemName = "Item 1", TypeId = 1 },
                new Item { Id = 2, ItemName = "Item 2", TypeId = 2 }
                // Add more sample item data as needed
            }.AsQueryable();

            var plantData = new List<Plant>
            {
                new Plant { Id = 1, Name = "Plant 1" },
                new Plant { Id = 2, Name = "Plant 2" }
                // Add more sample plant data as needed
            }.AsQueryable();

            var takeAndSendMaterialData = new List<TakeAndSendMaterial>
            {
                new TakeAndSendMaterial { Id = 1, PlantId = 1, InventoryId = 1, Quality = "High", LastUpdate = DateTime.Now, Status = true },
                new TakeAndSendMaterial { Id = 2, PlantId = 1, InventoryId = 2, Quality = "Low", LastUpdate = DateTime.Now, Status = true }
                // Add more sample takeAndSendMaterial data as needed
            }.AsQueryable();

            var mockDbSetItem = new Mock<DbSet<Item>>();
            var mockDbSetPlant = new Mock<DbSet<Plant>>();
            var mockDbSetTakeAndSendMaterial = new Mock<DbSet<TakeAndSendMaterial>>();

            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(itemData.Provider);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(itemData.Expression);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(itemData.ElementType);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(itemData.GetEnumerator());

            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(plantData.GetEnumerator());

            mockDbSetTakeAndSendMaterial.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Provider).Returns(takeAndSendMaterialData.Provider);
            mockDbSetTakeAndSendMaterial.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Expression).Returns(takeAndSendMaterialData.Expression);
            mockDbSetTakeAndSendMaterial.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.ElementType).Returns(takeAndSendMaterialData.ElementType);
            mockDbSetTakeAndSendMaterial.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.GetEnumerator()).Returns(takeAndSendMaterialData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Inventories).Returns(mockDbSetItem.Object);
            mockContext.Setup(c => c.Plants).Returns(mockDbSetPlant.Object);
            mockContext.Setup(c => c.TakeAndSendMaterials).Returns(mockDbSetTakeAndSendMaterial.Object);

            var service = new PlantMaterialHistoryRepo(mockContext.Object);

            // Act
            var result = service.GetPlantMaterialHistory(plantId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakeAndSendMaterialDTO>>(result);
            // Add more assertions as needed to validate the returned data
        }
        [Fact]
        public void GetPlantMaterialHistoryByType_Returns_CorrectMaterialHistory()
        {
            // Arrange
            int plantId = 1; // Specify the plantId for testing
            int typeId = 1; // Specify the typeId for testing

            // Mock items and plants data
            var itemData = new List<Item>
            {
                new Item { Id = 1, ItemName = "Item 1", TypeId = 1 },
                new Item { Id = 2, ItemName = "Item 2", TypeId = 2 }
                // Add more sample item data as needed
            }.AsQueryable();

            var plantData = new List<Plant>
            {
                new Plant { Id = 1, Name = "Plant 1" },
                new Plant { Id = 2, Name = "Plant 2" }
                // Add more sample plant data as needed
            }.AsQueryable();

            var takeAndSendMaterialData = new List<TakeAndSendMaterial>
            {
                new TakeAndSendMaterial { Id = 1, PlantId = 1, InventoryId = 1, Quality = "High", LastUpdate = DateTime.Now, Status = true },
                new TakeAndSendMaterial { Id = 2, PlantId = 1, InventoryId = 2, Quality = "Low", LastUpdate = DateTime.Now, Status = true }
                // Add more sample takeAndSendMaterial data as needed
            }.AsQueryable();

            var mockDbSetItem = new Mock<DbSet<Item>>();
            var mockDbSetPlant = new Mock<DbSet<Plant>>();
            var mockDbSetTakeAndSendMaterial = new Mock<DbSet<TakeAndSendMaterial>>();

            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(itemData.Provider);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(itemData.Expression);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(itemData.ElementType);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(itemData.GetEnumerator());

            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(plantData.GetEnumerator());

            mockDbSetTakeAndSendMaterial.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Provider).Returns(takeAndSendMaterialData.Provider);
            mockDbSetTakeAndSendMaterial.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Expression).Returns(takeAndSendMaterialData.Expression);
            mockDbSetTakeAndSendMaterial.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.ElementType).Returns(takeAndSendMaterialData.ElementType);
            mockDbSetTakeAndSendMaterial.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.GetEnumerator()).Returns(takeAndSendMaterialData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Inventories).Returns(mockDbSetItem.Object);
            mockContext.Setup(c => c.Plants).Returns(mockDbSetPlant.Object);
            mockContext.Setup(c => c.TakeAndSendMaterials).Returns(mockDbSetTakeAndSendMaterial.Object);

            var service = new PlantMaterialHistoryRepo(mockContext.Object);

            // Act
            var result = service.GetPlantMaterialHistoryByType(plantId, typeId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakeAndSendMaterialDTO>>(result);
            // Add more assertions as needed to validate the returned data
        }
        [Fact]
        public void GetPlantMaterialRecord_Returns_CorrectMaterialRecord()
        {
            // Arrange
            int milestoneId = 1; // Specify the milestoneId for testing

            // Mock diaries data
            var diaryData = new List<TakeAndSendMaterial>
            {
                new TakeAndSendMaterial { Id = 1, PlantId = 1, InventoryId = 1, Quality = "High", LastUpdate = DateTime.Now, Status = true },
                new TakeAndSendMaterial { Id = 2, PlantId = 1, InventoryId = 2, Quality = "Low", LastUpdate = DateTime.Now, Status = true }
                // Add more sample diary data as needed
            }.AsQueryable();

            var mockDbSetDiary = new Mock<DbSet<TakeAndSendMaterial>>();

            mockDbSetDiary.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Provider).Returns(diaryData.Provider);
            mockDbSetDiary.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Expression).Returns(diaryData.Expression);
            mockDbSetDiary.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.ElementType).Returns(diaryData.ElementType);
            mockDbSetDiary.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.GetEnumerator()).Returns(diaryData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.TakeAndSendMaterials).Returns(mockDbSetDiary.Object);

            var service = new PlantMaterialHistoryRepo(mockContext.Object);

            // Act
            var result = service.GetPlantMaterialRecord(milestoneId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TakeAndSendMaterial>(result);
            // Add more assertions as needed to validate the returned data
        }

        [Fact]
        public void GetFarmMaterialHistory_Returns_CorrectMaterialHistory()
        {
            // Arrange
            int farmId = 1; // Specify the farmId for testing

            // Mock plants, items, and diaries data
            var plantData = new List<Plant>
            {
                new Plant { Id = 1, MilestoneId = 1, Name = "Plant 1", Status = true },
                new Plant { Id = 2, MilestoneId = 1, Name = "Plant 2", Status = true }
                // Add more sample plant data as needed
            }.AsQueryable();

            var itemData = new List<Item>
            {
                new Item { Id = 1, ItemName = "Item 1", TypeId = 1 },
                new Item { Id = 2, ItemName = "Item 2", TypeId = 2 }
                // Add more sample item data as needed
            }.AsQueryable();

            var diaryData = new List<TakeAndSendMaterial>
            {
                new TakeAndSendMaterial { Id = 1, PlantId = 1, InventoryId = 1, Quality = "High", LastUpdate = DateTime.Now, Status = true },
                new TakeAndSendMaterial { Id = 2, PlantId = 1, InventoryId = 2, Quality = "Low", LastUpdate = DateTime.Now, Status = true }
                // Add more sample diary data as needed
            }.AsQueryable();

            var mockDbSetPlant = new Mock<DbSet<Plant>>();
            var mockDbSetItem = new Mock<DbSet<Item>>();
            var mockDbSetDiary = new Mock<DbSet<TakeAndSendMaterial>>();

            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(plantData.GetEnumerator());

            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(itemData.Provider);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(itemData.Expression);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(itemData.ElementType);
            mockDbSetItem.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(itemData.GetEnumerator());

            mockDbSetDiary.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Provider).Returns(diaryData.Provider);
            mockDbSetDiary.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.Expression).Returns(diaryData.Expression);
            mockDbSetDiary.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.ElementType).Returns(diaryData.ElementType);
            mockDbSetDiary.As<IQueryable<TakeAndSendMaterial>>().Setup(m => m.GetEnumerator()).Returns(diaryData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSetPlant.Object);
            mockContext.Setup(c => c.Inventories).Returns(mockDbSetItem.Object);
            mockContext.Setup(c => c.TakeAndSendMaterials).Returns(mockDbSetDiary.Object);

            var service = new PlantMaterialHistoryRepo(mockContext.Object);

            // Act
            var result = service.GetFarmMaterialHistory(farmId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakeAndSendMaterialDTO>>(result);
            // Add more assertions as needed to validate the returned data
        }
    }

}
