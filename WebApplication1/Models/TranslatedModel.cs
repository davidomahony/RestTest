using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TranslatedModel
    {
        public Success success { get; set; }

        public Contents content { get; set; }

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
