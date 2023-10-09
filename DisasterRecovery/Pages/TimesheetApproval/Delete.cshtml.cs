using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DisasterRecovery.Pages.TimesheetApproval
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
        public string ContractorName { get; set; }
        [BindProperty]
        public Timesheet Timesheet { get; set; }
        [BindProperty]
        public List<LaborEntry> LaborEntries { get; set; }
        [BindProperty]
        public List<MachineEntry> MachineEntries { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetTimesheetById/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Timesheet = JsonConvert.DeserializeObject<Timesheet>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetContractorById/{Timesheet.ContractorID}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Contractor contractor = JsonConvert.DeserializeObject<Contractor>(apiResponse);
                    ContractorName = contractor.FirstName + " " + contractor.LastName;
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetLaborEntriesForTimesheet/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    LaborEntries = JsonConvert.DeserializeObject<List<LaborEntry>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetMachineEntriesForTimesheet/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    MachineEntries = JsonConvert.DeserializeObject<List<MachineEntry>>(apiResponse);
                }
            }


            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new System.Uri(apiBaseUrl);
                var postTask = httpClient.DeleteAsync($"/api/Timesheet/DeleteTimesheet/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage(nameof(Index));
                }
                else
                {
                    ErrorMessage = "We ran into a problem deleting this timesheet. Please Contact Administrator for Assistance.";
                    return Page();
                }
            }

        }
    }
}
