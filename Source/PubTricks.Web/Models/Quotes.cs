using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PubTricks.Web.Models {
    public class Quotes {
        public static dynamic FromUsers() {
            var quotes = new string[] {"The pen trick was great - took me a while to get it"
                ,"This is really great"
                ,"The kids loved the pen trick"
                ,"It took my mates 3 pints of beer to get the pen trick right"
            };
            Random rnd = new Random();
            return quotes.OrderBy(x => rnd.Next()).ToList();
        }
    }
}