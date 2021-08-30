using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Data.Access.DAL.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly MainDbContext _context;

        public GenericRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Add(obj);
            Save();
        }

        public void Update<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Attach(obj);
            Save();
        }

        public void Remove<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Remove(obj);
            Save();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
