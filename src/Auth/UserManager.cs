using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Helper;
using Auth.Model;
using Auth.Services;
using Mongo;

namespace Auth
{
    public class UserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }

        public async Task CreateUser(string email, string name, string password)
        {

            var existingUser = await _userService.GetByEmailAsync(email);

            if(existingUser != null)
                throw new InvalidOperationException($"Пользователь с таким email уже существует: {email}");

            var salt = SaltGenerator.GenerateSalt(15);

            var user = new User
            {
                Email = email,
                Name = name,
                Registration = DateTime.Now,
                Password = PasswordServie.HashPassword(password, salt),
                Salt = salt,
                IsConfirm = false,
                
            };

            await _userService.CreateAsync(user);
        }

        public async Task<bool> IsEmailExist(string email)
        {
             return await _userService.IsExistAsync(email);
        }
        
    }
}
