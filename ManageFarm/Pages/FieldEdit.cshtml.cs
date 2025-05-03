using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ManageFarm.Pages
{
    public class FieldEditModel : PageModel
    {
        private readonly AppDbContext _context;

        public FieldEditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Field Field { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // load the field to be edited
            Field = await _context.Fields.FirstOrDefaultAsync(f => f.FieldId == id);

            if (Field == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Save Changes triggered!");

            if (!ModelState.IsValid)
            {
                // Llog the errors in the ModelState
                Console.WriteLine("ModelState is invalid.");
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                    }
                }

                return Page();  // return to the same page to show validation errors
            }

            _context.Attach(Field).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Changes saved successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldExists(Field.FieldId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Fields");  // redirect to the list of fields after saving
        }

        private bool FieldExists(int id)
        {
            return _context.Fields.Any(e => e.FieldId == id);
        }
    }
}
