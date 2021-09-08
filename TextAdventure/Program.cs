using System;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Text Adventure!");

            Hero hero = new Hero();
            while (hero.Location != "quit")
            {
                if (hero.Location == "newgame")
                {
                    NewGame(hero);
                } else
                {
                  Console.Error.WriteLine($"You forgot to implement '{hero.Location}'!");  
                }
            }
        }
        
        static void NewGame(Hero hero)
        {
            Console.Clear();
            string name = "";
            
            do
            {
                name = Ask("What is your name, adventurer? ");
            } while (!AskYesOrNo($"So, {name} is it?"));

            hero.Name = name;
            hero.Location = "tableroom";
        }

        static void TableRoom(Hero hero)
        {
            Console.Clear();
            hero.Items.Add("wooden sword");
            Console.WriteLine("you are equipped with one wooden sword, and your task " + 
                          "is to slay the monster at the end of the adventure. ");
            Console.WriteLine("");
            Console.WriteLine("In front of you is a stone table with two items on it, " + 
                              "a knife and a key");
            Console.WriteLine("");
            Console.WriteLine("You can only pick up one of these items");

            string pickedUpItem = Ask("Which one do you want to pick up?");
            hero.Items.Add(pickedUpItem);

        }
        
        static string Ask(string question)
        {
            string response;
            do 
            {
                Console.WriteLine(question);
                response = Console.ReadLine().Trim();
            } while (response == "");
            return response;
        }

        static bool AskYesOrNo(string question)
        {
            while (true)
            {
                string response = Ask(question).ToLower();

                switch (response)
                {
                    case "yes":
                    case "ok":
                        return true;
                    case "no":
                        return false;
                }
            }
        }
    }
}