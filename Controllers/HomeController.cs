using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Williams.Models;

namespace Mission06_Williams.Controllers
{
    public class HomeController : Controller
    {
        private MovieCollectionContext _context;
        
        public HomeController(MovieCollectionContext temp) //Puts database context into the controller
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnow()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EnterMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterMovie(Movie response)
        {
            // Validates that all required fields (like Title, Year, Rating) are filled
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response); // Add record to the database
                _context.SaveChanges();        // Save changes to the SQLite file
                
                // Return a confirmation view or redirect
                return View("Confirmation", response); 
            }
            else 
            {
                // If validation fails, show the form again with error messages
                return View(response);
            }
        }
    }
}