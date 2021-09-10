using System.Collections.Generic;
namespace TextAdventure
{
    public class Hero
    {
        public string Name = "";
        public int Health = 100;
        public int Damage = 50;
        public List<string> Items = new List<string>();
        public string Location = "newgame";
    }
}