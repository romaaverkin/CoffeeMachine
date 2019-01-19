﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachine
{
    class VendingMachine
    {
        public delegate void MethodSetValueDrink(string drink);
        public event MethodSetValueDrink SetValueDrink;

        public delegate void MethodVisibleButtonsDrink(bool visible);
        public event MethodVisibleButtonsDrink SetVisibleButtonsDrink;

        public delegate void MethodVisibleButtonsMoney(bool visible);
        public event MethodVisibleButtonsMoney SetVisibleButtonsMoney;

        public delegate void MethodSetValueInvestedClient(string message);
        public event MethodSetValueInvestedClient SetValueInvestedClient;

        public delegate void MethodSetVisibilityButtonBuy(bool visible);
        public event MethodSetVisibilityButtonBuy SetVisibilityButtonBuy;

        //Тег Выбранного напитка
        public int? selectedDrinkTag;

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
            selectedDrinkTag = tag;
            PriceSelectedDrink = drink.Price;

            SetValueDrink?.Invoke($"Вы выбрали\n{drink.Name} - {drink.Price} руб.");
            SetVisibleButtonsDrink?.Invoke(CoffeBuy);
            SetVisibleButtonsMoney?.Invoke(true);
        }

        //Целчок по кнопке внести монету
        public void ClickButonMoney(int tag)
        {
            Coin coin = coinsInVendingMashine[tag];
            AmountPaid += coin.Rating;
            string message;

            if (PriceSelectedDrink < AmountPaid)
            {
                message = $"Вы внесли {AmountPaid.ToString()} руб.\n" +
                    $"Ваша сдача {AmountPaid - PriceSelectedDrink} руб.";
            }
            else
            {
                message = $"Вы внесли {AmountPaid.ToString()} руб.\n" +
                    $"Осталось {PriceSelectedDrink - AmountPaid} руб.";
            }

            SetValueInvestedClient?.Invoke(message);

            if (PriceSelectedDrink < AmountPaid)
            {
                SetVisibleButtonsMoney?.Invoke(false);
                SetVisibilityButtonBuy?.Invoke(true);
            }
        }
    }
}