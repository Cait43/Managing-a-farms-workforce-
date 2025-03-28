using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageFarm.Models;

namespace ManageFarm.Controllers
{
    public class TaskController : Controller
    {
        private readonly FarmDatabaseContext _context;

        public TaskController(FarmDatabaseContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var farmDatabaseContext = _context.Tasks.Include(t => t.Field).Include(t => t.Machine).Include(t => t.Staff);
            return View(await farmDatabaseContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Field)
                .Include(t => t.Machine)
                .Include(t => t.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["FieldId"] = new SelectList(_context.Fields, "Id", "Id");
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Id");
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Id");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,FieldId,MachineId,StaffId")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FieldId"] = new SelectList(_context.Fields, "Id", "Id", task.FieldId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Id", task.MachineId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Id", task.StaffId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["FieldId"] = new SelectList(_context.Fields, "Id", "Id", task.FieldId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Id", task.MachineId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Id", task.StaffId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,FieldId,MachineId,StaffId")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FieldId"] = new SelectList(_context.Fields, "Id", "Id", task.FieldId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Id", task.MachineId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Id", task.StaffId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Field)
                .Include(t => t.Machine)
                .Include(t => t.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
