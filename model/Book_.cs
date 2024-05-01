using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{
    [Serializable]
    public class Book_
    {
        public int _id { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public int pageCount { get; set; }
        public PublishedDate_ publishedDate { get; set; }
        public string thumbnailUrl { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
        public string status { get; set; }
        public List<string> authors { get; set; }
        public List<string> categories { get; set; }

    }
}
