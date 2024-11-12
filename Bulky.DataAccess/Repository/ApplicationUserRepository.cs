using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ApplicationUser applicationUser)
        {
                var trackedEntity = _context.ApplicationUsers.Local.FirstOrDefault(c => c.Id == applicationUser.Id);
                if (trackedEntity != null)
                {
                    _context.Entry(trackedEntity).State = EntityState.Detached;
                }
                _context.ApplicationUsers.Update(applicationUser);
        }
    }
}
