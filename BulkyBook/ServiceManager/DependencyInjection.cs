using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BulkyBook.Models.AutoMapper;
using BulkyBook.Pages;
using BulkyBook.Dialogs;

namespace BulkyBook.ServiceManager
{
    public class DependencyInjection
    {
        public void ConfigureServices(IServiceCollection service, IConfiguration configuration, string databaseName)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(databaseName)).EnableSensitiveDataLogging();
            });

            // Auto update database
            using (var applicationDbContext = service.BuildServiceProvider().GetService<ApplicationDbContext>())
            {
                applicationDbContext?.Database.Migrate();
            }
            service.AddAutoMapper(typeof(MappingProfile));

            #region Repositories
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Windows
            service.AddScoped<MainWindow>();
            service.AddScoped<LoginWindow>();
            service.AddScoped<RegisterWindow>();
            service.AddScoped<AdminWindow>();
            #endregion

            #region Page
            service.AddScoped<UserManagementPage>();
            service.AddScoped<CategoryManagementPage>();
            service.AddScoped<CoverTypeManagementPage>();
            service.AddScoped<ProductManagementPage>();
            #endregion

            #region UserControl
            service.AddScoped<CategoryDialog>();
            service.AddScoped<ProductDialog>();
            service.AddScoped<CoverTypeDialog>();
            #endregion
        }
    }
}
