using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageFarm.Pages
{
    public class MachineEditModel : PageModel
    {
        private readonly AppDbContext _context;

        public MachineEditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Machine Machine { get; set; }

        public List<Field> Fields { get; set; } // add Fields property for dropdown

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // load the machine to be edited
            Machine = await _context.Machines.Include(m => m.Field).FirstOrDefaultAsync(m => m.MachineId == id);

            if (Machine == null)
            {
                return NotFound();
            }

            // populate the list of fields for the dropdown
            Fields = await _context.Fields.ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Save Changes triggered!");

            
            Console.WriteLine($"Machine.FieldId: {Machine.FieldId}");

            if (!ModelState.IsValid)
            {
                // log the errors in the ModelState
                Console.WriteLine("ModelState is invalid.");
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                    }
                }

                // reload the list of fields for the dropdown
                Fields = await _context.Fields.ToListAsync();
                return Page();  // return to the same page to show validation errors
            }

            // if the FieldId is set correctly, the 'Field' property should be updated automatically
            _context.Attach(Machine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Changes saved successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineExists(Machine.MachineId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Machines");  // redirect to the list of machines after saving
        }







        private bool MachineExists(int id)
        {
            return _context.Machines.Any(e => e.MachineId == id);
        }
    }
}
