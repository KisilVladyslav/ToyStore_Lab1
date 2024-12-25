using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToyStore.Services.Interfaces;
//using YourNamespace.Data; // Replace with your data namespace
//using YourNamespace.Models; // Replace with your models namespace
//using YourNamespace.Services; // Replace with your services namespace

namespace ToyStore.Model
{
    public class MainPageModel : PageModel
    {
        private readonly ILogger<MainPageModel> _logger;
        private readonly IToyService _toyService;


        public MainPageModel(ILogger<MainPageModel> logger, IToyService toyService)
        {
            _logger = logger;
            _toyService = toyService;
        }

        public List<Toy> Toys { get; set; } = new List<Toy>();
        public string SearchTerm { get; set; }
        public string SortOrder { get; set; } = "NameAsc"; // Default sort order
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // Number of items per page
        public int TotalPages { get; set; }
        public Dictionary<string, string> RouteData { get; set; }


        public async Task OnGetAsync(string? searchTerm, string? sortOrder, int? pageNumber)
        {
            SearchTerm = searchTerm;
            SortOrder = sortOrder ?? SortOrder;
            CurrentPage = pageNumber ?? 1;

            RouteData = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                RouteData.Add("searchTerm", SearchTerm);
            }
            RouteData.Add("sortOrder", SortOrder);

            if (CurrentPage > 1)
            {
                RouteData.Add("pageNumber", CurrentPage.ToString());
            }

            var toysQuery = (await GetAllToysAsync()).AsQueryable();


            // Filter by search term
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                toysQuery = toysQuery.Where(t => t.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Sort based on selected option
            switch (SortOrder)
            {
                case "NameAsc":
                    toysQuery = toysQuery.OrderBy(t => t.Name);
                    break;
                case "NameDesc":
                    toysQuery = toysQuery.OrderByDescending(t => t.Name);
                    break;
                case "PriceAsc":
                    toysQuery = toysQuery.OrderBy(t => t.Price);
                    break;
                case "PriceDesc":
                    toysQuery = toysQuery.OrderByDescending(t => t.Price);
                    break;
                default:
                    toysQuery = toysQuery.OrderBy(t => t.Name);
                    break;
            }


            // Apply pagination
            TotalPages = (int)Math.Ceiling((double)toysQuery.Count() / PageSize);
            var toys = toysQuery.Skip((CurrentPage - 1) * PageSize)
                  .Take(PageSize)
                  .ToList();

            Toys = toys;
        }

        public async Task<List<Toy>> GetAllToysAsync()
        {
            return await _toyService.GetAllToysAsync();
        }
    }
}