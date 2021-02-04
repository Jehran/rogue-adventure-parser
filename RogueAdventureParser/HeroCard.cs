using System;
using System.Collections.Generic;
using System.Text;

namespace RogueAdventureParser
{
    public class HeroCard
    {
        public string id { get; set; }
        public string name { get; set; }
        public string rarity { get; set; }
        public Rarity Rarity => rarity == "null" ? Rarity.Treasure : (Rarity)int.Parse(rarity);
        public string type { get; set; }
        public CardType Type => (CardType)int.Parse(type);
        public string level { get; set; }

    }

    public enum CardType
    {
        None = 0,
        Combat = 1,
        Magic = 2
    }

    public enum Rarity
    {
        Summoned = 0,
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Starter = 97,
        Upgraded = 98,
        Upgraded2 = 99,
        Treasure = 100
    }
}
