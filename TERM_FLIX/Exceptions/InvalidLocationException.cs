using System;
using System.Collections.Generic;
using System.Text;

namespace TERM_FLIX.Exceptions
{
    internal class InvalidLocationException : MediaItemException
    {
        public InvalidLocationException(string message)
        :base (message) { }
    }
}
