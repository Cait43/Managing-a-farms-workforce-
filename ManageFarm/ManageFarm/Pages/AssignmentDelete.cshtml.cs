using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageFarm.Pages
{
    public class AssignmentDeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public AssignmentDeleteModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            try
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Cannot delete this assignment because it is referenced elsewhere. Please unassign it first before attempting to delete. ";
                return RedirectToPage("/Assignments"); 
            }

            return RedirectToPage("/Assignments"); // redirect to table page
        }
    }
}
