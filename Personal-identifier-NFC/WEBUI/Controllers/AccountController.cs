using Business.Abstract;
using Entities.Models;
using Entities.Models.Dto.LoginModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using WEBUI.Entities;
using WEBUI.Kernel;

namespace WEBUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly IPersonalService _personalService;


        public AccountController(SignInManager<CustomIdentityUser> signInManager, RoleManager<CustomIdentityRole> roleManager, UserManager<CustomIdentityUser> userManager, IPersonalService personalService)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _personalService = personalService;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        //[ValidateAntiForgeryToken]
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
                    KvkkIsSign = registerViewModel.KvkkIsSign,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
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
                            ModelState.AddModelError("", "We can't add the role!");
                            return View(registerViewModel);
                        }
                    }
                    _userManager.AddToRoleAsync(user, "User").Wait();
                    var userIdFind = await _userManager.FindByEmailAsync(registerViewModel.Email);
                    var SlugCreator = (String.IsNullOrEmpty(registerViewModel.Slug) ? SlugHelper.Slugify(registerViewModel.FirstName + " " + registerViewModel.LastName) : registerViewModel.Slug);
                    var slugcheck = _personalService.SlugCheck(SlugCreator);
                    _personalService.CreateNewPersonal(new Personal { Slug = (slugcheck ? SlugCreator : registerViewModel.UserName), UserId = userIdFind.Id, LastUpdate = DateTime.Now, });
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("Index", "Home", registerViewModel);
        }

        [HttpPost("Login")]
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
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
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
                    var userIdFind = await _userManager.FindByEmailAsync(registerViewModel.Email);
                    var SlugCreator = (String.IsNullOrEmpty(registerViewModel.Slug) ? SlugHelper.Slugify(registerViewModel.FirstName + " " + registerViewModel.LastName) : registerViewModel.Slug);
                    var slugcheck = _personalService.SlugCheck(SlugCreator);
                    _personalService.CreateNewPersonal(new Personal { Slug = (slugcheck ? SlugCreator : registerViewModel.UserName), UserId = userIdFind.Id, LastUpdate = DateTime.Now, });
                    return RedirectToAction("/");
                }

            }
            return View(registerViewModel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterAdmin()
        {
            return View();
        }

    }
}
