namespace OnlineCourseManagementSystem.Web.Areas.Identity.Pages.Account
{
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
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext dbContext;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext dbContext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._emailSender = emailSender;
            this.dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [MinLength(5)]
            [MaxLength(30)]
            public string UserName { get; set; }

            [Required]
            [MinLength(3)]
            [MaxLength(30)]

            public string FirstName { get; set; }

            [Required]
            [MinLength(3)]
            [MaxLength(50)]

            public string LastName { get; set; }

            [Required]
            [MinLength(10)]
            [MaxLength(10)]
            public string PhoneNumber { get; set; }

            [Required]
            [MinLength(10)]
            [MaxLength(100)]
            public string Address { get; set; }

            [Required]
            public string Gender { get; set; }

            [Required]
            public string Title { get; set; }

            public string Background { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime BirthDate { get; set; }

            [Required]
            public int TownId { get; set; }

            public string Role { get; set; }

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

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = this.Input.UserName,
                    Email = this.Input.Email,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    PhoneNumber = this.Input.PhoneNumber,
                    TownId = this.Input.TownId,
                    Gender = (Gender)Enum.Parse(typeof(Gender), this.Input.Gender),
                    Title = (Title)Enum.Parse(typeof(Title), this.Input.Title),
                    BirthDate = this.Input.BirthDate,
                    Background = this.Input.Background,
                    Address = this.Input.Address,
                    MachineLearningId = this.dbContext.Users.OrderBy(u => u.MachineLearningId).LastOrDefault().MachineLearningId + 1,
                };

                var result = await this._userManager.CreateAsync(user, this.Input.Password);
                await this._userManager.AddToRoleAsync(user, this.Input.Role);

                if (this.Input.Role == GlobalConstants.StudentRoleName)
                {
                    Student student = new Student
                    {
                        UserId = user.Id,
                    };
                    user.StudentId = student.Id;
                    await this.dbContext.Students.AddAsync(student);
                }
                else if (this.Input.Role == GlobalConstants.LecturerRoleName)
                {
                    Lecturer lecturer = new Lecturer
                    {
                        UserId = user.Id,
                    };
                    user.LecturerId = lecturer.Id;
                    await this.dbContext.Lecturers.AddAsync(lecturer);
                }

                await this.dbContext.SaveChangesAsync();

                if (result.Succeeded)
                {
                    this._logger.LogInformation("User created a new account with password.");

                    var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this._emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this._userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this._signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
