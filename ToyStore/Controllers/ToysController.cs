using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ToyStore.Model;
using ToyStore.Services.Interfaces;
using ToyStore.Controllers.Base;
using ToyStore.Api_mapping.Toys.Create;
using ToyStore.Api_mapping.Toys.GetAll;
using ToyStore.Api_mapping.Toys.Update;
using ToyStore.Api_mapping.Toys.Delete;



namespace ToyStore.Controllers
{
    public class ToysController : ApiController
    {
        private readonly ToyStoreDbContext _context;
                         ILogger<MainPageModel> _logger;
                         IToyService _toyService;
                         IMapper _mapper;

        public ToysController(ToyStoreDbContext context, ILogger<MainPageModel> logger, IToyService toyService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _toyService = toyService;
            _mapper = mapper;
        }

        // GET: Toys
        //public async Task<IActionResult> Index(string? searchTerm, string? sortOrder, int? pageNumber)
        //{
        //    var pageModel = new MainPageModel(_logger, _toyService); // You can also inject the logger here
        //                                                // Retrieve data using model
        //    await pageModel.OnGetAsync(searchTerm, sortOrder, pageNumber);

        //    // Return View, passing the populated model.
        //    return View("~/Views/Toys/Index.cshtml", pageModel);
        //    // return View("MainPage", pageModel);

        //}

        // GET: Toys/Details/5
        [HttpGet("get/all")]
        [ProducesResponseType(typeof(GetAllToysResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Details(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var toy = await _context.Toys
            //    .Include(t => t.Category)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (toy == null)
            //{
            //    return NotFound();
            //}

            //return View(toy);
            try
            {
                var toys = await _toyService.GetAllToysAsync();
                var response = new GetAllToysResponse
                {
                    Toys = _mapper.Map<List<Toy>, List<GetAllToysDTO>>(toys)
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        // GET: Toys/Create
        //[Authorize]
        //public IActionResult Create()
        //{
        //    ViewData["CategoryId"] = new SelectList(_context.ToyCategories, "Id", "Name");
        //    return View();
        //}

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateToyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateToyRequest request)
        {
            //var category = await _context.ToyCategories.FindAsync(toy.CategoryId);
            //if (category != null)
            //{
            //    toy.Category = category;
            //}

            //ModelState.Remove("Category");


            //if (ModelState.IsValid)
            //{
            //    await _toyService.CreateToyAsync(toy);
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.ToyCategories, "Id", "Name", toy.CategoryId);
            //return View(toy);
            try
            {
                var category = await _context.ToyCategories.FindAsync(request.CategoryId);
                if (category != null)
                {
                    request.Category = category;
                    ModelState.Remove("Category");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var toy = _mapper.Map<CreateToyRequest, Toy>(request);

                var createdTask = await _toyService.CreateToyAsync(toy);

                var response = new CreateToyResponse { Id = createdTask.Id };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        //// GET: Toys/Edit/5
        //[Authorize]
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var toy = await _context.Toys.FindAsync(id);
        //    if (toy == null)
        //    {
        //        return NotFound();
        //    }
        //    //else
        //    //{
        //    //    await _toyService.UpdateToyAsync(toy);
        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    ViewData["CategoryId"] = new SelectList(_context.ToyCategories, "Id", "Name", toy.CategoryId);
        //    return View(toy);
        //}


        // POST: Toys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit_Submit(UpdateToysRequest request)
        {
            //if (id != toy.Id)
            //{
            //    return NotFound();
            //}

            //ModelState.Remove("Category");

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        //_context.Update(toy);
            //        //await _context.SaveChangesAsync();
            //        await _toyService.UpdateToyAsync(toy);
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ToyExists(toy.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.ToyCategories, "Id", "Name", toy.CategoryId);
            //return View(toy);

            try
            {
                var category = await _context.ToyCategories.FindAsync(request.CategoryId);
                if (category != null)
                {
                    request.Category = category;
                    ModelState.Remove("Category");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var toy = _mapper.Map<UpdateToysRequest, Toy>(request);
                await _toyService.UpdateToyAsync(toy);
                return Ok();
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        // GET: Toys/Delete/5
        //[Authorize]
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var toy = await _context.Toys
        //        .Include(t => t.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (toy == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(toy);
        //}

        // POST: Toys/Delete/5
        [HttpPost("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteConfirmed(DeleteToyRequest request)
        {
            try
            {
                var deleteRequest = new DeleteToyRequest { Id = request.Id };
                var toy = _mapper.Map<Toy>(deleteRequest);
                if (toy != null)
                {
                    await _toyService.DeleteToyAsync(toy.Id);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }

            //var toy = await _context.Toys.FindAsync(id);
            //if (toy != null)
            //{
            //    _context.Toys.Remove(toy);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        //private bool ToyExists(Guid id)
        //{
        //    return _context.Toys.Any(e => e.Id == id);
        //}
    }
}
