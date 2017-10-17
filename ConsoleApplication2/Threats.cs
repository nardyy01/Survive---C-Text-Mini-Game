using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Threats
    {

        protected string name ;
        protected int damage;
        protected int health;
        protected int exp;
        protected int maxHealth;

        public virtual void getAttacked(int playerDamage) //Get attacked by the player
        {
           
        }
        public virtual int Attack(int playerHealth, bool blockSome) //Attack the player
        {
            return 99999999;
        }
        public virtual void statCheck(int playerLevel) //Adjusts threat based on the level of the player 
        {

        }

        //getters
        public virtual int getHealth()
        {
            return 99999999;
        }
        public virtual int getExp()
        {
            return 99999999;
        }
        public virtual string getName()
        {
            return "";
        }
        public virtual int getDamage()
        {
            return 99999999;
        }

    }
}
