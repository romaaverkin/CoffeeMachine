using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachine
{
    class Drink : IComparable
    {
        public Drink(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public int Price { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is Drink m)
                return Price.CompareTo(m.Price);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }
}
