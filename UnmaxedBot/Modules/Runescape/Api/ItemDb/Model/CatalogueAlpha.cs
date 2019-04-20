﻿using Newtonsoft.Json;

namespace UnmaxedBot.Modules.Runescape.Api.ItemDb.Model
{
    public class CatalogueAlpha
    {
        [JsonProperty("letter")]
        public string Letter { get; set; }

        [JsonProperty("items")]
        public int Items { get; set; }
    }
}
