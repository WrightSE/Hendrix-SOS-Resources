// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SOS_Resources.Areas.Identity.Data;
 
namespace SOS_Resources.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<SOS_User> _userManager;
        private readonly SignInManager<SOS_User> _signInManager;

        public IndexModel(
            UserManager<SOS_User> userManager,
            SignInManager<SOS_User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public string Username { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Hendrix ID")]
            public string HendrixID { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Pronouns*")]
            public string Pronouns { get; set; }

            [Required]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Display(Name = "Preferred Name")]
            public string PrefName { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Classification")]
            public string Class { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }


            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Campus Address")]
            public string CampusAdd { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Campus Email")]
            public string CampusEmail { get; set; }


            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Contact First Name")]
            public string EmergFName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Contact Last Name")]
            public string EmergLName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Emergency Contact Email")]
            public string EmergEmail { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Contact Relationship")]
            public string EmergRelation { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Contact Phone Number")]
            public string EmergPhone { get; set; }


            [DataType(DataType.Text)]
            [Display(Name = "Employer Name")]
            public string Employer { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Employer Phone Number")]
            public string EmployerPhone { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Job Position")]
            public string JobPosition { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Pay Type")]
            public string PayType { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Pay Frequency")]
            public string PayFreq { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Monthly Wage Amount")]
            public int MonthlyWages { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Referred By")]
            public string ReferredBy { get; set; }
                    
        }

        private async Task LoadAsync(SOS_User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var

            Username = userName;

            Input = new InputModel
            {
                HendrixID = user.HendrixID,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Pronouns = user.Pronouns,
                PrefName = user.PrefName,
                PhoneNumber = user.PhoneNumber,
                Class = user.Class,
                CampusAdd = user.CampusAdd,
                CampusEmail = user.CampusEmail,
                EmergFName = user.EmergFName,
                EmergLName = user.EmergLName,
                EmergEmail = user.EmergEmail,
                EmergRelation = user.EmergRelation,
                EmergPhone = user.EmergPhone,
                Employer = user.Employer,
                EmployerPhone = user.EmployerPhone,
                JobPosition = user.JobPosition,
                PayType = user.PayType,
                PayFreq = user.PayFreq,
                MonthlyWages = user.MonthlyWages,
                ReferredBy = user.ReferredBy
                
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
        if (Input.HendrixID != user.HendrixID){
            user.HendrixID = Input.HendrixID;
        }
        if (Input.FName != user.FName){
            user.FName = Input.FName;
        }
        if (Input.LName != user.LName){
            user.LName = Input.LName;
        }
        if (Input.Email != user.Email){
            user.Email = Input.Email;
        }
        if (Input.PhoneNumber != user.PhoneNumber){
            user.PhoneNumber = Input.PhoneNumber;
        }
        if (Input.PrefName != user.PrefName){
            user.PrefName = Input.PrefName;
        }
        if (Input.Pronouns != user.Pronouns){
            user.Pronouns = Input.Pronouns;
        }


        if (Input.Class != user.Class){
            user.Class = Input.Class;
        }
        if(Input.CampusAdd != user.CampusAdd){
            user.CampusAdd = Input.CampusAdd;
        }
        if(Input.CampusEmail != user.CampusEmail){
            user.CampusEmail = Input.CampusEmail;
        }


        if (Input.EmergFName != user.EmergFName){
            user.EmergFName = Input.EmergFName;
        }
        if (Input.EmergLName != user.EmergLName){
            user.EmergLName = Input.EmergLName;
        }
        if (Input.EmergEmail != user.EmergEmail){
            user.EmergEmail = Input.EmergEmail;
        }
        if (Input.EmergPhone != user.EmergPhone){
            user.EmergPhone = Input.EmergPhone;
        }


        if (Input.Employer != user.Employer){
            user.Employer = Input.Employer;
        }
        if (Input.EmployerPhone != user.EmployerPhone){
            user.EmployerPhone = Input.EmployerPhone;
        }
        if (Input.JobPosition != user.JobPosition){
            user.JobPosition = Input.JobPosition;
        }
        if (Input.PayType != user.PayType){
            user.PayType = Input.PayType;
        }
        if (Input.PayFreq != user.PayFreq) {
            user.PayFreq = Input.PayFreq;
        }
        if (Input.MonthlyWages != user.MonthlyWages){
            user.MonthlyWages = Input.MonthlyWages;
        }

        if (Input.ReferredBy != user.ReferredBy){
            user.ReferredBy = Input.ReferredBy;
        }

        await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
