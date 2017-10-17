using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Scale_Armor : Items
    {
        private int healthBonus = 2000;
        private int evadeBonus = 10;
        public Scale_Armor()
        {
            description ="| 2000 Health | 10 Evasion    |";
            price = 0;
            name = "Scale Armor";
        }

        public override string getName()
        {
            return this.name;
        }
        public override string getDescription()
        {
            return this.description;
        }
        public override int getHealthBonus()
        {
            return healthBonus;
        }
        public override int getEvadeBonus()
        {
            return evadeBonus;
        }
        public override int getPrice()
        {
            return this.price;
        }
    }
}
