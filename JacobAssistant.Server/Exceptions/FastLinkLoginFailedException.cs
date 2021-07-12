using System;

namespace JacobAssistant.Exceptions
{
    public class FastLinkLoginFailedException : Exception
    {
        public FastLinkLoginFailedException() : base()
        {
        }

        public FastLinkLoginFailedException(string message) : base(message)
        {
        }
    }
}