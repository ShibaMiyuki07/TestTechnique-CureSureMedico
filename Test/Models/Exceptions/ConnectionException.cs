﻿using System.Runtime.Serialization;

namespace Test.Models.Exceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException()
        {
            throw new ConnectionException("Database connection error");
        }

        public ConnectionException(string? message) : base(message)
        {
        }

        public ConnectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
