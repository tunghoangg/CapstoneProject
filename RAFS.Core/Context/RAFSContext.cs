using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RAFS.Core.Configs;
using RAFS.Core.Initializers;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Context
{
    public partial class RAFSContext : IdentityDbContext<AspUser>
    {
        public RAFSContext()
        {
                
        }

        public RAFSContext(DbContextOptions<RAFSContext> options) : base(options) 
        {
        
        }

        public virtual DbSet<AspUser> AspUsers { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Farm> Farms { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Milestone> Milestones { get; set; }
        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }
        public virtual DbSet<TypeCashFlow> TypeCashFlows { get; set; }
        public virtual DbSet<CashFlow> CashFlows { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<BlogTag> BlogTags { get; set; }
        public virtual DbSet<BlogImage> BlogImages { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<UserFunctionFarm> UserFunctionFarms { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<TypeItem> TypeItems { get; set; }
        public virtual DbSet<Item> Inventories { get; set; }
        public virtual DbSet<TakeAndSendMaterial> TakeAndSendMaterials { get; set; }
        public virtual DbSet<QuestionForm> QuestionForms { get; set; }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Config dữ liệu vào database sử dụng FluentAPI
            builder.ApplyConfigurationsFromAssembly(typeof(CashFlowConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(FarmConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(FunctionConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ItemConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(TypeCashFlowConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(TypeItemConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(UnitConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(UserFunctionFarmConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ImageConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(QuestionFormConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(BlogConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(BlogImageConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(BlogTagConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(TagConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(MilestoneConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(PlantConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(DiaryConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(TakeAndSendMaterialConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(AspUserConfig).Assembly);



            //Dữ liệu khởi tạo
            //builder.SeedData();
        }

    }
}
