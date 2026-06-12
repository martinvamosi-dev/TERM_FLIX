using System;
using System.Collections.Generic;
using System.Text;
using TERM_FLIX.Exceptions;
using TERM_FLIX.Models.Enums;

namespace TERM_FLIX.Models
{
    public class ExtractedMediaInfo
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public MediaType MediaType { get; set; }
        public ExtractedMediaType ExtractedMediaType { get; set; }
        public string YearOfMaking { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }

        public string CollectionTitle { get; set; }

        public ExtractedMediaInfo(string title, string location,MediaType mediaType, ExtractedMediaType extractedMediaType, int seasonNumber, int episodeNumber, string yearOfMaking, string collectionTitle)
        {
            if (string.IsNullOrWhiteSpace(title))
            { throw new InvalidTitleException("The title for an ExtractedMediaInfo cant be null/empty/full of white space"); }
            if (string.IsNullOrWhiteSpace(location))
            { throw new InvalidLocationException("The location for an ExtractedMediaInfo cant be null/empty/full of white space"); }

            this.Title = title;
            this.Location = location;
            this.MediaType = MediaType.Uncategorized;
            this.ExtractedMediaType = extractedMediaType;
            this.SeasonNumber = seasonNumber;
            this.EpisodeNumber = episodeNumber;
            this.YearOfMaking = yearOfMaking;
            this.CollectionTitle = collectionTitle;
        }
    }
}
