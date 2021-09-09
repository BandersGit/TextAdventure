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
                } else if (hero.Location == "tableroom")
                {
                    TableRoom(hero);
                } else if (hero.Location == "corridor")
                {
                    Corridor(hero);
                }else if (hero.Location == "lockedroom")
                {
                    LockedRoom(hero);
                }else if (hero.Location == "thirdroom")
                {
                    ThirdRoom(hero);
                }else if (hero.Location == "backoutside")
                {
                    BackOutside(hero);
                }else if (hero.Location == "bossfight")
                {
                    BossFight(hero);
                }else if (hero.Location == "win")
                {
                    Win(hero);
                }else if (hero.Location == "lose")
                {
                    Lose(hero);
                }else if (hero.Location == "gameover")
                {
                    GameOver(hero);
                }else
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

            hero.Items.Add("woodensword");

            Console.WriteLine("You are equipped with a wooden sword, and your task " + 
                              "is to slay the monster at the end of the adventure. ");
            Console.WriteLine("");
            Console.WriteLine("In front of you is a stone table with two items on it, " + 
                              "a knife and a key.");
            Console.WriteLine("");
            Console.WriteLine("You can only pick up one of these items.");

            string pickedUpItem = Ask("Which one do you want to pick up?");
            while (pickedUpItem != "key" && pickedUpItem != "knife" && pickedUpItem != "none")
            {
                pickedUpItem = Ask("Not an option, please try again: ");
            }
            
            hero.Items.Add(pickedUpItem);
            Console.Clear();
            Console.WriteLine($"You pick up the {pickedUpItem}.");

            Console.ReadLine();

            hero.Location = "corridor";
        }

        static void Corridor(Hero hero)
        {
            Console.Clear();

            Console.WriteLine("You exit the room and find yourself standing in a dark " + 
                              "hallway.");
            Console.WriteLine("");
            Console.WriteLine("You can either enter another room on your right " + 
                              "side, or continue down the hallway on your left.");

            string rightOrLeft = Ask("Do you want to go left or right?");

            while (rightOrLeft != "right" && rightOrLeft != "left")
            {
                 rightOrLeft = Ask("That is not possible, please try again:");
            }

            while (rightOrLeft == "right" && !hero.Items.Contains("key"))
            {
                 Console.WriteLine("The door is locked! Try another way.");
                 rightOrLeft = Ask("Do you want to go left or right?");
            }

            if (rightOrLeft == "right" && hero.Items.Contains("key"))
            {
                hero.Location = "lockedroom";
                hero.Items.Remove("Key");   
            }else
            {
                hero.Location = "thirdroom";
            }
        }

        static void LockedRoom(Hero hero)
        {
            Console.WriteLine("You had to use the key to get into the room. " +
                              "Inside the locked room you find a shiny sword!");
            if (AskYesOrNo("Do you want it instead of your wooden sword?"))
            {
                Console.WriteLine("You picked up the shiny sword!");
                hero.Items.Remove("woodensword");
                hero.Items.Add("shinysword");
            }else
            {
                Console.WriteLine("You kept your wooden sword.");
            }

            hero.Location = "thirdroom";

            Console.ReadLine();
        }
        
        static void ThirdRoom(Hero hero)
        {
            Console.WriteLine("You continued down the hallway");

            hero.Location = "backoutside";

            Console.ReadLine();
        }

        static void BackOutside(Hero hero)
        {
            hero.Location = "bossfight";
        }

        static void BossFight(Hero hero)
        {
            hero.Location = "win";
            hero.Location = "lose";
        }

        static void Win (Hero hero)
        {
            hero.Location = "gameover";
        }

        static void Lose (Hero hero)
        {
            hero.Location = "gameover";
        }

        static void GameOver (Hero hero)
        {
            hero.Location = "quit";
        }

        static string Ask(string question)
        {
            string response;
            do 
            {
                Console.WriteLine(question);
                response = Console.ReadLine().Trim().ToLower();
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