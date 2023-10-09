using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DisasterRecovery.Pages.UserManagement
{
    public class DeleteAdminModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteAdminModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Admin Admin { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.Admins == null)
                return NotFound();

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminID == id);

            if (admin == null)
                return NotFound();
            else
                Admin = admin;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null || _context.Admins == null)
                return NotFound();

            var admin = await _context.Admins.FindAsync(id);
            var user = await _context.Users.FindAsync(id);

            if(admin != null)
                _context.Admins.Remove(admin);

            if (user != null)
                _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return RedirectToPage("./ContractorManagement");
        }
    }
}
