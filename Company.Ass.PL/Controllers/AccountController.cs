using Company.Ass.DAL.Models;
using Company.Ass.PL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Ass.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> _UserManager { get; }
        public SignInManager<AppUser> _SignInManager { get; }

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }


        //SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByNameAsync(model.UserName);
                if(user is null)
                {
                    user = await _UserManager.FindByEmailAsync(model.Email);
                    if(user is null)
                    {

                 user = new AppUser()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsAgree = model.IsAgree
                };
                var result = await _UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                            //
                    return RedirectToAction("SignIn");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                    }
                }
                ModelState.AddModelError("", "Invalid SignUp !!!");
            }
            return View(model);
        }

        //SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var flag = await _UserManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        //Signin
                        var result= await _SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if(result.Succeeded)
                        { 
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }

                }
                ModelState.AddModelError("", "Invalid Login !");
            }
            return View(model);
        }

    }
}
