﻿using Newtonsoft.Json;
using System;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class GraphPoint
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }
}
