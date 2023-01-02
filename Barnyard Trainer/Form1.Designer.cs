namespace Barnyard_Trainer
{
    partial class BarnyardTrainer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarnyardTrainer));
            this.label1 = new System.Windows.Forms.Label();
            this.moneyTextBox = new System.Windows.Forms.TextBox();
            this.moneyApplyButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fovTrackBar = new System.Windows.Forms.TrackBar();
            this.staminaCheckBox = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.milkCheckBox = new System.Windows.Forms.CheckBox();
            this.squirtCheckBox = new System.Windows.Forms.CheckBox();
            this.itemsApplyButton = new System.Windows.Forms.Button();
            this.itemsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.positionApplyButton = new System.Windows.Forms.Button();
            this.xPosTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.yPosTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.zPosTextBox = new System.Windows.Forms.TextBox();
            this.positionRefreshButton = new System.Windows.Forms.Button();
            this.zoomCheckBox = new System.Windows.Forms.CheckBox();
            this.firstPersonCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Money";
            // 
            // moneyTextBox
            // 
            this.moneyTextBox.Location = new System.Drawing.Point(13, 30);
            this.moneyTextBox.Name = "moneyTextBox";
            this.moneyTextBox.Size = new System.Drawing.Size(55, 20);
            this.moneyTextBox.TabIndex = 1;
            this.moneyTextBox.Text = "9999";
            // 
            // moneyApplyButton
            // 
            this.moneyApplyButton.Location = new System.Drawing.Point(74, 30);
            this.moneyApplyButton.Name = "moneyApplyButton";
            this.moneyApplyButton.Size = new System.Drawing.Size(75, 23);
            this.moneyApplyButton.TabIndex = 2;
            this.moneyApplyButton.Text = "Apply";
            this.moneyApplyButton.UseVisualStyleBackColor = true;
            this.moneyApplyButton.Click += new System.EventHandler(this.MoneyApplyButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "FOV";
            // 
            // fovTrackBar
            // 
            this.fovTrackBar.Location = new System.Drawing.Point(4, 162);
            this.fovTrackBar.Maximum = 250;
            this.fovTrackBar.Name = "fovTrackBar";
            this.fovTrackBar.Size = new System.Drawing.Size(104, 45);
            this.fovTrackBar.TabIndex = 4;
            this.fovTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.fovTrackBar.Value = 100;
            this.fovTrackBar.Scroll += new System.EventHandler(this.FovTrackBar_Scroll);
            // 
            // staminaCheckBox
            // 
            this.staminaCheckBox.AutoSize = true;
            this.staminaCheckBox.Location = new System.Drawing.Point(10, 190);
            this.staminaCheckBox.Name = "staminaCheckBox";
            this.staminaCheckBox.Size = new System.Drawing.Size(98, 17);
            this.staminaCheckBox.TabIndex = 5;
            this.staminaCheckBox.Text = "Infinite Stamina";
            this.staminaCheckBox.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // milkCheckBox
            // 
            this.milkCheckBox.AutoSize = true;
            this.milkCheckBox.Location = new System.Drawing.Point(10, 213);
            this.milkCheckBox.Name = "milkCheckBox";
            this.milkCheckBox.Size = new System.Drawing.Size(111, 17);
            this.milkCheckBox.TabIndex = 6;
            this.milkCheckBox.Text = "Infinite Milk Ammo";
            this.milkCheckBox.UseVisualStyleBackColor = true;
            // 
            // squirtCheckBox
            // 
            this.squirtCheckBox.AutoSize = true;
            this.squirtCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.squirtCheckBox.Location = new System.Drawing.Point(10, 236);
            this.squirtCheckBox.Name = "squirtCheckBox";
            this.squirtCheckBox.Size = new System.Drawing.Size(128, 17);
            this.squirtCheckBox.TabIndex = 7;
            this.squirtCheckBox.Text = "Squirt without glasses";
            this.squirtCheckBox.UseVisualStyleBackColor = true;
            this.squirtCheckBox.CheckedChanged += new System.EventHandler(this.SquirtCheckBox_CheckedChanged);
            // 
            // itemsApplyButton
            // 
            this.itemsApplyButton.Location = new System.Drawing.Point(74, 74);
            this.itemsApplyButton.Name = "itemsApplyButton";
            this.itemsApplyButton.Size = new System.Drawing.Size(75, 23);
            this.itemsApplyButton.TabIndex = 11;
            this.itemsApplyButton.Text = "Apply";
            this.itemsApplyButton.UseVisualStyleBackColor = true;
            this.itemsApplyButton.Click += new System.EventHandler(this.ItemsApplyButton_Click);
            // 
            // itemsTextBox
            // 
            this.itemsTextBox.Location = new System.Drawing.Point(13, 74);
            this.itemsTextBox.Name = "itemsTextBox";
            this.itemsTextBox.Size = new System.Drawing.Size(55, 20);
            this.itemsTextBox.TabIndex = 10;
            this.itemsTextBox.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Items in hand";
            // 
            // positionApplyButton
            // 
            this.positionApplyButton.Location = new System.Drawing.Point(254, 120);
            this.positionApplyButton.Name = "positionApplyButton";
            this.positionApplyButton.Size = new System.Drawing.Size(75, 23);
            this.positionApplyButton.TabIndex = 14;
            this.positionApplyButton.Text = "Apply";
            this.positionApplyButton.UseVisualStyleBackColor = true;
            this.positionApplyButton.Click += new System.EventHandler(this.PositionApplyButton_Click);
            // 
            // xPosTextBox
            // 
            this.xPosTextBox.Location = new System.Drawing.Point(78, 120);
            this.xPosTextBox.Name = "xPosTextBox";
            this.xPosTextBox.Size = new System.Drawing.Size(46, 20);
            this.xPosTextBox.TabIndex = 13;
            this.xPosTextBox.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Position";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(127, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Y";
            // 
            // yPosTextBox
            // 
            this.yPosTextBox.Location = new System.Drawing.Point(139, 120);
            this.yPosTextBox.Name = "yPosTextBox";
            this.yPosTextBox.Size = new System.Drawing.Size(46, 20);
            this.yPosTextBox.TabIndex = 16;
            this.yPosTextBox.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Z";
            // 
            // zPosTextBox
            // 
            this.zPosTextBox.Location = new System.Drawing.Point(202, 120);
            this.zPosTextBox.Name = "zPosTextBox";
            this.zPosTextBox.Size = new System.Drawing.Size(46, 20);
            this.zPosTextBox.TabIndex = 18;
            this.zPosTextBox.Text = "0";
            // 
            // positionRefreshButton
            // 
            this.positionRefreshButton.Location = new System.Drawing.Point(11, 118);
            this.positionRefreshButton.Name = "positionRefreshButton";
            this.positionRefreshButton.Size = new System.Drawing.Size(53, 23);
            this.positionRefreshButton.TabIndex = 20;
            this.positionRefreshButton.Text = "Refresh";
            this.positionRefreshButton.UseVisualStyleBackColor = true;
            this.positionRefreshButton.Click += new System.EventHandler(this.PositionRefreshButton_Click);
            // 
            // zoomCheckBox
            // 
            this.zoomCheckBox.AutoSize = true;
            this.zoomCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomCheckBox.Location = new System.Drawing.Point(10, 259);
            this.zoomCheckBox.Name = "zoomCheckBox";
            this.zoomCheckBox.Size = new System.Drawing.Size(130, 17);
            this.zoomCheckBox.TabIndex = 21;
            this.zoomCheckBox.Text = "Disable Camera Zoom";
            this.zoomCheckBox.UseVisualStyleBackColor = true;
            this.zoomCheckBox.CheckedChanged += new System.EventHandler(this.ZoomCheckBox_CheckedChanged);
            // 
            // firstPersonCheckBox
            // 
            this.firstPersonCheckBox.AutoSize = true;
            this.firstPersonCheckBox.Location = new System.Drawing.Point(10, 283);
            this.firstPersonCheckBox.Name = "firstPersonCheckBox";
            this.firstPersonCheckBox.Size = new System.Drawing.Size(168, 17);
            this.firstPersonCheckBox.TabIndex = 22;
            this.firstPersonCheckBox.Text = "First Person Mode (unfinished)";
            this.firstPersonCheckBox.UseVisualStyleBackColor = true;
            this.firstPersonCheckBox.CheckedChanged += new System.EventHandler(this.FirstPersonCheckBox_CheckedChanged);
            // 
            // BarnyardTrainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.firstPersonCheckBox);
            this.Controls.Add(this.zoomCheckBox);
            this.Controls.Add(this.positionRefreshButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.zPosTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.yPosTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.positionApplyButton);
            this.Controls.Add(this.xPosTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.itemsApplyButton);
            this.Controls.Add(this.itemsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.squirtCheckBox);
            this.Controls.Add(this.milkCheckBox);
            this.Controls.Add(this.staminaCheckBox);
            this.Controls.Add(this.fovTrackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.moneyApplyButton);
            this.Controls.Add(this.moneyTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BarnyardTrainer";
            this.Text = "Barnyard Trainer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox moneyTextBox;
        private System.Windows.Forms.Button moneyApplyButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar fovTrackBar;
        private System.Windows.Forms.CheckBox staminaCheckBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox milkCheckBox;
        private System.Windows.Forms.CheckBox squirtCheckBox;
        private System.Windows.Forms.Button itemsApplyButton;
        private System.Windows.Forms.TextBox itemsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button positionApplyButton;
        private System.Windows.Forms.TextBox xPosTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox yPosTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox zPosTextBox;
        private System.Windows.Forms.Button positionRefreshButton;
        private System.Windows.Forms.CheckBox zoomCheckBox;
        private System.Windows.Forms.CheckBox firstPersonCheckBox;
    }
}

