using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using thesis_lawyer.Models;
namespace thesis_lawyer.Controllers;

public class ChatController : Controller
{
    public IActionResult chatlawyer()
    {
        return View();
    }
}