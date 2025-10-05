using Company.Ass.BLL.Interfaces;
using Company.Ass.DAL.Data.Contexts;
using Company.Ass.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Ass.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _Context;
        public GenericRepository(CompanyDbContext context)
        {

            _Context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await _Context.Employees.Include(E => E.Department).ToListAsync();
            }
            return await _Context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _Context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.id == id) as T;
            }
            return _Context.Set<T>().Find(id);
        }

        public async Task AddAsync(T model)
        {
            await _Context.Set<T>().AddAsync(model);
        }

        public void Delete(T model)
        {
            _Context.Set<T>().Remove(model);
        }


        public void Update(T model)
        {
            _Context.Set<T>().Update(model);
        }
    }
}
