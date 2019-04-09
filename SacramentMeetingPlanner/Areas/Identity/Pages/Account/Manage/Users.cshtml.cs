using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Areas.Identity.Pages.Account.Manage
{
    public partial class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersModel(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public InputModel Input { get; set; }

        public class InputModel
        {
            public Dictionary<IdentityUser, IList<string>> Users { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new InputModel
            {
                Users = new Dictionary<IdentityUser, IList<string>>()
            };

            var usersList = _userManager.Users.ToList();
            foreach (var user in usersList)
            {
                Input.Users.Add(user, await _userManager.GetRolesAsync(user));
            }

            return Page();
        }
    }
}
