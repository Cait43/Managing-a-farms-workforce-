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
           
            _context.Fields.Remove(field);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Fields"); // Redirect back to the Fields list
        }
    }
}
