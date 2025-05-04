using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageFarm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ManageFarm.Pages
{
    public class AssignmentsListModel : PageModel
    {
        private readonly AppDbContext _context;

        public AssignmentsListModel(AppDbContext context)
        {
            _context = context;
        }

        // properties for the list of assignments, machines, staff, and fields
        public List<Assignment> AssignmentsList { get; set; }
        public List<Machine> Machines { get; set; }
        public List<Field> Fields { get; set; }
        public List<Staff> StaffMembers { get; set; }

        // property to bind the new assignment form
        [BindProperty]
        public Assignment NewAssignment { get; set; }

        // on GET request, load assignments and available fields, staff, machines
        public void OnGet()
        {
            // load all assignments with related fields, staff, and machines
            AssignmentsList = _context.Assignments
                .Include(a => a.Staff)
                .Include(a => a.Machine)
                .Include(a => a.Field)
                .ToList();

            // load available fields, machines, and staff for dropdown lists
            Machines = _context.Machines.ToList();
            Fields = _context.Fields.ToList();
            StaffMembers = _context.Staff.ToList();
        }

        // on POST request, handle creating a new assignment
        public IActionResult OnPost()
        {
            Console.WriteLine("Form submitted"); // debugging to ensure the form is being submitted

            // log validation errors
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Error in {modelState.Key}: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                // if model state is invalid, reload assignments and dropdown lists
                AssignmentsList = _context.Assignments
                    .Include(a => a.Staff)
                    .Include(a => a.Machine)
                    .Include(a => a.Field)
                    .ToList();
                Machines = _context.Machines.ToList();
                Fields = _context.Fields.ToList();
                StaffMembers = _context.Staff.ToList();

                return Page(); // return to the page with validation errors
            }

            // add the new assignment to the database
            _context.Assignments.Add(NewAssignment);
            _context.SaveChanges();
            Console.WriteLine("Assignment added to the database");

            return RedirectToPage(); // refresh the page after adding the assignment
        }
    }
}
