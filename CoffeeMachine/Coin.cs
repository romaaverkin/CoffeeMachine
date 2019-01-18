using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachine
{
    class Coin : IComparable
    {
        public Coin(int rating, int quantity)
        {
            Rating = rating;
            Quantity = quantity;
        }

        public int Rating { get; set; }
        public int Quantity { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is Coin m)
                return Rating.CompareTo(m.Rating);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }

}