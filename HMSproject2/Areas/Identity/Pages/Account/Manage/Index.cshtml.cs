// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HMSproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HMSproject.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Patient> _userManager;
        private readonly SignInManager<Patient> _signInManager;

        public IndexModel(
            UserManager<Patient> userManager,
            SignInManager<Patient> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            
            [Phone]
            [Display(Name = "Phone number")]
            [StringLength(11, ErrorMessage = "The {0} must be at of {2} digits long.", MinimumLength = 11)]
            [RegularExpression("^[0-9]*$",ErrorMessage = "The {0} field must contain only digits.")]
            public string? PhoneNumber { get; set; }
            
            [StringLength(6, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
            [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "The {0} field must contain only characters.")]
            [Display(Name = "Gender")]
            public string? Gender { get; set; }
            
            [Display(Name = "Age")]
            [Range(0,100,ErrorMessage = "The age must be between {1} and {2}")]
            public int? Age { get; set; }
            
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
            [Display(Name = "Address")]
            public string? Address { get; set; }
            
            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [RegularExpression("^[a-zA-Z ]+$",ErrorMessage = "The {0} field must contain only characters.")]
            [Display(Name = "Full Name")]
            public string? Name { get; set; }
            
            [Required]
            [StringLength(14, ErrorMessage = "The {0} must be of {2} digits long.", MinimumLength = 14)]
            [RegularExpression("^[0-9]*$", ErrorMessage = "The {0} field must contain only digits.")]
            [Display(Name = "National ID: (SSN)")]
            public string SSN { get; set; }
            
            [Display(Name = "Your Profile Picture")]
            public byte[] profilePic { get; set; }

        }

        private async Task LoadAsync(Patient user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Name = user.Name,
                SSN = user.SSN,
                PhoneNumber = phoneNumber,
                profilePic = user.profilePic,
                Gender = user.Gender,
                Age = user.Age,
                Address = user.Address
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
            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }

            if (Input.SSN != user.SSN)
            {
                user.SSN = Input.SSN;
            }
            if (Input.Age != user.Age)
            {
                user.Age = Input.Age;
            }
            if (Input.Gender != user.Gender)
            {
                user.Gender = Input.Gender;
            }
            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
            }
            if (Input.PhoneNumber != user.PhoneNumber)
            {
                user.PhoneNumber = Input.PhoneNumber;
            }
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
            
                //check file size and extension
            
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.profilePic = dataStream.ToArray();
                }
            
                await _userManager.UpdateAsync(user);
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
