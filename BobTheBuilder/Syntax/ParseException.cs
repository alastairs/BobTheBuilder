using System;

namespace BobTheBuilder.Syntax
{
    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}
