using Microsoft.EntityFrameworkCore;
using Moq;
using RAFS.Core.Context;
using RAFS.Core.Models;
using RAFS.Core.Repositories.IRepo;
using RAFS.Core.Repositories.Repo;
using RAFS.Core.Services.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Test.Repositories
{
    public class QuestionFormRepositoryTest
    {
        [Fact]
        public void GetQuestionContent_ReturnsCorrectQuestionForm()
        {
            // Arrange
            var mockContext = new Mock<RAFSContext>();
            var formsData = new List<QuestionForm>
            {
                new QuestionForm { Id = 1, GuestName = "John", Email = "john@example.com", Title = "Test question", Description = "This is a test question", Status = false, SendDate = DateTime.Now },
                new QuestionForm { Id = 2, GuestName = "Alice", Email = "alice@example.com", Title = "Another question", Description = "This is another test question", Status = true, SendDate = DateTime.Now }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<QuestionForm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.Provider).Returns(formsData.Provider);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.Expression).Returns(formsData.Expression);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.ElementType).Returns(formsData.ElementType);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.GetEnumerator()).Returns(() => formsData.GetEnumerator());

            mockContext.Setup(c => c.QuestionForms).Returns(mockDbSet.Object);

            var service = new QuestionFormRepo(mockContext.Object);

            // Act
            var result = service.GetQuestionCotent(1);

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.Equal(1, result.Id); // Ensure Id matches
            Assert.Equal("John", result.GuestName); // Ensure GuestName matches
            Assert.Equal("john@example.com", result.Email); // Ensure Email matches
            Assert.Equal("Test question", result.Title); // Ensure Title matches
            Assert.Equal("This is a test question", result.Description); // Ensure Description matches
            Assert.False(result.Status); // Ensure Status matches
            Assert.Equal(DateTime.Now.Date, result.SendDate.Date); // Ensure SendDate matches
        }

        [Fact]
        public void GetQuestionContent_ReturnsNullQuestionForm()
        {
            // Arrange
            var mockContext = new Mock<RAFSContext>();
            var formsData = new List<QuestionForm>
            {
                new QuestionForm { Id = 2, GuestName = "John", Email = "john@example.com", Title = "Test question", Description = "This is a test question", Status = false, SendDate = DateTime.Now },
                new QuestionForm { Id = 3, GuestName = "Alice", Email = "alice@example.com", Title = "Another question", Description = "This is another test question", Status = true, SendDate = DateTime.Now }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<QuestionForm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.Provider).Returns(formsData.Provider);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.Expression).Returns(formsData.Expression);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.ElementType).Returns(formsData.ElementType);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.GetEnumerator()).Returns(() => formsData.GetEnumerator());

            mockContext.Setup(c => c.QuestionForms).Returns(mockDbSet.Object);

            var service = new QuestionFormRepo(mockContext.Object);

            // Act
            var result = service.GetQuestionCotent(1);

            // Assert
            Assert.Null(result); // Ensure result is not null
        }

        [Fact]
        public void GetQuestionFormList_ReturnsSortedQuestionForms()
        {
            // Arrange
            var mockContext = new Mock<RAFSContext>();
            var formsData = new List<QuestionForm>
            {
                new QuestionForm { Id = 1, GuestName = "John", Email = "john@example.com", Title = "Test question 1", Description = "This is a test question 1", Status = false, SendDate = DateTime.Now },
                new QuestionForm { Id = 2, GuestName = "Alice", Email = "alice@example.com", Title = "Test question 2", Description = "This is a test question 2", Status = true, SendDate = DateTime.Now.AddDays(-1) },
                new QuestionForm { Id = 3, GuestName = "Bob", Email = "bob@example.com", Title = "Test question 3", Description = "This is a test question 3", Status = false, SendDate = DateTime.Now.AddDays(-2) }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<QuestionForm>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.Provider).Returns(formsData.Provider);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.Expression).Returns(formsData.Expression);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.ElementType).Returns(formsData.ElementType);
            mockDbSet.As<IQueryable<QuestionForm>>().Setup(m => m.GetEnumerator()).Returns(() => formsData.GetEnumerator());

            mockContext.Setup(c => c.QuestionForms).Returns(mockDbSet.Object);

            var service = new QuestionFormRepo(mockContext.Object);

            // Act
            var result1 = service.GetQuestionFormList("not read");
            var result2 = service.GetQuestionFormList("read");
            var result3 = service.GetQuestionFormList("none");

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotNull(result3);

        }
    }
}
