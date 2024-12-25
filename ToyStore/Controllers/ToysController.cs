using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ToyStore.Model;
using ToyStore.Services.Interfaces;

namespace ToyStore.Controllers
{
    public class ToysController : Controller
    {
        private readonly ToyStoreDbContext _context; ILogger<MainPageModel> _logger; IToyService _toyService;

        public ToysController(ToyStoreDbContext context, ILogger<MainPageModel> logger, IToyService toyService)
        {
            _context = context;
            _logger = logger;
            _toyService = toyService;
        }

        // GET: Toys
        public async Task<IActionResult> Index(string? searchTerm, string? sortOrder, int? pageNumber)
        {
            var pageModel = new MainPageModel(_logger, _toyService); // You can also inject the logger here
                                                        // Retrieve data using model
            await pageModel.OnGetAsync(searchTerm, sortOrder, pageNumber);

            // Return View, passing the populated model.
            return View("~/Views/Toys/Index.cshtml", pageModel);
            // return View("MainPage", pageModel);

        }

        // GET: Toys/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toys
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toy == null)
            {
                return NotFound();
            }

            return View(toy);
        }

        // GET: Toys/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ToyCategories, "Id", "Name");
            return View();
        }

        // POST: Toys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(Toy toy)
        {
            var category = await _context.ToyCategories.FindAsync(toy.CategoryId);
            if (category != null)
            {
                toy.Category = category;
            }
            
            ModelState.Remove("Category");
                        

            if (ModelState.IsValid)
            {
                await _toyService.CreateToyAsync(toy);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ToyCategories, "Id", "Name", toy.CategoryId);
            return View(toy);
        }

        // GET: Toys/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toys.FindAsync(id);
            if (toy == null)
            {
                return NotFound();
            }
            //else
            //{
            //    await _toyService.UpdateToyAsync(toy);
            //    return RedirectToAction(nameof(Index));
            //}
            ViewData["CategoryId"] = new SelectList(_context.ToyCategories, "Id", "Name", toy.CategoryId);
            return View(toy);
        }

        // POST: Toys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Submit(Guid id, Toy toy)
        {
            if (id != toy.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(toy);
                    //await _context.SaveChangesAsync();
                    await _toyService.UpdateToyAsync(toy);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToyExists(toy.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.ToyCategories, "Id", "Name", toy.CategoryId);
            return View(toy);
        }

        // GET: Toys/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toys
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toy == null)
            {
                return NotFound();
            }

            return View(toy);
        }

        // POST: Toys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var toy = await _context.Toys.FindAsync(id);
            if (toy != null)
            {
                _context.Toys.Remove(toy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToyExists(Guid id)
        {
            return _context.Toys.Any(e => e.Id == id);
        }
    }
}
