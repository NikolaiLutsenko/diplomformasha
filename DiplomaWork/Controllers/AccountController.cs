using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DiplomaWork.Data;
using DiplomaWork.Models;
using DiplomaWork.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWork.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(
            [FromServices] SignInManager<User> signInManager,
            [FromForm] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimConstants.EmployeeId, user.Id.ToString())
                    };
                    await signInManager.SignInWithClaimsAsync(user, false, claims);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Пароль или имейл введен не правильно");
            }

            return View(loginModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.HrManager)]
        public IActionResult Register()
        {
            return View(new RegisterUserModel());
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.HrManager)]
        public async Task<IActionResult> Register(
            [FromServices] IPasswordValidator<User> passwordValidator,
            [FromForm] RegisterUserModel registerUserModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(registerUserModel.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email уже занят");
                }
                else
                {
                    var newUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = registerUserModel.Email,
                        UserName = registerUserModel.Name
                    };
                    var result = await passwordValidator.ValidateAsync(_userManager, newUser, registerUserModel.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    else
                    {
                        var createUserResult = await _userManager.CreateAsync(newUser, registerUserModel.Password);
                        if (createUserResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home", new { message = "Пользователь создан" });
                        }
                        foreach (var error in createUserResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return View(registerUserModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout([FromServices] SignInManager<User> signInManager)
        {
            if (signInManager.IsSignedIn(User))
                await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
