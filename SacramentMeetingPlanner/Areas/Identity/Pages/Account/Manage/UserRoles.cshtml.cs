using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Areas.Identity.Pages.Account.Manage
{
    public partial class UserRolesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UserModel UserInput { get; set; }

        [BindProperty]
        public List<string> Roles { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> AllRoles { get; set; }

        public class UserModel
        {
            [Required]
            public string Id { get; set; }

            [DataType(DataType.Text)]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Phone")]
            [Phone]
            public string Phone { get; set; }

            [Display(Name = "Old Password")]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }

            [Display(Name = "New Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Confirm New Password")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
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

            UserInput = new UserModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            List<SelectListItem> rolesList = new List<SelectListItem>();
            var userRolesList = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles;

            foreach (var role in roles)
            {
                rolesList.Add(new SelectListItem(role.Name, role.Name, userRolesList.IndexOf(role.Name) >= 0));
            }

            AllRoles = rolesList.AsEnumerable<SelectListItem>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(UserInput.Id);
            if (user == null)
            {
                return NotFound(string.Format("Unable to load user with ID {0}.", UserInput.Id));
            }

            // Update user name
            if (!user.UserName.Equals(UserInput.Name))
                await _userManager.SetUserNameAsync(user, UserInput.Name);

            // Update email address
            if (!user.Email.Equals(UserInput.Email))
                await _userManager.SetEmailAsync(user, UserInput.Email);

            if (user.PhoneNumber != UserInput.Phone)
                await _userManager.SetPhoneNumberAsync(user, UserInput.Phone);

            // Update password
            if (!string.IsNullOrEmpty(UserInput.Password) 
            && !string.IsNullOrEmpty(UserInput.ConfirmPassword)
            && !string.IsNullOrEmpty(UserInput.OldPassword)
            && UserInput.Password == UserInput.ConfirmPassword)
            {
                var changePasswordResult = _userManager.ChangePasswordAsync(user, UserInput.OldPassword, UserInput.Password).Result;
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            // Update roles
            var currentUserRoles = await _userManager.GetRolesAsync(user);

            if (currentUserRoles.Count > 0)
                await _userManager.RemoveFromRolesAsync(user, currentUserRoles);
            if (Roles.Count > 0)
                await _userManager.AddToRolesAsync(user, Roles);
            
            StatusMessage = "User details have been updated.";
            return RedirectToPage("Users");
        }
    }
}
