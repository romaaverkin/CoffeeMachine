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
        public delegate void MethodVisibleAndSetHundler(bool visible, string message);
        public event MethodVisibleAndSetHundler VisibleAndSetHundler;
        public delegate void DelegateEnabledDrinks(int tag);
        public event DelegateEnabledDrinks EventEnabledDrinks;

        //цена выбранного клиентом напитка
        public int PriceSelectedDrink = 0;

        //Внесенная клиентом сумма
        public int AmountPaid = 0;

        //Кофе куплен
        public bool CoffeBuy = false;

        //монеты для сдачи
        public List<Coin> moneyForChange = new List<Coin>();

        //монеты внесенные клиентом
        public List<Coin> moneyInvestedClient = new List<Coin>();

        //всего денег в машине
        public int totalSum = 0;

        //Хватит ли сдачи
        public bool EnoughChange = true;

        // Цена самого дорого кофе
        public int PriceMostExpensiveDrink = 0;

        //Коллекция видов кофе
        public List<Drink> myDrinks = new List<Drink>
        {
            //Можно добавить новые виды кофе
            new Drink("Черный кофе", 16),
            new Drink("Кипяток", 8),
            new Drink("Капучино", 35),
            new Drink("Кофе с молоком", 22),
            new Drink("Латте", 39),
        };

        //Коллекция видов монет
        public List<Coin> coinsInVendingMashine = new List<Coin>
        {
            //Можно добавлять новые номиналы
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

            //коллекцию для внесенных монет заполняем нолями
            for (int i = 0; i < coinsInVendingMashine.Count; i++)
            {
                moneyInvestedClient.Add(new Coin(coinsInVendingMashine[i].Rating, 0));
            }

            //сумма денег в автомате
            for (int i = 0; i < coinsInVendingMashine.Count; i++)
            {
                totalSum += coinsInVendingMashine[i].Rating * coinsInVendingMashine[i].Quantity;
            }

            //Устанавливаем цену самого дорогого напитка
            PriceMostExpensiveDrink = myDrinks[myDrinks.Count - 1].Price;

        }

        //Клик по кнопке выбора напитков
        public void ClickButtonDrink(int tag)
        {
            Drink drink = myDrinks[tag];
            PriceSelectedDrink = drink.Price;

            EventEnabledDrinks?.Invoke(-1);
            if (AmountPaid < PriceSelectedDrink) 
            {
                CoffeBuy = false;
                SetValueDrink?.Invoke($"Недостаточно денег\nдля покупки.");
                VisibleAndSetHundler?.Invoke(CoffeBuy, "Спасибо за покупку!");
                ChangeInMachine?.Invoke("");
            }
            else if (AmountPaid == PriceSelectedDrink)
            {
                CoffeBuy = true;
                SetVisibleButtonsMoney?.Invoke(CoffeBuy);
                AmountPaid = 0;
                SetValueInvestedClient?.Invoke($"Внесите деньги");
                SetValueDrink?.Invoke($"Вы выбрали\n{drink.Name} - {drink.Price} руб.");
                VisibleAndSetHundler?.Invoke(CoffeBuy, "Спасибо за покупку!");
                ChangeInMachine?.Invoke("Спасибо, что без сдачи!");
            }
            else
            {
                CoffeBuy = true;
                SetValueInvestedClient?.Invoke($"Внесите деньги");
                VisibleAndSetHundler?.Invoke(CoffeBuy, "Спасибо за покупку!");
                MoneyForChange();
                CoinsInMachineValue();
            }

        }

        //Щелчок по кнопке внести монету
        public void ClickButonMoney(int tag)
        {
            CoffeBuy = false;
            VisibleAndSetHundler?.Invoke(false, "");
            ChangeInMachine?.Invoke("");
            SetValueDrink?.Invoke($"Выберите напиток");
            Coin coin = coinsInVendingMashine[tag];
            AmountPaid += coin.Rating;

            if (AmountPaid >= PriceMostExpensiveDrink)
            {
                SetVisibleButtonsMoney?.Invoke(CoffeBuy);
            }

            SetValueInvestedClient?.Invoke($"Вы внесли {AmountPaid.ToString()} руб.\n");

            coin.Quantity++;
            moneyInvestedClient[tag].Quantity++;
            totalSum += coin.Rating;
            CoinsInMachineValue();

            int MaximalTag = -1;

            for (int i = myDrinks.Count - 1; i >= 0; i--)
            {
                if (myDrinks[i].Price <= AmountPaid)
                {
                    MaximalTag = i;
                    break;
                }
            }

            EventEnabledDrinks?.Invoke(MaximalTag);
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
                        //coinsInVendingMashine[i].Quantity--;
                        moneyForChange[i].Quantity++;
                        remainingChange -= coinsInVendingMashine[i].Rating;
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
                        //coinsInVendingMashine[i].Quantity--;
                        moneyForChange[i].Quantity++;
                        remainingChange -= coinsInVendingMashine[i].Rating;
                    }
                }
                else
                {
                    continue;
                }

            }

            //В автомате не хватило денег для сдачи
            if (remainingChange != 0)
            {
                clientChange = "Извините, нет монет для сдачи.\nЗаберите внесенные деньги.\n";
                ChangeInMachine?.Invoke(clientChange);

                //возвращаем монеты из коллекции монеты для сдачи в машину
                for (int n = 0; n < moneyForChange.Count; n++)
                {
                    coinsInVendingMashine[n].Quantity += moneyForChange[n].Quantity;
                }

                for (int k = 0; k < moneyInvestedClient.Count; k++)
                {
                    coinsInVendingMashine[k].Quantity -= moneyInvestedClient[k].Quantity;
                    clientChange += $"{moneyInvestedClient[k].Rating.ToString()} руб. в количестве {moneyInvestedClient[k].Quantity.ToString()} штук\n";
                }

                int totalSumTemp = 0;

                for (int b = 0; b < coinsInVendingMashine.Count; b++)
                {
                    totalSumTemp += coinsInVendingMashine[b].Rating * coinsInVendingMashine[b].Quantity;
                }

                totalSum = totalSumTemp;
                EnoughChange = false;
            }
            else
            {
                //Убираем из автомата деньги для сдачи
                for (int i = 0; i < moneyForChange.Count; i++)
                {
                    coinsInVendingMashine[i].Quantity -= moneyForChange[i].Quantity;
                }

                //Формируем строку для сдачи
                for (int j = 0; j < moneyForChange.Count; j++)
                {
                    clientChange += $"{moneyForChange[j].Rating.ToString()} руб. в количестве {moneyForChange[j].Quantity.ToString()} штук\n";
                }
                totalSum -= AmountPaid - PriceSelectedDrink;
                EnoughChange = true;
            }

            ChangeInMachine?.Invoke(clientChange);
            AmountPaid = 0;
        }

        //Обнуляем коллекцию для сдачи
        public void ClearMoneyForChange()
        {
            for (int i = 0; i < moneyForChange.Count; i++)
            {
                moneyForChange[i].Quantity = 0;
            }
        }

        //Обнуляем коллекцию монет внесенных клиентом
        public void ClearMoneyInvestedClient()
        {
            for (int i = 0; i < moneyInvestedClient.Count; i++)
            {
                moneyInvestedClient[i].Quantity = 0;
            }
        }
    }
}