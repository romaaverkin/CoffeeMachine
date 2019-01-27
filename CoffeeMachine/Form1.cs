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

            //Подписались на событие клиет кликнул по кнопке внесения денег
            vendingMachine.EventClickButtonMoney += VisibleThankLabel;
            vendingMachine.EventClickButtonMoney += SetValueForChange;
            vendingMachine.EventClickButtonMoney += SetValueSelectedDrinkLabel;
            vendingMachine.EventClickButtonMoney += SetEnabledButtonsMoney;
            vendingMachine.EventClickButtonMoney += SetValueInvesteClientMoney;
            vendingMachine.EventClickButtonMoney += SetValueCoinsInMachine;
            vendingMachine.EventClickButtonMoney += SetEnabledButtonsDrinks;

            //Подписались на событие клиет кликнул по кнопке выбора кофе
            vendingMachine.EventClickButtonDrink += SetEnabledButtonsMoney;
            vendingMachine.EventClickButtonDrink += SetValueSelectedDrinkLabel;
            vendingMachine.EventClickButtonDrink += SetValueInvesteClientMoney;
            vendingMachine.EventClickButtonDrink += SetEnabledButtonsDrinks;
            vendingMachine.EventClickButtonDrink += SetValueForChange;
            vendingMachine.EventClickButtonDrink += SetValueCoinsInMachine;
            vendingMachine.EventClickButtonDrink += VisibleThankLabel;

            for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
            {
                Button drinkButton = new Button()
                {
                    Width = 150,
                    Name = "drinkButton" + i,
                    Tag = i,
                    Text = $"{vendingMachine.myDrinks[i].Name} - {vendingMachine.myDrinks[i].Price} руб.",
                    Enabled = false
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
                    Text = $"Внести {vendingMachine.coinsInVendingMashine[i].Rating} руб."
                };

                moneyButton.Click += CoinsInVendingMashine;
                CoinsFlowLayoutPanel.Controls.Add(moneyButton);
            };

            vendingMachine.CoinsInMachineValue();
            SetValueCoinsInMachine();
        }

        //Щелчок по кнопке выбора напитков
        private void DrinkButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int buttonTag = Convert.ToInt32(button.Tag);

            vendingMachine.ClickButtonDrink(buttonTag);
        }

        //Щелчок по кнопке внесения денег
        private void CoinsInVendingMashine(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int buttonTag = Convert.ToInt32(button.Tag);

            vendingMachine.ClickButonMoney(buttonTag);
        }

        //Устанавливае значение поля выбранного напитка
        public void SetValueSelectedDrinkLabel()
        {
            selectedDrinkLabel.Text = vendingMachine.ChoiceClient;
        }

        //Устанавливает значение поля внесенных клиентом денег
        public void SetValueInvesteClientMoney()
        {
            paymentLabel.Text = $"Вы внесли {vendingMachine.AmountPaid.ToString()} руб.\n";
        }

        //Устанавливает активность кнопок внесения денег
        public void SetEnabledButtonsMoney()
        {
            for (int i = 0; i < vendingMachine.coinsInVendingMashine.Count; i++)
            {
                CoinsFlowLayoutPanel.Controls["moneyButton" + i].Enabled = vendingMachine.EnabledButtonsMoney;
            }
        }

        // Устанавливает активность кнопок выбора напитков
        public void SetEnabledButtonsDrinks()
        {
            if (vendingMachine.MaximalTag == -1)
            {
                for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
                {
                    DrinksFlowLayoutPanel.Controls["drinkButton" + i].Enabled = false;
                }
            }
            else
            {
                for (int i = 0; i <= vendingMachine.MaximalTag; i++)
                {
                    DrinksFlowLayoutPanel.Controls["drinkButton" + i].Enabled = true;
                }
            }
        }

        //Устанавливает сколько монет и какого номинала есть в машине
        public void SetValueCoinsInMachine()
        {
            currentBalanceVendingMachineLabel.Text = vendingMachine.CoinsInTheMachine;
        }

        //Устанавливает поле сдачи
        public void SetValueForChange()
        {
            yourСhangelabel.Text = vendingMachine.clientChange;
        }

        //Устанавливает видимость поля спасибо за покупку
        public void VisibleThankLabel()
        {
            thankLabel.Visible = vendingMachine.CoffeBuy;
            thankLabel.Text = vendingMachine.MessageBuyCoffee;
        }
    }
}
