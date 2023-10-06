using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterRecovery.Pages.UserManagement
{
	public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IEnumerable<Admin> Admins { get; set; }

        public IEnumerable<Contractor> Contractors { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Admins = _context.Admins.ToList();
            Contractors = _context.Contractors.ToList();
        }
    }
}
