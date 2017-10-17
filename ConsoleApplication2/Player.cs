using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace ConsoleApplication2
{
    class Player
    {
        //Variables
        private int health = 250;
        private int damage = 100;
        private int boneShards = 0; // Money
        private int exp = 0;
        private int level = 1;
        private string name;

        //Hit and Critical chance
        private int critChance = 1; //1%
        private int hitChance = 75; //75%
        //Evasion chance
        private int evadeChance = 0; //0%
        //Required Experience
        private double reqExp = 100;
        //Maximum health
        private int maxHealth = 250;

        //Player Bag
        List<Items> bag = new List<Items>();
        //Player Equipment
        List<Items> equippedItems = new List<Items>();

        //Constructor
        public Player(string name)
        {
            this.name = name;
            Console.WriteLine("Welcome to Survive {0} \n\nCurrent Stats:\nLevel: {1}\nExperience: {2}", name, level, exp);
            Console.WriteLine("Health: {0}\nDamage: {1}\nBone Shards: {2}", health, damage, boneShards);
            Console.WriteLine("Goodluck trying to survive...");                       
        }

        //Getters and Setters
        public int getHealth()
        {
            return this.health;
        }
        public void setHealth(int health)
        {
            this.health = health;
        }
        public int getDamage()
        {
            return this.damage;
        }
        public void setDamage(int damage)
        {
            this.damage = damage;
        }
        public int getBoneShards()
        {
            return this.boneShards;
        }
        public void setBoneShards(int boneShards)
        {
            this.boneShards = boneShards;
        }
        public int getExp()
        {
            return this.exp;
        }
        public void setExp(int exp)
        {
            this.exp = exp;
        }
        public int getCritChance()
        {
            return this.critChance;
        }
        public int getEvadeChance()
        {
            return this.evadeChance;
        }
        public int getHitChance()
        {
            return this.hitChance;
        }
        public int getLevel()
        {
            return this.level;
        }
        public List<Items> getBag()
        {
            return bag;
        }
        public List<Items> getEquipped()
        {
            return equippedItems;
        }

        //Leveling up method
        public void levelUp()
        {
            maxHealth += 80;
            health = maxHealth;
            damage += 45;
            level += 1;
        }
        public void checkForLevelUp()
        {
            while (this.exp >= reqExp)
            {
                levelUp();
                reqExp *= 1.5;
            }

        }

        //Show stats
        public void showStats()
        {
            Console.WriteLine("\n\nLevel: {0}\nCurrent Experience: {1}/{2}\n", level, exp, Math.Ceiling(reqExp));
            Console.WriteLine("Health: {0}/{1}\nDamage: {2}\nBone Shards: {3}", health, maxHealth, damage, boneShards);
        }

        //Show bag
        public void showBag()
        {
            foreach (var items in bag)
            {
                Console.WriteLine(items.getName() + "|   " + items.getDescription());
            }
        }

        //Add items to bag
        public void toBag(Items item)
        {
            bag.Add(item);
        }

        //Equipping items
        public void equipItem(Items item)
        {
            equippedItems.Add(item);
            //Apply item stats to player
            critChance += equippedItems[equippedItems.Count-1].getCritChanceBonus();
            damage += equippedItems[equippedItems.Count-1].getDamageBonus();
            evadeChance += equippedItems[equippedItems.Count-1].getEvadeBonus();
            health += equippedItems[equippedItems.Count-1].getHealthBonus();
            maxHealth += equippedItems[equippedItems.Count-1].getHealthBonus();
        }

        //Unequipping items
        public void unequipItem(Items item)
        {
            equippedItems.Remove(item);
            //Apply stat changes
            critChance -= item.getCritChanceBonus();
            damage -= item.getDamageBonus();
            evadeChance -= item.getEvadeBonus();
            health -= item.getHealthBonus();
            maxHealth -= item.getHealthBonus();
        }
    }
}
