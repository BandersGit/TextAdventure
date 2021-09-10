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

            Console.Read();

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

            Console.Read();
        }
        
        static void ThirdRoom(Hero hero)
        {
            Console.WriteLine("You continued down the hallway...");
            Console.Read();
            Console.Clear();
            Console.WriteLine("On the floor before you lies a lifeless corpse.");
            Console.WriteLine("Its hand is clasped around something shiny.");

            if (AskYesOrNo("Do you want to loot the corpse?"))
            {
                Console.WriteLine("You pick up an old silver necklace");

                if (RollD6() <= 3)
                {
                    Console.WriteLine("A warm feeling spreads over your body");
                    hero.Items.Add("blessedamulet");
                }else
                {
                    Console.WriteLine("A cold shiver runs down your spine");
                    hero.Items.Add("cursedamulet");
                }
            }

            Console.WriteLine("You leave the corpse an continue into the next room.");

            Console.Read();

            hero.Location = "backoutside";
        }

        static void BackOutside(Hero hero)
        {
            Console.WriteLine("As you open the door to the next room, a Minotaur charges through and knocks you to the ground!");
            Console.Read();
            hero.Location = "bossfight";
        }

        static void BossFight(Hero hero)
        {
            ItemEffect(hero);

            Enemy enemy = new Enemy();

            while (hero.Health > 0 && enemy.Health > 0)
            {
                EnemyMove(enemy);

                Console.WriteLine("You can either parry, jump or duck.");
                string action = Ask("What do you want to do?");

                while (action != "parry" && action != "jump" && action != "duck")
                {
                    action = Ask("That is not not an option, please try again:");
                }

                if (action == "parry")
                {
                    if (enemy.Move == "torsoattack")
                    {
                        Console.WriteLine("You successfully parried the Minotaurs attack!");
                        
                    }else
                    {
                        
                    }
                    
                }else if (action == "dodge")
                {
                    if (enemy.Move == "headattack")
                    {
                        Console.WriteLine("You dodged the Minotaurs swing!");
                        
                    }else
                    {
                        
                    }
                    
                }else
                {
                    if (enemy.Move == "legattack")
                    {
                        Console.WriteLine("You jumped over the Minotaurs strike!");
                        
                    }else
                    {
                        
                    }
                }
            }





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

        static int RollD6()
        {
            Random random = new Random();
            int roll = random.Next(1,7);
            return roll;
        }

        static void ItemEffect(Hero hero)
        {
            if (hero.Items.Contains("shinysword"))
            {
                hero.Damage = 100;
            }else if (hero.Items.Contains("knife"))
            {
                hero.Damage = 75;
            }

            if (hero.Items.Contains("blessedamulet"))
            {
                Console.WriteLine("The necklace you picked up suddenly starts to shine.");
                Console.Read();
                Console.WriteLine("Your body feels rejuvenated! (Health increased to 120)");
                hero.Health = 120;
            }else if (hero.Items.Contains("cursedamulet"))
            {
                Console.WriteLine("The necklace you picked up suddenly starts burn into your chest.");
                Console.Read();
                Console.WriteLine("Your body feels weaker! (Health decreased to 80)");
                hero.Health = 80;
            }

            Console.Read();
            Console.Clear();
        }

        static void EnemyMove(Enemy enemy)
        {
            if (RollD6() <= 2)
                {
                    Console.WriteLine("The Minotaur swings at your head!");
                    enemy.Move = "headattack";
                }else if (RollD6() <= 4 && RollD6() >= 3)
                {
                    Console.WriteLine("The Minotaur strikes at your legs!");
                    enemy.Move = "legattack";
                }else if (RollD6() >= 5)
                {
                    Console.WriteLine("The Minotaur lashes out towards your torso!");
                    enemy.Move = "torsoattack";
                }

                Console.Read();
        }
    }
}