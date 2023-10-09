using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DisasterRecovery.Pages.MachineCodeManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private IConfiguration _configuration;
        private string apiBaseUrl = "http://localhost:5113"; // no slash at the end

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public MachineCode Input { get; set; } = new MachineCode();

        public async Task<IActionResult> OnGet(int id)
        {
            using(var httpClient = new HttpClient())
            {
                using(var response = await  httpClient.GetAsync($"{apiBaseUrl}/api/MachineCode/GetById/{id}"))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Input = JsonConvert.DeserializeObject<MachineCode>(apiResponse);
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
            string InitialMachineCodeName = "";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/MachineCode/GetById/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        InitialMachineCodeName = JsonConvert.DeserializeObject<MachineCode>(apiResponse).MachineCodeName;
                    }
                    else
                    {
                        ErrorMessage = "We ran into a problem updating. Please Contact Administrator for Assistance.";
                    }
                }
            }

            if (Input.MachineCodeName != InitialMachineCodeName)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiBaseUrl}/api/MachineCode/AlreadyExist/{Input.MachineCodeName}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        bool alreadyexists = JsonConvert.DeserializeObject<bool>(apiResponse);
                        if (alreadyexists)
                        {
                            ErrorMessage = $"A Machine Code with this name already exists.";
                            return Page();
                        }
                    }
                }
            }         

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new System.Uri(apiBaseUrl);
                var postTask = httpClient.PutAsJsonAsync<MachineCode>($"/api/MachineCode/Edit/{id}", Input);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage(nameof(Index));
                }
                else
                {
                    ErrorMessage = "We ran into a problem updating. Please Contact Administrator for Assistance.";
                    return Page();
                }
            }

        }
    }
}
