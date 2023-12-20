using Loginpage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loginpage.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var chkEmail = await userManager.FindByEmailAsync(model.Email);
                    if (chkEmail != null)
                    {
                        ModelState.AddModelError(string.Empty, "Email already exist");
                        return View(model);
                    }
                    var user = new IdentityUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                    };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;

            }
            return View(model);
        } 
        public IActionResult LoginPage() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    IdentityUser checkEmail=await userManager.FindByEmailAsync(model.Email);
                    if (checkEmail==null)
                    {
                        ModelState.AddModelError(string.Empty, "Email not found");
                        return View(model);

                    }
                    if(await userManager.CheckPasswordAsync(checkEmail, model.Password)==false)
                    {
                        ModelState.AddModelError(string.Empty, "Invaild credentials");
                        return View(model);
                    }
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded) 
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Invaild Login Attempt");
                    
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        public async Task<IActionResult>Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LoginPage", "Login");
        }
    }
}
