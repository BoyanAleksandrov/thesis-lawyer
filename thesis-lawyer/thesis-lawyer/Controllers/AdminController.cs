using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using thesis_lawyer.Areas.Identity.Pages.Account;
using thesis_lawyer.Data;
using thesis_lawyer.Models;

namespace thesis_lawyer.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserModel> _userManager;

    public AdminController(ApplicationDbContext context, UserManager<UserModel> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    
    public IActionResult Dashboard()
    {
        var users = _context.UserModels.ToList();
        return View(users);
    }

    public IActionResult Edit(string id)
    {
        var user = _context.UserModels.Find(id);
        if (user == null)
        {
            return RedirectToAction("Dashboard", "Admin");
        }
        var userDto = new UpdateUserModel()
        {
            FirstName = user.FirstName,
            Email = user.Email,
            IsPremium = user.isPremium,
            UserName = user.Email,
            NormalizedUserName = user.Email.ToUpper(),
            NormalizedEmail = user.Email.ToUpper()

        };
        ViewData["UserId"] = user.Id;
        return View(userDto);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(string id,UpdateUserModel model)
    {
        var user = _context.UserModels.Find(id);

        user.FirstName = model.FirstName;
        user.Email = model.Email;
        user.isPremium = model.IsPremium;
        user.UserName = model.Email;
        user.NormalizedUserName = model.Email.ToUpper();
        user.NormalizedEmail = model.Email.ToUpper();
        if (!string.IsNullOrEmpty(model.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
        }
        _context.SaveChanges();

        return RedirectToAction("Dashboard", "Admin");
    }

    public IActionResult Delete(string id)
    {
        var user = _context.UserModels.Find(id);
       if (user == null)
       {
           return RedirectToAction("Dashboard", "Admin");
       }

       _context.UserModels.Remove(user);
       _context.SaveChanges(true);

       return RedirectToAction("Dashboard", "Admin");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserModel user)
    {
        if (ModelState.IsValid)
        {


            var userM = new UserModel()
            {
                FirstName = user.FirstName,
                Email = user.Email,
                isPremium = user.IsPremium,
                UserName = user.Email,
                NormalizedUserName = user.Email.ToUpper(),
                NormalizedEmail = user.Email.ToUpper()

            };
           
            var result = await _userManager.CreateAsync(userM, user.Password);


            if (result.Succeeded)
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View();
    }

    
}