using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterRecovery.Pages
{
    [Authorize(Policy = "AnyUser")]
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
