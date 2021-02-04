using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace RogueAdventureParser
{
    class Program
    {
        static StringList _resources;
        static void Main(string[] args)
        {
            Strings();
            Heroes();
            Monsters();
            Skills();
        }

        public static void Strings()
        {
            var allText = File.ReadAllText("strings.json");
            _resources = JsonSerializer.Deserialize<StringList>(allText);
        }

        private static void Monsters()
        {
            var allText = File.ReadAllText("monster_card_list.json");
            var cardList = JsonSerializer.Deserialize<MonsterCardList>(allText);

            using (var csv = File.OpenWrite("monster_cards.csv"))
            {
                var writer = new StreamWriter(csv);

                writer.WriteLine("Id,Name,Life,Attack,Armor,Heal,Call,Weakness,Frail,Curse,Blind,Block,Silence,Wound,Innate,World,Sequences");
                foreach (var card in cardList)
                {
                    var innate = card.innate;
                    if (innate != "")
                    {
                        var items = innate.Split('|').Select(i => 
                            _resources.Contains($"innate_{i}") ?  _resources[$"innate_{i}"].text : i);
                        innate = String.Join('|', items);
                    }

                    writer.WriteLine($"{card.id},{card.name},{card.life},{card.attack},{card.armor},{card.heal},{card.call},{card.weakness},{card.frail},{card.curse},{card.blind},{card.block},{card.silence},{card.wound},\"{innate}\",{card.world},{card.sequences}");
                }
                writer.Flush();
            }

            allText = File.ReadAllText("enemy_groups.json");
            var groupList = JsonSerializer.Deserialize<List<EnemyGroup>>(allText);

            using (var csv = File.OpenWrite("groups.csv"))
            {
                var writer = new StreamWriter(csv);

                writer.WriteLine("Id,Type,Left,Center,Right,World");
                foreach (var group in groupList)
                {
                    var left = group.idLeft == "null" ? "" : cardList[group.idLeft].name;
                    var center = group.idCenter== "null" ? "" : cardList[group.idCenter].name;
                    var right = group.idRight == "null" ? "" : cardList[group.idRight].name;
                    writer.WriteLine($"{group.id},{group.type},{left} ({group.idLeft}),{center} ({group.idCenter}),{right} ({group.idRight}),{group.world}");
                }
                writer.Flush();
            }
        }

        private static void Heroes()
        {
            var allText = File.ReadAllText("hero_card_list.json");
            var cardList = JsonSerializer.Deserialize<List<HeroCard>>(allText);

            using (var csv = File.OpenWrite("hero_cards.csv"))
            {
                var writer = new StreamWriter(csv);

                writer.WriteLine("Id,Name,Rarity,Type,Level");
                foreach (var card in cardList)
                {
                    writer.WriteLine($"{card.id},{card.name},{card.Rarity},{card.Type},{card.level}");
                }
                writer.Flush();
            }
        }

        private static void Skills()
        {
            var allText = File.ReadAllText("hero_skill_list.json");
            var cardList = JsonSerializer.Deserialize<List<Skill>>(allText);

            using (var csv = File.OpenWrite("skills.csv"))
            {
                var writer = new StreamWriter(csv);

                writer.WriteLine("Id,Name,Classe,Text,Cost,Level");
                foreach (var card in cardList)
                {
                    if (card.name == "comingsoon") continue;
                    writer.WriteLine($"{card.id},{card.name},{card.classe},\"{_resources[card.text].text}\",{card.cost},{card.level}");
                }
                writer.Flush();
            }
        }
    }
}
