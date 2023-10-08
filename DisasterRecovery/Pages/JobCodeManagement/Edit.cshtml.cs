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
    public class EditModel : PageModel
    {
        private string apiBaseUrl = "http://localhost:5113";

        [BindProperty]
        public JobCode jobCode { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5113/api/JobCode/Get/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var JobCode = JsonConvert.DeserializeObject<JobCode>(responseData);

                jobCode = JobCode;
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new System.Uri(apiBaseUrl);
                var postTask = httpClient.PutAsJsonAsync<JobCode>($"/api/JobCode/Edit/{id}", jobCode);
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
