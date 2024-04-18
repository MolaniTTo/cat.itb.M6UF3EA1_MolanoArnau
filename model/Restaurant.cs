using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{

    public class Restaurant
    {
        public Address address { get; set; }
        public string borough { get; set; }
        public string cuisine { get; set; }
        public List<Grade> grades { get; set; }
        public string name { get; set; }
        public string restaurant_id { get; set; }

        public override string ToString()
        {
            return
                "Restaurant{" +
                "address = '" + address + '\'' +
                ",borough = '" + borough + '\'' +
                ",cuisine = '" + cuisine + '\'' +
                ",grades = '" + grades + '\'' +
                ",name = '" + name + '\'' +
                ",restaurant_id = '" + restaurant_id + '\'' +
                "}";
        }
    }
}

