using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZipApp
{
    class ErrorManager
    {
        List<Error> _errors;

        public ErrorManager()
        {
            _errors = new List<Error>();
        }

        public void GetErrors()
        {
            foreach (Error err in _errors)
            {
                err.PrintLog();
            }
        }

        public void SetError(Error error)
        {
            _errors.Add(error);
        }
    }

    class Error
    {
        string _errMessage;
        ErrorType _type;

        public Error(string ErrorMessage, ErrorType ErrorType)
        {
            _errMessage = ErrorMessage;
            _type = ErrorType;
        }

        public void PrintLog()
        {
            Console.WriteLine($"error: {_errMessage} :: type: {_type}");
        }
    }

    enum ErrorType
    {
        StartingError,
        AccessError
    }
}
