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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context):base(context)
        {
           }
        //private readonly CompanyDbContext _Context;
        //public EmployeeRepository(CompanyDbContext context) {

        //    _Context = _Context;
        //}


        //public IEnumerable<Employee> GetAll()
        //{
        //    return _Context.Employees.ToList();
        //}
        //public Employee? Get(int id)
        //{
        //    return _Context.Employees.Find(id);
        //}
        //public int Add(Employee model)
        //{
        //    _Context.Employees.Add(model);
        //    return _Context.SaveChanges();
        //}

        //public int Delete(Employee model)
        //{

        //    _Context.Employees.Remove(model);
        //    return _Context.SaveChanges();
        //}



        //public int Update(Employee model)
        //{
        //    _Context.Employees.Update(model);
        //    return _Context.SaveChanges();
        //}
    }
}
