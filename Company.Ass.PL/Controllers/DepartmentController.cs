using Company.Ass.BLL.Interfaces;
using Company.Ass.BLL.Repositories;
using Company.Ass.DAL.Models;
using Company.Ass.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Ass.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        //ASK CLR Create Object From DepartmentRepository
        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet] //Get: /Department/index
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)//Server Sid Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                var Count = await _unitOfWork.completeAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detalis(int? id, string viewName = "Detalis")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, massage = $"Department With id{id} is not found" });

            return View(viewName, department);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Detalis(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.id) return BadRequest(); //400
                _unitOfWork.DepartmentRepository.Update(department);
                var Count = await _unitOfWork.completeAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            id = id,
        //            Name = model.Name,
        //            Code = model.Code,
        //            CreateAt = model.CreateAt

        //        };
        //        var Count = _departmentRepository.Update(department);
        //        if (Count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(model);
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Detalis(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.id) return BadRequest(); //400
                _unitOfWork.DepartmentRepository.Delete(department);

                var Count = await _unitOfWork.completeAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

    }
}
