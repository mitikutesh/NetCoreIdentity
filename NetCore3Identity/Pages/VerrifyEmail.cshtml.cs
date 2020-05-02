using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetCore3Identity.Pages
{
    public class VerrifyEmailModel : PageModel
    {
        //public string UserName { get; set; }
        //public string Token { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public string UserId { get; set; }
        public string Token { get; set; }

        public VerrifyEmailModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
             _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            UserId = userId;
            Token = code;
            var user = await _userManager.FindByIdAsync(userId);
            if(user!=null)
            {
                var res = await _userManager.ConfirmEmailAsync(user, code);
                if (res.Succeeded)
                    return Page();
            }
            return RedirectToPage("/Account/Register");
        }
    }
}
