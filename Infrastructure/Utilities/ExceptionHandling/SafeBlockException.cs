﻿using System;
using System.Runtime.Serialization;
using System.Security;

namespace Controls.ExceptionHandling
{
    public class SafeBlockException : Exception
    {
        //
        // Summary:
        //     Initializes a new instance of the System.Exception class with serialized
        //     data.
        //
        // Parameters:
        //   info:
        //     The System.Runtime.Serialization.SerializationInfo that holds the serialized
        //     object data about the exception being thrown.
        //
        //   context:
        //     The System.Runtime.Serialization.StreamingContext that contains contextual
        //     information about the source or destination.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The info parameter is null.
        //
        //   System.Runtime.Serialization.SerializationException:
        //     The class name is null or System.Exception.HResult is zero (0).
        [SecuritySafeCritical]
        protected SafeBlockException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        //
        // Summary:
        //     Initializes a new instance of the System.Exception class with a specified
        //     error message and a reference to the inner exception that is the cause of
        //     this exception.
        //
        // Parameters:
        //   message:
        //     The error message that explains the reason for the exception.
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public SafeBlockException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}