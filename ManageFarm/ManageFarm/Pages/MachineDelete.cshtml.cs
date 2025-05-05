using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageFarm.Pages
{
    public class MachineDeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public MachineDeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            try
            {
                _context.Machines.Remove(machine);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ErrorMessage = "Cannot delete this machine because it's in use elsewhere. Please unassign it first before attempting to delete.";
                return RedirectToPage("/Machines"); // Redirect to the list page or stay here with 'return Page();'
            }

            return RedirectToPage("/Machines");
        }
    }
}

