using Company.Ass.BLL.Interfaces;
using Company.Ass.DAL.Data.Contexts;
using Company.Ass.DAL.Models;
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
        public IEnumerable<T> GetAll()
        {
            return _Context.Set<T>().ToList();
        }
        public T? Get(int id)
        {
            return _Context.Set<T>().Find(id);
        }

        public int Add(T model)
        {
            _Context.Set<T>().Add(model);
            return _Context.SaveChanges();
        }

        public int Delete(T model)
        {
            _Context.Set<T>().Remove(model);
            return _Context.SaveChanges();
        }


        public int Update(T model)
        {
            _Context.Set<T>().Update(model);
            return _Context.SaveChanges();
        }
    }
}
