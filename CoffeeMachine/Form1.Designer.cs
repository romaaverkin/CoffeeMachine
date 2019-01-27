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
            this.CoinsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.currentBalanceVendingMachineLabel = new System.Windows.Forms.Label();
            this.yourСhangelabel = new System.Windows.Forms.Label();
            this.thankLabel = new System.Windows.Forms.Label();
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
            // CoinsFlowLayoutPanel
            // 
            this.CoinsFlowLayoutPanel.AutoScroll = true;
            this.CoinsFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CoinsFlowLayoutPanel.Location = new System.Drawing.Point(223, 50);
            this.CoinsFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CoinsFlowLayoutPanel.Name = "CoinsFlowLayoutPanel";
            this.CoinsFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.CoinsFlowLayoutPanel.Size = new System.Drawing.Size(180, 300);
            this.CoinsFlowLayoutPanel.TabIndex = 3;
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
            // thankLabel
            // 
            this.thankLabel.AutoSize = true;
            this.thankLabel.Location = new System.Drawing.Point(142, 409);
            this.thankLabel.Name = "thankLabel";
            this.thankLabel.Size = new System.Drawing.Size(0, 13);
            this.thankLabel.TabIndex = 7;
            this.thankLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 646);
            this.Controls.Add(this.thankLabel);
            this.Controls.Add(this.yourСhangelabel);
            this.Controls.Add(this.currentBalanceVendingMachineLabel);
            this.Controls.Add(this.CoinsFlowLayoutPanel);
            this.Controls.Add(this.DrinksFlowLayoutPanel);
            this.Controls.Add(this.paymentLabel);
            this.Controls.Add(this.selectedDrinkLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label selectedDrinkLabel;
        private System.Windows.Forms.Label paymentLabel;
        private System.Windows.Forms.FlowLayoutPanel DrinksFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel CoinsFlowLayoutPanel;
        private System.Windows.Forms.Label currentBalanceVendingMachineLabel;
        private System.Windows.Forms.Label yourСhangelabel;
        private System.Windows.Forms.Label thankLabel;
    }
}

