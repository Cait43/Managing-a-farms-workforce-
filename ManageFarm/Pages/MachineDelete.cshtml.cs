using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using System.Threading.Tasks;

namespace ManageFarm.Pages
{
    public class MachineDeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public MachineDeleteModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Machines");
        }
    }
}

