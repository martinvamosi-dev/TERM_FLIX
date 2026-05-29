using System;
using System.Collections.Generic;
using System.Text;

namespace TERM_FLIX.Exceptions
{
    public class InvalidTitleException : MediaItemException
    {
        public InvalidTitleException(string message)
            : base(message) { }
    }
}
