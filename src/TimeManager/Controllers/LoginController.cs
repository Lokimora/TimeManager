using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Security;
using Auth;
using Auth.Services;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TimeManager.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeManager.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager _userManager;

        public LoginController(IUserService userService,
            UserManager userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// TODO: Дописать хрень, которая бы редириктила залогиненых пользователей
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {            
                var isSuccess =  await _userManager.ValidateUserAsync(model.Email, model.Password);

                if (isSuccess)
                {


                    var user = await _userService.GetByEmailAsync(model.Email);
                    await Authenticate(user.Id.ToString());
                    return new RedirectToRouteResult("home", null);
                }
              
               ModelState.AddModelError("AuthError", "Проверьте правильность почты и пароля");
               return View(model);

            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.Authentication.SignOutAsync("CustomSheme");
            return new RedirectToRouteResult("home");
        }

        private async Task Authenticate(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userId)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "TimeManagerClaims", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.Authentication.SignInAsync("CustomSheme", new ClaimsPrincipal(identity));

        }
    }
}
