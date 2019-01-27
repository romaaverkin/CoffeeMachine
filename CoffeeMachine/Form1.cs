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
            vendingMachine.SetVisibleButtonsMoney += SetEnabledButtonsMoney;
            vendingMachine.SetValueInvestedClient += SetValueInvesteClientMoney;
            vendingMachine.CoinsInMachine += SetValueCoinsInMachine;
            vendingMachine.ChangeInMachine += SetValueForChange;
            vendingMachine.VisibleAndSetHundler += VisibleThankLabel;
            vendingMachine.EventEnabledDrinks += SetEnabledButtonsDrinks;

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
        public void SetValueSelectedDrinkLabel(string name)
        {
            selectedDrinkLabel.Text = name;
        }

        //Устанавливает значение поля внесенных клиентом денег
        public void SetValueInvesteClientMoney(string message)
        {
            paymentLabel.Text = message;
        }

        //Устанавливает активность кнопок внесения денег
        public void SetEnabledButtonsMoney(bool visible)
        {
            for (int i = 0; i < vendingMachine.coinsInVendingMashine.Count; i++)
            {
                CoinsFlowLayoutPanel.Controls["moneyButton" + i].Enabled = visible;
            }
        }

        // Устанавливает активность кнопок выбора напитков
        public void SetEnabledButtonsDrinks(int tag)
        {
            if (tag == -1)
            {
                for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
                {
                    DrinksFlowLayoutPanel.Controls["drinkButton" + i].Enabled = false;
                }
            }
            else
            {
                for (int i = 0; i <= tag; i++)
                {
                    DrinksFlowLayoutPanel.Controls["drinkButton" + i].Enabled = true;
                }
            }
        }

        //Устанавливает сколько монет и какого номинала есть в машине
        public void SetValueCoinsInMachine(string message)
        {
            currentBalanceVendingMachineLabel.Text = message;
        }

        //Устанавливает поле сдачи
        public void SetValueForChange(string message)
        {
            yourСhangelabel.Text = message;
        }

        //Устанавливает видимость поля спасибо за покупку
        public void VisibleThankLabel(bool visible, string message)
        {
            thankLabel.Visible = visible;
            thankLabel.Text = message;
        }
    }
}
