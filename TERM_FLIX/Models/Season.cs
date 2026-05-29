using System;
using System.Collections.Generic;
using System.Text;
using TERM_FLIX.Exceptions;
using TERM_FLIX.Models.Enums;

namespace TERM_FLIX.Models
{
    internal class Season
    {
        public List<Episode> Episodes { get; set; }
        public string MainSeriesTitle { get; set; }
        public int SeasonNumber { get; set; }
        public string Description { get; set; }
        public WatchProgress WatchProgress { get; set; }
        public double WatchProgressPercentage { get; set; }

        public Season(string mainSeriesTitle, int seasonNumber, string description, WatchProgress watchProgress, double watchProgressPercentage)
        {
            if (string.IsNullOrWhiteSpace(mainSeriesTitle))
            { throw new InvalidTitleException("The MainSeriesTitle for a Series cant be null/empty/full of white space"); }
            this.MainSeriesTitle = mainSeriesTitle;
            this.SeasonNumber = seasonNumber;
            this.Description = description;
            this.WatchProgress = watchProgress;
            this.WatchProgressPercentage = watchProgressPercentage;
        }
    }
}
