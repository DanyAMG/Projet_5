using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly UserManager<IdentityUser> userManager;
    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }
    [HttpGet]
    public IActionResult Login()
    {
        ViewData["Title"] = "Connexion";
        return View(new LoginViewModel());
    }

    [HttpPost]
    [Route("Account/Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        Console.WriteLine("Login POST method called");

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe incorrect.");
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}