using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Model
{
    public class ConfirmResult
    {
        public bool IsSuccess { get; set; }

        public string ErrorResult { get; set; }


        public ConfirmResult(bool isSuccess, string errorResult)
        {
            IsSuccess = isSuccess;
            ErrorResult = errorResult;
        }
    }
}
