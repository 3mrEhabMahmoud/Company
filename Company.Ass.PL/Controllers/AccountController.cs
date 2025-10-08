using Company.Ass.DAL.Models;
using Company.Ass.PL.Dtos;
using Company.Ass.PL.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model )
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    //Geerate Token
                    var token = _UserManager.GeneratePasswordResetTokenAsync(user);

                    //Create Url

                    var url = Url.Action("ResetPassword", "Action", new { Email = model.Email }, Request.Scheme);
                    //Create Email
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };
                    //send Email
                    var flag = EmailSetting.SendEmail(email);
                    if (flag)
                    {
                        //check your Inbox

                        return RedirectToAction("CheckYourInbox");
                    }

                }
            }
            ModelState.AddModelError("", "Invalid Reset Passwod Operation !!");
            return View("ForgetPassword",model);
        }

        [HttpGet]
        public IActionResult CheckYourInbox(string email, string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckYourInbox(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["Email"] as string;
                var token = TempData["Token"] as string;
                if (email is null || token is null) return BadRequest("Invalid Operations");
                var user = await _UserManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _UserManager.ResetPasswordAsync(user, token, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                }

            }
            ModelState.AddModelError("", "Invalid Reset Passwod Operation !!");

            return View();
        }

    }

}




