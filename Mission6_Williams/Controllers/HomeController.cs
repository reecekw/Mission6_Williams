using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission6_Williams.Models;
using Microsoft.EntityFrameworkCore;

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

    public IActionResult MovieList()
    {
        // Get all movies, include the Category details, and sort by Title
        var movies = _context.Movies
            .Include(x => x.Category) 
            .OrderBy(x => x.Title)
            .ToList();

        return View(movies);
    }
    
    // GET: Fills the form with the movie's current data
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var record = _context.Movies
            .Single(x => x.MovieId == id);

        // Reuse the dropdown list logic
        ViewBag.Categories = _context.Categories
            .OrderBy(x => x.CategoryName)
            .ToList();

        // Send the user to the "EnterMovie" view, but with data this time
        return View("EnterMovie", record);
    }

    // POST: Saves the changes
    [HttpPost]
    public IActionResult Edit(Movie updatedInfo)
    {
        if (ModelState.IsValid)
        {
            _context.Update(updatedInfo); // Updates the record instead of Adding
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }
        else
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View("EnterMovie", updatedInfo);
        }
    }
    // GET: Finds the movie and sends it to the Delete confirmation page
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var record = _context.Movies
            .Single(x => x.MovieId == id);
            
        return View(record);
    }

    // POST: Actually deletes the movie after the user clicks "Delete"
    [HttpPost]
    public IActionResult Delete(Movie movie)
    {
        _context.Movies.Remove(movie);
        _context.SaveChanges();
        
        return RedirectToAction("MovieList");
    }
}