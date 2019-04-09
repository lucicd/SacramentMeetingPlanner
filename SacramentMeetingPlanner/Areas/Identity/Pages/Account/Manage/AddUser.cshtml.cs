using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Areas.Identity.Pages.Account.Manage
{
    public partial class AddUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AddUserModel(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Confirm Password")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Passwords do not match
            if (Input.Password != Input.ConfirmPassword)
            {
                StatusMessage = "The passwords do not match!";
                return Page();
            }

            // Username or email does not exist
            if (_userManager.FindByEmailAsync(Input.Email).Result != null)
            {
                StatusMessage = "The email address already exists! Choose another.";
                return Page();
            }

            if (_userManager.FindByNameAsync(Input.Name).Result != null)
            {
                StatusMessage = "The username already exists! Choose another.";
                return Page();
            }

            var user = new IdentityUser
            {
                UserName = Input.Name,
                Email = Input.Email,
                EmailConfirmed = true,
                TwoFactorEnabled = false
            };

            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                var userSaved = await _userManager.FindByNameAsync(Input.Name);
                await _userManager.AddToRoleAsync(userSaved, "Administrator");

                StatusMessage = "User account successfully created.";
                return RedirectToPage();
            }

            return NotFound(string.Join(" ", result.Errors));
        }
    }
}
