using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models; 
using System.Collections.Generic;
using System.Linq;

namespace ManageFarm.Pages
{
    public class StaffListModel : PageModel
    {
        private readonly AppDbContext _context;

        public StaffListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Staff> StaffList { get; set; }

        [BindProperty]
        public Staff NewStaff { get; set; }

        public void OnGet()
        {
            StaffList = _context.Staff.ToList();
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("Form submitted"); // checking the db is writing properly
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Error in {modelState.Key}: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                StaffList = _context.Staff.ToList(); // print list
                return Page();
            }

            _context.Staff.Add(NewStaff);
            _context.SaveChanges();

            return RedirectToPage(); // Refresh
        }

    }
}

