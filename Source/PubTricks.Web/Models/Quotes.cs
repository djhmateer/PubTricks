using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PubTricks.Web.Models {
    public class Quotes {
        public static dynamic FromUsers() {
            var quotes = new string[] {"I just rocked the interview blah blah"
                ,"This is really great"
                ,"Bough mastering jquery from vidpub and so far it rocks"
                ,"2Bough mastering jquery from vidpub and so far it rocks"
                ,"3Bough mastering jquery from vidpub and so far it rocks"
                ,"4Bough mastering jquery from vidpub and so far it rocks"
            };
            Random rnd = new Random();
            return quotes.OrderBy(x => rnd.Next()).ToList();
        }
    }
}