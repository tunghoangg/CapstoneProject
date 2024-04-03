using RAFS.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Generics
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        //Lớp dùng chung các nghiệp vụ chung CRUD
        protected readonly RAFSContext _context;

        public GenericRepo(RAFSContext context)
        {
            _context = context;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return _context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await _context.Set<List<T>>().AddRangeAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteById(int id)
        {
            _context.Set<T>().Remove(GetById(id));
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Set<T>().Remove(GetById(id));
        }

        public async Task DeleteRangeAsync(List<T> entity)
        {
            _context.Set<List<T>>().RemoveRange(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task UpdateRangeAsync(List<T> entity)
        {
            _context.Set<List<T>>().UpdateRange(entity);
        }
    }
}
