﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghasedak.Exceptions
{
    public class ConnectionException : GhasedakApiException
    {
        /// <summary>
        /// GhasedakApi error code
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// HTTP status code
        /// </summary>
        public int Status { get; }


        /// <summary>
        /// Create a ApiException with message
        /// </summary>
        /// <param name="message">Exception message</param>
        public ConnectionException(string message) : base(message) { }

        /// <summary>
        /// Create an ApiException from another Exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="exception">Exception to copy detatils from</param>
        public ConnectionException(string message, Exception exception) : base(message, exception) { }

        /// <summary>
        /// Create an ApiException
        /// </summary>
        /// <param name="code">GhasedakApi error code</param>
        /// <param name="status">HTTP status code</param>
        /// <param name="message">Error message</param>
        /// <param name="exception">Original exception</param>
        public ConnectionException(
            int code,
            int status,
            string message,
            Exception exception = null
        ) : base(message, exception)
        {
            Code = code;
            Status = status;
        }
    }
}