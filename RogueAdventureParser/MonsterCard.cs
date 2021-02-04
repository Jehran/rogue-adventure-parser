using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RogueAdventureParser
{
    class MonsterCard
    {
        public string id { get; set; }
        public string name { get; set; }
        public string life { get; set; }
        public string attack { get; set; }
        public string armor { get; set; }
        public string heal { get; set; }
        public string call { get; set; }
        public string weakness { get; set; }
        public string frail { get; set; }
        public string curse { get; set; }
        public string blind { get; set; }
        public string block { get; set; }
        public string silence { get; set; }
        public string wound { get; set; }
        public string innate { get; set; }
        public string world { get; set; }
        public string sequences { get; set; }
    }

    class MonsterCardList : KeyedCollection<string, MonsterCard>
    {
        protected override string GetKeyForItem(MonsterCard item)
        {
            return item.id;
        }
    }
}
