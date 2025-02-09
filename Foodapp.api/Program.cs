using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using FoodApp.Api.config;
using Autofac.Core;
using FoodApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FoodApp.Core.ViewModle.authVIewModel;
using AutoMapper;
using FoodApp.Core.Helper;
using Repository.Data;
using FoodApp.Core.Settings;
using FoodApp.Core.interfaces.RecipeService;
using FoodApp.Core;
using FoodApp.Core.interfaces;
using FoodApp.Core.ViewModle.Profiles;
using FoodApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>
(option=> {
        option.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        option.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
        option.Tokens.ChangeEmailTokenProvider = "Default";
    option.SignIn.RequireConfirmedEmail = true; 

    })
    .AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
 

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAutoMapper(typeof(RecipesProfile).Assembly);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule(new autofac());
});
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
//builder.Services.AddScoped<IcategoryService, categoryService>(); 
//builder.Services.AddScoped<IRecpieService, RecipeService>();
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole(); 
//builder.Logging.AddDebug(); 
var app = builder.Build();

AutomapperService.mapper = app.Services.GetService<IMapper>(); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<GlobalErrorHandling>();
app.MapControllers();

app.Run();
