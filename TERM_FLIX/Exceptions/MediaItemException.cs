using System;
using System.Collections.Generic;
using System.Text;

namespace TERM_FLIX.Exceptions
{
    public class MediaItemException : Exception
    {
        public MediaItemException(string message) 
            :base (message)
        {
            Console.WriteLine(message);
        }
    }
}
