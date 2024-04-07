using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RAFS.Common.Models;
using RAFS.Core.Context;
using RAFS.Core.Mapper;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;
using RAFS.Web.Validation;
using System.Net.Sockets;
using System.Runtime;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<RAFSContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

//Read mail config and inject settings into system
builder.Services.AddOptions();
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings> (mailSettings);


builder.Services.AddIdentity<AspUser, IdentityRole>().AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<AspUser>>()
    .AddEntityFrameworkStores<RAFSContext>()
    .AddRoles<IdentityRole>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        // Đọc thông tin Authentication:Google từ appsettings.json
        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
        options.AccessDeniedPath = "/Authentication/Login";

        // Thiết lập ClientID và ClientSecret để truy cập API google
        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
        // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
        options.CallbackPath = "/Authentication/Login/GoogleLogin";
    }
    );

//Configure Identity Option
builder.Services.Configure<IdentityOptions>(options => {
    options.Tokens.AuthenticatorTokenProvider = "email";
    //Requires two-factor to sign in
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;
    //Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
    //Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    //User settings
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
});

builder.Services.ConfigureApplicationCookie(options => {
    //Cookie settings   
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.AccessDeniedPath = "/AccessDenied";
    options.SlidingExpiration = true;
});

//Token settings
//Token lifetime
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromMinutes(60));


//Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFarmService, FarmService>();
builder.Services.AddScoped<IMilestoneService, MilestoneService>();
builder.Services.AddScoped<IDiaryService, DiaryService>();
builder.Services.AddScoped<IPlantMaterialHistoryService, PlantMaterialHistoryService>();
builder.Services.AddScoped<IPlantService, PlantService>();
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddTransient<ISendMailService, SendMailService>();
builder.Services.AddSingleton<DriveAPIController>();
builder.Services.AddScoped<IFarmAdminService, FarmAdminService>();
builder.Services.AddScoped<IQuestionFormService, QuestionFormService>();
builder.Services.AddScoped<ICashFlowService, CashFlowService>();
builder.Services.AddScoped<IMaterialsService, MaterialsService>();
builder.Services.AddScoped<IStatisticServices, StatisticServices>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (ctx, next) =>
{
    await next();

    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        //Re-execute the request so the user gets the error page
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/Error404";
        await next();
    }

    if (ctx.Response.StatusCode == 500 && !ctx.Response.HasStarted)
    {
        //Re-execute the request so the user gets the error page
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/Error500";
        await next();
    }
});

app.UseCors("CORSPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//Create and seed roles to DB
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Technician", "Owner", "Staff"};

    foreach(var role in roles)
    {
        //Check if role exists
        if(!await roleManager.RoleExistsAsync(role)){
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
