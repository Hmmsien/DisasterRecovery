using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Reflection.Emit;
using System.Security.Claims;

namespace DisasterRecovery.Pages.TimesheetSubmission
{
    [Authorize(Policy = "ContractorOnly")]
    public class CreateModel : PageModel
    {
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string apiBaseUrl = "http://localhost:5113"; // no slash at the end

        public CreateModel(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string SiteCode { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;
        [BindProperty]
        public List<JobCode> JobCodes { get; set; }
        [BindProperty]
        public List<MachineCode> MachineCodes { get; set; }
        [BindProperty]
        public Timesheet Timesheet { get; set; }
        [BindProperty]
        public LaborEntry LaborEntry1 { get; set; }
        [BindProperty]
        public LaborEntry LaborEntry2 { get; set; }
        [BindProperty]
        public LaborEntry LaborEntry3 { get; set; }
        [BindProperty]
        public MachineEntry MachineEntry1 { get; set; }
        [BindProperty]
        public MachineEntry MachineEntry2 { get; set; }
        [BindProperty]
        public MachineEntry MachineEntry3 { get; set; }
        public List<LaborEntry> LaborEntries { get; set; } = new List<LaborEntry>();
        public List<MachineEntry> MachineEntries { get; set; } = new List<MachineEntry>();

        public async Task<IActionResult> OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetJobCodes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JobCodes = JsonConvert.DeserializeObject<List<JobCode>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetMachineCodes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    MachineCodes = JsonConvert.DeserializeObject<List<MachineCode>>(apiResponse);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetJobCodes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JobCodes = JsonConvert.DeserializeObject<List<JobCode>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetMachineCodes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    MachineCodes = JsonConvert.DeserializeObject<List<MachineCode>>(apiResponse);
                }
            }

            if (Timesheet.SiteCode.IsNullOrEmpty())
            {
                ErrorMessage = "Site code cannot be empty.";
                return Page();
            }

            Timesheet.ContractorID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Timesheet.SubmitedDate = DateTime.Now;

            List<LaborEntry> laborEntriesBuffer = new List<LaborEntry>();
            laborEntriesBuffer.Add(LaborEntry1);
            laborEntriesBuffer.Add(LaborEntry2);
            laborEntriesBuffer.Add(LaborEntry3);
            List<MachineEntry> machineEntriesBuffer = new List<MachineEntry>();
            machineEntriesBuffer.Add(MachineEntry1);
            machineEntriesBuffer.Add(MachineEntry2);
            machineEntriesBuffer.Add(MachineEntry3);

            foreach (var laborEntry in laborEntriesBuffer)
            {
                if (laborEntry.JobCodeName.IsNullOrEmpty()) continue;
                JobCode jobCode = JobCodes.Find(x => x.JobCodeName == laborEntry.JobCodeName);
                if (laborEntry.HrsWorked > jobCode.MaxHoursPerDay || laborEntry.HrsWorked <= 0)
                {
                    ErrorMessage = $"Invalid input. {laborEntry.JobCodeName} must have a minimum of 1 Hrs Worked and a maximum of {jobCode.MaxHoursPerDay} Hrs Worked.";
                    return Page();
                }
                laborEntry.TotalAmount = jobCode.HourlyRate * laborEntry.HrsWorked;
                LaborEntries.Add(laborEntry);
            }

            foreach (var machineEntry in machineEntriesBuffer)
            {
                if (machineEntry.MachineCodeName.IsNullOrEmpty()) continue;
                MachineCode machineCode = MachineCodes.Find(x => x.MachineCodeName == machineEntry.MachineCodeName);
                if (machineEntry.HrsUsed > machineCode.MaxHoursPerDay || machineEntry.HrsUsed <= 0)
                {
                    ErrorMessage = $"Invalid input. {machineEntry.MachineCodeName} must have a minimum of 1 Hrs Used and a maximum of {machineCode.MaxHoursPerDay} Hrs Used.";
                    return Page();
                }
                machineEntry.TotalAmount = machineCode.UsageRate * machineEntry.HrsUsed;
                MachineEntries.Add(machineEntry);
            }

            if (LaborEntries.Count == 0 && MachineEntries.Count == 0)
            {
                ErrorMessage = "Please enter at least 1 entry before you submit";
                return Page();
            }

            Timesheet.LaborEntries = LaborEntries;
            Timesheet.MachineEntries = MachineEntries;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new System.Uri(apiBaseUrl);
                var postTask = httpClient.PostAsJsonAsync<Timesheet>("/api/Timesheet/AddNewTimesheet", Timesheet);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage(nameof(Index));
                }
                else
                {
                    ErrorMessage = "We ran into an error during this request. PLease check your inputs or contact administrator for assistance.";
                    return Page();
                }
            }
        }
    }
}
