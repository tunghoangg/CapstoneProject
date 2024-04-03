using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RAFS.Core.Context;
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
    public class BlogRepositoryTest
    {

        [Fact]
        public void TagCheck_ReturnsTrue_WhenTagExists()
        {
            // Arrange
            var tagName = "TestTag";
            var tags = new List<Tag>
        {
            new Tag { Name = tagName }
        }.AsQueryable();

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Tags).Returns(MockDbSet(tags));

            var tagService = new TagRepo(mockContext.Object);

            // Act
            var result = tagService.tagCheck(tagName);

            // Assert
            result.Should().NotBe(false);
        }

        [Fact]
        public void TagCheck_ReturnsFalse_WhenTagDoesNotExist()
        {
            // Arrange
            var tagName = "NonExistentTag";
            var tags = new List<Tag>().AsQueryable();

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Tags).Returns(MockDbSet(tags));

            var tagService = new TagRepo(mockContext.Object);

            // Act
            var result = tagService.tagCheck(tagName);

            // Assert
            result.Should().NotBe(true);
        }

        // Helper method to mock DbSet
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
        public void GetTagByName_ReturnsTagIfExists()
        {
            // Arrange
            var tagName = "SampleTag";
            var tagData = new List<Tag>
        {
            new Tag { Id = 1, Name = "SampleTag" },
            new Tag { Id = 2, Name = "AnotherTag" },
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Tag>>();
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(tagData.Provider);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(tagData.Expression);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(tagData.ElementType);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(() => tagData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(c => c.Tags).Returns(mockDbSet.Object);

            var service = new TagRepo(mockContext.Object);

            // Act
            var result = service.getTagByName(tagName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Tag>(result);
            Assert.Equal(tagName, result.Name);

        }

        [Fact]
        public void GetTagByName_ReturnsNullForNonExistingTag()
        {
            // Arrange
            var tagName = "NonExistingTag";
            var tagData = new List<Tag>().AsQueryable(); // Empty tag data

            var mockDbSet = new Mock<DbSet<Tag>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(tagData.Provider);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(tagData.Expression);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(tagData.ElementType);
            mockDbSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(() => tagData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>(); // Replace YourDbContext with your actual DbContext class
            mockContext.Setup(c => c.Tags).Returns(mockDbSet.Object);

            var service = new TagRepo(mockContext.Object); // Replace YourTagService with your actual service class

            // Act
            var result = service.getTagByName(tagName);

            // Assert
            Assert.Null(result);

            // Add more assertions as needed
        }

        [Fact]
        public void GetBlogTagsById_ReturnsBlogTagsForBlogId()
        {
            // Arrange
            int blogId = 1;
            var tag1 = new Tag { Id = 1, Name = "Tag1" };
            var tag2 = new Tag { Id = 2, Name = "Tag2" };

            var blogTagData = new List<BlogTag> // Replace BlogTag with your actual entity class
        {
            new BlogTag { TagId = 1, BlogId = 1, Tag = tag1 },
            new BlogTag { TagId = 2, BlogId = 1, Tag = tag2 },
            new BlogTag { TagId = 3, BlogId = 2, Tag = new Tag { Id = 3, Name = "Tag3" } }, // BlogId different from blogId
            // Add more sample data as needed
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<BlogTag>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.Provider).Returns(blogTagData.Provider);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.Expression).Returns(blogTagData.Expression);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.ElementType).Returns(blogTagData.ElementType);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.GetEnumerator()).Returns(() => blogTagData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>(); // Replace YourDbContext with your actual DbContext class
            mockContext.Setup(c => c.BlogTags).Returns(mockDbSet.Object);

            var service = new BlogTagRepo(mockContext.Object); // Replace YourBlogTagService with your actual service class

            // Act
            var result = service.GetBlogTagsById(blogId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<BlogTag>>(result);
            Assert.Equal(2, result.Count); // Assuming there are 2 blog tags for the blog with blogId = 1

            // Add more assertions as needed
        }

        [Fact]
        public void GetBlogTagsById_ReturnsEmptyListForNonExistingBlogId()
        {
            // Arrange
            int nonExistingBlogId = 999;
            var blogTagData = new List<BlogTag>().AsQueryable(); // Empty blog tag data

            var mockDbSet = new Mock<DbSet<BlogTag>>(); // Replace DbSet with your actual DbSet type
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.Provider).Returns(blogTagData.Provider);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.Expression).Returns(blogTagData.Expression);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.ElementType).Returns(blogTagData.ElementType);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.GetEnumerator()).Returns(() => blogTagData.GetEnumerator());

            var mockContext = new Mock<RAFSContext>(); // Replace YourDbContext with your actual DbContext class
            mockContext.Setup(c => c.BlogTags).Returns(mockDbSet.Object);

            var service = new BlogTagRepo(mockContext.Object); // Replace YourBlogTagService with your actual service class

            // Act
            var result = service.GetBlogTagsById(nonExistingBlogId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // Expecting an empty list for non-existing blog id

            // Add more assertions as needed
        }
        [Fact]
        public void GetBlogImageById_ReturnsCorrectBlogImages()
        {
            // Arrange
            int blogId = 1;
            var expectedBlogImages = new List<BlogImage>
            {
                new BlogImage { Id = 1, URL = "example.com/image1.jpg", BlogId = blogId }, // Initialize properties as needed
                new BlogImage { Id = 2, URL = "example.com/image2.jpg", BlogId = blogId } // Initialize properties as needed
                // Add more expected BlogImage objects if necessary
            };

            var mockBlogImages = expectedBlogImages.AsQueryable();

            var mockContext = new Mock<RAFSContext>(); // Thay YourDbContext bằng DbContext của bạn
            mockContext.Setup(c => c.BlogImages).Returns(MockDbSet(mockBlogImages));

            var blogService = new BlogImageRepo(mockContext.Object); // Thay BlogService bằng class chứa phương thức GetBlogImageById

            // Act
            var result = blogService.GetBlogImageById(blogId);

            // Assert
            Assert.Equal(expectedBlogImages.Count, result.Count);
            foreach (var expectedBlogImage in expectedBlogImages)
            {
                Assert.Contains(expectedBlogImage, result);
            }
        }

        [Fact]
        public void DeleteBlogImageById_RemovesBlogImages_WhenBlogImagesExist()
        {
            // Arrange
            int blogId = 1;
            var mockBlogImages = new List<BlogImage>
        {
            new BlogImage { Id = 1, BlogId = blogId },
            new BlogImage { Id = 2, BlogId = blogId }
        }.AsQueryable();

            var mockContext = new Mock<RAFSContext>(); // Thay YourDbContext bằng DbContext của bạn
            mockContext.Setup(c => c.BlogImages).Returns(MockDbSet(mockBlogImages));
            var blogService = new BlogImageRepo(mockContext.Object); // Thay BlogService bằng class chứa phương thức DeleteBlogImageById

            // Act
            blogService.DeleteBlogImageById(blogId);

            // Assert
            mockContext.Verify(c => c.SaveChanges(), Times.Once); // Ensure SaveChanges is called once
            mockContext.Verify(c => c.BlogImages.RemoveRange(It.IsAny<IEnumerable<BlogImage>>()), Times.Once); // Ensure RemoveRange is called once
        }

        [Fact]
        public void DeleteBlogImageById_DoesNotRemoveBlogImages_WhenBlogImagesDoNotExist()
        {
            // Arrange
            int blogId = 1;
            var mockBlogImages = new List<BlogImage>().AsQueryable(); // No blog images

            var mockContext = new Mock<RAFSContext>(); // Thay YourDbContext bằng DbContext của bạn
            mockContext.Setup(c => c.BlogImages).Returns(MockDbSet(mockBlogImages));
            var blogService = new BlogImageRepo(mockContext.Object); // Thay BlogService bằng class chứa phương thức DeleteBlogImageById

            // Act
            blogService.DeleteBlogImageById(blogId);

            // Assert
            mockContext.Verify(c => c.SaveChanges(), Times.Never); // Ensure SaveChanges is not called
            mockContext.Verify(c => c.BlogImages.RemoveRange(It.IsAny<IEnumerable<BlogImage>>()), Times.Never); // Ensure RemoveRange is not called
        }
        [Fact]
        public void GetBlogById_Returns_Null_When_Blog_Not_Found()
        {
            // Arrange
            var data = new List<Blog>().AsQueryable();
            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetBlogById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetBlogById_Returns_Correct_Blog()
        {
            // Arrange
            var blogs = new List<Blog>
        {
            new Blog { Id = 1, Title = "Blog 1" },
            new Blog { Id = 2, Title = "Blog 2" },
            new Blog { Id = 3, Title = "Blog 3" }
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogs.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetBlogById(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("Blog 2", result.Title);
        }
        [Fact]
        public void GetAllBlog_Returns_Empty_List_When_No_Blogs_Available()
        {
            // Arrange
            var data = new List<Blog>().AsQueryable();
            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetAllBlog();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllBlog_Returns_Correct_Ordered_List()
        {
            var blogs = new List<Blog>
    {
        new Blog { Id = 1, Title = "Blog 1", LastUpdated = DateTime.Now.AddDays(-3), Status = true },
        new Blog { Id = 2, Title = "Blog 2", LastUpdated = DateTime.Now.AddDays(-1), Status = true },
        new Blog { Id = 3, Title = "Blog 3", LastUpdated = DateTime.Now.AddDays(-2), Status = true }
    };

            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogs.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetAllBlog();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result[2].Id); // Blog 1 has the most recent LastUpdated
            Assert.Equal(3, result[1].Id);
            Assert.Equal(2, result[0].Id);
        }
        [Fact]
        public void GetWaitingBlog_Returns_Correct_Blogs()
        {
            // Arrange
            var blogs = new List<Blog>
        {
            new Blog { Id = 1, Title = "Blog 1", Status = true },
            new Blog { Id = 2, Title = "Blog 2", Status = false },
            new Blog { Id = 3, Title = "Blog 3", Status = false }
        };

            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogs.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetWaitingBlog();

            // Assert
            Assert.Equal(2, result.Count); // There are 2 waiting blogs
            Assert.Contains(result, blog => blog.Id == 2); // Blog 2 is in waiting status
            Assert.Contains(result, blog => blog.Id == 3); // Blog 3 is in waiting status
        }
        [Fact]
        public void GetWaitingBlog_Returns_Empty_List_When_No_Blogs_Available()
        {
            // Arrange
            var blogs = new List<Blog>
    {
        new Blog { Id = 1, Title = "Blog 1", Status = true },
        new Blog { Id = 2, Title = "Blog 2", Status = true },
        new Blog { Id = 3, Title = "Blog 3", Status = true }
    };

            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogs.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetWaitingBlog();

            // Assert
            Assert.Empty(result); // Không có blog nào có trạng thái chờ duyệt, nên kết quả trả về phải là danh sách rỗng
        }
        [Fact]
        public void GetBlogsByTag_Returns_Correct_Blogs()
        {
            // Arrange
            var blogs = new List<Blog>
    {
        new Blog { Id = 1, Title = "Blog 1", Status = true, BlogTags = new List<BlogTag>() },
        new Blog { Id = 2, Title = "Blog 2", Status = true, BlogTags = new List<BlogTag>() },
        new Blog { Id = 3, Title = "Blog 3", Status = true, BlogTags = new List<BlogTag>() }
    };

            blogs[0].BlogTags.Add(new BlogTag { Tag = new Tag { Name = "Technology" } });
            blogs[1].BlogTags.Add(new BlogTag { Tag = new Tag { Name = "Programming" } });
            blogs[2].BlogTags.Add(new BlogTag { Tag = new Tag { Name = "Technology" } });

            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogs.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetBlogsByTag("Technology");

            // Assert
            Assert.Equal(2, result.Count); // Có 2 blog có tag "Technology"
            Assert.Equal(1, result[0].Id); // Blog 1
            Assert.Equal(3, result[1].Id); // Blog 3
        }

        [Fact]
        public void GetBlogsByTag_Returns_Empty_List_When_No_Blogs_Available()
        {
            // Arrange
            var blogs = new List<Blog>
    {
        new Blog { Id = 1, Title = "Blog 1", Status = true, BlogTags = new List<BlogTag>() },
        new Blog { Id = 2, Title = "Blog 2", Status = true, BlogTags = new List<BlogTag>() },
        new Blog { Id = 3, Title = "Blog 3", Status = true, BlogTags = new List<BlogTag>() }
    };

            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogs.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetBlogsByTag("NonexistentTag");

            // Assert
            Assert.Empty(result); // Không có blog nào có tag "NonexistentTag", nên kết quả trả về phải là danh sách rỗng
        }
        [Fact]
        public void SearchBlogByTitle_Returns_Correct_Blogs()
        {
            // Arrange
            var blogs = new List<Blog>
        {
            new Blog { Id = 1, Title = "Nông nghiệp", Status = true },
            new Blog { Id = 2, Title = "Nhà", Status = true },
            new Blog { Id = 3, Title = "Introduction to Python", Status = true }
        };

            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogs.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.SearchBlogByTitle("Nông nghiệp");

            // Assert
            Assert.Single(result); // Chỉ có một blog có tiêu đề chứa từ khóa "Python"
            Assert.Equal(3, result[0].Id); // Blog có Id = 3
        }

        [Fact]
        public void SearchBlogByTitle_Returns_Empty_List_When_No_Blogs_Match()
        {
            // Arrange
            var blogs = new List<Blog>
        {
            new Blog { Id = 1, Title = "Technology Trends", Status = true },
            new Blog { Id = 2, Title = "Programming Basics", Status = true },
            new Blog { Id = 3, Title = "Introduction to Python", Status = true }
        };

            var mockDbSet = new Mock<DbSet<Blog>>();
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogs.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.SearchBlogByTitle("Nonexistent Title");

            // Assert
            Assert.Empty(result); // Không có blog nào có tiêu đề chứa từ khóa "Nonexistent Title", nên kết quả trả về phải là danh sách rỗng
        }
        [Fact]
        public void GetAllTagNameInBlog_Returns_Correct_Tags()
        {
            // Arrange
            var blog = new Blog { Id = 1 };
            var blogTags = new List<BlogTag>
        {
            new BlogTag { BlogId = 1, Tag = new Tag { Name = "Technology" } },
            new BlogTag { BlogId = 1, Tag = new Tag { Name = "Programming" } },
            new BlogTag { BlogId = 1, Tag = new Tag { Name = "Python" } }
        };

            var mockDbSet = new Mock<DbSet<BlogTag>>();
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.Provider).Returns(blogTags.AsQueryable().Provider);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.Expression).Returns(blogTags.AsQueryable().Expression);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.ElementType).Returns(blogTags.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.GetEnumerator()).Returns(blogTags.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.BlogTags).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetAllTagNameInBlog(blog);

            // Assert
            Assert.Equal(3, result.Count); // Blog có 3 tag
            Assert.Contains("Technology", result); // "Technology" là một trong các tag
            Assert.Contains("Programming", result); // "Programming" là một trong các tag
            Assert.Contains("Python", result); // "Python" là một trong các tag
        }

        [Fact]
        public void GetAllTagNameInBlog_Returns_Empty_List_When_No_Tags_Available()
        {
            // Arrange
            var blog = new Blog { Id = 1 };
            var mockDbSet = new Mock<DbSet<BlogTag>>();
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.Provider).Returns(Enumerable.Empty<BlogTag>().AsQueryable().Provider);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.Expression).Returns(Enumerable.Empty<BlogTag>().AsQueryable().Expression);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.ElementType).Returns(Enumerable.Empty<BlogTag>().AsQueryable().ElementType);
            mockDbSet.As<IQueryable<BlogTag>>().Setup(m => m.GetEnumerator()).Returns(Enumerable.Empty<BlogTag>().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.BlogTags).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetAllTagNameInBlog(blog);

            // Assert
            Assert.Empty(result); // Không có tag nào cho blog này, nên kết quả trả về phải là danh sách rỗng
        }
        [Fact]
        public void GetAllImageURLInBlog_Returns_Correct_URLs()
        {
            // Arrange
            var blog = new Blog { Id = 1 };
            var blogImages = new List<BlogImage>
        {
            new BlogImage { BlogId = 1, URL = "https://example.com/image1.jpg" },
            new BlogImage { BlogId = 1, URL = "https://example.com/image2.jpg" },
            new BlogImage { BlogId = 1, URL = "https://example.com/image3.jpg" }
        };

            var mockDbSet = new Mock<DbSet<BlogImage>>();
            mockDbSet.As<IQueryable<BlogImage>>().Setup(m => m.Provider).Returns(blogImages.AsQueryable().Provider);
            mockDbSet.As<IQueryable<BlogImage>>().Setup(m => m.Expression).Returns(blogImages.AsQueryable().Expression);
            mockDbSet.As<IQueryable<BlogImage>>().Setup(m => m.ElementType).Returns(blogImages.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<BlogImage>>().Setup(m => m.GetEnumerator()).Returns(blogImages.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.BlogImages).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetAllImageURLInBlog(blog);

            // Assert
            Assert.Equal(3, result.Count); // Blog có 3 ảnh
            Assert.Contains("https://example.com/image1.jpg", result); // URL "https://example.com/image1.jpg" là một trong các URL
            Assert.Contains("https://example.com/image2.jpg", result); // URL "https://example.com/image2.jpg" là một trong các URL
            Assert.Contains("https://example.com/image3.jpg", result); // URL "https://example.com/image3.jpg" là một trong các URL
        }

        [Fact]
        public void GetAllImageURLInBlog_Returns_Empty_List_When_No_Images_Available()
        {
            // Arrange
            var blog = new Blog { Id = 1 };
            var mockDbSet = new Mock<DbSet<BlogImage>>();
            mockDbSet.As<IQueryable<BlogImage>>().Setup(m => m.Provider).Returns(Enumerable.Empty<BlogImage>().AsQueryable().Provider);
            mockDbSet.As<IQueryable<BlogImage>>().Setup(m => m.Expression).Returns(Enumerable.Empty<BlogImage>().AsQueryable().Expression);
            mockDbSet.As<IQueryable<BlogImage>>().Setup(m => m.ElementType).Returns(Enumerable.Empty<BlogImage>().AsQueryable().ElementType);
            mockDbSet.As<IQueryable<BlogImage>>().Setup(m => m.GetEnumerator()).Returns(Enumerable.Empty<BlogImage>().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.BlogImages).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetAllImageURLInBlog(blog);

            // Assert
            Assert.Empty(result); // Không có ảnh nào cho blog này, nên kết quả trả về phải là danh sách rỗng
        }
        [Fact]
        public void GetUserById_Returns_Correct_User()
        {
            // Arrange
            var authorId = "author123";
            var author = new AspUser { Id = authorId, FullName = "testuser" };

            var mockDbSet = new Mock<DbSet<AspUser>>();
            mockDbSet.As<IQueryable<AspUser>>().Setup(m => m.Provider).Returns(new List<AspUser> { author }.AsQueryable().Provider);
            mockDbSet.As<IQueryable<AspUser>>().Setup(m => m.Expression).Returns(new List<AspUser> { author }.AsQueryable().Expression);
            mockDbSet.As<IQueryable<AspUser>>().Setup(m => m.ElementType).Returns(new List<AspUser> { author }.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<AspUser>>().Setup(m => m.GetEnumerator()).Returns(new List<AspUser> { author }.AsQueryable().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.AspUsers).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetUserById(authorId);

            // Assert
            Assert.NotNull(result); // Kết quả không được null
            Assert.Equal(authorId, result.Id); // Kết quả trả về phải là user có Id là authorId
        }

        [Fact]
        public void GetUserById_Returns_Null_When_User_Not_Found()
        {
            // Arrange
            var authorId = "author123";

            var mockDbSet = new Mock<DbSet<AspUser>>();
            mockDbSet.As<IQueryable<AspUser>>().Setup(m => m.Provider).Returns(Enumerable.Empty<AspUser>().AsQueryable().Provider);
            mockDbSet.As<IQueryable<AspUser>>().Setup(m => m.Expression).Returns(Enumerable.Empty<AspUser>().AsQueryable().Expression);
            mockDbSet.As<IQueryable<AspUser>>().Setup(m => m.ElementType).Returns(Enumerable.Empty<AspUser>().AsQueryable().ElementType);
            mockDbSet.As<IQueryable<AspUser>>().Setup(m => m.GetEnumerator()).Returns(Enumerable.Empty<AspUser>().GetEnumerator());

            var mockContext = new Mock<RAFSContext>();
            mockContext.Setup(x => x.AspUsers).Returns(mockDbSet.Object);

            var service = new BlogRepo(mockContext.Object);

            // Act
            var result = service.GetUserById(authorId);

            // Assert
            Assert.Null(result); // Không tìm thấy user nào có Id là authorId, nên kết quả trả về phải là null
        }
    }
}

