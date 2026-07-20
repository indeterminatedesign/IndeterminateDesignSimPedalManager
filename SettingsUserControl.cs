using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace IndeterminateDesignSimPedalManager
{
    public partial class SettingsUserControl : UserControl
    {
        private PedalData _pedalData;
        private string _curveName;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long CurrentPedalValue
        {
            get => pedalCurveControl1.CurrentPedalValue;
            set
            {
                pedalCurveControl1.CurrentPedalValue = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PedalData PedalData
        {
            get => _pedalData;
            set
            {
                pedalCurveControl1.PedalData = value;
                pedalCurveControl1.Enabled = true;
                _pedalData = value;
                numericUpDown20Percent.Value = value.inputToOutputRatios[1];
                numericUpDown40Percent.Value = value.inputToOutputRatios[3];
                numericUpDown60Percent.Value = value.inputToOutputRatios[5];
                numericUpDown80Percent.Value = value.inputToOutputRatios[7];
                this.Enabled = true;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurveName
        {
            get => CurveName;
            set
            {
                _curveName = value;
                pedalCurveControl1.CurveName = value;
                Invalidate();
            }
        }

        public SettingsUserControl()
        {
            InitializeComponent();
        }

        private void numericUpDown20Percent_ValueChanged(object sender, EventArgs e)
        {
            _pedalData.inputToOutputRatios[1] = (short)numericUpDown20Percent.Value;
            pedalCurveControl1.Invalidate();
        }

        private void numericUpDown40Percent_ValueChanged(object sender, EventArgs e)
        {

            _pedalData.inputToOutputRatios[3] = (short)numericUpDown40Percent.Value;
            pedalCurveControl1.Invalidate();
        }

        private void numericUpDown60Percent_ValueChanged(object sender, EventArgs e)
        {
            _pedalData.inputToOutputRatios[5] = (short)numericUpDown60Percent.Value;
            pedalCurveControl1.Invalidate();
        }

        private void numericUpDown80Percent_ValueChanged(object sender, EventArgs e)
        {
            _pedalData.inputToOutputRatios[7] = (short)numericUpDown80Percent.Value;
            pedalCurveControl1.Invalidate();
        }

        private void SettingsUserControl_Load(object sender, EventArgs e)
        {

        }
    }
}
