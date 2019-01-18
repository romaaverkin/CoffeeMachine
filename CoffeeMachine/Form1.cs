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
        }

        private void DrinkButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int buttonTag = Convert.ToInt32(button.Tag);

            vendingMachine.ClickButtonDrink(buttonTag);
        }

        private void CoinsInVendingMashine(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //Устанавливае значение поля выбранного напитка
        public void SetValueSelectedDrinkLabel()
        {
            selectedDrinkLabel.Text = vendingMachine.selectedDrink;
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
    }
}
