using AutoMapper;
using Company.Ass.BLL.Interfaces;
using Company.Ass.DAL.Models;
using Company.Ass.PL.Dtos;
using Company.Ass.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Ass.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepositry;
        private readonly IMapper _mapper;
        //ASK CLR Create Object From DepartmentRepository

        public EmployeeController(//IEmployeeRepository employeeRepository,
                                  //IDepartmentRepository departmentRepository,
                                  IUnitOfWork unitOfWork,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepositry = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet] //Get: /Department/index
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();

            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByName(SearchInput);
            }
            //Dictionary : 3 Property
            //1. viewData : Transformation From Controller(Action) to View
            //ViewData["Massage"] = "hello from ViewData ";

            //2. viewBag : Transformation From Controller(Action) to View
            //ViewBag.Massege = new { Message = "Hello from ViewBag" };
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
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

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                }
                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var Count = await _unitOfWork.completeAsync();
                if (Count > 0)
                {
                    TempData["Massage"] = "Employee is Created !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detalis(int? id, string viewName = "Detalis")
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, massage = $"Employee With id{id} is not found" });

            return View(viewName, employee);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Detalis(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "images");
                }
                if (model.Image is not null)
                {

                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);
                employee.id = id;

                if (id != employee.id) return BadRequest(); //400
                _unitOfWork.EmployeeRepository.Update(employee);
                var Count = await _unitOfWork.completeAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Detalis(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.id = id;
                if (id != employee.id) return BadRequest(); //400
                _unitOfWork.EmployeeRepository.Delete(employee);
                var Count = await _unitOfWork.completeAsync();
                if (Count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile(model.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
