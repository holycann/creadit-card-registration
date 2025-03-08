using System;
using System.Threading.Tasks;
using BCrypt.Net;
using CC_Regist_System.Database;
using CC_Regist_System.Models.Users;
using CC_Regist_System.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AuthController : Controller
{
    private readonly ApplicationDBContext _context;

    public AuthController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["IsRegisterPage"] = true;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
        {
            return RedirectToAction("Index", "Home");
        }

        if (Request.Cookies.TryGetValue("RememberMe", out var username))
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("UserID", user.UserID);
                return RedirectToAction("Index", "Home");
            }
        }

        ViewData["IsLoginPage"] = true;
        return View();
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        Response.Cookies.Delete("RememberMe");

        return Ok(new { message = "Logout successfully!" });
    }

    [HttpPost]
    public async Task<IActionResult> UserLogin(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Username and Password are required!" });
        }

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username);
        if (user == null || !VerifyPassword(user.Password, model.Password))
        {
            return BadRequest(new { message = "Invalid Username or Password!" });
        }

        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetInt32("UserID", user.UserID);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(7),
            SameSite = SameSiteMode.Strict,
        };
        Response.Cookies.Append("RememberMe", user.Username, cookieOptions);

        return Ok(new { message = "Login successful!" });
    }

    [HttpPost]
    public async Task<IActionResult> UserRegister(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await _context.Users.AnyAsync(u => u.Username == model.Username))
        {
            return BadRequest(new { message = "Username already taken." });
        }

        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
        {
            return BadRequest(new { message = "Email already registered." });
        }

        if (await _context.Users.AnyAsync(u => u.PhoneNumber == model.PhoneNumber))
        {
            return BadRequest(new { message = "Phone Number already registered." });
        }

        var user = new UsersModel
        {
            Username = model.Username,
            Fullname = model.Fullname,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Password = HashPassword(model.Password),
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully!" });
    }

    private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    private bool VerifyPassword(string hashedPassword, string inputPassword) =>
        BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
}
