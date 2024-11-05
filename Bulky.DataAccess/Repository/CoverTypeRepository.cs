using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public CoverTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(CoverType coverType)
        {
            var trackedEntity = _context.CoverTypes.Local.FirstOrDefault(c => c.Id == coverType.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
            _context.CoverTypes.Remove(coverType);
        }

        public void Update(CoverType coverType)
        {
            var trackedEntity = _context.CoverTypes.Local.FirstOrDefault(c => c.Id == coverType.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
            _context.CoverTypes.Update(coverType);
        }
    }
}
