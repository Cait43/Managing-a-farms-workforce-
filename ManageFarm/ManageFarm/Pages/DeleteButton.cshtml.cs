using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;

namespace ManageFarm.Pages
{
    public class DeleteButtonModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteButtonModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Staff");
        }
    }
}

