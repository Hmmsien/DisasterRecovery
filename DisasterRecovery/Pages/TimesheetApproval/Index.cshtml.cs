using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace DisasterRecovery.Pages.TimesheetApproval
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
        public List<TimesheetTotals> PendingTimesheetTotals { get; set; } = new List<TimesheetTotals>();
        public List<TimesheetTotals> ApprovedTimesheetTotals { get; set; } = new List<TimesheetTotals>();
        public class TimesheetTotals
        {
            public int TimesheetID { get; set; }
            public string ContractorName { get; set; } = string.Empty;
            public DateTime SubmitedDate { get; set; }
            public string SiteCode { get; set; } = string.Empty;
            public int TotalHrs { get; set; }
            public double TotalAmount { get; set; }
            public string Status { get; set; } = string.Empty;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            IEnumerable<Timesheet> allPendingTimesheets;
            IEnumerable<Timesheet> allApprovedTimesheets;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetAllPendingTimesheets"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    allPendingTimesheets = JsonConvert.DeserializeObject<IEnumerable<Timesheet>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetAllApprovedTimesheets"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    allApprovedTimesheets = JsonConvert.DeserializeObject<IEnumerable<Timesheet>>(apiResponse);
                }
            }

            if (!(allPendingTimesheets.Count() > 0) && !(allApprovedTimesheets.Count() > 0))
            {
                return Page();
            }

            foreach (var timesheet in allPendingTimesheets)
            {
                TimesheetTotals t = new TimesheetTotals();
                int totalHrs;
                double totalAmount;
                Contractor contractor;

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
                

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetContractorById/{timesheet.ContractorID}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        contractor = JsonConvert.DeserializeObject<Contractor>(apiResponse);
                    }
                }

                t.TimesheetID = timesheet.TimesheetID;
                t.ContractorName = contractor.FirstName + " " + contractor.LastName;
                t.SubmitedDate = timesheet.SubmitedDate;
                t.SiteCode = timesheet.SiteCode;
                t.TotalHrs = totalHrs;
                t.TotalAmount = totalAmount;
                t.Status = timesheet.Status;

                PendingTimesheetTotals.Add(t);
            }

            foreach (var timesheet in allApprovedTimesheets)
            {
                TimesheetTotals t = new TimesheetTotals();
                int totalHrs;
                double totalAmount;
                Contractor contractor;

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


                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/Timesheet/GetContractorById/{timesheet.ContractorID}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        contractor = JsonConvert.DeserializeObject<Contractor>(apiResponse);
                    }
                }

                t.TimesheetID = timesheet.TimesheetID;
                t.ContractorName = contractor.FirstName + " " + contractor.LastName;
                t.SubmitedDate = timesheet.SubmitedDate;
                t.SiteCode = timesheet.SiteCode;
                t.TotalHrs = totalHrs;
                t.TotalAmount = totalAmount;
                t.Status = timesheet.Status;

                ApprovedTimesheetTotals.Add(t);
            }

            return Page();
        }
    }
}
