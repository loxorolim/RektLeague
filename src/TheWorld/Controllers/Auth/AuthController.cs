using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;
using System.Net.Http;

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInManager;
        private UserManager<WorldUser> _userManager;

        public AuthController(SignInManager<WorldUser> signInManager,  UserManager<WorldUser> userManager)
        {
            _signInManager = signInManager;
            _userManager= userManager;

        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("WebPosts","App", new { id = 1 });
                
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                    vm.Password,
                    true, false);
                if(signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("WebPosts", "App", new { id = 1 });
                    else
                        return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Login ou/e password incorreto(s)");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Password == model.PasswordConfirmation)
                {
                    bool loginVerification = (await _userManager.FindByNameAsync(model.Login) == null);
                    bool emailVerification = (await _userManager.FindByEmailAsync(model.Email) == null);
                    if (loginVerification && emailVerification)
                    {
                        //Add user
                        var newUser = new WorldUser()
                        {
                            UserName = model.Login,
                            Email = model.Email
                        };
                        await _userManager.CreateAsync(newUser, model.Password);
                    }
                    else
                    {
                        if(!loginVerification)
                            ModelState.AddModelError("", "Login já existente");
                        if(!emailVerification)
                            ModelState.AddModelError("", "E-mail já existente");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Password e confirmação de password diferem");
                }
   
            }
            return View();
        }


        public async Task<ActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }
    }
}
