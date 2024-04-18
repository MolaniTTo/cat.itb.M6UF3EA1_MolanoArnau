using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UF3_test.model
{
    public class IdType
    {
        [JsonProperty("$oid")]
        public string oid { get; set; }
    }
}
