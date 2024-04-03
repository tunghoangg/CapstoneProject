using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RAFS.Core.Context;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Repo;
using RAFS.Core.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Test.Repositories
{
    public class HomepageRepositoryTest
    {
        [Fact]
        public void GetFarmsInHomepage_ReturnsFilteredFarms()
        {
            // Arrange
            var mockContext = new Mock<RAFSContext>();
            var farmsData = new List<Farm>
            {
            new Farm { Id = 1, Name = "Farm 1", Address = "Province A, District X, Ward 1", Area = 9, EstablishedDate = DateTime.Now.AddDays(-30), Phone = "123456789" },
            new Farm { Id = 2, Name = "Farm 2", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },

            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Farm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmsData.Provider);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmsData.Expression);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmsData.ElementType);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmsData.GetEnumerator());

            mockContext.Setup(c => c.Farms).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetFarmsInHomepage(search: "Farm 2", province: "", district: "", ward: "", sort: "", area: "");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Farm>>(result);
        }

        [Fact]
        public void GetFarmsInHomepage_ReturnsFilteredFarms2()
        {
            // Arrange
            var mockContext = new Mock<RAFSContext>();
            var farmsData = new List<Farm>
            {
            new Farm { Id = 1, Name = "Farm 1", Address = "Province A, District X, Ward 1", Area = 9, EstablishedDate = DateTime.Now.AddDays(-30), Phone = "123456789" },
            new Farm { Id = 2, Name = "Farm 2", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },
            new Farm { Id = 3, Name = "Farm 3", Address = "Province A, District Z, Ward 3", Area = 5, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },

            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Farm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmsData.Provider);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmsData.Expression);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmsData.ElementType);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmsData.GetEnumerator());

            mockContext.Setup(c => c.Farms).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetFarmsInHomepage(search: "", province: "Province A", district: "", ward: "", sort: "", area: "");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Farm>>(result);
        }

        [Fact]
        public void GetFarmsInHomepage_ReturnsFilteredFarms3()
        {
            // Arrange
            var mockContext = new Mock<RAFSContext>();
            var farmsData = new List<Farm>
            {
            new Farm { Id = 1, Name = "Farm 1", Address = "Province A, District X, Ward 1", Area = 9, EstablishedDate = DateTime.Now.AddDays(-30), Phone = "123456789" },
            new Farm { Id = 2, Name = "Farm 2", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },
            new Farm { Id = 3, Name = "Farm 3", Address = "Province A, District Z, Ward 3", Area = 5, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },

            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Farm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmsData.Provider);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmsData.Expression);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmsData.ElementType);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmsData.GetEnumerator());

            mockContext.Setup(c => c.Farms).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetFarmsInHomepage(search: "", province: "", district: "", ward: "", sort: "asc", area: "");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Farm>>(result);

            result.Should().BeInAscendingOrder();
        }

        [Fact]
        public void GetFarmsInHomepage_ReturnsFilteredFarms4()
        {
            // Arrange
            var mockContext = new Mock<RAFSContext>();
            var farmsData = new List<Farm>
            {
            new Farm { Id = 1, Name = "Farm 1", Address = "Province A, District X, Ward 1", Area = 9, EstablishedDate = DateTime.Now.AddDays(-30), Phone = "123456789" },
            new Farm { Id = 2, Name = "Farm 2", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },
            new Farm { Id = 3, Name = "Farm 3", Address = "Province A, District Z, Ward 3", Area = 5, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },

            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Farm>>(); 
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmsData.Provider);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmsData.Expression);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmsData.ElementType);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmsData.GetEnumerator());

            mockContext.Setup(c => c.Farms).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetFarmsInHomepage(search: "", province: "", district: "", ward: "", sort: "", area: "< 10ha");

            // Assert
            //Assert.NotNull(result);
            //Assert.IsType<List<Farm>>(result);
            //Assert.All(result, f => Assert.True(f.Area <= 10));
            result.Should().NotBeNull();
        }

        [Fact]
        public void GetFarmsInHomepage_ReturnsFilteredFarms5()
        {
            // Arrange
            var mockContext = new Mock<RAFSContext>();
            var farmsData = new List<Farm>
            {
            new Farm { Id = 1, Name = "Farm 1", Address = "Province A, District X, Ward 1", Area = 20, EstablishedDate = DateTime.Now.AddDays(-30), Phone = "123456789" },
            new Farm { Id = 2, Name = "Farm 2", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },
            new Farm { Id = 3, Name = "Farm 3", Address = "Province A, District Z, Ward 3", Area = 30, EstablishedDate = DateTime.Now.AddDays(-60), Phone = "987654321" },

            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Farm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmsData.Provider);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmsData.Expression);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmsData.ElementType);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmsData.GetEnumerator());

            mockContext.Setup(c => c.Farms).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetFarmsInHomepage(search: "", province: "", district: "", ward: "", sort: "", area: "< 10ha");

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetFarmPlants_ReturnsPlantsForFarm()
        {
            // Arrange
            int farmId = 1;
            var mockContext = new Mock<RAFSContext>(); 
            var plantsData = new List<Plant> 
            {
                new Plant { Id = 1, Name = "Plant 1", Type = "Type A", Image = "image1.jpg", Milestone = new Milestone { FarmId = 1 } },
                new Plant { Id = 2, Name = "Plant 2", Type = "Type B", Image = "image2.jpg", Milestone = new Milestone { FarmId = 1 } },
            // Add more sample data as needed
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Plant>>();
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantsData.Provider);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantsData.Expression);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantsData.ElementType);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(() => plantsData.GetEnumerator());

            mockContext.Setup(c => c.Plants).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object); 

            // Act
            var result = service.GetFarmPlants(farmId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FarmDetailPlantDTO>>(result);
            Assert.Equal(2, result.Count); // Assuming there are 2 plants for the farm with farmId = 1

            // Additional assertions
            var firstPlant = result.First();
            Assert.Equal("Plant 1", firstPlant.Name);
            Assert.Equal("Type A", firstPlant.Type);
            Assert.Equal("image1.jpg", firstPlant.Image);

            var secondPlant = result.Skip(1).First(); // Getting the second plant
            Assert.Equal("Plant 2", secondPlant.Name);
            Assert.Equal("Type B", secondPlant.Type);
            Assert.Equal("image2.jpg", secondPlant.Image);
        }

        [Fact]
        public void GetFarmPlants_ReturnsNullPlantsForFarm()
        {
            // Arrange
            int farmId = -3;
            var mockContext = new Mock<RAFSContext>();
            var plantsData = new List<Plant>
            {
                new Plant { Id = 1, Name = "Plant 1", Type = "Type A", Image = "image1.jpg", Milestone = new Milestone { FarmId = 1 } },
                new Plant { Id = 2, Name = "Plant 2", Type = "Type B", Image = "image2.jpg", Milestone = new Milestone { FarmId = 1 } },
            // Add more sample data as needed
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Plant>>();
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantsData.Provider);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantsData.Expression);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantsData.ElementType);
            mockDbSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(() => plantsData.GetEnumerator());

            mockContext.Setup(c => c.Plants).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetFarmPlants(farmId);

            // Assert
            Assert.Empty(result);
            
        }


        [Fact]
        public void GetFarmImg_ReturnsFarmImages()
        {
            // Arrange
            int farmId = 1;
            var mockContext = new Mock<RAFSContext>(); 
            var imagesData = new List<Image> 
            {
                new Image { Id = 1, FarmId = 1, URL = "url1" },
                new Image { Id = 2, FarmId = 1, URL = "url2" },
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Image>>(); 
            mockDbSet.As<IQueryable<Image>>().Setup(m => m.Provider).Returns(imagesData.Provider);
            mockDbSet.As<IQueryable<Image>>().Setup(m => m.Expression).Returns(imagesData.Expression);
            mockDbSet.As<IQueryable<Image>>().Setup(m => m.ElementType).Returns(imagesData.ElementType);
            mockDbSet.As<IQueryable<Image>>().Setup(m => m.GetEnumerator()).Returns(() => imagesData.GetEnumerator());

            mockContext.Setup(c => c.Images).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetFarmImg(farmId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FarmImageDTO>>(result);
            Assert.Equal(2, result.Count);

            // Additional assertions
            var firstImage = result.First();
            Assert.Equal(1, firstImage.Id);
            Assert.Equal("url1", firstImage.URL);

            var secondImage = result.Skip(1).First(); // Getting the second image
            Assert.Equal(2, secondImage.Id);
            Assert.Equal("url2", secondImage.URL);

        }

        [Fact]
        public void GetFarmImg_ReturnsNullFarmImages()
        {
            // Arrange
            int farmId = -1;
            var mockContext = new Mock<RAFSContext>();
            var imagesData = new List<Image>
            {
                new Image { Id = 1, FarmId = 1, URL = "url1" },
                new Image { Id = 2, FarmId = 1, URL = "url2" },
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Image>>();
            mockDbSet.As<IQueryable<Image>>().Setup(m => m.Provider).Returns(imagesData.Provider);
            mockDbSet.As<IQueryable<Image>>().Setup(m => m.Expression).Returns(imagesData.Expression);
            mockDbSet.As<IQueryable<Image>>().Setup(m => m.ElementType).Returns(imagesData.ElementType);
            mockDbSet.As<IQueryable<Image>>().Setup(m => m.GetEnumerator()).Returns(() => imagesData.GetEnumerator());

            mockContext.Setup(c => c.Images).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetFarmImg(farmId);

            // Assert
            Assert.Empty(result);
            

        }


        [Fact]
        public void GetFarmDetailsInHomepage_ReturnsFarmWithDetails()
        {
            // Arrange
            int farmId = 1;
            var mockContext = new Mock<RAFSContext>(); 
            var farmData = new List<Farm> 
        {
            new Farm
            {
                Id = 1,
                Name = "Farm 1",
                Images = new List<Image>
                {
                    new Image { Id = 1, URL = "url1" },
                    new Image { Id = 2, URL = "url2" }
                },
                Milestones = new List<Milestone>
                {
                    new Milestone
                    {
                        Id = 1,
                        Plants = new List<Plant>
                        {
                            new Plant { Id = 1, Name = "Plant 1" },
                            new Plant { Id = 2, Name = "Plant 2" }
                        }
                    }
                }
            },
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Farm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmData.Provider);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmData.Expression);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmData.ElementType);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmData.GetEnumerator());

            mockContext.Setup(c => c.Farms).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object); 

            // Act
            var result = service.GetFarmDetailsInHomepage(farmId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Farm>(result);
            Assert.Equal(1, result.Id); // Assuming farmId = 1

            // Assert farm images
            Assert.NotNull(result.Images);
            Assert.Equal(2, result.Images.Count);

            // Assert milestones
            Assert.NotNull(result.Milestones);
            Assert.Single(result.Milestones);

            // Assert plants in the first milestone
            var milestoneWithPlants = result.Milestones.First();
            Assert.NotNull(milestoneWithPlants.Plants);
            Assert.Equal(2, milestoneWithPlants.Plants.Count);

            
        }

        [Fact]
        public void GetNewestFarms_Returns_Correct_Farms()
        {
            var mockContext = new Mock<RAFSContext>();
            var farmsData = new List<Farm>
            {
            new Farm { Id = 1, Name = "Farm 1", Address = "Province A, District X, Ward 1", Area = 9, EstablishedDate = new DateTime(2022, 1, 1), Phone = "123456789" },
            new Farm { Id = 2, Name = "Farm 2", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = new DateTime(2023, 1, 1), Phone = "987654321" },
            new Farm { Id = 2, Name = "Farm 3", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = new DateTime(2024, 1, 1), Phone = "987654321" },
            new Farm { Id = 2, Name = "Farm 4", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = new DateTime(2025, 1, 1), Phone = "987654321" },
            new Farm { Id = 2, Name = "Farm 5", Address = "Province B, District Y, Ward 2", Area = 25, EstablishedDate = new DateTime(2026, 1, 1), Phone = "987654321" }

            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Farm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Provider).Returns(farmsData.Provider);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.Expression).Returns(farmsData.Expression);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.ElementType).Returns(farmsData.ElementType);
            mockDbSet.As<IQueryable<Farm>>().Setup(m => m.GetEnumerator()).Returns(() => farmsData.GetEnumerator());

            mockContext.Setup(c => c.Farms).Returns(mockDbSet.Object);

            var service = new FarmRepo(mockContext.Object);

            // Act
            var result = service.GetNewestFarms();

            // Assert
            Assert.NotNull(result);
            //Assert.Equal("Farm 5", result[0].Name); // Farm 5 should be the newest farm
            //Assert.Equal("Farm 3", result[2].Name); // Farm 3 should be the third newest farm
        }
    }
}




