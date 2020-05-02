using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NETCore.MailKit.Core;

namespace NetCore3Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [FromQuery]
        public string UserId { get; set; }
       [FromQuery]
        public string Token { get; set; }
        [FromQuery]
        public string action { get; set; }
        [FromQuery]
        public string controller { get; set; }

        [FromQuery]
        public string ReturnUrl { get; set; }


        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailService emaiService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emaiService;
        }

        public void OnGet()
        {
           
        }

        [HttpGet]
        public IActionResult EmailConfirmation(string userId, string token, string action, string controller)
        {
            return RedirectToPage("/EmailVerification");
        }

        public async Task<IActionResult> OnPost()
        {
            var user = new IdentityUser { UserName = UserName };
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Page(
                       "/VerrifyEmail",
                       pageHandler: null,
                       values: new { userId = user.Id, code = token },
                       protocol: Request.Scheme);
                await _emailService.SendAsync("test@test.com", "Email Verifiy",$"<a href=\"{callbackUrl}\"></a>");

                return RedirectToPage("/EmailVerification");
            }

            return RedirectToPage("/Home");
        }

    }
}
