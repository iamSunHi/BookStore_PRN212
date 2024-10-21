using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BulkyBook.ServiceManager
{
    public class DependencyInjection
    {
        public void ConfigureServices(IServiceCollection service, IConfiguration configuration, string databaseName)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(databaseName));
            });

            // Auto update database
            using (var applicationDbContext = service.BuildServiceProvider().GetService<ApplicationDbContext>())
            {
                applicationDbContext?.Database.Migrate();
            }

            #region Repositories
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Windows
            service.AddScoped<MainWindow>();
            #endregion
        }
    }
}
