using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageFarm.Pages
{
    public class AssignmentEditModel : PageModel
    {
        private readonly AppDbContext _context;

        public AssignmentEditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Assignment Assignment { get; set; }

        public List<Field> Fields { get; set; } // add properties for dropdown.
        public List<Staff> StaffList { get; set; } 
        public List<Machine> Machines { get; set; } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // load the assignment to be edited, including related entities (Field, Staff, Machine)
            Assignment = await _context.Assignments
                .Include(a => a.Field)
                .Include(a => a.Staff)
                .Include(a => a.Machine)
                .FirstOrDefaultAsync(a => a.AssignmentId == id);

            if (Assignment == null)
            {
                return NotFound();
            }

            // populate the list of fields, staff, and machines for the dropdowns
            Fields = await _context.Fields.ToListAsync();
            StaffList = await _context.Staff.ToListAsync();
            Machines = await _context.Machines.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Save Changes triggered!");

            // debugging - log the value of the fields
            Console.WriteLine($"Assignment.FieldId: {Assignment.FieldId}, Assignment.StaffId: {Assignment.StaffId}, Assignment.MachineId: {Assignment.MachineId}");

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

                // reload the list of fields, staff, and machines for the dropdowns
                Fields = await _context.Fields.ToListAsync();
                StaffList = await _context.Staff.ToListAsync();
                Machines = await _context.Machines.ToListAsync();
                return Page();  // return to the same page to show validation errors
            }

            
            if (Assignment.AssignmentId == 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid assignment ID.");
                return Page();
            }

            // attach the entity and mark it as modified
            _context.Attach(Assignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Changes saved successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(Assignment.AssignmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Assignments");  // redirect to the list of assignments after saving
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.AssignmentId == id);
        }
    }
}
