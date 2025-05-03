using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ManageFarm.Pages
{
    public class MachinesListModel : PageModel
    {
        private readonly AppDbContext _context;

        public MachinesListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Machine> MachinesList { get; set; }

        // list of fields to display in the dropdown
        public List<Field> Fields { get; set; }

        [BindProperty]
        public Machine NewMachine { get; set; }

        public void OnGet()
        {
            // load all machines with related fields 
            MachinesList = _context.Machines
                .Include(m => m.Field) // include related field data
                .ToList();

            // load available fields
            Fields = _context.Fields.ToList();
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("Form submitted"); // checking if the form is being submitted

            // checking the model state
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Error in {modelState.Key}: {error.ErrorMessage}");
                }
            }
            ModelState.Remove("NewMachine.Field");
            if (!ModelState.IsValid)
            {
                MachinesList = _context.Machines
                    .Include(m => m.Field)
                    .ToList();
                Fields = _context.Fields.ToList();
                return Page();
            }

            _context.Machines.Add(NewMachine);
            _context.SaveChanges();
            Console.WriteLine("Machine added to the database");

            return RedirectToPage(); // refresh the page after adding the machine
        }
    }
}
