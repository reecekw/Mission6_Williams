using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission6_Williams.Models;

namespace Mission6_Williams.Controllers;

public class HomeController : Controller
{
    private MovieCollectionContext _context;

    public HomeController(MovieCollectionContext temp)
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
        // Send the list of categories to the view for the dropdown
        ViewBag.Categories = _context.Categories
            .OrderBy(x => x.CategoryName)
            .ToList();

        return View("EnterMovie", new Movie());
    }

    [HttpPost]
    public IActionResult EnterMovie(Movie response)
    {
        if (ModelState.IsValid)
        {
            _context.Movies.Add(response);
            _context.SaveChanges();
            
            // Show the confirmation page and pass the movie title
            return View("Confirmation", response);
        }
        else
        {
            // If invalid, reload the page with the categories again
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();
                
            return View(response);
        }
    }
}