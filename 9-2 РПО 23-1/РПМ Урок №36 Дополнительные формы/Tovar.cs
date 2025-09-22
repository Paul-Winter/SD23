using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace РПМ_Урок__36_Дополнительные_формы
{
    public class Tovar
    {
        private string name;
        private string made_id;
        private double price;

        public string Name { get => name; set => name = value; }
        public string Made_id { get => made_id; set => made_id = value; }
        public double Price
        { 
            get => price;
            set
            {
                if(price < 0)
                {
                    throw new System.Exception("Цена не может быть меньше нуля!");
                }
                price = value;
            }
        }

        public Tovar()
        {
            Name    = "unknown";
            Made_id = "unknown";
            Price   = 0;
        }
        public Tovar(string name, string made, double price)
        {
            Name    = name;
            Made_id = made;
            Price   = price;
        }

        public override string ToString()
        {
            return Name + " Изготовитель: " + Made_id + " Цена: " + Price;
        }
    }
}
