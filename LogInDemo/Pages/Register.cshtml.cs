using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogInDemo.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogInDemo.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        [BindProperty]
        public Register Model { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager=userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
            RedirectToAction("");
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = Model.Email,
                    Email = Model.Email
                };
            var result=   await userManager.CreateAsync(user, Model.Password);
                if(result.Succeeded)
                {
                  await  signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }

                foreach (var Error in result.Errors)
                {
                    ModelState.AddModelError("", Error.Description);
                }

            }

            return Page();
        }
    }
}
