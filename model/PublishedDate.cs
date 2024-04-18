using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UF3_test.model
{
    public class PublishedDate
    {
        [JsonProperty("$date")]
        public String date { get; set; }

        public override string ToString()
        {
            return
                "PublishedDate{" +
                "$date = '" + date + '\'' +
                "}";
        }
    }
}
