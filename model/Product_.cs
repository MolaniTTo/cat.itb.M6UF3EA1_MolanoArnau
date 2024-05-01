using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{
    public class Product_
    {
        public int _id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public string picture { get; set; }
        public List<string> categories { get; set; }
    }
}
