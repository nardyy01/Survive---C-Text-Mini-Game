using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Items
    {
        protected string description;
        protected int price;
        protected string name;

        //Getters
        public virtual string getName()
        {
            return "";
        }
        public virtual string getDescription()
        {
            return "";
        }
        public virtual int getHealthBonus()
        {
            return 0;
        }
        public virtual int getEvadeBonus()
        {
            return 0;
        }
        public virtual int getCritChanceBonus()
        {
            return 0;
        }
        public virtual int getDamageBonus()
        {
            return 0;
        }
        public virtual int getPrice()
        {
            return 0;
        }
    }
}
