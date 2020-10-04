using System;
using System.Collections.Generic;
using System.Net;

namespace App.Common.Exceptions
{
    public abstract class AppBaseException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }

        private IList<AppError> _appErrorsList;
        public IList<AppError> AppErrorsList { get { return _appErrorsList; } }
        public AppBaseException(string message) : base(message)
        {
            _appErrorsList = new List<AppError>
            {
                new AppError
                {
                    Message = message
                }
            };
        }

        public AppBaseException(IList<AppError> AppErrorsList) : base("")
        {
            _appErrorsList = AppErrorsList;
        }
    }
}
