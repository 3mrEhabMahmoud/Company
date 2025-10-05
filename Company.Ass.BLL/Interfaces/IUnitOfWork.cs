using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Ass.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }

        Task<int> completeAsync();
    }
}
