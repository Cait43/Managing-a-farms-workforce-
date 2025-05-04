using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageFarm.Pages
{
    public class EditButtonModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditButtonModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.Staff.FindAsync(id);

            if (Staff == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(Staff.StaffId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Staff");  
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.StaffId == id);
        }
    }
}
