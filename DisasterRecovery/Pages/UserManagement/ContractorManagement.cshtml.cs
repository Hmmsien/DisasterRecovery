using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterRecovery.Pages.UserManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class ContractorManagementModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IEnumerable<Contractor> Contractors { get; set; }

        public string Email { get; set; }

        public ContractorManagementModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            Contractors = _context.Contractors.ToList();
        }

        public async Task<string> getEmailById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            Email = user.Email;

            await Task.Delay(1000);
            return Email;
        }
    }
}
