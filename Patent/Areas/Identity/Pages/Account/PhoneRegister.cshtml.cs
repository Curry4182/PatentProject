using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Sender;
using Utiles;

namespace Patent.Areas.Identity.Pages.Account
{
    public class PhoneRegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMessageSender _messageSender;
        private readonly IVerifyPhoneNumber _verifyPhoneNumber;
        private readonly ISaveAndLoad _saveAndLoad;

        [BindProperty]
        public InputModel Input { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public PhoneRegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IMessageSender messageSender,
            IVerifyPhoneNumber verifyPhoneNumber,
            ISaveAndLoad saveAndLoad
            )
        {
            Input = new InputModel();
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _messageSender = messageSender;
            _verifyPhoneNumber = verifyPhoneNumber;
            _saveAndLoad = saveAndLoad;
        }

        public class InputModel
        {

            [Required]
            [StringLength(11)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Code")]
            public string Code { get; set; }
        }

        public async Task<IActionResult> OnPostSendVerificationPhoneNumberAsync()
        {
            var phoneNumber = Input.PhoneNumber;

            string code = _verifyPhoneNumber.GetCodeByPhoneNumber(phoneNumber);

            await _messageSender.SendMessageAsync(phoneNumber, code);

            _saveAndLoad.Save(Input.PhoneNumber, new Tuple<bool>(IsPhoneNumberConfirmed));
            return RedirectToAction("PhoneRegister", new { phoneNumber = Input.PhoneNumber });
        }

        public async Task<IActionResult> OnPostVerifyPhoneNumberAsync()
        {
            IsPhoneNumberConfirmed = _verifyPhoneNumber.CheckCode(Input.PhoneNumber, Input.Code);

            if (IsPhoneNumberConfirmed)
            {
                StatusMessage = "인증이 완료되었습니다.";
            }
            else
            {
                StatusMessage = "인증에 실패했습니다.";
            }

            _saveAndLoad.Save(Input.PhoneNumber, new Tuple<bool>(IsPhoneNumberConfirmed));
            return RedirectToAction("PhoneRegister", new { phoneNumber = Input.PhoneNumber });
        }

        public void Load(string phoneNumber)
        {
            var item = _saveAndLoad.Load(phoneNumber);

            if (item is Tuple<bool> model)
            {
                IsPhoneNumberConfirmed = model.Item1;
                Input.PhoneNumber = phoneNumber;
            }
        }
        public IActionResult OnGet(string phoneNumber = null)
        {
            if (phoneNumber == null) return Page();

            Load(phoneNumber);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Load(Input.PhoneNumber);

            if (ModelState.IsValid && IsPhoneNumberConfirmed)
            {
                _saveAndLoad.Save(Input.PhoneNumber, new Tuple<bool> (IsPhoneNumberConfirmed ));
                return RedirectToPage("Register", new { phoneNumber = Input.PhoneNumber });
            }

            return RedirectToAction("PhoneRegister", new { phoneNumber = Input.PhoneNumber });
        }
    }
}
