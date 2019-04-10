using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SacramentMeetingPlanner.Areas.Identity.Pages.Account.Manage
{
    public partial class DeleteUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteUserModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Id { get; set; }

            [DataType(DataType.Text)]
            public string Name { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Phone")]
            [Phone]
            public string Phone { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(string.Format("Unable to load user with ID {0}.", id));
            }

            Input = new InputModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null)
            {
                return NotFound(string.Format("Unable to load user with ID {0}.", Input.Id));
            }

            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                StatusMessage = "The user was deleted sucessfully!";
                return RedirectToPage("Users");
            }
            else
            {
                StatusMessage = "Unable to delete user. Please, try again!";
                ModelState.AddModelError("delete_user", StatusMessage);
                return Page();
            }
        }
    }
}
