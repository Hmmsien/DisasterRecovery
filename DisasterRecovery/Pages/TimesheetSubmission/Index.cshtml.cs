using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DisasterRecovery.Pages.TimesheetSubmission
{
    [Authorize(Policy = "ContractorOnly")]
    public class IndexModel : PageModel
    {
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string apiBaseUrl = "http://localhost:5113"; // no slash at the end

        public IndexModel(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public List<TimesheetTotals> timesheetTotals { get; set; } = new List<TimesheetTotals>();
        public class TimesheetTotals
        {
            public int TimesheetID { get; set; }
            public DateTime SubmitedDate { get; set; }
            public string SiteCode { get; set; } = string.Empty;
            public int TotalHrs { get; set; }
            public double TotalAmount { get; set; }
            public string Status { get; set; } = string.Empty;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Timesheet> timesheets;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetTimesheetsForContractor/{userId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    timesheets = JsonConvert.DeserializeObject<IEnumerable<Timesheet>>(apiResponse);
                }
            }

            if(!(timesheets.Count() > 0))
            {
                return Page();
            }

            foreach(var timesheet in timesheets)
            {
                TimesheetTotals t = new TimesheetTotals();
                int totalHrs;
                double totalAmount;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetTotalHrsForTimesheet/{timesheet.TimesheetID}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        totalHrs = JsonConvert.DeserializeObject<int>(apiResponse);
                    }
                }

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetTotalAmountForTimesheet/{timesheet.TimesheetID}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        totalAmount = JsonConvert.DeserializeObject<double>(apiResponse);
                    }
                }

                t.TimesheetID = timesheet.TimesheetID;
                t.SubmitedDate = timesheet.SubmitedDate;
                t.SiteCode = timesheet.SiteCode;
                t.TotalHrs = totalHrs;
                t.TotalAmount = totalAmount;
                t.Status = timesheet.Status;

                timesheetTotals.Add(t);
            }

            return Page();
        }
    }
}
