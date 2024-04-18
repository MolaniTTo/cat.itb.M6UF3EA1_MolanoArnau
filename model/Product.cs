using System;

namespace UF3_test.model
{

    public class Product
    {
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public string picture { get; set; }
        public string[] categories { get; set; }

        public override string ToString()
        {
            return
                "Products{" +
                "name = '" + name + '\'' +
                ",price = '" + price + '\'' +
                ",stock = '" + stock + '\'' +
                ",picture = '" + picture + '\'' +
                ",categories = '" + categories + '\'' +
                "}";
        }
    }
}
