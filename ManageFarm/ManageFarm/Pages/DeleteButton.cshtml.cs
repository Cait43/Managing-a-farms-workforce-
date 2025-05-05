using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using Microsoft.EntityFrameworkCore;

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

            try
            {
                // try to remove the staff member
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) // if staff cannot be deleted due to constraints
            {
                // set error message 
                TempData["ErrorMessage"] = "Cannot delete this staff member because they are in use elsewhere. Please unassign it first before attempting to delete.";
                return RedirectToPage("/Staff"); // redirect to Staff page where the error is  displayed
            }

            return RedirectToPage("/Staff");
        }
    }
}

