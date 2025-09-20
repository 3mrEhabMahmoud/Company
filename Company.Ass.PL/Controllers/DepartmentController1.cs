using Company.Ass.BLL.Interfaces;
using Company.Ass.BLL.Repositories;
using Company.Ass.DAL.Models;
using Company.Ass.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Ass.PL.Controllers
{
    public class DepartmentController1 : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        //ASK CLR Create Object From DepartmentRepository
        public DepartmentController1(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet] //Get: /Department/index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)//Server Sid Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name=model.Name,
                    CreateAt = model.CreateAt
                };
                var Count =_departmentRepository.Add(department);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}
