using ManageFarm.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageFarm.Pages
{

    public class FieldDeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public FieldDeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Field Field { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Field = await _context.Fields.FindAsync(id);

            if (Field == null)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null)
            {
                return NotFound();
            }

            try
            {
                _context.Fields.Remove(field);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Cannot delete this field because it is referenced elsewhere. Please unassign it first before attempting to delete.";
                return RedirectToPage("/Fields"); // redirect back to the Field table page
            }

            return RedirectToPage("/Fields"); // redirect if deletion happens
        }
    }
}

