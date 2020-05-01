using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetCore3Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [FromQuery]
        public string ReturnUrl { get; set; }


        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = new IdentityUser { UserName = UserName };
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                var temp = await _signInManager.PasswordSignInAsync(user, Password, false, false);
                if (temp.Succeeded)
                    return RedirectToPage("/Index");
            }

            return RedirectToPage("/Account/Register");
        }
    }
}
