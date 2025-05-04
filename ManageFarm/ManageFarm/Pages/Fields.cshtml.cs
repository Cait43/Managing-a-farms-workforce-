using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageFarm.Pages
{
    public class FieldsModel : PageModel
    {
        private readonly AppDbContext _context;

        public FieldsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Field> FieldList { get; set; }

        [BindProperty]
        public Field NewField { get; set; }

        public void OnGet()
        {
            // load all fields from the database
            FieldList = _context.Fields.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Form submitted");

            // checking the model state
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Error in {modelState.Key}: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                // reload the list of fields in case of validation error
                FieldList = _context.Fields.ToList();
                return Page();
            }

            // add the new field to the database
            _context.Fields.Add(NewField);
            await _context.SaveChangesAsync();

            // after adding the new field, redirect to the same page to refresh the list
            return RedirectToPage();
        }
    }
}
