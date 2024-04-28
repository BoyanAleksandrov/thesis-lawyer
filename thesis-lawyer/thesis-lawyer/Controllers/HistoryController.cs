using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thesis_lawyer.Data;
using thesis_lawyer.Models;

namespace thesis_lawyer.Controllers;

public class HistoryController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserModel> _userManager;
    
    public HistoryController(UserManager<UserModel> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
       
    }
    // GET
    public IActionResult List()
    {
        var user = _userManager.GetUserAsync(User).Result;
        var ChatHistory = _context.Chat.Include(c => c.User).Where(c => c.User.Id == user.Id).ToList();
        return View(ChatHistory);
    }
}