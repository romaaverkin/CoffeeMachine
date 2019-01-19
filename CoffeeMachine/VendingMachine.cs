using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachine
{
    class VendingMachine
    {
        public delegate void MethodSetValue(string value);
        public event MethodSetValue SetValueInvestedClient;
        public event MethodSetValue SetValueDrink;
        public event MethodSetValue CoinsInMachine;
        public event MethodSetValue ChangeInMachine;
        public delegate void MethodVisibleHandler(bool visible);
        public event MethodVisibleHandler SetVisibleButtonsMoney;
        public event MethodVisibleHandler SetVisibilityButtonBuy;
        public event MethodVisibleHandler SetVisibleButtonsDrink;

        //Тег Выбранного напитка
        public int? selectedDrinkTag;

        //цена выбранного клиентом напитка
        public int PriceSelectedDrink = 0;

        //Внесенная клиентом сумма
        public int AmountPaid = 0;

        //Кофе куплен
        public bool CoffeBuy = false;

        public List<Coin> moneyForChange = new List<Coin>(); //монеты для сдачи

        //всего денег
        public int totalSum = 0;

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

            //заполняем коллекцию для сдачи 
            for (int i = 0; i < coinsInVendingMashine.Count; i++)
            {
                moneyForChange.Add(new Coin(coinsInVendingMashine[i].Rating, 0));
            }

            for (int i = 0; i < coinsInVendingMashine.Count; i++)
            {
                totalSum += coinsInVendingMashine[i].Rating * coinsInVendingMashine[i].Quantity;
            }
        }

        //Клик по кнопек выбора напитков
        public void ClickButtonDrink(int tag)
        {
            Drink drink = myDrinks[tag];
            selectedDrinkTag = tag;
            PriceSelectedDrink = drink.Price;

            SetValueDrink?.Invoke($"Вы выбрали\n{drink.Name} - {drink.Price} руб.");
            CoffeBuy = false;
            SetVisibleButtonsDrink?.Invoke(CoffeBuy);
            SetVisibleButtonsMoney?.Invoke(!CoffeBuy);
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

            if (PriceSelectedDrink <= AmountPaid)
            {
                SetVisibleButtonsMoney?.Invoke(CoffeBuy);
                SetVisibilityButtonBuy?.Invoke(true);
            }

            coinsInVendingMashine[tag].Quantity++;
            totalSum += coinsInVendingMashine[tag].Rating;
        }

        //какие монеты в машине
        public void CoinsInMachineValue()
        {
            string CoinsInTheMachine = "В автомате есть\n";

            for (int i = 0; i < coinsInVendingMashine.Count; i++)
            {
                CoinsInTheMachine += $"{coinsInVendingMashine[i].Rating} руб. в количестве {coinsInVendingMashine[i].Quantity} штук\n";
            }

            CoinsInTheMachine += $"Общая сумма {totalSum} руб.";

            CoinsInMachine?.Invoke(CoinsInTheMachine);
        }

        //Сдача
        public void MoneyForChange()
        {
            int remainingChange = AmountPaid - PriceSelectedDrink;
            string clientChange = "Ваша сдача\n";

            for (int i = coinsInVendingMashine.Count - 1; i >= 0; i--)
            {

                if (remainingChange < coinsInVendingMashine[i].Rating)
                {
                    continue;
                }
                else if (remainingChange == coinsInVendingMashine[i].Rating)
                {
                    if (coinsInVendingMashine[i].Quantity != 0)
                    {
                        coinsInVendingMashine[i].Quantity--;
                        moneyForChange[i].Quantity++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (coinsInVendingMashine[i].Quantity != 0)
                {
                    while (coinsInVendingMashine[i].Quantity != 0 && remainingChange >= coinsInVendingMashine[i].Rating)
                    {
                        coinsInVendingMashine[i].Quantity--;
                        moneyForChange[i].Quantity++;
                        remainingChange -= coinsInVendingMashine[i].Rating;
                    }
                }
                else
                {
                    continue;
                }

            }

            //Формируем строку для сдачи
            for (int j = 0; j < moneyForChange.Count; j++)
            {
                clientChange += $"{moneyForChange[j].Rating.ToString()} руб. в количестве {moneyForChange[j].Quantity.ToString()} штук\n";
            }

            totalSum -= AmountPaid - PriceSelectedDrink;
            ChangeInMachine?.Invoke(clientChange);
        }

        //Обнуляем коллекцию для сдачи
        public void ClearMoneyForChange()
        {
            for (int i = 0; i < moneyForChange.Count; i++)
            {
                moneyForChange[i].Quantity = 0;
            }
        }

        //снова выбрать кофе
        public void AgainSelectCoffe()
        {
            PriceSelectedDrink = 0;
            AmountPaid = 0;
            CoffeBuy = true;
            SetVisibleButtonsDrink?.Invoke(CoffeBuy);
        }

        ////Общая сумма денег в автомате
        //public void TotalSumInMachine()
        //{
        //    int totalSumTemp = 0;

        //    for (int i = 0; i < coinsInVendingMashine.Count; i++)
        //    {
        //        totalSumTemp += coinsInVendingMashine[i].Rating * coinsInVendingMashine[i].Quantity;
        //    }

        //    totalSum = totalSumTemp;
        //}
    }
}