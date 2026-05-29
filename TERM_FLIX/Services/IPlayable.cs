using System;
using System.Collections.Generic;
using System.Text;
using TERM_FLIX.Models.Enums;

namespace TERM_FLIX.Services
{
    internal interface IPlayable
    {
        string Location { get; set; }
        WatchProgress WatchProgress { get; set; }
        double WatchProgressPercentage { get; set; }
        void Play();
    }
}
