using System.Diagnostics;
using CC_Regist_System.Database;
using CC_Regist_System.Enums;
using CC_Regist_System.Models;
using CC_Regist_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CC_Regist_System.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDBContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, ApplicationDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
        {
            return RedirectToAction("Login", "Auth");
        }

        var user = _context.Users
        .Include(u => u.Card)
        .ThenInclude(c => c.CardDetail)
        .FirstOrDefault(u => u.Username == HttpContext.Session.GetString("Username"));

        ViewData["HasCard"] = user.Card != null;

        return View(user);
    }

    [Route("DeleteCard/{id}")]
    [HttpDelete]
    public IActionResult DeleteCard(int id)
    {
        var card = _context.Cards.Find(id);
        if (card == null)
        {
            return NotFound(new { message = "Card not found" });
        }

        _context.Cards.Remove(card);
        _context.SaveChanges();

        return Ok(new { message = "Card deleted successfully" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
