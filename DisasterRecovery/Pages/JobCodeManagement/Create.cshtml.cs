using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterRecovery.Pages.JobCodeManagement
{
	public class CreateModel : PageModel
    {
        private string apiBaseUrl = "http://localhost:5113";

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty]
        public JobCode jobCode { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var httpClient = new HttpClient())
            {
                using (var httpClient2 = new HttpClient())
                {
                    httpClient2.BaseAddress = new System.Uri(apiBaseUrl);
                    var postTask = httpClient2.PostAsJsonAsync<JobCode>("/api/JobCode/Create", jobCode);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToPage(nameof(Index));
                    }
                    else
                    {
                        return Page();
                    }
                }

            }
        }
    }
}
