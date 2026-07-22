namespace IndeterminateDesignSimPedalManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonSave = new Button();
            buttonConnect = new Button();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblMessage = new ToolStripStatusLabel();
            tabConfigure = new TabControl();
            tabPageConfig = new TabPage();
            settingsClutch = new SettingsUserControl();
            settingsBrake = new SettingsUserControl();
            settingsThrottle = new SettingsUserControl();
            tabPageCalibrate = new TabPage();
            panelWizardContainer = new Panel();
            panelCalibrateEnd = new TableLayoutPanel();
            label7 = new Label();
            label8 = new Label();
            lblThrottleMax = new Label();
            lblClutchMax = new Label();
            lblBrakeMax = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            lblThrottleMin = new Label();
            label9 = new Label();
            lblClutchMin = new Label();
            label10 = new Label();
            lblBrakeMin = new Label();
            label11 = new Label();
            lblBrakeRaw = new Label();
            lblBrakeRawLabel = new Label();
            lblClutchRaw = new Label();
            lblClutchRawLabel = new Label();
            lblThrottleRaw = new Label();
            lblThrottleRawLabel = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            btnFinishCalibration = new Button();
            panelCalibrateInitialPosition = new FlowLayoutPanel();
            lblInitial = new Label();
            btnWizardNext = new Button();
            tabPage1 = new TabPage();
            txtDebug = new TextBox();
            label5 = new Label();
            label6 = new Label();
            numericUpDown3 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            numThrottleMaxDeadzone = new NumericUpDown();
            numThrottleMinDeadzone = new NumericUpDown();
            btnLoadSettingsFromProfile = new Button();
            statusStrip1.SuspendLayout();
            tabConfigure.SuspendLayout();
            tabPageConfig.SuspendLayout();
            tabPageCalibrate.SuspendLayout();
            panelWizardContainer.SuspendLayout();
            panelCalibrateEnd.SuspendLayout();
            panelCalibrateInitialPosition.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numThrottleMaxDeadzone).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numThrottleMinDeadzone).BeginInit();
            SuspendLayout();
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(313, 885);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(171, 47);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "&Save Settings";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(12, 885);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(260, 47);
            buttonConnect.TabIndex = 6;
            buttonConnect.Text = "Connect To Pedals";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonScanAndConnect_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, lblMessage });
            statusStrip1.Location = new Point(0, 938);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 13, 0);
            statusStrip1.Size = new Size(1499, 32);
            statusStrip1.TabIndex = 25;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 25);
            // 
            // lblMessage
            // 
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(179, 25);
            lblMessage.Text = "toolStripStatusLabel1";
            // 
            // tabConfigure
            // 
            tabConfigure.Controls.Add(tabPageConfig);
            tabConfigure.Controls.Add(tabPageCalibrate);
            tabConfigure.Controls.Add(tabPage1);
            tabConfigure.Enabled = false;
            tabConfigure.Location = new Point(0, 12);
            tabConfigure.Name = "tabConfigure";
            tabConfigure.SelectedIndex = 0;
            tabConfigure.Size = new Size(1498, 867);
            tabConfigure.TabIndex = 26;
            // 
            // tabPageConfig
            // 
            tabPageConfig.Controls.Add(settingsClutch);
            tabPageConfig.Controls.Add(settingsBrake);
            tabPageConfig.Controls.Add(settingsThrottle);
            tabPageConfig.Location = new Point(4, 34);
            tabPageConfig.Name = "tabPageConfig";
            tabPageConfig.Padding = new Padding(3);
            tabPageConfig.Size = new Size(1490, 829);
            tabPageConfig.TabIndex = 0;
            tabPageConfig.Text = "Configure";
            tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // settingsClutch
            // 
            settingsClutch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            settingsClutch.Enabled = false;
            settingsClutch.Location = new Point(992, 6);
            settingsClutch.Name = "settingsClutch";
            settingsClutch.Size = new Size(499, 611);
            settingsClutch.TabIndex = 32;
            // 
            // settingsBrake
            // 
            settingsBrake.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            settingsBrake.Enabled = false;
            settingsBrake.Location = new Point(499, 6);
            settingsBrake.Name = "settingsBrake";
            settingsBrake.Size = new Size(499, 611);
            settingsBrake.TabIndex = 31;
            // 
            // settingsThrottle
            // 
            settingsThrottle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            settingsThrottle.Enabled = false;
            settingsThrottle.Location = new Point(8, 6);
            settingsThrottle.Name = "settingsThrottle";
            settingsThrottle.Size = new Size(499, 611);
            settingsThrottle.TabIndex = 30;
            // 
            // tabPageCalibrate
            // 
            tabPageCalibrate.Controls.Add(panelWizardContainer);
            tabPageCalibrate.Location = new Point(4, 34);
            tabPageCalibrate.Name = "tabPageCalibrate";
            tabPageCalibrate.Padding = new Padding(3);
            tabPageCalibrate.Size = new Size(1490, 829);
            tabPageCalibrate.TabIndex = 1;
            tabPageCalibrate.Text = "Calibration";
            tabPageCalibrate.UseVisualStyleBackColor = true;
            tabPageCalibrate.Enter += tabPageCalibrate_Enter;
            // 
            // panelWizardContainer
            // 
            panelWizardContainer.Controls.Add(panelCalibrateEnd);
            panelWizardContainer.Controls.Add(panelCalibrateInitialPosition);
            panelWizardContainer.Location = new Point(0, 6);
            panelWizardContainer.Name = "panelWizardContainer";
            panelWizardContainer.Size = new Size(1014, 633);
            panelWizardContainer.TabIndex = 0;
            // 
            // panelCalibrateEnd
            // 
            panelCalibrateEnd.ColumnCount = 7;
            panelCalibrateEnd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            panelCalibrateEnd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 166F));
            panelCalibrateEnd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            panelCalibrateEnd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 126F));
            panelCalibrateEnd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            panelCalibrateEnd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132F));
            panelCalibrateEnd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            panelCalibrateEnd.Controls.Add(label7, 0, 0);
            panelCalibrateEnd.Controls.Add(label8, 0, 3);
            panelCalibrateEnd.Controls.Add(lblThrottleMax, 6, 1);
            panelCalibrateEnd.Controls.Add(lblClutchMax, 6, 2);
            panelCalibrateEnd.Controls.Add(lblBrakeMax, 6, 4);
            panelCalibrateEnd.Controls.Add(label14, 5, 4);
            panelCalibrateEnd.Controls.Add(label13, 5, 2);
            panelCalibrateEnd.Controls.Add(label12, 5, 1);
            panelCalibrateEnd.Controls.Add(lblThrottleMin, 4, 1);
            panelCalibrateEnd.Controls.Add(label9, 3, 1);
            panelCalibrateEnd.Controls.Add(lblClutchMin, 4, 2);
            panelCalibrateEnd.Controls.Add(label10, 3, 2);
            panelCalibrateEnd.Controls.Add(lblBrakeMin, 4, 4);
            panelCalibrateEnd.Controls.Add(label11, 3, 4);
            panelCalibrateEnd.Controls.Add(lblBrakeRaw, 2, 4);
            panelCalibrateEnd.Controls.Add(lblBrakeRawLabel, 1, 4);
            panelCalibrateEnd.Controls.Add(lblClutchRaw, 2, 2);
            panelCalibrateEnd.Controls.Add(lblClutchRawLabel, 1, 2);
            panelCalibrateEnd.Controls.Add(lblThrottleRaw, 2, 1);
            panelCalibrateEnd.Controls.Add(lblThrottleRawLabel, 1, 1);
            panelCalibrateEnd.Controls.Add(label15, 0, 1);
            panelCalibrateEnd.Controls.Add(label16, 0, 2);
            panelCalibrateEnd.Controls.Add(label17, 0, 4);
            panelCalibrateEnd.Controls.Add(btnFinishCalibration, 6, 5);
            panelCalibrateEnd.Location = new Point(6, 139);
            panelCalibrateEnd.Name = "panelCalibrateEnd";
            panelCalibrateEnd.RowCount = 6;
            panelCalibrateEnd.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            panelCalibrateEnd.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            panelCalibrateEnd.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            panelCalibrateEnd.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            panelCalibrateEnd.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            panelCalibrateEnd.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            panelCalibrateEnd.Size = new Size(980, 475);
            panelCalibrateEnd.TabIndex = 2;
            panelCalibrateEnd.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            panelCalibrateEnd.SetColumnSpan(label7, 7);
            label7.Font = new Font("Segoe UI", 24F);
            label7.Location = new Point(3, 0);
            label7.Name = "label7";
            label7.Size = new Size(961, 65);
            label7.TabIndex = 2;
            label7.Text = "Move Throttle and Clutch through full range";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            panelCalibrateEnd.SetColumnSpan(label8, 7);
            label8.Font = new Font("Segoe UI", 24F);
            label8.Location = new Point(3, 236);
            label8.Name = "label8";
            label8.Size = new Size(968, 130);
            label8.TabIndex = 28;
            label8.Text = "For the Brake press to the max desired brake pressure";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblThrottleMax
            // 
            lblThrottleMax.AutoSize = true;
            lblThrottleMax.Location = new Point(727, 140);
            lblThrottleMax.Name = "lblThrottleMax";
            lblThrottleMax.Size = new Size(26, 25);
            lblThrottleMax.TabIndex = 36;
            lblThrottleMax.Text = "--";
            // 
            // lblClutchMax
            // 
            lblClutchMax.AutoSize = true;
            lblClutchMax.Location = new Point(727, 188);
            lblClutchMax.Name = "lblClutchMax";
            lblClutchMax.Size = new Size(26, 25);
            lblClutchMax.TabIndex = 36;
            lblClutchMax.Text = "--";
            // 
            // lblBrakeMax
            // 
            lblBrakeMax.AutoSize = true;
            lblBrakeMax.Location = new Point(727, 376);
            lblBrakeMax.Name = "lblBrakeMax";
            lblBrakeMax.Size = new Size(26, 25);
            lblBrakeMax.TabIndex = 39;
            lblBrakeMax.Text = "--";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(595, 376);
            label14.Name = "label14";
            label14.Size = new Size(120, 25);
            label14.TabIndex = 34;
            label14.Text = "Max Pressure:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(595, 188);
            label13.Name = "label13";
            label13.Size = new Size(114, 25);
            label13.TabIndex = 33;
            label13.Text = "End Position:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(595, 140);
            label12.Name = "label12";
            label12.Size = new Size(114, 25);
            label12.TabIndex = 32;
            label12.Text = "End Position:";
            // 
            // lblThrottleMin
            // 
            lblThrottleMin.AutoSize = true;
            lblThrottleMin.Location = new Point(510, 140);
            lblThrottleMin.Name = "lblThrottleMin";
            lblThrottleMin.Size = new Size(26, 25);
            lblThrottleMin.TabIndex = 35;
            lblThrottleMin.Text = "--";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(384, 140);
            label9.Name = "label9";
            label9.Size = new Size(120, 25);
            label9.TabIndex = 29;
            label9.Text = "Start Position:";
            // 
            // lblClutchMin
            // 
            lblClutchMin.AutoSize = true;
            lblClutchMin.Location = new Point(510, 188);
            lblClutchMin.Name = "lblClutchMin";
            lblClutchMin.Size = new Size(26, 25);
            lblClutchMin.TabIndex = 37;
            lblClutchMin.Text = "--";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(384, 188);
            label10.Name = "label10";
            label10.Size = new Size(120, 25);
            label10.TabIndex = 30;
            label10.Text = "Start Position:";
            // 
            // lblBrakeMin
            // 
            lblBrakeMin.AutoSize = true;
            lblBrakeMin.Location = new Point(510, 376);
            lblBrakeMin.Name = "lblBrakeMin";
            lblBrakeMin.Size = new Size(26, 25);
            lblBrakeMin.TabIndex = 38;
            lblBrakeMin.Text = "--";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(384, 376);
            label11.Name = "label11";
            label11.Size = new Size(99, 25);
            label11.TabIndex = 31;
            label11.Text = "Start Value:";
            // 
            // lblBrakeRaw
            // 
            lblBrakeRaw.AutoSize = true;
            lblBrakeRaw.Location = new Point(299, 376);
            lblBrakeRaw.Name = "lblBrakeRaw";
            lblBrakeRaw.Size = new Size(26, 25);
            lblBrakeRaw.TabIndex = 23;
            lblBrakeRaw.Text = "--";
            // 
            // lblBrakeRawLabel
            // 
            lblBrakeRawLabel.AutoSize = true;
            lblBrakeRawLabel.Location = new Point(133, 376);
            lblBrakeRawLabel.Name = "lblBrakeRawLabel";
            lblBrakeRawLabel.Size = new Size(159, 25);
            lblBrakeRawLabel.TabIndex = 24;
            lblBrakeRawLabel.Text = "Current Raw Value:";
            // 
            // lblClutchRaw
            // 
            lblClutchRaw.AutoSize = true;
            lblClutchRaw.Location = new Point(299, 188);
            lblClutchRaw.Name = "lblClutchRaw";
            lblClutchRaw.Size = new Size(26, 25);
            lblClutchRaw.TabIndex = 26;
            lblClutchRaw.Text = "--";
            // 
            // lblClutchRawLabel
            // 
            lblClutchRawLabel.AutoSize = true;
            lblClutchRawLabel.Location = new Point(133, 188);
            lblClutchRawLabel.Name = "lblClutchRawLabel";
            lblClutchRawLabel.Size = new Size(159, 25);
            lblClutchRawLabel.TabIndex = 27;
            lblClutchRawLabel.Text = "Current Raw Value:";
            // 
            // lblThrottleRaw
            // 
            lblThrottleRaw.AutoSize = true;
            lblThrottleRaw.Location = new Point(299, 140);
            lblThrottleRaw.Name = "lblThrottleRaw";
            lblThrottleRaw.Size = new Size(26, 25);
            lblThrottleRaw.TabIndex = 20;
            lblThrottleRaw.Text = "--";
            // 
            // lblThrottleRawLabel
            // 
            lblThrottleRawLabel.AutoSize = true;
            lblThrottleRawLabel.Location = new Point(133, 140);
            lblThrottleRawLabel.Name = "lblThrottleRawLabel";
            lblThrottleRawLabel.Size = new Size(159, 25);
            lblThrottleRawLabel.TabIndex = 21;
            lblThrottleRawLabel.Text = "Current Raw Value:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(3, 140);
            label15.Name = "label15";
            label15.Size = new Size(123, 38);
            label15.TabIndex = 40;
            label15.Text = "Throttle";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(3, 188);
            label16.Name = "label16";
            label16.Size = new Size(100, 38);
            label16.TabIndex = 41;
            label16.Text = "Clutch";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(3, 376);
            label17.Name = "label17";
            label17.Size = new Size(92, 38);
            label17.TabIndex = 42;
            label17.Text = "Brake";
            // 
            // btnFinishCalibration
            // 
            btnFinishCalibration.Location = new Point(727, 427);
            btnFinishCalibration.Name = "btnFinishCalibration";
            btnFinishCalibration.Size = new Size(112, 34);
            btnFinishCalibration.TabIndex = 43;
            btnFinishCalibration.Text = "Finish Calibration";
            btnFinishCalibration.UseVisualStyleBackColor = true;
            btnFinishCalibration.Click += btnFinishCalibration_Click;
            // 
            // panelCalibrateInitialPosition
            // 
            panelCalibrateInitialPosition.Controls.Add(lblInitial);
            panelCalibrateInitialPosition.Controls.Add(btnWizardNext);
            panelCalibrateInitialPosition.Location = new Point(8, 17);
            panelCalibrateInitialPosition.Name = "panelCalibrateInitialPosition";
            panelCalibrateInitialPosition.Size = new Size(890, 116);
            panelCalibrateInitialPosition.TabIndex = 0;
            // 
            // lblInitial
            // 
            lblInitial.AutoSize = true;
            lblInitial.Font = new Font("Segoe UI", 24F);
            lblInitial.Location = new Point(3, 0);
            lblInitial.Name = "lblInitial";
            lblInitial.Size = new Size(878, 65);
            lblInitial.TabIndex = 0;
            lblInitial.Text = "Remove Feet from Pedals and Click Next";
            lblInitial.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnWizardNext
            // 
            btnWizardNext.Location = new Point(3, 68);
            btnWizardNext.Name = "btnWizardNext";
            btnWizardNext.Size = new Size(112, 34);
            btnWizardNext.TabIndex = 1;
            btnWizardNext.Text = "Next >";
            btnWizardNext.UseVisualStyleBackColor = true;
            btnWizardNext.Click += btnWizardNext_Click;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtDebug);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1490, 829);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Debug";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtDebug
            // 
            txtDebug.Location = new Point(8, 31);
            txtDebug.Multiline = true;
            txtDebug.Name = "txtDebug";
            txtDebug.ReadOnly = true;
            txtDebug.ScrollBars = ScrollBars.Both;
            txtDebug.Size = new Size(1452, 747);
            txtDebug.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(122, 448);
            label5.Name = "label5";
            label5.Size = new Size(128, 25);
            label5.TabIndex = 30;
            label5.Text = "Top DeadZone";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(94, 409);
            label6.Name = "label6";
            label6.Size = new Size(159, 25);
            label6.TabIndex = 29;
            label6.Text = "Bottom DeadZone";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(249, 446);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(86, 31);
            numericUpDown3.TabIndex = 28;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(248, 407);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(86, 31);
            numericUpDown4.TabIndex = 27;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(105, 443);
            label3.Name = "label3";
            label3.Size = new Size(128, 25);
            label3.TabIndex = 26;
            label3.Text = "Top DeadZone";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(77, 404);
            label4.Name = "label4";
            label4.Size = new Size(159, 25);
            label4.TabIndex = 25;
            label4.Text = "Bottom DeadZone";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(233, 441);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(86, 31);
            numericUpDown1.TabIndex = 24;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(232, 402);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(86, 31);
            numericUpDown2.TabIndex = 23;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(62, 445);
            label2.Name = "label2";
            label2.Size = new Size(128, 25);
            label2.TabIndex = 22;
            label2.Text = "Top DeadZone";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 406);
            label1.Name = "label1";
            label1.Size = new Size(159, 25);
            label1.TabIndex = 21;
            label1.Text = "Bottom DeadZone";
            // 
            // numThrottleMaxDeadzone
            // 
            numThrottleMaxDeadzone.Location = new Point(189, 443);
            numThrottleMaxDeadzone.Name = "numThrottleMaxDeadzone";
            numThrottleMaxDeadzone.Size = new Size(86, 31);
            numThrottleMaxDeadzone.TabIndex = 20;
            // 
            // numThrottleMinDeadzone
            // 
            numThrottleMinDeadzone.Location = new Point(188, 404);
            numThrottleMinDeadzone.Name = "numThrottleMinDeadzone";
            numThrottleMinDeadzone.Size = new Size(86, 31);
            numThrottleMinDeadzone.TabIndex = 19;
            // 
            // btnLoadSettingsFromProfile
            // 
            btnLoadSettingsFromProfile.Location = new Point(508, 887);
            btnLoadSettingsFromProfile.Name = "btnLoadSettingsFromProfile";
            btnLoadSettingsFromProfile.Size = new Size(200, 45);
            btnLoadSettingsFromProfile.TabIndex = 27;
            btnLoadSettingsFromProfile.Text = "&Load Profile";
            btnLoadSettingsFromProfile.UseVisualStyleBackColor = true;
            btnLoadSettingsFromProfile.Click += btnLoadSettingsFromProfile_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1499, 970);
            Controls.Add(btnLoadSettingsFromProfile);
            Controls.Add(tabConfigure);
            Controls.Add(statusStrip1);
            Controls.Add(buttonConnect);
            Controls.Add(buttonSave);
            Name = "Form1";
            Text = "Indeterminate Design Sim Pedal Manager";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabConfigure.ResumeLayout(false);
            tabPageConfig.ResumeLayout(false);
            tabPageCalibrate.ResumeLayout(false);
            panelWizardContainer.ResumeLayout(false);
            panelCalibrateEnd.ResumeLayout(false);
            panelCalibrateEnd.PerformLayout();
            panelCalibrateInitialPosition.ResumeLayout(false);
            panelCalibrateInitialPosition.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numThrottleMaxDeadzone).EndInit();
            ((System.ComponentModel.ISupportInitialize)numThrottleMinDeadzone).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSave;
        private Button buttonConnect;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private TabControl tabConfigure;
        private TabPage tabPageConfig;
        private TabPage tabPageCalibrate;
          private Label label5;
        private Label label6;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
         private Label label3;
        private Label label4;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Panel panelWizardContainer;
        private FlowLayoutPanel panelCalibrateInitialPosition;
        private Button btnWizardNext;
        private Label lblInitial;
          private Label label2;
        private Label label1;
        private NumericUpDown numThrottleMaxDeadzone;
        private NumericUpDown numThrottleMinDeadzone;
        private Label lblThrottleRawLabel;
        private Label lblThrottleRaw;
        private Label lblBrakeRawLabel;
        private Label lblBrakeRaw;
        private Label lblClutchRaw;
        private Label label8;
        private TableLayoutPanel panelCalibrateEnd;
        private Label label7;
        private Label label9;
        private Label lblClutchRawLabel;
        private Label label10;
        private Label label11;
        private Label lblBrakeMax;
        private Label lblBrakeMin;
        private Label lblClutchMax;
        private Label lblClutchMin;
        private Label lblThrottleMax;
        private Label lblThrottleMin;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Button btnFinishCalibration;
        private SettingsUserControl settingsThrottle;
        private TabPage tabPage1;
        private TextBox txtDebug;
        private ToolStripStatusLabel lblMessage;
        private SettingsUserControl settingsBrake;
        private SettingsUserControl settingsClutch;
        private Button btnLoadSettingsFromProfile;
    }
}
