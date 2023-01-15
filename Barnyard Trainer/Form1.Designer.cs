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
            this.inGameTimer = new System.Windows.Forms.Timer(this.components);
            this.milkCheckBox = new System.Windows.Forms.CheckBox();
            this.squirtGlassesCheckBox = new System.Windows.Forms.CheckBox();
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
            this.outGameTimer = new System.Windows.Forms.Timer(this.components);
            this.statusText = new System.Windows.Forms.Label();
            this.clampCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.jumpTextBox = new System.Windows.Forms.TextBox();
            this.decTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.walkTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.runTextBox = new System.Windows.Forms.TextBox();
            this.sprintTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.accTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.movementApplyButton = new System.Windows.Forms.Button();
            this.speedLabel = new System.Windows.Forms.Label();
            this.controllerCheckBox = new System.Windows.Forms.CheckBox();
            this.windowedCheckBox = new System.Windows.Forms.CheckBox();
            this.infoButton = new System.Windows.Forms.Button();
            this.gravityCheckBox = new System.Windows.Forms.CheckBox();
            this.noclipCheckBox = new System.Windows.Forms.CheckBox();
            this.squirtDelayCheckBox = new System.Windows.Forms.CheckBox();
            this.ladderCheckBox = new System.Windows.Forms.CheckBox();
            this.builtinCheatsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Money";
            // 
            // moneyTextBox
            // 
            this.moneyTextBox.Location = new System.Drawing.Point(231, 226);
            this.moneyTextBox.Name = "moneyTextBox";
            this.moneyTextBox.Size = new System.Drawing.Size(55, 20);
            this.moneyTextBox.TabIndex = 1;
            this.moneyTextBox.Text = "9999";
            // 
            // moneyApplyButton
            // 
            this.moneyApplyButton.Location = new System.Drawing.Point(292, 226);
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
            this.label2.Location = new System.Drawing.Point(228, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "FOV";
            // 
            // fovTrackBar
            // 
            this.fovTrackBar.Location = new System.Drawing.Point(223, 274);
            this.fovTrackBar.Maximum = 250;
            this.fovTrackBar.Name = "fovTrackBar";
            this.fovTrackBar.Size = new System.Drawing.Size(332, 45);
            this.fovTrackBar.TabIndex = 4;
            this.fovTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.fovTrackBar.Value = 100;
            this.fovTrackBar.Scroll += new System.EventHandler(this.FovTrackBar_Scroll);
            // 
            // staminaCheckBox
            // 
            this.staminaCheckBox.AutoSize = true;
            this.staminaCheckBox.Location = new System.Drawing.Point(12, 81);
            this.staminaCheckBox.Name = "staminaCheckBox";
            this.staminaCheckBox.Size = new System.Drawing.Size(98, 17);
            this.staminaCheckBox.TabIndex = 5;
            this.staminaCheckBox.Text = "Infinite Stamina";
            this.staminaCheckBox.UseVisualStyleBackColor = true;
            // 
            // inGameTimer
            // 
            this.inGameTimer.Interval = 110;
            this.inGameTimer.Tick += new System.EventHandler(this.InGameTimer_Tick);
            // 
            // milkCheckBox
            // 
            this.milkCheckBox.AutoSize = true;
            this.milkCheckBox.Location = new System.Drawing.Point(12, 104);
            this.milkCheckBox.Name = "milkCheckBox";
            this.milkCheckBox.Size = new System.Drawing.Size(111, 17);
            this.milkCheckBox.TabIndex = 6;
            this.milkCheckBox.Text = "Infinite Milk Ammo";
            this.milkCheckBox.UseVisualStyleBackColor = true;
            // 
            // squirtGlassesCheckBox
            // 
            this.squirtGlassesCheckBox.AutoSize = true;
            this.squirtGlassesCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.squirtGlassesCheckBox.Location = new System.Drawing.Point(12, 150);
            this.squirtGlassesCheckBox.Name = "squirtGlassesCheckBox";
            this.squirtGlassesCheckBox.Size = new System.Drawing.Size(133, 17);
            this.squirtGlassesCheckBox.TabIndex = 7;
            this.squirtGlassesCheckBox.Text = "Squirt Without Glasses";
            this.squirtGlassesCheckBox.UseVisualStyleBackColor = true;
            this.squirtGlassesCheckBox.CheckedChanged += new System.EventHandler(this.SquirtGlassesCheckBox_CheckedChanged);
            // 
            // itemsApplyButton
            // 
            this.itemsApplyButton.Location = new System.Drawing.Point(472, 226);
            this.itemsApplyButton.Name = "itemsApplyButton";
            this.itemsApplyButton.Size = new System.Drawing.Size(75, 23);
            this.itemsApplyButton.TabIndex = 11;
            this.itemsApplyButton.Text = "Apply";
            this.itemsApplyButton.UseVisualStyleBackColor = true;
            this.itemsApplyButton.Click += new System.EventHandler(this.ItemsApplyButton_Click);
            // 
            // itemsTextBox
            // 
            this.itemsTextBox.Location = new System.Drawing.Point(411, 226);
            this.itemsTextBox.Name = "itemsTextBox";
            this.itemsTextBox.Size = new System.Drawing.Size(55, 20);
            this.itemsTextBox.TabIndex = 10;
            this.itemsTextBox.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(408, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Items in hand";
            // 
            // positionApplyButton
            // 
            this.positionApplyButton.Location = new System.Drawing.Point(472, 176);
            this.positionApplyButton.Name = "positionApplyButton";
            this.positionApplyButton.Size = new System.Drawing.Size(75, 23);
            this.positionApplyButton.TabIndex = 14;
            this.positionApplyButton.Text = "Apply";
            this.positionApplyButton.UseVisualStyleBackColor = true;
            this.positionApplyButton.Click += new System.EventHandler(this.PositionApplyButton_Click);
            // 
            // xPosTextBox
            // 
            this.xPosTextBox.Location = new System.Drawing.Point(296, 176);
            this.xPosTextBox.Name = "xPosTextBox";
            this.xPosTextBox.Size = new System.Drawing.Size(46, 20);
            this.xPosTextBox.TabIndex = 13;
            this.xPosTextBox.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(228, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Position";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(345, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Y";
            // 
            // yPosTextBox
            // 
            this.yPosTextBox.Location = new System.Drawing.Point(357, 176);
            this.yPosTextBox.Name = "yPosTextBox";
            this.yPosTextBox.Size = new System.Drawing.Size(46, 20);
            this.yPosTextBox.TabIndex = 16;
            this.yPosTextBox.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(408, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Z";
            // 
            // zPosTextBox
            // 
            this.zPosTextBox.Location = new System.Drawing.Point(420, 176);
            this.zPosTextBox.Name = "zPosTextBox";
            this.zPosTextBox.Size = new System.Drawing.Size(46, 20);
            this.zPosTextBox.TabIndex = 18;
            this.zPosTextBox.Text = "0";
            // 
            // positionRefreshButton
            // 
            this.positionRefreshButton.Location = new System.Drawing.Point(229, 174);
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
            this.zoomCheckBox.Location = new System.Drawing.Point(12, 173);
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
            this.firstPersonCheckBox.Location = new System.Drawing.Point(12, 197);
            this.firstPersonCheckBox.Name = "firstPersonCheckBox";
            this.firstPersonCheckBox.Size = new System.Drawing.Size(168, 17);
            this.firstPersonCheckBox.TabIndex = 22;
            this.firstPersonCheckBox.Text = "First Person Mode (unfinished)";
            this.firstPersonCheckBox.UseVisualStyleBackColor = true;
            this.firstPersonCheckBox.CheckedChanged += new System.EventHandler(this.FirstPersonCheckBox_CheckedChanged);
            // 
            // outGameTimer
            // 
            this.outGameTimer.Interval = 200;
            this.outGameTimer.Tick += new System.EventHandler(this.OutGameTimer_Tick);
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusText.Location = new System.Drawing.Point(0, 318);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(79, 13);
            this.statusText.TabIndex = 23;
            this.statusText.Text = "Not Connected";
            // 
            // clampCheckBox
            // 
            this.clampCheckBox.AutoSize = true;
            this.clampCheckBox.Location = new System.Drawing.Point(12, 221);
            this.clampCheckBox.Name = "clampCheckBox";
            this.clampCheckBox.Size = new System.Drawing.Size(201, 17);
            this.clampCheckBox.TabIndex = 24;
            this.clampCheckBox.Text = "Un-clamped Pitch (look higher/lower)";
            this.clampCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.jumpTextBox, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.decTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.walkTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.runTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.sprintTextBox, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label12, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.accTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label13, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(223, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.48276F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.51724F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(253, 108);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // jumpTextBox
            // 
            this.jumpTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.jumpTextBox.Location = new System.Drawing.Point(175, 69);
            this.jumpTextBox.Name = "jumpTextBox";
            this.jumpTextBox.Size = new System.Drawing.Size(70, 20);
            this.jumpTextBox.TabIndex = 34;
            this.jumpTextBox.Text = "8.5";
            // 
            // decTextBox
            // 
            this.decTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.decTextBox.Location = new System.Drawing.Point(91, 69);
            this.decTextBox.Name = "decTextBox";
            this.decTextBox.Size = new System.Drawing.Size(70, 20);
            this.decTextBox.TabIndex = 34;
            this.decTextBox.Text = "30";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(171, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 15);
            this.label10.TabIndex = 30;
            this.label10.Text = "Sprint Speed";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(91, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 15);
            this.label9.TabIndex = 29;
            this.label9.Text = "Run Speed";
            // 
            // walkTextBox
            // 
            this.walkTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.walkTextBox.Location = new System.Drawing.Point(7, 18);
            this.walkTextBox.Name = "walkTextBox";
            this.walkTextBox.Size = new System.Drawing.Size(70, 20);
            this.walkTextBox.TabIndex = 26;
            this.walkTextBox.Text = "1.5";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 15);
            this.label8.TabIndex = 26;
            this.label8.Text = "Walk Speed";
            // 
            // runTextBox
            // 
            this.runTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.runTextBox.Location = new System.Drawing.Point(91, 18);
            this.runTextBox.Name = "runTextBox";
            this.runTextBox.Size = new System.Drawing.Size(70, 20);
            this.runTextBox.TabIndex = 27;
            this.runTextBox.Text = "4";
            // 
            // sprintTextBox
            // 
            this.sprintTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.sprintTextBox.Location = new System.Drawing.Point(175, 18);
            this.sprintTextBox.Name = "sprintTextBox";
            this.sprintTextBox.Size = new System.Drawing.Size(70, 20);
            this.sprintTextBox.TabIndex = 28;
            this.sprintTextBox.Text = "7";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(5, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 15);
            this.label11.TabIndex = 31;
            this.label11.Text = "Acceleration";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(87, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 15);
            this.label12.TabIndex = 32;
            this.label12.Text = "Deceleration";
            // 
            // accTextBox
            // 
            this.accTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.accTextBox.Location = new System.Drawing.Point(7, 69);
            this.accTextBox.Name = "accTextBox";
            this.accTextBox.Size = new System.Drawing.Size(70, 20);
            this.accTextBox.TabIndex = 33;
            this.accTextBox.Text = "10";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(174, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 15);
            this.label13.TabIndex = 35;
            this.label13.Text = "Jump Force";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(228, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 15);
            this.label14.TabIndex = 26;
            this.label14.Text = "Movement Stats";
            // 
            // movementApplyButton
            // 
            this.movementApplyButton.Location = new System.Drawing.Point(230, 127);
            this.movementApplyButton.Name = "movementApplyButton";
            this.movementApplyButton.Size = new System.Drawing.Size(238, 23);
            this.movementApplyButton.TabIndex = 27;
            this.movementApplyButton.Text = "Apply All";
            this.movementApplyButton.UseVisualStyleBackColor = true;
            this.movementApplyButton.Click += new System.EventHandler(this.MovementApplyButton_Click);
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(342, 11);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(127, 13);
            this.speedLabel.TabIndex = 28;
            this.speedLabel.Text = "Horizontal Speed = 0 m/s";
            // 
            // controllerCheckBox
            // 
            this.controllerCheckBox.AutoSize = true;
            this.controllerCheckBox.Location = new System.Drawing.Point(12, 244);
            this.controllerCheckBox.Name = "controllerCheckBox";
            this.controllerCheckBox.Size = new System.Drawing.Size(188, 17);
            this.controllerCheckBox.TabIndex = 29;
            this.controllerCheckBox.Text = "Controller Support (requires restart)";
            this.controllerCheckBox.UseVisualStyleBackColor = true;
            this.controllerCheckBox.CheckedChanged += new System.EventHandler(this.ControllerCheckBox_CheckedChanged);
            // 
            // windowedCheckBox
            // 
            this.windowedCheckBox.AutoSize = true;
            this.windowedCheckBox.Location = new System.Drawing.Point(12, 268);
            this.windowedCheckBox.Name = "windowedCheckBox";
            this.windowedCheckBox.Size = new System.Drawing.Size(185, 17);
            this.windowedCheckBox.TabIndex = 30;
            this.windowedCheckBox.Text = "Windowed Mode (requires restart)";
            this.windowedCheckBox.UseVisualStyleBackColor = true;
            this.windowedCheckBox.CheckedChanged += new System.EventHandler(this.WindowedCheckBox_CheckedChanged);
            // 
            // infoButton
            // 
            this.infoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoButton.Location = new System.Drawing.Point(474, 28);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(22, 22);
            this.infoButton.TabIndex = 32;
            this.infoButton.Text = "?";
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // gravityCheckBox
            // 
            this.gravityCheckBox.AutoSize = true;
            this.gravityCheckBox.Location = new System.Drawing.Point(12, 58);
            this.gravityCheckBox.Name = "gravityCheckBox";
            this.gravityCheckBox.Size = new System.Drawing.Size(97, 17);
            this.gravityCheckBox.TabIndex = 33;
            this.gravityCheckBox.Text = "Disable Gravity";
            this.gravityCheckBox.UseVisualStyleBackColor = true;
            this.gravityCheckBox.CheckedChanged += new System.EventHandler(this.GravityCheckBox_CheckedChanged);
            // 
            // noclipCheckBox
            // 
            this.noclipCheckBox.AutoSize = true;
            this.noclipCheckBox.Location = new System.Drawing.Point(12, 12);
            this.noclipCheckBox.Name = "noclipCheckBox";
            this.noclipCheckBox.Size = new System.Drawing.Size(59, 17);
            this.noclipCheckBox.TabIndex = 34;
            this.noclipCheckBox.Text = "No-clip";
            this.noclipCheckBox.UseVisualStyleBackColor = true;
            this.noclipCheckBox.CheckedChanged += new System.EventHandler(this.NoclipCheckBox_CheckedChanged);
            // 
            // squirtDelayCheckBox
            // 
            this.squirtDelayCheckBox.AutoSize = true;
            this.squirtDelayCheckBox.Location = new System.Drawing.Point(12, 127);
            this.squirtDelayCheckBox.Name = "squirtDelayCheckBox";
            this.squirtDelayCheckBox.Size = new System.Drawing.Size(100, 17);
            this.squirtDelayCheckBox.TabIndex = 35;
            this.squirtDelayCheckBox.Text = "No Squirt Delay";
            this.squirtDelayCheckBox.UseVisualStyleBackColor = true;
            this.squirtDelayCheckBox.CheckedChanged += new System.EventHandler(this.SquirtDelayCheckBox_CheckedChanged);
            // 
            // ladderCheckBox
            // 
            this.ladderCheckBox.AutoSize = true;
            this.ladderCheckBox.Location = new System.Drawing.Point(12, 35);
            this.ladderCheckBox.Name = "ladderCheckBox";
            this.ladderCheckBox.Size = new System.Drawing.Size(82, 17);
            this.ladderCheckBox.TabIndex = 36;
            this.ladderCheckBox.Text = "Fast Ladder";
            this.ladderCheckBox.UseVisualStyleBackColor = true;
            this.ladderCheckBox.CheckedChanged += new System.EventHandler(this.LadderCheckBox_CheckedChanged);
            // 
            // builtinCheatsButton
            // 
            this.builtinCheatsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.builtinCheatsButton.Location = new System.Drawing.Point(231, 297);
            this.builtinCheatsButton.Name = "builtinCheatsButton";
            this.builtinCheatsButton.Size = new System.Drawing.Size(82, 22);
            this.builtinCheatsButton.TabIndex = 38;
            this.builtinCheatsButton.Text = "Built-in Cheats";
            this.builtinCheatsButton.UseVisualStyleBackColor = true;
            this.builtinCheatsButton.Click += new System.EventHandler(this.BuiltinCheatsButton_Click);
            // 
            // BarnyardTrainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 331);
            this.Controls.Add(this.builtinCheatsButton);
            this.Controls.Add(this.ladderCheckBox);
            this.Controls.Add(this.squirtDelayCheckBox);
            this.Controls.Add(this.noclipCheckBox);
            this.Controls.Add(this.gravityCheckBox);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.windowedCheckBox);
            this.Controls.Add(this.controllerCheckBox);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.movementApplyButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.clampCheckBox);
            this.Controls.Add(this.statusText);
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
            this.Controls.Add(this.squirtGlassesCheckBox);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Timer inGameTimer;
        private System.Windows.Forms.CheckBox milkCheckBox;
        private System.Windows.Forms.CheckBox squirtGlassesCheckBox;
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
        private System.Windows.Forms.Timer outGameTimer;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.CheckBox clampCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox walkTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox runTextBox;
        private System.Windows.Forms.TextBox sprintTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox accTextBox;
        private System.Windows.Forms.TextBox decTextBox;
        private System.Windows.Forms.TextBox jumpTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button movementApplyButton;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.CheckBox controllerCheckBox;
        private System.Windows.Forms.CheckBox windowedCheckBox;
        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.CheckBox gravityCheckBox;
        private System.Windows.Forms.CheckBox noclipCheckBox;
        private System.Windows.Forms.CheckBox squirtDelayCheckBox;
        private System.Windows.Forms.CheckBox ladderCheckBox;
        private System.Windows.Forms.Button builtinCheatsButton;
    }
}

