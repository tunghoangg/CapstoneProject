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

    public class MilestoneRepositoryTest 
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
        public void MilestoneCheck_GetAllMilestone()
        {
            var milestoneName = 6;
            var milestoneData = new List<Milestone>
        {
            new Milestone { Id = 1,FarmId=1, Name = "MS1" },
            new Milestone { Id = 3,FarmId=1, Name = "MS2" },
             new Milestone { Id = 4,FarmId=1, Name = "MS3" },
            new Milestone { Id = 5,FarmId=1, Name = "MS4" },
             new Milestone { Id = 6,FarmId=1, Name = "MS6" },
            new Milestone { Id = 18,FarmId=1, Name = "MS7" },
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Milestone>>();
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Milestones).Returns(mockDbSet.Object);

            var service = new MilestoneRepo(mockContext.Object);

            // Act
            var result = service.GetMilestones(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Milestone>>(result);
            
        }

        [Fact]
        public void MilestoneCheck_GetMilestoneById()
        {
            var milestoneName = "MS1";
            var milestoneData = new List<Milestone>
        {
            new Milestone { Id = 1,FarmId=1, Name = "MS1"}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Milestone>>();
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Milestones).Returns(mockDbSet.Object);

            var service = new MilestoneRepo(mockContext.Object);

            // Act
            var result = service.GetMilestonesById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Milestone>(result);
            Assert.Equal(milestoneName, result.Name);
        }
        [Fact]
        public void MilestoneCheck_CreateMilestone()
        {
            var milestoneName = true;
            var milestoneData = new List<Milestone>
        {
            new Milestone { Id = 1,FarmId=1, Name = "MS1"}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Milestone>>();
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Milestones).Returns(mockDbSet.Object);

            var service = new MilestoneRepo(mockContext.Object);

            Milestone testData = new Milestone();
            testData.FarmId = 1;
            testData.Name = "MS8";
            testData.LastUpdate = DateTime.Now;
            testData.Status = true;

            // Act
            var result = service.CreateMilestone(testData);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void MilestoneCheck_UpdateMilestone()
        {
            var milestoneName = true;
            var milestoneData = new List<Milestone>
        {
            new Milestone { Id = 1,FarmId=1, Name = "MS1"}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Milestone>>();
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Milestones).Returns(mockDbSet.Object);

            var service = new MilestoneRepo(mockContext.Object);

            Milestone testData = new Milestone();
            testData.Id = 1;
            testData.FarmId = 1;
            testData.Name = "MS8";
            testData.LastUpdate = DateTime.Now;
            testData.Status = true;

            // Act
            var result = service.UpdateMilestone(testData);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void MilestoneCheck_DeleteMilestone()
        {
            var milestoneName = true;
            var milestoneData = new List<Milestone>
        {
            new Milestone { Id = 1,FarmId=1, Name = "MS1"}
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Milestone>>();
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Provider).Returns(milestoneData.Provider);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.Expression).Returns(milestoneData.Expression);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.ElementType).Returns(milestoneData.ElementType);
            mockDbSet.As<IQueryable<Milestone>>().Setup(m => m.GetEnumerator()).Returns(() => milestoneData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Milestones).Returns(mockDbSet.Object);

            var service = new MilestoneRepo(mockContext.Object);


            // Act
            var result = service.DeleteMilestone(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.Equal(milestoneName, result);
        }
        [Fact]
        public void MilestoneCheck_GetPlantOfMilestone()
        {
            var milestoneName = true;
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
            var service = new MilestoneRepo(mockContext.Object);


            // Act
            var result = service.GetPlantOfMilestone(8).Count;
            Assert.NotNull(result);
            //Assert.Equal(1, result);
        }
    }

}
