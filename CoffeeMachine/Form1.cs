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
        public void SetValueSelectedDrinkLabel(string name)
        {
            selectedDrinkLabel.Text = name;
        }

        //Устанавливает значение поля внесенных клиентом денег и сдачи
        public void SetValueInvesteClientMoney(string message)
        {
            paymentLabel.Text = message;
        }

        //Видимы ли кнопки выбора напитков
        public void SetVisibilityButtoncDrink(bool CoffeBuy)
        {
            for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
            {
                DrinksFlowLayoutPanel.Controls["drinkButton" + i].Enabled = CoffeBuy;
            }
        }

        //Видимы ли кнопки внесения денег
        public void SetVisibilityButtonsMoney(bool visible)
        {
            for (int i = 0; i < vendingMachine.coinsInVendingMashine.Count; i++)
            {
                CoinsFlowLayoutPanel.Controls["moneyButton" + i].Enabled = visible;
            }

            for (int i = 0; i < vendingMachine.coinsInVendingMashine.Count; i++)
            {
                CoinsFlowLayoutPanel.Controls["moneyButton" + i].Enabled = visible;
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
        public void SetVisibleBuyButton(bool visible)
        {
            buyButton.Enabled = visible;
        }

        //Щелчок по кнопке купить
        private void BuyButton_Click(object sender, EventArgs e)
        {
            vendingMachine.selectedDrinkTag = null;
            vendingMachine.PriceSelectedDrink = 0;
            vendingMachine.AmountPaid = 0;
        }
    }
}
