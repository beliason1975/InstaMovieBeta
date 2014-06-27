using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaMovieBeta.Models
{
    public class Movie
    {
        public string TmsId { get; set; }
        public int RootId { get; set; }
        public string Title { get; set; }
        public string TitleLang { get; set; }
        public string ShortDescription { get; set; }
        public string DescriptionLang { get; set; }
        public Ratings Ratings { get; set; }
        public string Genres { get; set; }
        public string TopCast { get; set; }

    }

    public class Ratings
    {
        public string Body { get; set; }
        public string Code { get; set; }

    }
}