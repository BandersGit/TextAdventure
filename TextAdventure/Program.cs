using System.Security.Cryptography;
using System;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Text Adventure!");
            Console.WriteLine("");
            Console.WriteLine("Instructions: press ENTER when you want to proceed, except when there is a question,");
            Console.WriteLine("you must then answer that question with an appropriate answer.");
            Console.ReadLine();

            Hero hero = new Hero();
            while (hero.Location != "quit") //"Main Loop" that switches between rooms until the game ends
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
                }else if (hero.Location == "chickenroom")
                {
                    ChickenRoom(hero);
                }else if (hero.Location == "anotherlockedroom")
                {
                    AnotherLockedRoom(hero);
                }else if (hero.Location == "chestroom")
                {
                    ChestRoom(hero);
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
                Console.Clear();
            } while (!AskYesOrNo($"So, {name} is it?"));

            hero.Name = name;
            hero.Location = "tableroom";
        }

        static void TableRoom(Hero hero)
        {
            Console.Clear();

            hero.Items.Add("woodensword");

            Console.WriteLine("You are equipped with a wooden sword, and your task " + 
                              "is to slay the monster at the end of the adventure.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("In front of you is a stone table with two items on it, " + 
                              "a knife and a key.");
            Console.ReadLine();
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
                Console.Clear();
                Console.WriteLine("The door is locked! Try another way.");
                rightOrLeft = Ask("Do you want to go left or right?");
            }

            if (rightOrLeft == "right" && hero.Items.Contains("key"))
            {
                hero.Location = "lockedroom";  
            }else
            {
                hero.Location = "thirdroom";
            }
        }

        static void LockedRoom(Hero hero)
        {
            hero.Items.Remove("key");

            Console.Clear();
            Console.WriteLine("You had to use the key to get into the room. " +
                              "Inside the locked room you find a shiny sword!");
            Console.ReadLine();
            if (AskYesOrNo("Do you want it instead of your wooden sword?"))
            {
                Console.Clear();
                Console.WriteLine("You picked up the shiny sword!");
                hero.Items.Remove("woodensword");
                hero.Items.Add("shinysword");
            }else
            {
                Console.Clear();
                Console.WriteLine("You kept your wooden sword.");
            }

            hero.Location = "thirdroom";

            Console.ReadLine();
        }
        
        static void ThirdRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("You continued down the hallway...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("On the floor before you lies a lifeless corpse.");
            Console.WriteLine("Its hand is clasped around something shiny.");
            Console.ReadLine();

            if (AskYesOrNo("Do you want to loot the corpse?"))
            {
                Console.Clear();
                Console.WriteLine("You pick up an old silver necklace");
                Console.ReadLine();
                if (RollD6() <= 3)
                {
                    Console.WriteLine("A warm feeling spreads over your body.");
                    hero.Items.Add("blessedamulet");
                }else
                {
                    Console.WriteLine("A cold shiver runs down your spine.");
                    hero.Items.Add("cursedamulet");
                }
                Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine("You leave the corpse and continue into the next room.");
            Console.ReadLine();

            hero.Location = "chickenroom";
        }

        static void ChickenRoom(Hero hero)     //New room
        {
            Console.Clear();
            Console.WriteLine("In front of you in the next room is another stone table!");
            Console.ReadLine();
            System.Console.WriteLine("This time it has a whole roasted chicken on it!");
            Console.WriteLine("");

            if (AskYesOrNo("Do you want to pick up the chicken?"))
            {
                Console.Clear();
                Console.WriteLine("You pick up the roasted chicken and continue into the next room.");
                Console.ReadLine();

                hero.Items.Add("chicken");
            }else
            {
                Console.Clear();
                Console.WriteLine("You leave the chicken on the table and continue into the next room.");
                Console.ReadLine();
            }

            hero.Location = "anotherlockedroom";
        }

        static void AnotherLockedRoom(Hero hero)    //New room
        {
            Console.Clear();
            System.Console.WriteLine("In the next room, you come across two doors, however one is locked.");
            Console.ReadLine();
            System.Console.WriteLine("If you still have a key, you can use it on the locked door.");
            System.Console.WriteLine("");

            bool key = AskYesOrNo("Do you have a key?");

            if (key && hero.Items.Contains("key"))
            {
                System.Console.WriteLine("Great!");
                Console.ReadLine();

                if (AskYesOrNo("Do you want to use the key?") && hero.Items.Contains("key"))
                {
                    Console.Clear();
                    System.Console.WriteLine("You used the key to unlock the door!");
                    Console.ReadLine();

                    hero.Items.Remove("key");

                    hero.Location = "chestroom";
                }else
                {
                    Console.Clear();
                    System.Console.WriteLine("You did not use the key!");
                    System.Console.WriteLine("So you took the other door instead.");
                    Console.ReadLine();

                    hero.Location = "backoutside";
                }
            }else if (key && !hero.Items.Contains("key"))
            {
                Console.Clear();
                System.Console.WriteLine("You searched for the key in your backpack, but ultimately you found nothing.");
                System.Console.WriteLine("So you took the other door instead.");
                Console.ReadLine();

                hero.Location = "backoutside";
            }else if (!key)
            {
                Console.Clear();
                System.Console.WriteLine("Alas you do not have a key.");
                System.Console.WriteLine("So you took the other door instead.");
                Console.ReadLine();

                hero.Location = "backoutside";
            }
            
        }

        static void ChestRoom(Hero hero)    //New room
        {
            Console.Clear();
            System.Console.WriteLine("Inside the room you find a chest!");
            System.Console.WriteLine("You opened the chest and found a rusty claymore.");
            Console.ReadLine();

            if (AskYesOrNo("Do you want to pick up the claymore?"))
            {
                Console.Clear();
                System.Console.WriteLine("You pick up the claymore and leave your wooden sword behind.");
                Console.ReadLine();

                hero.Items.Remove("woodensword");
                hero.Items.Add("claymore");

                Console.Clear();
                System.Console.WriteLine("You approach the door to the next room...");
                Console.ReadLine();
            }else
            {
                Console.Clear();
                System.Console.WriteLine("You decide not to pick up the claymore");
                Console.ReadLine();
                Console.Clear();
                System.Console.WriteLine("You approach the door to the next room...");
                Console.ReadLine();
            }

            hero.Location = "backoutside";
        }

        static void BackOutside(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("As you open the door to the next room, a Minotaur charges through and knocks you to the ground!");
            Console.ReadLine();
            hero.Location = "bossfight";
        }

        static void BossFight(Hero hero)
        {
            Enemy enemy = new Enemy();

            ItemEffect(hero,enemy);

            while (hero.Health > 0 && enemy.Health > 0)
            {
                Console.Clear();
                EnemyMove(enemy);

                Console.WriteLine("You can either parry, jump or duck.");
                string action = Ask("What do you want to do?");

                while (action != "parry" && action != "jump" && action != "duck")
                {
                    action = Ask("That is not not an option, please try again:");
                }

                Console.Clear();

                if (action == "parry")
                {
                    if (enemy.Move == "torsoattack")
                    {
                        Console.WriteLine($"You successfully parried the Minotaurs attack! (You deal {hero.Damage} damage)");
                        enemy.Health -= hero.Damage;
                        
                    }else if(enemy.Move == "legattack")
                    {
                        Console.WriteLine($"The Minotaur hits you right in your shins! (You take {enemy.Damage} damage)");
                        hero.Health -= enemy.Damage;
                    }else if (enemy.Move == "headattack")
                    {
                        Console.WriteLine($"The Minotaurs swing hits you in the side of the head! (You take {enemy.Damage} damage)");
                        hero.Health -= enemy.Damage;
                    }

                }else if (action == "duck")
                {
                    if (enemy.Move == "headattack")
                    {
                        Console.WriteLine("You dodged the Minotaurs swing! (You take 0 damage)");
                        
                    }else if(enemy.Move == "legattack")
                    {
                        Console.WriteLine($"The Minotaur hits you right in your shins! (You take {enemy.Damage} damage)");
                        hero.Health -= enemy.Damage;
                    }else if (enemy.Move == "torsoattack")
                    {
                        Console.WriteLine($"The Minotaur scrapes your torso! (You take {enemy.Damage} damage)");
                        hero.Health -= enemy.Damage;
                    }

                }else if (action == "jump")
                {
                    if (enemy.Move == "legattack")
                    {
                        Console.WriteLine("You jumped over the Minotaurs strike! (You take 0 damage)");
                        
                    }else if(enemy.Move == "torsoattack")
                    {
                        Console.WriteLine($"The Minotaur scrapes your torso! (You take {enemy.Damage} damage)");
                        hero.Health -= enemy.Damage;
                    }else if (enemy.Move == "headattack")
                    {
                        Console.WriteLine($"The Minotaurs swing hits you in the side of the head! (You take {enemy.Damage} damage)");
                        hero.Health -= enemy.Damage;
                    }
                }

                Console.ReadLine();
                Console.Clear();

                if (hero.Health < 0)
                {
                    hero.Health = 0;
                }else if (enemy.Health < 0)
                {
                    enemy.Health = 0;
                }

                Console.WriteLine($"You now have {hero.Health} HP!");
                Console.WriteLine("");
                Console.WriteLine($"The Minotaur now has {enemy.Health} HP!");
                Console.ReadLine();
                Console.Clear();
            }

            if (hero.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine("You bleed your last drop of blood, and you fall to the ground.");
                Console.ReadLine();

                hero.Location = "lose";
            }else if (enemy.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine("You stabbed the Minotaur through the heart, its lifeless body falls to the ground.");
                Console.ReadLine();
                
                hero.Location = "win";
            }
            Console.Clear();
        }

        static void Win (Hero hero)
        {
            Console.WriteLine($"Congratulations {hero.Name}! You slayed the Minotaur and completed your quest!");
            Console.ReadLine();
            hero.Location = "gameover";
        }

        static void Lose (Hero hero)
        {
            Console.WriteLine("You got slain by the Minotaur and failed your quest...");
            Console.ReadLine();
            hero.Location = "gameover";
        }

        static void GameOver (Hero hero)
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.ReadLine();

            if (AskYesOrNo("Do you want to play again?")) //Resets hero stats if game is restarted
            {
                hero.Items.Clear();
                hero.Health = 100;
                hero.Damage = 50;
                hero.Location = "newgame";
            }else
            {
                hero.Location = "quit";
            }
        }

        static string Ask(string question) //Takes in a question from a method with a question string
        {
            string response;
            do 
            {
                Console.WriteLine(question);
                response = Console.ReadLine().Trim().ToLower();
            } while (response == "");

            return response;
        }

        static bool AskYesOrNo(string question) //Gets a bool from an answer to a question
        {
            while (true)
            {
                string response = Ask(question).ToLower().Trim();

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

        static void EnemyMove(Enemy enemy) //Randomizes the move the enemy makes
        {
            int attackChoice = RollD6();

            if (attackChoice <= 2)
            {
                Console.WriteLine("The Minotaur swings at your head!");
                enemy.Move = "headattack";
            }else if (attackChoice <= 4 && attackChoice >= 3)
            {
                Console.WriteLine("The Minotaur strikes at your legs!");
                enemy.Move = "legattack";
            }else if (attackChoice >= 5)
            {
                Console.WriteLine("The Minotaur lashes out towards your torso!");
                enemy.Move = "torsoattack";
            }
            Console.ReadLine();
        }

        static void ItemEffect(Hero hero, Enemy enemy)  //Applies the effects from teh weapons
        {
            if (hero.Items.Contains("chicken"))
            {
                System.Console.WriteLine("The Minotaur seems to really want to get the chicken that you picked up!");
                Console.ReadLine();
                System.Console.WriteLine("It will be more freocious (Its damage increased to 25)");
                Console.ReadLine();
                enemy.Damage = 25;
            }



            if (hero.Items.Contains("shinysword"))
            {
                hero.Damage = 100;
            }else if (hero.Items.Contains("knife"))
            {
                hero.Damage = 75;
            }else if (hero.Items.Contains("claymore"))
            {
                hero.Damage = 75;
            }
        

            if (hero.Items.Contains("blessedamulet"))
            {
                Console.WriteLine("The necklace you picked up suddenly starts to shine.");
                Console.ReadLine();
                Console.WriteLine("Your body feels rejuvenated! (Health increased to 120)");
                hero.Health = 120;
            }else if (hero.Items.Contains("cursedamulet"))
            {
                Console.WriteLine("The necklace you picked up suddenly starts to burn into your chest.");
                Console.ReadLine();
                Console.WriteLine("Your body feels weaker! (Health decreased to 80)");
                hero.Health = 80;
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
}