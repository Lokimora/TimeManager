using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using Auth.Model;
using Auth.Services;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TimeManager.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeManager.Controllers
{
    [AllowAnonymous]
    public class RegistrationController : Controller
    {

        private readonly UserManager _userManager;
        private readonly IUserService _userService;

        public RegistrationController(UserManager userManager,
            IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (await _userService.IsExistAsync(model.Email))
            {
                ModelState.AddModelError("Email", "Email недоступен");
                return View();
            }

            await _userManager.CreateUser(model.Email, model.Name, model.Password);

            return RedirectToAction("login", "login", null);

        }

    }
}
