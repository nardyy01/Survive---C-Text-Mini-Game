using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class GodSword : Items
    {
        private int damageBonus = 1234;
        private int critChanceBonus = 50;
        public GodSword()
        {
            description = "| 1234 Damage | 50% Crit Chance  |";
            price = 0;
            name = "God Sword";
        }

        public override string getName()
        {
            return this.name;
        }
        public override string getDescription()
        {
            return this.description;
        }
        public override int getCritChanceBonus()
        {
            return critChanceBonus;
        }
        public override int getDamageBonus()
        {
            return damageBonus;
        }
        public override int getPrice()
        {
            return this.price;
        }
    }
}
