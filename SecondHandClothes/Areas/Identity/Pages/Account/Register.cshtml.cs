namespace SecondHandClothes.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using SecondHandClothes.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using static Data.DataConstants;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Полето 'Имейл' е задължително.")]
            [EmailAddress]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Display(Name = "Име")]
            [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да бъде между {2} и {1} символа дълго.")]
            [Required(ErrorMessage = "Полето 'Име' е задължително.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Полето 'Парола' е задължително.")]
            [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength , ErrorMessage = "Паролата трябва да бъде между {2} и {1} символа дълга.")]
            [Display(Name = "Парола")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Полето 'Потвърди Паролата' е задължително.")]
            [Display(Name = "Потвърди Паролата")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name
                };

                var result = await this.userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
