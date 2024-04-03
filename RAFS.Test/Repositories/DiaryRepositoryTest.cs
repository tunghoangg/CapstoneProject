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

    public class DiaryRepositoryTest
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
        public void GetDiaries_Returns_DiaryDTO_List()
        {
            // Arrange
            int farmId = 1; // Specify the farmId for testing

            // Mock farms, milestones, plants, and diaries data
            var farmData = new List<Farm>
        {
            new Farm { Id = 1, Status = true, Milestones = new List<Milestone>
                {
                    new Milestone { Id = 1, Plants = new List<Plant>
                        {
                            new Plant { Id = 1, Name = "Plant 1", Status = true }, // Sample plant data
                            new Plant { Id = 2, Name = "Plant 2", Status = true }
                            // Add more sample plant data as needed
                        }
                    }
                }
            }
            // Add more sample farm data as needed
        }.AsQueryable();

            var diaryData = new List<Diary>
        {
            new Diary { Id = 1, PlantId = 1, Title = "Diary 1", Type = 1, Body = "Body 1", LastUpdate = DateTime.Now, Status = true }, // Sample diary data
            new Diary { Id = 2, PlantId = 2, Title = "Diary 2", Type = 2, Body = "Body 2", LastUpdate = DateTime.Now, Status = true }
            // Add more sample diary data as needed
        }.AsQueryable();

            var mockDbSetFarm = new Mock<DbSet<Farm>>();
            var mockDbSetDiary = new Mock<DbSet<Diary>>();
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmData.Provider);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmData.Expression);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmData.ElementType);
            mockDbSetFarm.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(farmData.GetEnumerator());
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.Provider).Returns(diaryData.Provider);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.Expression).Returns(diaryData.Expression);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.ElementType).Returns(diaryData.ElementType);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.GetEnumerator()).Returns(diaryData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Farms).Returns(mockDbSetFarm.Object);
            mockContext.Setup(c => c.Diaries).Returns(mockDbSetDiary.Object);

            var service = new DiaryRepo(mockContext.Object);

            // Act
            var result = service.GetDiaries(farmId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<DiaryDTO>>(result);
            // Add more assertions as needed to validate the returned data
        }

        [Fact]
        public void DiaryCheck_CreateDiary()
        {
            var milestoneName = true;
            var milestoneData = new List<Diary>
        {
            new Diary { Id = 1,PlantId=1, Title="MS8",Type=1,Body="MS8",Image="MS8",CreatedDay=DateTime.Now,LastUpdate=DateTime.Now,Status=true}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Diary>>();
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Diaries).Returns(mockDbSet.Object);

            var service = new DiaryRepo(mockContext.Object);

            Diary testData = new Diary();
            testData.PlantId = 1;
            testData.Title = "MS8";
            testData.Type = 1;
            testData.Body = "MS8";
            testData.Image = "MS8";
            testData.CreatedDay = DateTime.Now;
            testData.LastUpdate = DateTime.Now;
            testData.Status = true;

            // Act
            var result = service.CreateDiary(testData);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void DiaryCheck_UpdateDiary()
        {
            var milestoneName = true;
            var milestoneData = new List<Diary>
        {
            new Diary { Id = 1,PlantId=1, Title="MS8",Type=1,Body="MS8",Image="MS8",CreatedDay=DateTime.Now,LastUpdate=DateTime.Now,Status=true}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Diary>>();
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Diaries).Returns(mockDbSet.Object);

            var service = new DiaryRepo(mockContext.Object);

            Diary testData = new Diary();
            testData.Id = 1;
            testData.PlantId = 1;
            testData.Title = "MS8";
            testData.Type = 1;
            testData.Body = "MS8";
            testData.Image = "MS9";
            testData.CreatedDay = DateTime.Now;
            testData.LastUpdate = DateTime.Now;
            testData.Status = true;

            // Act
            var result = service.UpdateDiary(testData);


            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void DiaryCheck_DeleteDiary()
        {
            var milestoneName = true;
            var milestoneData = new List<Diary>
        {
            new Diary { Id = 1,PlantId=1, Title="MS8",Type=1,Body="MS8",Image="MS8",CreatedDay=DateTime.Now,LastUpdate=DateTime.Now,Status=true}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Diary>>();
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<Diary>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Diaries).Returns(mockDbSet.Object);

            var service = new DiaryRepo(mockContext.Object);

            // Act
            var result = service.DeleteDiary(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void GetPlantDiaries_Returns_DiaryDTO_List()
        {
            // Arrange
            int plantId = 1; // Specify the plantId for testing

            // Mock plants and diaries data
            var plantData = new List<Plant>
        {
            new Plant { Id = 1, Name = "Plant 1" },
            new Plant { Id = 2, Name = "Plant 2" }
            // Add more sample plant data as needed
        }.AsQueryable();

            var diaryData = new List<Diary>
        {
            new Diary { Id = 1, PlantId = 1, Title = "Diary 1", Type = 1, Body = "Body 1", LastUpdate = DateTime.Now, Status = true }, // Sample diary data
            new Diary { Id = 2, PlantId = 1, Title = "Diary 2", Type = 2, Body = "Body 2", LastUpdate = DateTime.Now, Status = true }
            // Add more sample diary data as needed
        }.AsQueryable();

            var mockDbSetPlant = new Mock<DbSet<Plant>>();
            var mockDbSetDiary = new Mock<DbSet<Diary>>();
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(plantData.GetEnumerator());
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.Provider).Returns(diaryData.Provider);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.Expression).Returns(diaryData.Expression);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.ElementType).Returns(diaryData.ElementType);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.GetEnumerator()).Returns(diaryData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSetPlant.Object);
            mockContext.Setup(c => c.Diaries).Returns(mockDbSetDiary.Object);

            var service = new DiaryRepo(mockContext.Object);

            // Act
            var result = service.GetPlantDiaries(plantId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<DiaryDTO>>(result);
            // Add more assertions as needed to validate the returned data
        }
        [Fact]
        public void GetDiaryById_Returns_CorrectDiary()
        {
            // Arrange
            int diaryId = 1; // Specify the diaryId for testing

            var diaryData = new List<Diary>
        {
            new Diary { Id = 1, /* Add other properties here */ },
            new Diary { Id = 2, /* Add other properties here */ },
            // Add more sample diary data as needed
        }.AsQueryable();

            var mockDbSetDiary = new Mock<DbSet<Diary>>();
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.Provider).Returns(diaryData.Provider);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.Expression).Returns(diaryData.Expression);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.ElementType).Returns(diaryData.ElementType);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.GetEnumerator()).Returns(diaryData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Diaries).Returns(mockDbSetDiary.Object);

            var service = new DiaryRepo(mockContext.Object);

            // Act
            var result = service.GetDiaryById(diaryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Diary>(result);
            Assert.Equal(diaryId, result.Id);
            // Add more assertions as needed to validate the returned diary object
        }
        [Fact]
        public void GetPlantDiariesByType_Returns_CorrectDiaryDTOs()
        {
            // Arrange
            int plantId = 1; // Specify the plantId for testing
            int type = 1; // Specify the type for testing

            // Mock plants and diaries data
            var plantData = new List<Plant>
        {
            new Plant { Id = 1, Name = "Plant 1" },
            new Plant { Id = 2, Name = "Plant 2" }
            // Add more sample plant data as needed
        }.AsQueryable();

            var diaryData = new List<Diary>
        {
            new Diary { Id = 1, PlantId = 1, Type = 1, Title = "Diary 1", /* Add other properties here */ },
            new Diary { Id = 2, PlantId = 1, Type = 2, Title = "Diary 2", /* Add other properties here */ },
            // Add more sample diary data as needed
        }.AsQueryable();

            var mockDbSetPlant = new Mock<DbSet<Plant>>();
            var mockDbSetDiary = new Mock<DbSet<Diary>>();
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            mockDbSetPlant.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(plantData.GetEnumerator());
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.Provider).Returns(diaryData.Provider);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.Expression).Returns(diaryData.Expression);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.ElementType).Returns(diaryData.ElementType);
            mockDbSetDiary.As<IQueryable<Diary>>().Setup(m => m.GetEnumerator()).Returns(diaryData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Plants).Returns(mockDbSetPlant.Object);
            mockContext.Setup(c => c.Diaries).Returns(mockDbSetDiary.Object);

            var service = new DiaryRepo(mockContext.Object);

            // Act
            var result = service.GetPlantDiariesByType(plantId, type);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<DiaryDTO>>(result);
            // Add more assertions as needed to validate the returned data
        }
    }

}
