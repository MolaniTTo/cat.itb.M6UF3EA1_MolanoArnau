﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UF3_test.model
{
    public class Date
    {
        [JsonProperty("$date")]
        public long date { get; set; }

    }
}
