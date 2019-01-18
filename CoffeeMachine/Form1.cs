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

                moneyButton.Click += coinsInVendingMashine;
                CoinsFlowLayoutPanel.Controls.Add(moneyButton);
            };
        }

        private void DrinkButtonOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void coinsInVendingMashine(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
