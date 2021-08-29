using System.Collections.Generic;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model which contains neccessary informaiton from pokemon species request
    /// </summary>
    public class PokemonSpeciesModel
    {
        public string name { get; set; }

        public long id { get; set; }

        public bool is_legendary { get; set; }

        public Habitat habitat { get; set; }

        public IEnumerable<FlavorTextEntries> flavor_text_entries { get; set; }

        public class Habitat
        {
            public string name { get; set; }

            public string url { get; set; }
        }

        public class FlavorTextEntries
        {
            public string flavor_text { get; set; }

            public DescriptionLanguage language { get; set; }

            public class DescriptionLanguage
            {
                public string name { get; set; }

                public string url { get; set; }
            }
        }
    }
}
