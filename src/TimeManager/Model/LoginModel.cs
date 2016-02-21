using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManager.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите email адрес")]       
        [DataType(DataType.EmailAddress)]
        [StringLength(256, MinimumLength = 3)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
