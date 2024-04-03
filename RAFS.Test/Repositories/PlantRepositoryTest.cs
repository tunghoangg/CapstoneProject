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

    public class PlantRepositoryTest
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
        public void PlantCheck_GetAllPlants()
        {
            var plantcount = 10;
            var plantData = new List<Plant>
        {
            new Plant { Id = 3,MilestoneId=6, Name = "Cành đào updated" },
            new Plant { Id = 7,MilestoneId=1, Name = "Cành đào 7" },
             new Plant { Id = 10,MilestoneId=3, Name = "Cành đào 10" },
            new Plant { Id = 12,MilestoneId=5, Name = "Cành đào 12" },
             new Plant { Id = 13,MilestoneId=6, Name = "Cành đào 13" },
            new Plant { Id = 18,MilestoneId=1, Name = "Cây Đào" },
             new Plant { Id = 19,MilestoneId=1, Name = "Cây Đào tessting" },
            new Plant { Id = 20,MilestoneId=1, Name = " Cây Đào testing 22222" },
             new Plant { Id = 23,MilestoneId=4, Name = " Cây Đào testing final iter 3" },
            new Plant { Id = 25,MilestoneId=3, Name = "Cây nho" }
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Plant>>();
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(() => plantData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSet.Object);

            var service = new PlantRepo(mockContext.Object);

            // Act
            var result = service.GetPlants();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Plant>>(result);
           
        }
        [Fact]
        public void GetPlantDiaries_Returns_PlantRecordDTO_List()
        {
            // Arrange
            int plantId = 7; // Specify the plantId for testing
            var plantData = new List<Plant>
        {
            new Plant{
                Id = 5,
                MilestoneId = 1,
                Name = "Sample Plant",
                Type = "Sample Type",
                Description = "This is a sample plant description.",
                Area = 10.5,
                AreaUnit = "sqft",
                HealthCondition = 90,
                PlantingMethod = "Sample Planting Method",
                Image = "sample_image.jpg",
                QRCode = "sample_qr_code.png",
                LastUpdate = DateTime.Now,
                Status = true
            }
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Plant>>();
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(() => plantData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSet.Object);
            var service = new PlantRepo(mockContext.Object);

            // Act
            var result = service.GetPlantDiaries(plantId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PlantRecordDTO>>(result);
        }
        [Fact]
        public void PlantCheck_GetPlantById()
        {
            var plantName = "Cành đào updated";
            var plantData = new List<Plant>
        {
            new Plant { Id = 3,MilestoneId=6, Name = "Cành đào updated" },
            new Plant { Id = 7,MilestoneId=1, Name = "Cành đào 7" },
             new Plant { Id = 10,MilestoneId=3, Name = "Cành đào 10" },
            new Plant { Id = 12,MilestoneId=5, Name = "Cành đào 12" },
             new Plant { Id = 13,MilestoneId=6, Name = "Cành đào 13" },
            new Plant { Id = 18,MilestoneId=1, Name = "Cây Đào" },
             new Plant { Id = 19,MilestoneId=1, Name = "Cây Đào tessting" },
            new Plant { Id = 20,MilestoneId=1, Name = " Cây Đào testing 22222" },
             new Plant { Id = 23,MilestoneId=4, Name = " Cây Đào testing final iter 3" },
            new Plant { Id = 25,MilestoneId=3, Name = "Cây nho" },
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Plant>>();
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(() => plantData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSet.Object);

            var service = new PlantRepo(mockContext.Object);


            // Act
            var result = service.GetPlantById(3);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Plant>(result);
            Assert.Equal(plantName, result.Name);
        }
        [Fact]
        public void PlantCheck_CreatePlant()
        {
            var resultStatus = 1;
            var plantData = new List<Plant>
        {
            new Plant { Id = 3,MilestoneId=6, Name = "Cành đào updated" },
            new Plant { Id = 7,MilestoneId=1, Name = "Cành đào 7" },
             new Plant { Id = 10,MilestoneId=3, Name = "Cành đào 10" },
            new Plant { Id = 12,MilestoneId=5, Name = "Cành đào 12" },
             new Plant { Id = 13,MilestoneId=6, Name = "Cành đào 13" },
            new Plant { Id = 18,MilestoneId=1, Name = "Cây Đào" },
             new Plant { Id = 19,MilestoneId=1, Name = "Cây Đào tessting" },
            new Plant { Id = 20,MilestoneId=1, Name = " Cây Đào testing 22222" },
             new Plant { Id = 23,MilestoneId=4, Name = " Cây Đào testing final iter 3" },
            new Plant { Id = 25,MilestoneId=3, Name = "Cây nho" },
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Plant>>();
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(() => plantData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSet.Object);

            var service = new PlantRepo(mockContext.Object);


            Plant testData = new Plant();
            testData.MilestoneId =1 ;
            testData.Name = "test Fact";
            testData.Description = "tester";
            testData.Type = "cay canh";
            testData.Area = 1;
            testData.AreaUnit = "m2";
            testData.HealthCondition = 1;
            testData.PlantingMethod = "thu cong";
            testData.Image = "alt";
            testData.LastUpdate = DateTime.Now;
            testData.Status =true;

            // Act
            var result = service.CreatePlant(testData);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
            
            //Assert.Equal(resultStatus, result);
        }
        [Fact]
        public void PlantCheck_UpdatePlant()
        {
            var milestoneName = true;
            var plantData = new List<Plant>
        {
            new Plant { Id = 3,MilestoneId=6, Name = "Cành đào updated" },
            new Plant { Id = 7,MilestoneId=1, Name = "Cành đào 7" },
             new Plant { Id = 10,MilestoneId=3, Name = "Cành đào 10" },
            new Plant { Id = 12,MilestoneId=5, Name = "Cành đào 12" },
             new Plant { Id = 13,MilestoneId=6, Name = "Cành đào 13" },
            new Plant { Id = 18,MilestoneId=1, Name = "Cây Đào" },
             new Plant { Id = 19,MilestoneId=1, Name = "Cây Đào tessting" },
            new Plant { Id = 20,MilestoneId=1, Name = " Cây Đào testing 22222" },
             new Plant { Id = 23,MilestoneId=4, Name = " Cây Đào testing final iter 3" },
            new Plant { Id = 25,MilestoneId=3, Name = "Cây nho" },
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Plant>>();
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(() => plantData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSet.Object);

            var service = new PlantRepo(mockContext.Object);

            Plant testData = new Plant();
            testData.Id = 3;
            testData.MilestoneId = 1;
            testData.Name = "test Fact";
            testData.Description = "tester";
            testData.Type = "cay canh";
            testData.Area = 1;
            testData.AreaUnit = "m2";
            testData.HealthCondition = 1;
            testData.PlantingMethod = "thu cong";
            testData.Image = "alt";
            testData.LastUpdate = DateTime.Now;
            testData.Status = true;

            // Act
            var result = service.UpdatePlant(testData);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void PlantCheck_DeletePlant()
        {
            var milestoneName = true;
            var plantData = new List<Plant>
        {
            new Plant { Id = 3,MilestoneId=6, Name = "Cành đào updated" },
            new Plant { Id = 7,MilestoneId=1, Name = "Cành đào 7" },
             new Plant { Id = 10,MilestoneId=3, Name = "Cành đào 10" },
            new Plant { Id = 12,MilestoneId=5, Name = "Cành đào 12" },
             new Plant { Id = 13,MilestoneId=6, Name = "Cành đào 13" },
            new Plant { Id = 18,MilestoneId=1, Name = "Cây Đào" },
             new Plant { Id = 19,MilestoneId=1, Name = "Cây Đào tessting" },
            new Plant { Id = 20,MilestoneId=1, Name = " Cây Đào testing 22222" },
             new Plant { Id = 23,MilestoneId=4, Name = " Cây Đào testing final iter 3" },
            new Plant { Id = 25,MilestoneId=3, Name = "Cây nho" },
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Plant>>();
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(() => plantData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSet.Object);

            var service = new PlantRepo(mockContext.Object);



            // Act
            var result = service.DeletePlant(3);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
    }

}
