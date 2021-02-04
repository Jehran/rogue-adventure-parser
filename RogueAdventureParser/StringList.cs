using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RogueAdventureParser
{
    class StringItem
    {
        public string name { get; set; }
        public string text { get; set; }
    }

    class StringList : KeyedCollection<string, StringItem>
    {
        protected override string GetKeyForItem(StringItem item)
        {
            return item.name;
        }
    }
}
