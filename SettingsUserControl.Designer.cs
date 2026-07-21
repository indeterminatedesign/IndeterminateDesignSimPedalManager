namespace IndeterminateDesignSimPedalManager
{
    partial class SettingsUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            label1 = new Label();
            numericMaxDeadzone = new NumericUpDown();
            numericMinDeadzone = new NumericUpDown();
            lblThrottleRaw = new Label();
            numericUpDown80Percent = new NumericUpDown();
            numericUpDown60Percent = new NumericUpDown();
            numericUpDown40Percent = new NumericUpDown();
            numericUpDown20Percent = new NumericUpDown();
            pedalCurveControl1 = new PedalCurveControl();
            ((System.ComponentModel.ISupportInitialize)numericMaxDeadzone).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMinDeadzone).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown80Percent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown60Percent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown40Percent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown20Percent).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(130, 557);
            label2.Name = "label2";
            label2.Size = new Size(128, 25);
            label2.TabIndex = 29;
            label2.Text = "Top DeadZone";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(101, 518);
            label1.Name = "label1";
            label1.Size = new Size(159, 25);
            label1.TabIndex = 28;
            label1.Text = "Bottom DeadZone";
            // 
            // numericMaxDeadzone
            // 
            numericMaxDeadzone.Location = new Point(277, 555);
            numericMaxDeadzone.Name = "numericMaxDeadzone";
            numericMaxDeadzone.Size = new Size(86, 31);
            numericMaxDeadzone.TabIndex = 27;
            numericMaxDeadzone.ValueChanged += numThrottleMaxDeadzone_ValueChanged;
            // 
            // numericMinDeadzone
            // 
            numericMinDeadzone.Location = new Point(276, 516);
            numericMinDeadzone.Name = "numericMinDeadzone";
            numericMinDeadzone.Size = new Size(86, 31);
            numericMinDeadzone.TabIndex = 26;
            numericMinDeadzone.ValueChanged += numThrottleMinDeadzone_ValueChanged;
            // 
            // lblThrottleRaw
            // 
            lblThrottleRaw.AutoSize = true;
            lblThrottleRaw.Location = new Point(311, 182);
            lblThrottleRaw.Name = "lblThrottleRaw";
            lblThrottleRaw.Size = new Size(0, 25);
            lblThrottleRaw.TabIndex = 24;
            // 
            // numericUpDown80Percent
            // 
            numericUpDown80Percent.Location = new Point(306, 469);
            numericUpDown80Percent.Name = "numericUpDown80Percent";
            numericUpDown80Percent.Size = new Size(54, 31);
            numericUpDown80Percent.TabIndex = 39;
            numericUpDown80Percent.ValueChanged += numericUpDown80Percent_ValueChanged;
            // 
            // numericUpDown60Percent
            // 
            numericUpDown60Percent.Location = new Point(246, 469);
            numericUpDown60Percent.Name = "numericUpDown60Percent";
            numericUpDown60Percent.Size = new Size(54, 31);
            numericUpDown60Percent.TabIndex = 38;
            numericUpDown60Percent.ValueChanged += numericUpDown60Percent_ValueChanged;
            // 
            // numericUpDown40Percent
            // 
            numericUpDown40Percent.Location = new Point(181, 469);
            numericUpDown40Percent.Name = "numericUpDown40Percent";
            numericUpDown40Percent.Size = new Size(54, 31);
            numericUpDown40Percent.TabIndex = 37;
            numericUpDown40Percent.ValueChanged += numericUpDown40Percent_ValueChanged;
            // 
            // numericUpDown20Percent
            // 
            numericUpDown20Percent.Location = new Point(116, 469);
            numericUpDown20Percent.Name = "numericUpDown20Percent";
            numericUpDown20Percent.Size = new Size(54, 31);
            numericUpDown20Percent.TabIndex = 36;
            numericUpDown20Percent.ValueChanged += numericUpDown20Percent_ValueChanged;
            // 
            // pedalCurveControl1
            // 
            pedalCurveControl1.Enabled = false;
            pedalCurveControl1.Location = new Point(13, 13);
            pedalCurveControl1.Name = "pedalCurveControl1";
            pedalCurveControl1.Size = new Size(450, 450);
            pedalCurveControl1.TabIndex = 35;
            pedalCurveControl1.Text = "pedalCurveControl1";
            // 
            // SettingsUserControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(numericUpDown80Percent);
            Controls.Add(numericUpDown60Percent);
            Controls.Add(numericUpDown40Percent);
            Controls.Add(numericUpDown20Percent);
            Controls.Add(pedalCurveControl1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numericMaxDeadzone);
            Controls.Add(numericMinDeadzone);
            Controls.Add(lblThrottleRaw);
            Name = "SettingsUserControl";
            Size = new Size(500, 600);
            Load += SettingsUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)numericMaxDeadzone).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMinDeadzone).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown80Percent).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown60Percent).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown40Percent).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown20Percent).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label1;
        private NumericUpDown numericMaxDeadzone;
        private NumericUpDown numericMinDeadzone;
        private Label lblThrottleRaw;
        private NumericUpDown numericUpDown80Percent;
        private NumericUpDown numericUpDown60Percent;
        private NumericUpDown numericUpDown40Percent;
        private NumericUpDown numericUpDown20Percent;
        private PedalCurveControl pedalCurveControl1;
    }
}
