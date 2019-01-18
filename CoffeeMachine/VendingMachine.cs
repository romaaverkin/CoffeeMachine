using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachine
{
    class VendingMachine
    {
        public delegate void MethodSetValueDrink();
        public event MethodSetValueDrink SetValueDrink;

        public delegate void MethodVisibleButtonsDrink();
        public event MethodVisibleButtonsDrink SetVisibleButtonsDrink;

        //Выбранный напиток
        public string selectedDrink = "Выберите напиток";

        //цена выбранного клиентом напитка
        public int PriceSelectedDrink = 0;

        //Внесенная клиентом сумма
        public int AmountPaid = 0;

        //Кофе куплен
        public bool CoffeBuy = false;

        //Коллекция видов кофе
        public List<Drink> myDrinks = new List<Drink>
        {
            new Drink("Черный кофе", 16),
            new Drink("Кипяток", 8),
            new Drink("Капучино", 35),
            new Drink("Кофе с молоком", 22),
            new Drink("Латте", 39)
        };

        //Коллекция видов монет
        public List<Coin> coinsInVendingMashine = new List<Coin>
        {
            new Coin(2, 10),
            new Coin(10, 10),
            new Coin(5, 10),
            new Coin(25, 2),
            new Coin(1, 15)
        };

        //Конструктор
        public VendingMachine()
        {
            myDrinks.Sort();
            coinsInVendingMashine.Sort();
        }

        //Клик по кнопек выбора напитков
        public void ClickButtonDrink(int tag)
        {
            Drink drink = myDrinks[tag];
            selectedDrink = $"Вы выбрали\n{drink.Name} - {drink.Price} руб.";
            PriceSelectedDrink = drink.Price;

            SetValueDrink?.Invoke();
            SetVisibleButtonsDrink?.Invoke();
        }
    }
}