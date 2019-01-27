using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachine
{
    class VendingMachine
    {
        public delegate void MethodSetValue(string value);
        public event MethodSetValue ChangeInMachine;

        public delegate void DelegateClickButtonMoney();
        public event DelegateClickButtonMoney EventClickButtonMoney;

        public delegate void DelegateClickButtonDrink();
        public event DelegateClickButtonDrink EventClickButtonDrink;

        // Сообщение о покупке кофе
        public string MessageBuyCoffee = "";

        //Какой кофе выбрал клиент
        public string ChoiceClient = "Выберите напиток";

        //Активность кнопок внесения денег
        public bool EnabledButtonsMoney = true;

        //Выбранный клиентом кофе
        public Drink drink;

        //Внесенная клиентом сумма
        public int AmountPaid = 0;

        //Кофе куплен
        public bool CoffeBuy = false;

        //монеты для сдачи
        public List<Coin> moneyForChange = new List<Coin>();

        //всего денег в машине
        public int totalSum = 0;

        // Цена самого дорого кофе
        public int PriceMostExpensiveDrink = 0;

        // Какие монеты есть в машине
        public string CoinsInTheMachine = "";

        // Кнопки до какого тега включительно показывать
        public int MaximalTag = -1;

        //Сдача клиента
        public string clientChange = "";

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
            drink = myDrinks[tag];

            CoffeBuy = true;
            EnabledButtonsMoney = true;
            MaximalTag = -1;
            ChoiceClient = $"Вы выбрали\n{drink.Name} - {drink.Price} руб.";

            MoneyForChange();
            CoinsInMachineValue();
            AmountPaid = 0;

            EventClickButtonDrink?.Invoke();
        }

        //Щелчок по кнопке внести монету
        public void ClickButonMoney(int tag)
        {
            ClearMoneyForChange();
            clientChange = "";
            CoffeBuy = false;
            MessageBuyCoffee = "";
            Coin coin = coinsInVendingMashine[tag];
            AmountPaid += coin.Rating;
            ChoiceClient = "Выберите напиток";

            if (AmountPaid >= PriceMostExpensiveDrink)
            {
                EnabledButtonsMoney = false;
            }

            coin.Quantity++;
            totalSum += coin.Rating;
            CoinsInMachineValue();          

            for (int i = myDrinks.Count - 1; i >= 0; i--)
            {
                if (myDrinks[i].Price <= AmountPaid)
                {
                    MaximalTag = i;
                    break;
                }
            }

            EventClickButtonMoney?.Invoke();
        }

        //Сдача
        public void MoneyForChange()
        {
            int remainingChange = AmountPaid - drink.Price;

            clientChange = "Ваша сдача\n";

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
            //Todo: Надо использовать не только жадный метод
            if (remainingChange != 0)
            {
                clientChange = "Извините, нет монет для сдачи.\nЗаберите внесенные деньги.\n";

                int totalSumTemp = 0;

                for (int i = 0; i < coinsInVendingMashine.Count; i++)
                {
                    totalSumTemp += coinsInVendingMashine[i].Rating * coinsInVendingMashine[i].Quantity;
                }

                totalSum = totalSumTemp;
            }
            else
            {
                //Убираем из автомата деньги для сдачи
                for (int i = 0; i < moneyForChange.Count; i++)
                {
                    coinsInVendingMashine[i].Quantity -= moneyForChange[i].Quantity;
                }

                //Формируем строку для сдачи
                for (int i = 0; i < moneyForChange.Count; i++)
                {
                    clientChange += $"{moneyForChange[i].Rating.ToString()} руб. в количестве {moneyForChange[i].Quantity.ToString()} штук\n";
                }

                clientChange += $"Общая сумма {AmountPaid - drink.Price} руб.";
                totalSum -= AmountPaid - drink.Price;
                MessageBuyCoffee = "Спасибо за покупку!";
            }
        }

        //Обнуляем коллекцию для сдачи
        public void ClearMoneyForChange()
        {
            for (int i = 0; i < moneyForChange.Count; i++)
            {
                moneyForChange[i].Quantity = 0;
            }
        }

        //какие монеты в машине
        public void CoinsInMachineValue()
        {
            CoinsInTheMachine = "В автомате есть\n";

            for (int i = 0; i < coinsInVendingMashine.Count; i++)
            {
                CoinsInTheMachine += $"{coinsInVendingMashine[i].Rating} руб. в количестве {coinsInVendingMashine[i].Quantity} штук\n";
            }

            CoinsInTheMachine += $"Общая сумма {totalSum} руб.";
        }
    }
}