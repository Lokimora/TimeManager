using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Model
{
    public enum LoginResult
    {
        //Неверные данные
        WrongCrdentials = 1,
        
        //Необходимо подтвердить email
        NeedConfirmEmail = 2,

        //Удачно
        Success = 3

    }
}
