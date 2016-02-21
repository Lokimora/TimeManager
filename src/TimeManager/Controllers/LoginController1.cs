using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using Auth.Services;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TimeManager.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeManager.Controllers
{
    [AllowAnonymous]
    public class LoginController1 : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager _userManager;

        public LoginController1(IUserService userService,
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
            if (!ModelState.IsValid)
                return View();

            var isSuccess = await _userManager.ValidateUser(model.Email, model.Password);

            if (isSuccess)
            {
                var user = await _userService.GetByEmailAsync(model.Email);
                AuthManager.Login(user.Id.ToString());
                return new RedirectToActionResult("index", "home", null);
            }


            ModelState.AddModelError("AuthError", "Проверьте правильность почты и пароля");
            return View();
        }

        [HttpGet]
        public IActionResult Logoff()
        {
            AuthManager.Logoff();

            return new RedirectToActionResult("login", "login", null);
        }
    }
}
