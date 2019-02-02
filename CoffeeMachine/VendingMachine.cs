using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachine
{
    class VendingMachine
    {
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
            new Drink("Черный кофе", 16),
            new Drink("Кипяток", 8),
            new Drink("Капучино", 35),
            new Drink("Кофе с молоком", 22),
            new Drink("Латте", 39),
            new Drink("3 в 1", 37)
        };

        //Коллекция видов монет
        public List<Coin> coinsInVendingMashine = new List<Coin>
        {
            //Можно добавлять новые номиналы
            new Coin(2, 10),
            new Coin(10, 10),
            new Coin(5, 10),
            new Coin(25, 2),
            new Coin(1, 15),
            new Coin(100, 1),
            new Coin(21, 3)
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

            if (AmountPaid == drink.Price) //если без сдачи
            {
                MessageBuyCoffee = "Спсибо, что без сдачи!";
            }
            else if (MoneyForChangeNoGreedy(AmountPaid - drink.Price)) //не жадный метод, если в машине есть деньги для сдачи
            {
                totalSum -= AmountPaid - drink.Price;
                GiveChange();
                MessageBuyCoffee = "Спасибо за покупку!";
                CoinsInMachineValue();
            }
            else if (MoneyForChangeGreedy(AmountPaid - drink.Price)) //жадный метод, если в машине есть деньги для сдачи
            {
                totalSum -= AmountPaid - drink.Price;
                GiveChange();
                MessageBuyCoffee = "Спасибо за покупку!";
                CoinsInMachineValue();
            }
            else if(MoneyForChangeNoGreedy(AmountPaid)) //не жадный метод, если в машине нет денег для сдачи
            {
                CoffeBuy = false;
                GiveChange();
                totalSum -= AmountPaid;
                MessageBuyCoffee = "Приносим извинения!";
                CoinsInMachineValue();
            }
            else if(MoneyForChangeGreedy(AmountPaid)) //жадный метод, если в машине нет денег для сдачи
            {
                CoffeBuy = false;
                GiveChange();
                totalSum -= AmountPaid;
                MessageBuyCoffee = "Приносим извинения!";
                CoinsInMachineValue();
            }

            AmountPaid = 0;

            //Для всех подписавшихся на это событие
            EventClickButtonDrink?.Invoke();
        }

        //Щелчок по кнопке внести монету
        public void ClickButonMoney(int tag)
        {
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

            //Для всех подписавшихся на это событие
            EventClickButtonMoney?.Invoke();
        }

        //Не жадный метод
        public bool MoneyForChangeNoGreedy(int change)
        {
            List<int> coinCount = new List<int>();
            for (int i = 0; i <= change; i++)
            {
                coinCount.Add(0);
            }

            List<int> coinsUsed = new List<int>();
            for (int i = 0; i <= change; i++)
            {
                coinsUsed.Add(0);
            }

            foreach (int cents in Enumerable.Range(0, change + 1))
            {
                int coinQuantity = cents;
                int newCoin = 1;
                foreach (int j in (from c in coinsInVendingMashine
                                   where c.Rating <= cents
                                   select c.Rating).ToList())
                {
                    if (coinCount[cents - j] + 1 < coinQuantity)
                    {
                        coinQuantity = coinCount[cents - j] + 1;
                        newCoin = j;
                    }
                }
                coinCount[cents] = coinQuantity;
                coinsUsed[cents] = newCoin;
            }

            //Заполняем коллекцию монет для сдачи
            int coin = change;

            while (coin > 0)
            {
                int thisCoin = coinsUsed[coin];
                int iTemp = 0;
                for (int i = iTemp; i < moneyForChange.Count; i++)
                {
                    if (moneyForChange[i].Rating == thisCoin)
                    {
                        moneyForChange[i].Quantity++;
                        iTemp = i;
                        break;
                    }
                }
                coin -= thisCoin;
            }

            //Проверяем наличие необходимых монет для сдачи в автомате
            for (int i = 0; i < moneyForChange.Count; i++)
            {
                if (coinsInVendingMashine[i].Quantity - moneyForChange[i].Quantity < 0)
                {
                    ClearMoneyForChange();
                    return false;
                }
            }

            //Вынимем монеты для сдачи из машины
            for (int i = 0; i < moneyForChange.Count; i++)
            {
                coinsInVendingMashine[i].Quantity -= moneyForChange[i].Quantity;
            }

            return true;
        }

        //Жадный метод
        public bool MoneyForChangeGreedy(int change)
        {
            int changeTemp = change;
            for (int i = coinsInVendingMashine.Count - 1; i >= 0; i--)
            {
                if (changeTemp < coinsInVendingMashine[i].Rating)
                {
                    continue;
                }
                else if (changeTemp == coinsInVendingMashine[i].Rating)
                {
                    if (coinsInVendingMashine[i].Quantity != 0)
                    {
                        moneyForChange[i].Quantity++;
                        changeTemp -= coinsInVendingMashine[i].Rating;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (coinsInVendingMashine[i].Quantity != 0)
                {
                    int temp = coinsInVendingMashine[i].Quantity;

                    while (temp != 0 && changeTemp >= coinsInVendingMashine[i].Rating)
                    {
                        moneyForChange[i].Quantity++;
                        temp--;
                        changeTemp -= coinsInVendingMashine[i].Rating;
                    }
                }
                else
                {
                    continue;
                }
            }

            //Проверяем наличие необходимых монет для сдачи в автомате
            int sumInMoneyForChange = 0;

            for (int i = 0; i < moneyForChange.Count; i++)
            {
                sumInMoneyForChange += moneyForChange[i].Rating * moneyForChange[i].Quantity;
            }

            if (sumInMoneyForChange < change)
            {
                ClearMoneyForChange();
                return false;
            }
            else
            {
                //Вынимем монеты для сдачи из машины
                for (int i = 0; i < moneyForChange.Count; i++)
                {
                    coinsInVendingMashine[i].Quantity -= moneyForChange[i].Quantity;
                }

                return true;
            }
        }

        //Формируем строку для сдачи
        public void GiveChange()
        {
            clientChange = "Ваша сдача\n";

            for (int i = 0; i < moneyForChange.Count; i++)
            {
                clientChange += $"{moneyForChange[i].Rating.ToString()} руб. в количестве {moneyForChange[i].Quantity.ToString()} штук\n";
            }

            if (CoffeBuy)
            {
                clientChange += $"Общая сумма {AmountPaid - drink.Price} руб.";
            }
            else
            {
                clientChange += $"Общая сумма {AmountPaid} руб.";
            }

            ClearMoneyForChange(); //очищаем коллекцию для сдачи
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

        //возвращаем деньги в машину
        public void ReturnMoneyInMachine()
        {
            for (int i = 0; i < moneyForChange.Count; i++)
            {
                coinsInVendingMashine[i].Quantity += moneyForChange[i].Quantity;
            }
        }
    }
}