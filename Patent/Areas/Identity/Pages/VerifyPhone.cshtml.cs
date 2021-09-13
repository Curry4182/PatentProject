using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Sender;
using Utiles;

namespace Patent.Areas.Identity.Pages
{
    [Authorize]
    public class VerifyPhoneModel : PageModel
    {
        private readonly IVerifyPhoneNumber _verifyPhoneNumber;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMessageSender _messageSender;

        public VerifyPhoneModel(IVerifyPhoneNumber verifyPhoneNumber,
            UserManager<IdentityUser> userManager,
            IMessageSender messageSender
            
            )
        {
            _verifyPhoneNumber = verifyPhoneNumber;
            _userManager = userManager;
            _messageSender = messageSender;
        }

        public string PhoneNumber { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadPhoneNumber();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadPhoneNumber();

            string code = _verifyPhoneNumber.GetCodeByPhoneNumber(PhoneNumber);

            await _messageSender.SendMessageAsync(PhoneNumber, code);

             return Page();
        }

        private async Task LoadPhoneNumber()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new Exception($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            PhoneNumber = user.PhoneNumber;
        }
    }
}
