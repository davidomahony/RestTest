

namespace WebApplication1.Models
{
    /// <summary>
    /// Model which contains neccessary informaiton from translation response
    /// </summary>
    public class TranslatedModel
    {
        public Success success { get; set; }

        public Contents contents { get; set; }

        public class Success
        {
            public int total { get; set; }
        }

        public class Contents
        {
            public string translated { get; set; }

            public string text { get; set; }

            public string translation { get; set; }
        }
    }
}
