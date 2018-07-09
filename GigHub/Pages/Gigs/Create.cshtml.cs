using System.Linq;
using GigHub.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GigHub.Pages.Gigs
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public GigFormViewModel GigViewModel { get; set; }

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            GigViewModel = new GigFormViewModel { Genres = _dbContext.Genres.ToList() };
        }
    }
}