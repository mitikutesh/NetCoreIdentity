using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NetCore3Identity.Data;

namespace NetCore3Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string  Password { get; set; }

        [FromQuery]
        public string ReturnUrl { get; set; }
        public LoginModel( UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user!=null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password,false,false);
                if (result.Succeeded)
                    return RedirectToPage(ReturnUrl);
            }
            return RedirectToPage("/Account/Register");
        }

        //public IActionResult Authenticate()
        //{
        //    var myclaims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.Name, "Mitiku"),
        //        new Claim(ClaimTypes.Email, "mitikutesh@gmailc.om")
        //    };

        //    var myIdentity = new ClaimsIdentity(myclaims, "My Claim");
        //    var userPrincipal = new ClaimsPrincipal(new[] { myIdentity });

        //    HttpContext.SignInAsync(userPrincipal);
        //    return RedirectToAction("Index");
        //}

    }
}
