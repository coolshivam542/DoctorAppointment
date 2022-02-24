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
    public class LogInModel : PageModel
    {
        private readonly  SignInManager<IdentityUser> SignInManager;
        public LogInModel(SignInManager<IdentityUser> signInManager)
        {
            this.SignInManager = signInManager;
        }
        [BindProperty]
        public LogIn Model { get; set; }
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync( string ReturnUrl=null)
        {
            if(ModelState.IsValid)
            {
              var identityuser=  await SignInManager.PasswordSignInAsync(Model.EmailAddress, Model.Password, Model.RememberMe,false);
               
                if(identityuser.Succeeded)
                {
                    if(ReturnUrl==null || ReturnUrl=="/")
                    {
                    var res=    SignInManager.IsSignedIn(User);
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        return RedirectToPage(ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password is Incorrect");
                }

            }

            return Page();
        }
    }
}
