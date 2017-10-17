﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Rat : Threats
    {

        public Rat()
        {
            name = "rat";
            damage = 10;
            health = 100;
            exp = 50;
            maxHealth = 100;
        }
        public override void getAttacked(int playerDamage)
        {
            //loses health
            health -= playerDamage;
        }
        public override int Attack(int playerHealth, bool blockSome)
        {
            if (blockSome) //if this is true block some damage
            {
                playerHealth -= damage / 2;
                return playerHealth;
            }
            else
            {
                //player loses health
                playerHealth -= damage;
                return playerHealth;
            }
        }
        public override void statCheck(int playerLevel)
        {
            damage = damage * ((playerLevel / 2) + 1);
            exp = exp * ((playerLevel / 2) + 1); 
            maxHealth = maxHealth * ((playerLevel / 2) + 1);
            health = maxHealth;
        }
        //getters
        public override int getHealth()
        {
            return this.health;
        }
        public override int getExp()
        {
            return this.exp;
        }
        public override string getName()
        {
            return this.name;
        }
        public override int getDamage()
        {
            return this.damage;
        }

    }
}
