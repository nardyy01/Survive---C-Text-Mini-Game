using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/*TODO
 * Instead of multiplying threat stats by player level multiply it by a finite number and 
 * add that number to the threat's stats to prevent super growth.
 * 
 */

namespace ConsoleApplication2
{
    class Program
    {
        //Make it possible to generate a random number anywhere in the code
        public static Random randomNumber = new Random();
        //*****************************************************************

        static void Main(string[] args)
        {
            //Create a new player
            Player newPlayer = createPlayer();
            //Start game 
            startGame(newPlayer);


        }
        static Player createPlayer()
        {
            //Name the player
            print("Enter you name.");
            string name = takeInput();

            //Create instance of player with given name
            Player player = new ConsoleApplication2.Player(name);

            delay(3000);

            return player;
        }

        static void startGame(Player player)
        {
            //Player Instructions
            Instructions();

            Console.Write("\n\n\n\nLOADING INSTANCES..");
            for (int i = 0; i < 3; i++) { delay(1000); Console.Write("."); } // Makes dots 

            //load instances
            Bear bear = new ConsoleApplication2.Bear();
            Rat rat = new ConsoleApplication2.Rat();
            Dragon dragon = new ConsoleApplication2.Dragon();
            Thunder_God thunderGod = new ConsoleApplication2.Thunder_God();
            WildMan wildMan = new ConsoleApplication2.WildMan();

            Scale_Armor scaleArmor = new ConsoleApplication2.Scale_Armor();
            GodSword godSword = new ConsoleApplication2.GodSword();

            //Add threats to a list
            List<Threats> threats = new List<Threats>();
            threats.Add(bear);
            threats.Add(rat);
            threats.Add(wildMan);
            List<Threats> bossThreats = new List<Threats>();
            bossThreats.Add(dragon);
            bossThreats.Add(thunderGod);


            //Market
            List<Items> market = new List<Items>();
            market.Add(scaleArmor);
            market.Add(godSword);
            //******************************************
            int fightCount = 1;


            print("\nSTARTING GAME WORLD\n\n\n\n");
            delay(2000);

            //Story I guess..
            print("In a land far far far far far far far away.");    delay(1000);
            print("You were !!AAHHhhg!!");  delay(1000);
            print("...."); delay(1000);

            while (player.getHealth() > 0)
            {
                if(fightCount%5 == 0)
                {
                    int threatPick = randomNumber.Next(bossThreats.Count);
                    print(fightEncounter(bossThreats[threatPick], player) + "\n"); delay(2000);
                    player.showStats();
                    fightCount++;
                    delay(2500);
                }
                else
                {
                    int threatPick = randomNumber.Next(threats.Count);
                    print(fightEncounter(threats[threatPick], player) + "\n"); delay(2000);
                    player.showStats();
                    fightCount++;
                    delay(2500);
                }


                while (true)
                {
                    //Give player options
                    print("\n|    Market   |   Next Fight  |   View Inventory  |");
                    string playerAction = takeInput();

                    if (string.Equals(playerAction, "Market", StringComparison.OrdinalIgnoreCase))
                    {
                        //Open Market
                        print("\n\nWelcome to the Market!");
                        print("\nTo buy an item type in the item's name. \nTo exit market type EXIT.\n ");
                        foreach(var items in market)
                        {
                            Console.WriteLine(items.getName() + "|" + items.getDescription());
                        }
                        /* buy> player type item name > search list for item name> check item price>
                         * check if player has enough money> <<if yes> add item to player bag and remove from list>
                         * <<if no> throw error message to player
                         */
                        string response, response2;
                        while (!string.Equals(response= takeInput(), "Exit", StringComparison.OrdinalIgnoreCase))
                        {
                            //Handle player buying items
                            foreach (var item in market)
                            {
                            if(string.Equals(item.getName(), response, StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine("\n" + item.getName() + ": " + item.getPrice() + " bone shards"); //display item name and price
                                    print("\nWould you like to complete this purchase?");

                                    //if they say yes
                                    if (string.Equals(response2 = takeInput(), "Yes", StringComparison.OrdinalIgnoreCase))
                                    {
                                        //check player currency
                                        if (player.getBoneShards() >= item.getPrice())
                                        {
                                            //Handle market to bag interaction
                                            player.toBag(item);
                                            print("\nPurchase complete!\n");
                                        }
                                        else
                                            Console.WriteLine("\nInsufficient funds!\nYou have " + player.getBoneShards() + " Bone Shards");
                                    }
                                    else
                                        print("\nPurchase Cancelled!\n");
                                }
                            }
                            print("\nTo buy an item type in the item's name. \nTo exit market type EXIT.\n ");
                        }
                        
                    }
                    else if (string.Equals(playerAction, "View Inventory", StringComparison.OrdinalIgnoreCase))
                    {
                        player.showBag();

                        print("\nTo equip an item type in the item's name. \nTo exit your bag type EXIT.\n ");

                        string response, response2;
                        while (!string.Equals(response = takeInput(), "Exit", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach(var item in player.getBag())
                            {
                                if(string.Equals(response, item.getName(), StringComparison.OrdinalIgnoreCase)) //if player types an item
                                {
                                    if (player.getEquipped().Contains(item))
                                    {
                                        print("You already have a similar item equipped!");
                                    }
                                    else
                                    {
                                    player.equipItem(item);//equip it
                                    print("\nItem Equipped!\n");
                                    }

                                }
                            }
                            print("\nTo equip an item type in the item's name. \nTo exit your bag type EXIT.\n ");
                        }
                    }
                    else if (string.Equals(playerAction, "Next Fight", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }
            }
        }

        static string fightEncounter(Threats threat, Player player)
        {
            threat.statCheck(player.getLevel());//Check player stats and put threat on player's level

            print("\nIts a {0}! Prepare yourself!", threat.getName());
            do
            {
                print("|   ATTACK |   DEFEND  |   RUN     |");
                string action = takeInput();

                if (string.Equals(action, "attack", StringComparison.OrdinalIgnoreCase))
                {
                    //You attack
                    Attack(threat, player);
                    if(threat.getHealth()>0)
                    {
                    //Threat attacks if its not dead
                    player.setHealth(threat.Attack(player.getHealth(), false));
                    Console.WriteLine("The {0} Attacked!\n  -{1}", threat.getName(), threat.getDamage());
                    Console.WriteLine("Current HP: {0}\n", player.getHealth()); delay(2000);
                    }

                }
                else if (string.Equals(action, "defend", StringComparison.OrdinalIgnoreCase))
                {
                    //You defend || Threat attacks
                    Defend(threat, player);
                }
                else if (string.Equals(action, "run", StringComparison.OrdinalIgnoreCase))
                {
                    int getAway = Run(threat, player);

                    //You try to run.
                    if (getAway == 0)
                        break;
                    else
                    {
                        print("You failed to escape!"); //Unsuccessful... 
                        //Threat attacks
                        player.setHealth(threat.Attack(player.getHealth(), false));
                        Console.WriteLine("The {0} Attacked!\n  -{1}", threat.getName(), threat.getDamage());
                        Console.WriteLine("Current HP: {0}\n", player.getHealth()); delay(800);
                    }

                }
                else
                    print("The action you have entered is invalid.\n Please try again.\n");
            }
            while (player.getHealth() > 0 && threat.getHealth() > 0);

            //After the fight check results
            if (player.getHealth() > 0 && threat.getHealth() <= 0) //You won
            {
                player.setExp(player.getExp() + threat.getExp());
                player.checkForLevelUp();
                delay(500);
                return "You killed the " + threat.getName() + "!\n";
            }
            else if (player.getHealth() <= 0) //You lost
            {
                delay(800);
                return "You have died!\nGame Over";
            }
            else
                return "You got away successfully!\n"; //You ran
        }

        //List of doable player actions
        static void Attack(Threats threat, Player player)
        {
            int chance = randomNumber.Next(1,101);
            int chance2 = randomNumber.Next(1, 101);
            //Hit || Crit || Miss
            if (chance >= 1 && chance <= player.getHitChance())
            {
                if (chance >= 1 && chance <= player.getCritChance())
                {
                    threat.getAttacked(player.getDamage() * 2);
                    print("Critical Strike!\n   {0}", (player.getDamage()*2).ToString());
                    Console.WriteLine("{0} health: {1}\n", threat.getName(), threat.getHealth()); delay(500);
                }
                else
                {
                    threat.getAttacked(player.getDamage());
                    Console.WriteLine("You dealt {0} damage to the {1}.", player.getDamage(), threat.getName());
                    Console.WriteLine("{0} health: {1}\n", threat.getName(), threat.getHealth()); delay(500);
                }   

            }
            else
                print("You Missed!"); delay(2000);

            //10% Counter attack chance (does half original damage)
            if (chance2 >= 1 && chance2 <= 10)
            {
                player.setHealth(threat.Attack(player.getHealth(), true));
                Console.WriteLine("The {0} counter attacked for {1} damage!\n", threat.getName(), threat.getDamage()/2); delay(500);
            }


        }
        static void Defend(Threats threat, Player player)
        {
            int chance = randomNumber.Next(1, 101);
            int chance2 = randomNumber.Next(1, 101);
            //Take evade chance 1st --parry is evade successful--
            if (chance >= 1 && chance <= player.getEvadeChance())
            {
                print("You Evaded the {0}'s attack!\n", threat.getName());
                //hit back with a crit
                threat.getAttacked(player.getDamage() * 2);
                print("PARRY!\n {0}", (player.getDamage()*2).ToString());
                delay(2000);
            }

            //65% chance block-some || 20% chance block-all || 15% block-fail
            else if (chance2 >= 1 && chance2 <= 65)
            {
                player.setHealth(threat.Attack(player.getHealth(), true));
                print("Block.\n -{0}", (threat.getDamage()/2).ToString());
                print("Current HP: {0}\n", player.getHealth().ToString());
                //Bash the opponent for half your damage
                threat.getAttacked(player.getDamage() / 2);
                print("You Bash Opppnent!\n {0}", (player.getDamage() / 2).ToString());
                delay(500);
            }

            else if (chance2 >= 66 && chance2 <= 85)
            {
                print("Successful Block!\n");
                threat.getAttacked(player.getDamage() / 2);
                print("You Bash Opppnent!\n {0}", (player.getDamage() / 2).ToString());
                delay(500);
            }

            else
            {
                player.setHealth(threat.Attack(player.getHealth(), false));
                print("Block failed.\n -{0}", threat.getDamage().ToString());
                print("Current HP: {0}\n", player.getHealth().ToString()); delay(500);
            }

        }
        static int Run(Threats threat, Player player)
        {
            //Returns either 0 or 1 (0 = escape | 1 = stay)
            return randomNumber.Next(2); 
        }
        //***********************************************


        //My shortcuts to different existing methods
        static void print(string x)
        {
            Console.WriteLine(x); delay(500);
        }

        static void print(string x, string y)
        {
            Console.WriteLine(x, y);
        }

        static string takeInput()
        {
            return Console.ReadLine();
        }
        static void delay(int time)
        {
            Thread.Sleep(time);
        }
        //***************************************

        static void Instructions()
        {
            print("\n\nINSTRUCTIONS:"); 
            print("When entering a fight you will be prompted with 3 different selections."); 
            print("|   ATTACK |   DEFEND  |   RUN     |");
            print("To act simply type in one of the options and the battle will proceed.");
            print("Once either you or your opponent has reached 0 health, the battle will end."); 
            print("Press Enter to Start.");
            takeInput();
        }
    }

}
