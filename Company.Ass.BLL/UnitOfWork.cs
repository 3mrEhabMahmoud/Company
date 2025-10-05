using Company.Ass.BLL.Interfaces;
using Company.Ass.BLL.Repositories;
using Company.Ass.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Ass.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;
        public IEmployeeRepository EmployeeRepository { get; }

        public IDepartmentRepository DepartmentRepository { get; }
        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            EmployeeRepository = new EmployeeRepository(_context);
            DepartmentRepository = new DepartmentRepository(_context);
        }

        public async Task<int> completeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
