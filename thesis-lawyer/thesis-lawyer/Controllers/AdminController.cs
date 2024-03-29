using Microsoft.AspNetCore.Mvc;
using thesis_lawyer.Data;

namespace thesis_lawyer.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET
    public IActionResult Dashboard()
    {
        var users = _context.UserModels.ToList();
        return View(users);
    }

    public IActionResult Edit()
    {
        return View();
    }

    public IActionResult Delete()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }
}