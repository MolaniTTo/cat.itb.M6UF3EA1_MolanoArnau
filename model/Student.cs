using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace UF3_test.model
{
    public class Student
    {
        [JsonProperty("_id")]
        public IdType _id { get; set; }

        public string firstname { get; set; }
        public string lastname1 { get; set; }
        public string lastname2 { get; set; }
        public string dni { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string phone_aux { get; set; }
        public int birth_year { get; set; }

        public override string ToString()
        {
            return
                "Student{" +
                "_id = '" + _id + '\'' +
                ",firstname = '" + firstname + '\'' +
                ",lastname1 = '" + lastname1 + '\'' +
                ",lastname2 = '" + lastname2 + '\'' +
                ",dni = '" + dni + '\'' +
                ",gender = '" + gender + '\'' +
                ",email = '" + email + '\'' +
                ",phone = '" + phone + '\'' +
                ",phone_aux = '" + phone_aux + '\'' +
                ",birth_year = '" + birth_year + '\'' +
                "}";
        }
    }

   


}
