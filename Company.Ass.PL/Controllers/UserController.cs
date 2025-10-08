using Company.Ass.DAL.Models;
using Company.Ass.PL.Dtos;
using Company.Ass.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Company.Ass.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet] 
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _userManager.Users.Select(u => new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Roles = _userManager.GetRolesAsync(u).Result
                });

            }
            else
            {
                users = _userManager.Users.Select(u => new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).Where(u => u.FirstName.ToLower().Contains(SearchInput.ToLower()));
            }
            return View(users);
        }


        public async Task<IActionResult> Detalis(string? id, string viewName = "Detalis")
        {

            if (id is null) return BadRequest("Invalid Id"); //400
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new { StatusCode = 404, massage = $"user With id{id} is not found" });

            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).Result

            };
            return View(viewName, dto);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Detalis(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
               

                if (id != model.Id) return BadRequest("Invalid Operations"); //400
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("Invalid Operations"); //400
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Detalis(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            { 
                if (id != model.Id) return BadRequest("Invalid Operations"); //400
                var user = await _userManager.FindByIdAsync(id);
            if (user is null) return BadRequest("Invalid Operations"); //400


            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
        }
            return View(model);
        }


    }
}
