using TERM_FLIX.Models.Enums;
using TERM_FLIX.Exceptions;
using System.Diagnostics;
using TERM_FLIX.Services;

namespace TERM_FLIX.Models
{
    internal class Episode : IPlayable
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public int EpisodeNumber { get; set; }
        public string Description { get; set; }
        public WatchProgress WatchProgress { get; set; }
        public double WatchProgressPercentage { get; set; }

        public Episode(string title, string location, int episodeNumber, string description, WatchProgress watchProgress, double watchProgressPercentage)
        {
            if (string.IsNullOrWhiteSpace(title))
            { throw new InvalidTitleException("The title for an Episode cant be null/empty/full of white space"); }
            if (string.IsNullOrWhiteSpace(location))
            { throw new InvalidLocationException("The location for a Episode cant be null/empty/full of white space"); }
            this.Title = title;
            this.Location = location;
            this.EpisodeNumber = episodeNumber;
            this.Description = description;
            this.WatchProgress = watchProgress;
            this.WatchProgressPercentage = watchProgressPercentage;
        }

        public void Play()
        {
            ProcessStartInfo mediaPlay = new ProcessStartInfo();
            mediaPlay.FileName = this.Location;
            mediaPlay.UseShellExecute = true;
            Process.Start(mediaPlay).WaitForExit(); 
        }
    }
}
