using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TERM_FLIX.Models.Enums;
using TERM_FLIX.Services;

namespace TERM_FLIX.Models
{
    internal class SingleMediaItem : MediaItem, IPlayable
    {
        public SingleMediaItem(string title, string location, MediaType mediaType, string description, string yearOfMaking, WatchProgress watchProgress, double watchProgressPercentage)
            : base(title, location, mediaType, description, yearOfMaking, watchProgress, watchProgressPercentage)
        { } // constructor stays empty for now

        public void Play()
        {
            ProcessStartInfo mediaPlay = new ProcessStartInfo();
            mediaPlay.FileName = this.Location;
            mediaPlay.UseShellExecute = true;
            Process.Start(mediaPlay).WaitForExit();
        }
    }
}
