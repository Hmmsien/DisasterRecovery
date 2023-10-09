using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DisasterRecovery.Pages.UserManagement
{
    public class DeleteContractorModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteContractorModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contractor Contractor { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.Contractors == null)
                return NotFound();

            var contractor = await _context.Contractors.FirstOrDefaultAsync(c => c.ContractorID == id);

            if (contractor == null)
                return NotFound();
            else
                Contractor = contractor;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null || _context.Contractors == null)
                return NotFound();

            var contractor = await _context.Contractors.FindAsync(id);
            var user = await _context.Users.FindAsync(id);

            if (contractor != null)
                _context.Contractors.Remove(contractor);

            if (user != null)
                _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return RedirectToPage("./ContractorManagement");
        }
    }
}
