using TERM_FLIX.Models.Enums;
using TERM_FLIX.Exceptions;

namespace TERM_FLIX.Models
{
    abstract class MediaItem
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public MediaType MediaType { get; set; }
        public string Description { get; set; }
        public string YearOfMaking { get; set; }
        public WatchProgress WatchProgress { get; set; }
        public double WatchProgressPercentage { get; set; }

        protected MediaItem(string title, string location, MediaType mediaType, string description, string yearOfMaking, WatchProgress watchProgress, double watchProgressPercentage)
        {
            if (string.IsNullOrWhiteSpace(title))
            { throw new InvalidTitleException("The title for a MediaItem cant be null/empty/full of white space"); }
            if (string.IsNullOrWhiteSpace(location))
            { throw new InvalidLocationException("The location for a MediaItem cant be null/empty/full of white space"); }

            this.Title = title;
            this.Location = location;
            this.MediaType = mediaType;
            this.Description = description;
            this.YearOfMaking = yearOfMaking;
            this.WatchProgress = watchProgress;
            this.WatchProgressPercentage = watchProgressPercentage;
        }
    }
}
