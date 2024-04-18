using System;
using System.Collections.Generic;

namespace UF3_test.model

{
    [Serializable]

    public class Person
    {
        public bool isActive { get; set; }
        public string balance { get; set; }
        public string picture { get; set; }
        public int age { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string about { get; set; }
        public string registered { get; set; }
        public double latitude { get; set; }
        public string[] tags { get; set; }
        public List<Friend> friends { get; set; }
        public string gender { get; set; }
        public string randomArrayItem { get; set; }

        public override string ToString()
        {
            return 
                "Person{" +
                "isActive = '" + isActive + '\'' +
                ",balance = '" + balance + '\'' +
                ",picture = '" + picture + '\'' +
                ",age = '" + age + '\'' +
                ",name = '" + name + '\'' +
                ",company = '" + company + '\'' +
                ",phone = '" + phone + '\'' +
                ",email = '" + email + '\'' +
                ",address = '" + address + '\'' +
                ",about = '" + about + '\'' +
                ",registered = '" + registered + '\'' +
                ",latitude = '" + latitude + '\'' +
                ",tags = '" + tags + '\'' +
                ",friends = '" + friends + '\'' +
                ",gender = '" + gender + '\'' +
                ",randomArrayItem = '" + randomArrayItem + '\'' +
                "}";
        }
    }
}
