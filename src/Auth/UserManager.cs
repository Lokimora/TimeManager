using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Helper;
using Auth.Model;
using Auth.Services;
using Mongo;
using MongoDB.Bson;

namespace Auth
{
    public class UserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<bool> ValidateUser(string email, string password)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user == null)
                return false;

            return PasswordServie.ComparePassword(password, user.Password, user.Salt);
        }

        public async Task<User> CreateUser(string email, string name, string password)
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

            user.ConfirmationHash = HashGenerator.HashMD5(user.Id.ToString());

            await _userService.CreateAsync(user);

            return user;
        }

        
        public async Task<ConfirmResult> ConfirmEmail(string userId, string md5Hash)
        {
            ObjectId id;

            if (ObjectId.TryParse(userId, out id))
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                    return new ConfirmResult(false, "Пользователь ненайден");

                if (user.IsConfirm)
                    return new ConfirmResult(true, null);

                if (!HashGenerator.VerifyMd5(userId, md5Hash))
                    return new ConfirmResult(false, "Хэши не совпадают");

                user.IsConfirm = true;
                await _userService.UpdateAsync(user, p => p.IsConfirm);

                return new ConfirmResult(true, null);
            }

            return new ConfirmResult(false, "UserId не валиден");

        }

        public async Task<bool> IsEmailExist(string email)
        {
             return await _userService.IsExistAsync(email);
        }
        
    }
}
