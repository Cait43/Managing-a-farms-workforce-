using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using System.Threading.Tasks;

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

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Assignments");    //redirecting back to table
        }
    }
}
