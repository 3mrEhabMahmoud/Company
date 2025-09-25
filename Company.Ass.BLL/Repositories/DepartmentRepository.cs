using Company.Ass.BLL.Interfaces;
using Company.Ass.DAL.Data.Contexts;
using Company.Ass.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Company.Ass.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext context) : base(context)
        {

        }
        //private readonly CompanyDbContext _context;
        //public DepartmentRepository(CompanyDbContext context)
        //{
        //    _context = context;
        //}
        //public IEnumerable<Department> GetAll()
        //{
        //    return _context.Departments.ToList();
        //}
        //public Department? Get(int id)
        //{
        //    return _context.Departments.Find(id);
        //}

        //public int Add(Department model)
        //{
        //     _context.Departments.Add(model);
        //    return _context.SaveChanges();
        //}

        //public int Update(Department model)
        //{
        //    _context.Departments.Update(model);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department model)
        //{
        //    _context.Departments.Remove(model);
        //    return _context.SaveChanges();
        //}

    }
}
