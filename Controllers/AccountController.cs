using FacebookClone.Data;
using FacebookClone.Models;
using FacebookClone.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserDbContext _context;

    public AccountController(
        UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, 
        UserDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;

    }

    #region login
    [HttpGet]
    public IActionResult Login()
    {
        var user = new LoginViewModel();
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        return View(model);
    }
    #endregion

    #region  Register
    [HttpGet]
    public IActionResult Register()
    {
        var registraition = new RegisterViewModel();
        return View(registraition);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            var newUserInfo = new UserInfo {
                GivenName = model.GivenName,
                SurName = model.SurName,
                Genera = model.Genera,
                DateOfBirth = new DateOnly(model.SelectedYear,model.SelectedMonth,model.SelectedDay)
            };

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                _context.UserInfo.AddAsync(newUserInfo);
                _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
