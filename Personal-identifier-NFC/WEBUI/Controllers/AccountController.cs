using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WEBUI.Entities;
using WEBUI.LoginModels;

namespace WEBUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;


        public AccountController(SignInManager<CustomIdentityUser> signInManager, RoleManager<CustomIdentityRole> roleManager, UserManager<CustomIdentityUser> userManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new CustomIdentityUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    KvkkAgreeDate = DateTime.Now,
                    KvkkIsSign=registerViewModel.KvkkIsSign,

                };
                IdentityResult result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                    bool emailResponse = Kernel.SendMailService.sendMail(user.Email, confirmationLink, "Confirm Your Mail For Sugomi Account");
                    if (!_roleManager.RoleExistsAsync("User").Result)
                    {
                        CustomIdentityRole role = new CustomIdentityRole
                        {
                            Name = "User",
                            
                        };
                        IdentityResult roleResult = await _roleManager.CreateAsync(role);
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("","We can't add the role!");
                            return View(registerViewModel);
                        }
                    }
                    _userManager.AddToRoleAsync(user, "User").Wait();
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return RedirectToAction("Index", "Home", registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                CustomIdentityUser appUser = await _userManager.FindByEmailAsync(loginViewModel.Email);
                await _signInManager.SignOutAsync();

                bool emailStatus = await _userManager.IsEmailConfirmedAsync(appUser);
                if (emailStatus == false)
                {
                    ModelState.AddModelError(nameof(loginViewModel.Email), "Email is unconfirmed, please confirm it first");
                }
                else
                {
                    var result = _signInManager.PasswordSignInAsync(appUser, loginViewModel.Password, loginViewModel.RememberMe, false).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(nameof(loginViewModel.Email), "Login Failed: Invalid Email or password");
                }

            }
            
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            //var getset = _webSetService.GetID(1);
            return View(/*getset*/);
        }

        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new CustomIdentityUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    KvkkAgreeDate = DateTime.Now,
                    KvkkIsSign = registerViewModel.KvkkIsSign,
                };
                IdentityResult result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                    bool emailResponse = Kernel.SendMailService.sendMail(user.Email, confirmationLink, "Confirm Your Mail For Sugomi Account");
                    if (!_roleManager.RoleExistsAsync("Admin").Result)
                    {
                        CustomIdentityRole role = new CustomIdentityRole
                        {
                            Name = "Admin",
                            //OlusturulmaTarihi = System.DateTime.Now
                        };
                        IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "We can't add the role!");
                            return View(registerViewModel);
                        }
                    }
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                    return RedirectToAction("/");
                }

            }
            return View(registerViewModel);
        }
        //[Authorize(Roles = "Admin")]
        public ActionResult RegisterAdmin()
        {
            return View();
        }

    }
}
