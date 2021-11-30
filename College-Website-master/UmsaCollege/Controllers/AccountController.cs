﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmsaCollege.Models;
using UmsaCollege.Viewscs;


namespace UmsaCollege.Controllers {
    [Authorize]
    public class AccountController : Controller {

        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        
        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        

        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View(new RegisterView
            {
                Errors = new List<string>()
            });
        }

     
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
       
            public async Task<IActionResult> Registration(RegisterView model)
            {
                if (ModelState.IsValid)
                {
                    
                    var user = new AppUser { UserName = model.Email, Email = model.Email};
                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        model.Errors = result.Errors.Select(x => x.Description).ToList();
                    }
                }
                return View(model);
            }
        

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(
                            user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email),
                "Invalid user or password");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
