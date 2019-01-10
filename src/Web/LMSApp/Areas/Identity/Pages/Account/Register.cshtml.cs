using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LMSApp.Data.Models;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LMSApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        //Have to store it in Jason
        private const int EducatorPassword = 001100;

        private readonly SignInManager<LMSAppUser> _signInManager;
        private readonly UserManager<LMSAppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;
        private readonly IEducatorService _educatorService;
        private readonly IGroupService _groupService;

        public RegisterModel(
            UserManager<LMSAppUser> userManager,
            SignInManager<LMSAppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUserService userService,
            IEducatorService educatorService,
            IGroupService groupService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _userService = userService;
            _educatorService = educatorService;
            _groupService = groupService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            public string Role { get; set; }

            [Required]
            public int Code { get; set; }

            [Required]
            [Display(Name = "Faculty Name")]
            public FacultyOf FacultyName { get; set; }

            [Required]
            [Display(Name = "Group Number")]
            public int GroupNumber { get; set; }

            [Required]
            public Major Major { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Family Name")]
            public string FamilyName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (Input.Role == "Student")
            {
                if (_userService.AnyStudent(Input.Code, Input.FacultyName))
                {
                    ModelState.AddModelError(string.Empty, "Student with this UniId and Faculty already registered.");
                    return Page();
                }
            }
            else
            {
                if (Input.Code != EducatorPassword)
                {
                    ModelState.AddModelError(string.Empty, "Your educator code is wrong.");
                    return Page();
                }
            }

            if (ModelState.IsValid)
            {
                var user = new LMSAppUser {
                    UserName = Input.Username,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    FamilyName = Input.FamilyName };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {                   
                    if (Input.Role == "Student")
                    {
                       await this._userManager.AddToRoleAsync(user, "Student");

                        string groupId;

                        if (this._groupService.AnyGroup(Input.GroupNumber, Input.Major))
                        {
                            groupId = this._groupService.GetAll()
                                .Where(g => g.Major == Input.Major && g.Number == Input.GroupNumber)
                                .First().Id;
                        }
                        else
                        {
                            groupId = await this._groupService.CreateAsync(new GroupViewModel()
                            {
                                Major = Input.Major,
                                Number = Input.GroupNumber
                            });
                        }

                            await this._userService.CreateAsync(new StudentBindingModel()
                        {
                            UserId = user.Id,
                            StudentUniId = Input.Code,
                            FacultyName = Input.FacultyName,
                            Major = Input.Major,
                            GroupId = groupId
                        });
                    }
                    else
                    {
                        await this._userManager.AddToRoleAsync(user, "Educator");

                        await this._educatorService.CreateAsync(new EducatorBindingModel()
                        {
                            UserId = user.Id,
                            FacultyName = Input.FacultyName
                        });
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
