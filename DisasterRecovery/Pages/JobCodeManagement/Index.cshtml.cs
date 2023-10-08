using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DisasterRecovery.Pages.JobCodeManagement
{
	public class IndexModel : PageModel
    {
        public IEnumerable<JobCode> JobCodes { get; set; }

        public async Task<IActionResult> OnGet()
        {
            using(var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync("http://localhost:5113/api/JobCode/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JobCodes = JsonConvert.DeserializeObject<IEnumerable<JobCode>>(apiResponse);
                }
            };

            return Page();
        }
    }
}
