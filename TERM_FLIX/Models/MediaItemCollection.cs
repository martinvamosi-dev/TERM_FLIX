using TERM_FLIX.Models.Enums;

namespace TERM_FLIX.Models
{
    internal class MediaItemCollection : MediaItem
    {
        public List<Season> Seasons { get; set; }
        public int TotalSeasons {
            get;
            set { Seasons.Count(); }
        }
        public MediaItemCollection(string title, string location, MediaType mediaType, string description, string yearOfMaking, WatchProgress watchProgress, double watchProgressPercentage)
            :base(title,location,mediaType,description,yearOfMaking,watchProgress,watchProgressPercentage)
        {} // constructor stays empty for now
    }
}
