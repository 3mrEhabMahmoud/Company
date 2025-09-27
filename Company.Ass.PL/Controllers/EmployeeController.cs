using AutoMapper;
using Company.Ass.BLL.Interfaces;
using Company.Ass.DAL.Models;
using Company.Ass.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Ass.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepositry;
        private readonly IMapper _mapper;
        //ASK CLR Create Object From DepartmentRepository

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepositry = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet] //Get: /Department/index
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
            var employees = _employeeRepository.GetAll();

            }
            //Dictionary : 3 Property
            //1. viewData : Transformation From Controller(Action) to View
            ViewData["Massage"] = "hello from ViewData ";

            //2. viewBag : Transformation From Controller(Action) to View
            ViewBag.Massege = new { Message ="Hello from ViewBag" };
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepositry.GetAll();
            ViewData["departments"] = departments; 
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)//Server Sid Validation
            {
                //Manual Mapping
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Address = model.Address,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Email = model.Email,
                //    IsActive =model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    DepartmentId = model.DepartmentId

                //};
               var employee = _mapper.Map<Employee>(model);
                var Count = _employeeRepository.Add(employee);
                if (Count > 0)
                {
                    TempData["Massage"] = "Employee is Created !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detalis(int? id, string viewName = "Detalis")
        {
            var departments = _departmentRepositry.GetAll();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, massage = $"Employee With id{id} is not found" });

            return View(viewName, employee);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Detalis(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.id) return BadRequest(); //400
                var Count = _employeeRepository.Update(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Detalis(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.id) return BadRequest(); //400
                var Count = _employeeRepository.Delete(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }
    }
}
