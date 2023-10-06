using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DisasterRecovery.Pages.MachineCodeManagement
{
    public class IndexModel : PageModel
    {
        private IConfiguration _configuration;
        private string apiBaseUrl = "http://localhost:5113"; // no slash at the end

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public List<MachineCode> MachineCodeList { get; set; } = new List<MachineCode>();

        public async Task<IActionResult> OnGetAsync()
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/MachineCode/Index"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    MachineCodeList = JsonConvert.DeserializeObject<List<MachineCode>>(apiResponse);
                }
            }

            return Page();
        }
    }
}
