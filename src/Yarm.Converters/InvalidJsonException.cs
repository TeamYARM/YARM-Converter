using System;

namespace Yarm.Converters
{
    /// <summary>
    /// This represents the exception entity when invalid JSON string has been detected.
    /// </summary>
    public class InvalidJsonException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidJsonException"/> class.
        /// </summary>
        public InvalidJsonException() : this("Invalid JSON string")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidJsonException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidJsonException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidJsonException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner <see cref="Exception"/> instance.</param>
        public InvalidJsonException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}