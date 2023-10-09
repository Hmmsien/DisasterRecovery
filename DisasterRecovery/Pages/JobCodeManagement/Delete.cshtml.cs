using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DisasterRecovery.Pages.JobCodeManagement
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public JobCode JobCode { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5113/api/JobCode/Get/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var jobCode = JsonConvert.DeserializeObject<JobCode>(responseData);

                JobCode = jobCode;
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, JobCode jobCode)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync("http://localhost:5113/api/JobCode/Delete/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
            }
            return Page();
        }
    }
}