using System;
using System.Collections.Generic;

namespace UnmaxedBot.Libraries.Runesharp
{
    public class Models
    {
        public class ItemsResponse
        {
            public int total { get; set; }
            public List<Item> items { get; set; }
            public class Current
            {
                public string trend { get; set; }
                public string price { get; set; } //price has to be string because of "k" and "m" suffixes
            }

            public class Today
            {
                public string trend { get; set; }
                public string price { get; set; } //price has to be string because of "k" and "m" suffixes
            }

            public class Item
            {
                public string icon { get; set; }
                public string icon_large { get; set; }
                public int id { get; set; }
                public string type { get; set; }
                public string typeIcon { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public Current current { get; set; }
                public Today today { get; set; }
                public string members { get; set; }
            }
        }

        public enum ItemCategory
        {
            Miscellaneous,
            Ammo,
            Arrows,
            Bolts,
            ConstructionMaterials,
            ConstructionProjects,
            CookingIngredients,
            Costumes,
            CraftingMaterials,
            Familiars,
            FarmingProduce,
            FletchingMaterials,
            FoodAndDrink,
            HerbloreMaterials,
            HuntingEquipment,
            HuntingProduce,
            Jewellery,
            MageArmour,
            MageWeapons,
            MeleeArmourLowLevel,
            MeleeArmourMidLevel,
            MeleeArmourHighLevel,
            MeleeWeaponsLowLevel,
            MeleeWeaponsMidLevel,
            MeleeWeaponsHighLevel,
            MiningAndSmithing,
            Potions,
            PrayerArmour,
            PrayerMaterials,
            RangeArmour,
            RangeWeapons,
            Runecrafting,
            RunesSpellsAndTeleports,
            Seeds,
            SummoningScrolls,
            ToolsAndContainers,
            WoodcuttingProduct,
            PocketItems
        }


        public class DetailResponse
        {
            public Item item { get; set; }

            public class Current
            {
                public string trend { get; set; }
                public string price { get; set; }
            }

            public class Today
            {
                public string trend { get; set; }
                public string price { get; set; }
            }

            public class Day30
            {
                public string trend { get; set; }
                public string change { get; set; }
            }

            public class Day90
            {
                public string trend { get; set; }
                public string change { get; set; }
            }

            public class Day180
            {
                public string trend { get; set; }
                public string change { get; set; }
            }

            public class Item
            {
                public string icon { get; set; }
                public string icon_large { get; set; }
                public int id { get; set; }
                public string type { get; set; }
                public string typeIcon { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public Current current { get; set; }
                public Today today { get; set; }
                public string members { get; set; }
                public Day30 day30 { get; set; }
                public Day90 day90 { get; set; }
                public Day180 day180 { get; set; }
            }
        }

        public class CatalogueResponse
        {
            public List<object> types { get; set; }
            public List<Alpha> alpha { get; set; }

            public class Alpha
            {
                public string letter { get; set; }
                public int items { get; set; }
            }
        }

        public class GraphResponse
        {
            public Daily daily { get; set; }
            public Average average { get; set; }

            public class GraphPoint
            {
                public DateTime date { get; set; }
                public int price { get; set; }
            }

            public class Daily
            {
                public List<GraphPoint> GraphPoints { get; set; }
            }
            public class Average
            {
                public List<GraphPoint> GraphPoints { get; set; }
            }
        }
    }
}
