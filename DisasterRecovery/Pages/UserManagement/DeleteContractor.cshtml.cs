using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterRecovery.Pages.UserManagement
{
	public class DeleteContractorModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteContractorModel(ApplicationDbContext context)
        {
            _context = context;
        }

        //[BindProperty]
        //public 
        //public void OnGet()
        //{
        //}
    }
}
