using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages
{
    public class MessagesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        public void OnGet(int messageId)
        {
        }
    }
}
