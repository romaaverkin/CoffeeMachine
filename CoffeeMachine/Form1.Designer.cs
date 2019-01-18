namespace CoffeeMachine
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectedDrinkLabel = new System.Windows.Forms.Label();
            this.paymentLabel = new System.Windows.Forms.Label();
            this.DrinksFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.nameButton = new System.Windows.Forms.Button();
            this.currentBalanceVendingMachineLabel = new System.Windows.Forms.Label();
            this.yourСhangelabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectedDrinkLabel
            // 
            this.selectedDrinkLabel.AutoSize = true;
            this.selectedDrinkLabel.Location = new System.Drawing.Point(52, 18);
            this.selectedDrinkLabel.Name = "selectedDrinkLabel";
            this.selectedDrinkLabel.Size = new System.Drawing.Size(101, 13);
            this.selectedDrinkLabel.TabIndex = 0;
            this.selectedDrinkLabel.Text = "Выберите напиток";
            // 
            // paymentLabel
            // 
            this.paymentLabel.AutoSize = true;
            this.paymentLabel.Location = new System.Drawing.Point(260, 18);
            this.paymentLabel.Name = "paymentLabel";
            this.paymentLabel.Size = new System.Drawing.Size(93, 13);
            this.paymentLabel.TabIndex = 1;
            this.paymentLabel.Text = "Вы внесли 0 руб.";
            // 
            // DrinksFlowLayoutPanel
            // 
            this.DrinksFlowLayoutPanel.AutoScroll = true;
            this.DrinksFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrinksFlowLayoutPanel.Location = new System.Drawing.Point(15, 50);
            this.DrinksFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DrinksFlowLayoutPanel.Name = "DrinksFlowLayoutPanel";
            this.DrinksFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.DrinksFlowLayoutPanel.Size = new System.Drawing.Size(180, 300);
            this.DrinksFlowLayoutPanel.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(223, 50);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(180, 300);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // nameButton
            // 
            this.nameButton.Enabled = false;
            this.nameButton.Location = new System.Drawing.Point(172, 369);
            this.nameButton.Name = "nameButton";
            this.nameButton.Size = new System.Drawing.Size(75, 23);
            this.nameButton.TabIndex = 4;
            this.nameButton.Text = "Купить";
            this.nameButton.UseVisualStyleBackColor = true;
            // 
            // currentBalanceVendingMachineLabel
            // 
            this.currentBalanceVendingMachineLabel.AutoSize = true;
            this.currentBalanceVendingMachineLabel.Location = new System.Drawing.Point(12, 449);
            this.currentBalanceVendingMachineLabel.Name = "currentBalanceVendingMachineLabel";
            this.currentBalanceVendingMachineLabel.Size = new System.Drawing.Size(0, 13);
            this.currentBalanceVendingMachineLabel.TabIndex = 5;
            // 
            // yourСhangelabel
            // 
            this.yourСhangelabel.AutoSize = true;
            this.yourСhangelabel.Location = new System.Drawing.Point(220, 449);
            this.yourСhangelabel.Name = "yourСhangelabel";
            this.yourСhangelabel.Size = new System.Drawing.Size(0, 13);
            this.yourСhangelabel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 646);
            this.Controls.Add(this.yourСhangelabel);
            this.Controls.Add(this.currentBalanceVendingMachineLabel);
            this.Controls.Add(this.nameButton);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.DrinksFlowLayoutPanel);
            this.Controls.Add(this.paymentLabel);
            this.Controls.Add(this.selectedDrinkLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label selectedDrinkLabel;
        private System.Windows.Forms.Label paymentLabel;
        private System.Windows.Forms.FlowLayoutPanel DrinksFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button nameButton;
        private System.Windows.Forms.Label currentBalanceVendingMachineLabel;
        private System.Windows.Forms.Label yourСhangelabel;
    }
}

