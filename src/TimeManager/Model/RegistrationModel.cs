using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManager.Model
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Введите имя пользователя", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите вашу почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
