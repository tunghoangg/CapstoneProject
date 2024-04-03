using Microsoft.EntityFrameworkCore;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RAFS.Core.Initializers
{
    public static class RAFSInitializer
    {
        public static void SeedData(this ModelBuilder builder)
        {
            var hasher = new PasswordHasher<AspUser>();
            var appUser = new AspUser
            {
                //Id = 1,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "0913324785",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                LockoutEnd = DateTime.UtcNow,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                LastUpdated = DateTime.UtcNow,
                Status = true,
                Avatar = ""
            };

            appUser.PasswordHash = hasher.HashPassword(appUser, "admin");


            builder.Entity<AspUser>().HasData
            (
                appUser
            );

            //builder.Entity<Farm>().HasData
            //(
            //    new Farm()
            //    {

            //    },
            //    new Farm()
            //    {

            //    },
            //    new Farm()
            //    {

            //    },
            //    new Farm()
            //    {

            //    },
            //    new Farm()
            //    {

            //    }
            //);

            //builder.Entity<Employee>().HasData(
            //    new Employee()
            //    {
            //        Id = 1,
            //        Username = "employee1",
            //        Email = "employee1@mail.com",
            //        PhoneNumber = "0412342678",
            //        Gender = true,
            //        Address = "Hanoi",
            //        LastUpdated = DateTime.UtcNow,
            //        Status = true,
            //        FarmId = 1
            //    }) ;

            //builder.Entity<Unit>().HasData
            //(
            //    new Unit()
            //    {
            //        Id = 1,
            //        Name = "kilogram"
            //    },
            //    new Unit()
            //    {
            //        Id = 2,
            //        Name = "chiếc"
            //    },
            //    new Unit()
            //    {
            //        Id = 3,
            //        Name = "cái"
            //    },
            //    new Unit()
            //    {
            //        Id = 4,
            //        Name = "lít"
            //    },
            //    new Unit()
            //    {
            //        Id = 5,
            //        Name = "mét"
            //    }
            //);

            //builder.Entity<TypeItem>().HasData
            //(
            //    new TypeItem()
            //    {
            //        Id = 1,
            //        Name = "Thuốc BVTV"
            //    },
            //    new TypeItem()
            //    {
            //        Id = 2,
            //        Name = "Chế phẩm sinh học"
            //    },
            //    new TypeItem()
            //    {
            //        Id = 3,
            //        Name = "Phân hóa học"
            //    },
            //    new TypeItem()
            //    {
            //        Id = 4,
            //        Name = "Phân hữu cơ"
            //    },
            //    new TypeItem()
            //    {
            //        Id = 5,
            //        Name = "Phân vi sinh"
            //    },
            //    new TypeItem()
            //    {
            //        Id = 6,
            //        Name = "Máy móc"
            //    },
            //    new TypeItem()
            //    {
            //        Id = 7,
            //        Name = "Công cụ"
            //    },
            //    new TypeItem()
            //    {
            //        Id = 8,
            //        Name = "Kho chứa"
            //    },
            //    new TypeItem()
            //    {
            //        Id = 9,
            //        Name = "Sản phẩm"
            //    }
            //);

            //builder.Entity<TypeCashFlow>().HasData
            //(
            //    new TypeCashFlow()
            //    {
            //        Id = 1,
            //        Name = "Chi thuốc BVTV"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 2,
            //        Name = "Chi chế phẩm sinh học"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 3,
            //        Name = "Chi phân hóa học"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 4,
            //        Name = "Chi phân hữu cơ"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 5,
            //        Name = "Chi phân vi sinh"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 6,
            //        Name = "Chi máy móc"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 7,
            //        Name = "Chi công cụ"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 8,
            //        Name = "Chi kho chứa"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 9,
            //        Name = "Chi nhân công"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 10,
            //        Name = "Chi bán sản phẩm"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 11,
            //        Name = "Thu từ sản phẩm"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 12,
            //        Name = "Thu từ chế phẩm sinh học"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 13,
            //        Name = "Thu từ phân hữu cơ"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 14,
            //        Name = "Thu khác"
            //    },
            //    new TypeCashFlow()
            //    {
            //        Id = 15,
            //        Name = "Chi khác"
            //    }
            //);

            //builder.Entity<Function>().HasData
            //(
            //    new Function()
            //    {
            //        Id = 1,
            //        Name = "Khác"
            //    },
            //    new Function()
            //    {
            //        Id = 2,
            //        Name = "Sổ nhật ký"
            //    },
            //    new Function()
            //    {
            //        Id = 3,
            //        Name = "Dòng tiền"
            //    },
            //    new Function()
            //    {
            //        Id = 4,
            //        Name = "Kho chứa"
            //    },
            //    new Function()
            //    {
            //        Id = 5,
            //        Name = "Nhân công"
            //    },
            //    new Function()
            //    {
            //        Id = 6,
            //        Name = "Sản xuất"
            //    }
            //);

            //builder.Entity<CashFlow>().HasData
            //(
            //    new CashFlow()
            //    {

            //    },
            //    new CashFlow()
            //    {

            //    },
            //    new CashFlow()
            //    {

            //    },
            //    new CashFlow()
            //    {

            //    },
            //    new CashFlow()
            //    {

            //    }
            //);

            //builder.Entity<Inventory>().HasData
            //(
            //    new Inventory()
            //    {

            //    },
            //    new Inventory()
            //    {

            //    },
            //    new Inventory()
            //    {

            //    },
            //    new Inventory()
            //    {

            //    },
            //    new Inventory()
            //    {

            //    }
            //);

            //builder.Entity<UserFunctionFarm>().HasData
            //(
            //    new UserFunctionFarm()
            //    {

            //    },
            //    new UserFunctionFarm()
            //    {

            //    },
            //    new UserFunctionFarm()
            //    {

            //    },
            //    new UserFunctionFarm()
            //    {

            //    },
            //    new UserFunctionFarm()
            //    {

            //    }
            //);

            builder.Entity<Tag>().HasData
            (
                new Tag()
                {
                    Id = 1,
                    Name = "Nông Nghiệp xanhh"
                },
                new Tag()
                {
                    Id = 2,
                    Name = "Xoài Xanh"
                }
            );
            // builder.Entity<Milestone>().HasData
            //     (
            //         new Milestone()
            //         {
            //             Id = 1,
            //             FarmId = 1,
            //             Name = "Nhóm 1",
            //             Status = true,
            //             LastUpdate = DateTime.Now
            //         },
            //         new Milestone()
            //         {
            //             Id = 2,
            //             FarmId = 1,
            //             Name = "Nhóm 1",
            //             Status = true,
            //             LastUpdate = DateTime.Now
            //         },
            //         new Milestone()
            //         {
            //             Id = 3,
            //             FarmId = 1,
            //             Name = "Nhóm 1",
            //             Status = true,
            //             LastUpdate = DateTime.Now
            //         },
            //         new Milestone()
            //         {
            //             Id = 4,
            //             FarmId = 1,
            //             Name = "Nhóm 1",
            //             Status = true,
            //             LastUpdate = DateTime.Now
            //         },
            //         new Milestone()
            //         {
            //             Id = 5,
            //             FarmId = 1,
            //             Name = "Nhóm 1",
            //             Status = false,
            //             LastUpdate = DateTime.Now
            //         }
            //     );

            // builder.Entity<Plant>().HasData
            //(
            //    new Plant()
            //    {
            //        Id = 1,
            //        MilestoneId = 1,
            //        Name = "Cay cam",
            //        Type = "lau ngay",
            //        Description = "mot loai cay an qua",
            //        Area = 1.2,
            //        AreaUnit = "ha",
            //        HealthCondition = 1,
            //        PlantingMethod = "chuyên canh",
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },
            //    new Plant()
            //    {
            //        Id = 2,
            //        MilestoneId = 1,
            //        Name = "Cay ổi",
            //        Type = "Lâu ngày",
            //        Description = "mot loai cay an qua",
            //        Area = 0.2,
            //        AreaUnit = "ha",
            //        HealthCondition = 1,
            //        PlantingMethod = "tăng vụ",
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },
            //    new Plant()
            //    {
            //        Id = 3,
            //        MilestoneId = 2,
            //        Name = "Cây xà lách",
            //        Type = "Ngắn ngày",
            //        Description = "mot loai cay an qua",
            //        Area = 111.2,
            //        AreaUnit = "m2",
            //        HealthCondition = 1,
            //        PlantingMethod = "chuyên canh",
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },
            //    new Plant()
            //    {
            //        Id = 4,
            //        MilestoneId = 2,
            //        Name = "Cây đào",
            //        Type = "lau ngay",
            //        Description = "mot loai cay an qua",
            //        Area = 1.42,
            //        AreaUnit = "ha",
            //        HealthCondition = 1,
            //        PlantingMethod = "trồng trong nhà kính",
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },
            //    new Plant()
            //    {
            //        Id = 5,
            //        MilestoneId = 3,
            //        Name = "Cây dong",
            //        Type = "ngắn ngày",
            //        Description = "một loại cây năng suất cao trồng thủy sinh hoặc trên cạn",
            //        Area = 20.5,
            //        AreaUnit = "m2",
            //        HealthCondition = 1,
            //        PlantingMethod = "thủy sinh",
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    }
            //);
            // builder.Entity<Diary>().HasData
            //(
            //    new Diary()
            //    {
            //        Id = 1,
            //        PlantId = 1,
            //        Title = "Báo cáo ngày 1 của cây cam",
            //        Type = 1,
            //        Body = "Đã xới đất",
            //        CreatedDay = DateTime.Now,
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },
            //    new Diary()
            //    {
            //        Id = 2,
            //        PlantId = 1,
            //        Title = "Báo cáo ngày 2 của cây cam",
            //        Type = 1,
            //        Body = "Đã trồng cây",
            //        CreatedDay = DateTime.Now,
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },
            //    new Diary()
            //    {
            //        Id = 3,
            //        PlantId = 1,
            //        Title = "Báo cáo tuần 1 của cây cam",
            //        Type = 2,
            //        Body = "Cây phát triển tốt",
            //        CreatedDay = DateTime.Now,
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },
            //    new Diary()
            //    {
            //        Id = 4,
            //        PlantId = 1,
            //        Title = "Báo cáo tuần 2 của cây cam",
            //        Type = 2,
            //        Body = "Cây mọc mầm",
            //        CreatedDay = DateTime.Now,
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },
            //    new Diary()
            //    {
            //        Id = 5,
            //        PlantId = 1,
            //        Title = "Báo cáo tháng 1 của cây cam",
            //        Type = 3,
            //        Body = "Cây đã qua giai đoạn gây giống có thể bắt đầu trồng",
            //        CreatedDay = DateTime.Now,
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    },

            //    new Diary()
            //    {
            //        Id = 6,
            //        PlantId = 1,
            //        Title = "Báo cáo ngày 4 của cây cam",
            //        Type = 1,
            //        Body = "Đã bắt đầu cấy cây",
            //        CreatedDay = DateTime.Now,
            //        Image = "",
            //        Status = true,
            //        LastUpdate = DateTime.Now
            //    }
            //);

        }
    }
}
