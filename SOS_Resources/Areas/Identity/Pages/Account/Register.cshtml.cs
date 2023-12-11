// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SOS_Resources.Areas.Identity.Data;


namespace SOS_Resources.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<SOS_User> _signInManager;
        private readonly UserManager<SOS_User> _userManager;
        private readonly IUserStore<SOS_User> _userStore;
        private readonly IUserEmailStore<SOS_User> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<SOS_User> userManager,
            IUserStore<SOS_User> userStore,
            SignInManager<SOS_User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {


            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Hendrix ID*")]
            [RegularExpression(@"[0-9]{5,6}")]
            public string HendrixID { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "First Name*")]
            public string FName { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name*")]
            public string LName { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Pronouns*")]
            public string Pronouns { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Preferred Name*")]
            public string PrefName { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email*")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password*")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password*")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            
            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Classification*")]
            public string Class { get; set; }

            [Required]
            [StringLength(15)]
            [DataType(DataType.Text)]
            [Display(Name = "Phone Number*")]
            public string PhoneNumber { get; set; }


            [Required]
            [StringLength(70)]
            [DataType(DataType.Text)]
            [Display(Name = "Campus Address*")]
            public string CampusAdd { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Campus Email*")]
            public string CampusEmail { get; set; }


            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Contact First Name*")]
            public string EmergFName { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Contact Last Name*")]
            public string EmergLName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Emergency Contact Email*")]
            public string EmergEmail { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Contact Relationship*")]
            public string EmergRelation { get; set; }

            [Required]
            [StringLength(15)]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Contact Phone Number*")]
            public string EmergPhone { get; set; }


            [DataType(DataType.Text)]
            [StringLength(30)]
            [Display(Name = "Employer Name")]
            public string Employer { get; set; }

            [DataType(DataType.Text)]
            [StringLength(15)]
            [Display(Name = "Employer Phone Number")]
            public string EmployerPhone { get; set; }

            [DataType(DataType.Text)]
            [StringLength(50)]
            [Display(Name = "Job Position")]
            public string JobPosition { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Pay Type*")]
            public string PayType { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Pay Frequency*")]
            public string PayFreq { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Monthly Wage Amount")]
            public int MonthlyWages { get; set; }

            [DataType(DataType.Text)]
            [StringLength(50)]
            [Display(Name = "Referred By")]
            public string ReferredBy { get; set; }

            
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.HendrixID = Input.HendrixID;
                user.FName = Input.FName;
                user.LName = Input.LName;
                user.Email = Input.Email;
                user.PhoneNumber = Input.PhoneNumber;
                user.Class = Input.Class;
                user.CampusAdd = Input.CampusAdd;
                user.CampusEmail = Input.CampusEmail;
                user.EmergFName = Input.EmergFName;
                user.EmergLName = Input.EmergLName;
                user.EmergEmail = Input.EmergEmail;
                user.EmergRelation = Input.EmergRelation;
                user.EmergPhone = Input.EmergPhone;
                user.Employer = Input.Employer;
                user.EmployerPhone = Input.EmployerPhone;
                user.JobPosition = Input.JobPosition;
                user.PayType = Input.PayType;
                user.PayFreq = Input.PayFreq;
                user.MonthlyWages = Input.MonthlyWages;
                user.ReferredBy = Input.ReferredBy;


                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private SOS_User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<SOS_User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(SOS_User)}'. " +
                    $"Ensure that '{nameof(SOS_User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<SOS_User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<SOS_User>)_userStore;
        }
    }
}
