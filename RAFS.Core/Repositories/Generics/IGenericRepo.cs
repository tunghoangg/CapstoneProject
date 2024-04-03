using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Generics
{
    public interface IGenericRepo<T> where T : class
    {
        //Lớp dùng chung các nghiệp vụ chung CRUD
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        void DeleteById(int id);
        void Update(T entity);

        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task AddRangeAsync(List<T> entity);
        Task UpdateRangeAsync(List<T> entity);
        Task DeleteRangeAsync(List<T> entity);
    }
}
