using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DisasterRecovery.Pages.MachineCodeManagement
{
    public class CreateModel : PageModel
    {
        private IConfiguration _configuration;
        private string apiBaseUrl = "http://localhost:5113"; // no slash at the end

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public MachineCode Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new System.Uri(apiBaseUrl);
                var postTask = httpClient.PostAsJsonAsync<MachineCode>("/api/MachineCode/Create", Input);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage(nameof(Index));
                }
                else
                {
                    ErrorMessage = "Please Contact Administrator for Assistance.";
                    return Page();
                }
            }
        }
    }
}
