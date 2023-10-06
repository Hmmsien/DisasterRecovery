using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DisasterRecovery.Pages.MachineCodeManagement
{
    public class DeleteModel : PageModel
    {
        private IConfiguration _configuration;
        private string apiBaseUrl = "http://localhost:5113"; // no slash at the end

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public MachineCode machineCode { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/MachineCode/GetById/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        machineCode = JsonConvert.DeserializeObject<MachineCode>(apiResponse);
                        return Page();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        public async Task<IActionResult> OnPost(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{apiBaseUrl}/api/MachineCode/Delete/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToPage(nameof(Index));
                    }
                    else
                    {
                        ErrorMessage = "We ran into a problem deleting this item. Please Contact Administrator for Assistance.";
                        return Page();
                    }
                }
            }
        }
    }
}
