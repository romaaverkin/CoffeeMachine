using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoffeeMachine
{

    public partial class Form1 : Form
    {
        VendingMachine vendingMachine;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vendingMachine = new VendingMachine();
            //Подписались на событие клиет кликнул по кнопке выбора напитков
            vendingMachine.SetValueDrink += SetValueSelectedDrinkLabel;
            vendingMachine.SetVisibleButtonsDrink += SetVisibilityButtoncDrink;
            vendingMachine.SetVisibleButtonsMoney += SetVisibilityButtonsMoney;
            vendingMachine.SetValueInvestedClient += SetValueInvesteClientMoney;
            vendingMachine.SetVisibilityButtonBuy += SetVisibleBuyButton;

            for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
            {
                Button drinkButton = new Button()
                {
                    Width = 150,
                    Name = "drinkButton" + i,
                    Tag = i,
                    Text = $"{vendingMachine.myDrinks[i].Name} - {vendingMachine.myDrinks[i].Price} руб.",
                };

                drinkButton.Click += DrinkButtonOnClick;
                DrinksFlowLayoutPanel.Controls.Add(drinkButton);
            };

            for (int i = 0; i < vendingMachine.coinsInVendingMashine.Count; i++)
            {
                Button moneyButton = new Button()
                {
                    Name = "moneyButton" + i,
                    Width = 150,
                    Tag = i,
                    Text = $"Внести {vendingMachine.coinsInVendingMashine[i].Rating} руб.",
                    Enabled = false
                };

                moneyButton.Click += CoinsInVendingMashine;
                CoinsFlowLayoutPanel.Controls.Add(moneyButton);
            };

            CoinsInMachine();
        }

        //Щелчок по кнопке выбора напитков
        private void DrinkButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int buttonTag = Convert.ToInt32(button.Tag);

            vendingMachine.ClickButtonDrink(buttonTag);
        }

        //Щелсок по кнопке внесения денег
        private void CoinsInVendingMashine(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int buttonTag = Convert.ToInt32(button.Tag);

            vendingMachine.ClickButonMoney(buttonTag);
        }

        //Устанавливае значение поля выбранного напитка
        public void SetValueSelectedDrinkLabel()
        {
            selectedDrinkLabel.Text = vendingMachine.selectedDrink;
        }

        //Устанавливает значение поля внесенных клиентом денег и сдачи
        public void SetValueInvesteClientMoney()
        {
            if (vendingMachine.PriceSelectedDrink < vendingMachine.AmountPaid)
            {
                paymentLabel.Text = $"Вы внесли {vendingMachine.AmountPaid.ToString()} руб.\n" +
                    $"Ваша сдача {vendingMachine.AmountPaid - vendingMachine.PriceSelectedDrink} руб.";
            }
            else
            {
                paymentLabel.Text = $"Вы внесли {vendingMachine.AmountPaid.ToString()} руб.\n" +
                    $"Осталось {vendingMachine.PriceSelectedDrink - vendingMachine.AmountPaid} руб.";
            }
        }

        //Видимы ли кнопки выбора напитков
        public void SetVisibilityButtoncDrink()
        {
            bool CoffeBuy = vendingMachine.CoffeBuy;

            for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
            {
                DrinksFlowLayoutPanel.Controls["drinkButton" + i].Enabled = CoffeBuy;
            }
        }

        //Видимы ли кнопки внесения денег
        public void SetVisibilityButtonsMoney()
        {
            bool CoffeBuy = vendingMachine.CoffeBuy;

            if (!CoffeBuy && vendingMachine.PriceSelectedDrink > vendingMachine.AmountPaid)
            {
                for (int i = 0; i < vendingMachine.coinsInVendingMashine.Count; i++)
                {
                    CoinsFlowLayoutPanel.Controls["moneyButton" + i].Enabled = !CoffeBuy;
                }
            }
            else
            {
                for (int i = 0; i < vendingMachine.coinsInVendingMashine.Count; i++)
                {
                    CoinsFlowLayoutPanel.Controls["moneyButton" + i].Enabled = CoffeBuy;
                }
            }
        }

        //Узнать сколько монет есть в машине
        public void CoinsInMachine()
        {
            string CoinsInTheMachine = "Сейчас есть\n";

            for (int i = 0; i < vendingMachine.coinsInVendingMashine.Count; i++)
            {
                CoinsInTheMachine += $"{vendingMachine.coinsInVendingMashine[i].Rating} руб. в количестве {vendingMachine.coinsInVendingMashine[i].Quantity} штук\n";
            }

            currentBalanceVendingMachineLabel.Text = CoinsInTheMachine;
        }

        //Видимость кнопки купить
        public void SetVisibleBuyButton()
        {
            if (vendingMachine.PriceSelectedDrink < vendingMachine.AmountPaid)
            {
                buyButton.Enabled = true;
            }
        }
    }
}
