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
        private short[] _inputToOutputRatios = new short[12];
        private string _curveName;
        private bool _isLoading = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public short CurrentPedalPercent
        {
            get => pedalCurveControl1.CurrentPedalPercent;
            set
            {
                pedalCurveControl1.CurrentPedalPercent = value;

            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public short[] InputToOutputRatios
        {
            get => _inputToOutputRatios;
            set
            {
                _isLoading = true; // 1. Block NumericUpDown ValueChanged handlers
                try
                {
                    pedalCurveControl1.InputToOutputRatios = value;
                    pedalCurveControl1.Enabled = true;
                    _inputToOutputRatios = value;
                    numericMinDeadzone.Value = _inputToOutputRatios[0];
                    numericUpDown20Percent.Value = _inputToOutputRatios[3];
                    numericUpDown40Percent.Value = _inputToOutputRatios[5];
                    numericUpDown60Percent.Value = _inputToOutputRatios[7];
                    numericUpDown80Percent.Value = _inputToOutputRatios[9];
                    numericMaxDeadzone.Value = 100 - _inputToOutputRatios[10];  //Because top dead zone is a percentage of the whole range
                    this.Enabled = true;
                }
                finally
                {
                    _isLoading = false; // 2. Re-enable user interaction
                }
                // Force a clean repaint now that everything is populated
                Invalidate();
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurveName
        {
            get => _curveName;
            set
            {
                _curveName = value;
                pedalCurveControl1.CurveName = value;
                Invalidate();
            }
        }

        private short LowerDeadZone
        {
            get => (_inputToOutputRatios != null && _inputToOutputRatios.Length > 1)
                ? _inputToOutputRatios[0]
                : Convert.ToInt16(0);
            set
            {
                _inputToOutputRatios[0] = value;
            }
        }

        private short UpperDeadZone
        {
            get => (_inputToOutputRatios != null && _inputToOutputRatios.Length > 1)
                ? _inputToOutputRatios[10]
                : Convert.ToInt16(0);
            set
            {

                _inputToOutputRatios[10] = value;
            }
        }
        public SettingsUserControl()
        {
            InitializeComponent();
            // Check both paths as the out-of-process designer can sometimes bypass the legacy DesignMode flag
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime
                || this.DesignMode)
            {
                return; // Skip execution if inside Visual Studio
            }

        }

        private void numericUpDown20Percent_ValueChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _inputToOutputRatios[3] = Convert.ToInt16(numericUpDown20Percent.Value);
                pedalCurveControl1.Invalidate();
            }
        }

        private void numericUpDown40Percent_ValueChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _inputToOutputRatios[5] = Convert.ToInt16(numericUpDown40Percent.Value);
                pedalCurveControl1.Invalidate();
            }
        }

        private void numericUpDown60Percent_ValueChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _inputToOutputRatios[7] = Convert.ToInt16(numericUpDown60Percent.Value);
                pedalCurveControl1.Invalidate();
            }
        }

        private void numericUpDown80Percent_ValueChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _inputToOutputRatios[9] = Convert.ToInt16(numericUpDown80Percent.Value);
                pedalCurveControl1.Invalidate();
            }
        }

        private void SettingsUserControl_Load(object sender, EventArgs e)
        {

        }

        private void numThrottleMinDeadzone_ValueChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                LowerDeadZone = Convert.ToInt16(numericMinDeadzone.Value);
                pedalCurveControl1.Invalidate();

            }

        }

        private void numThrottleMaxDeadzone_ValueChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                UpperDeadZone = Convert.ToInt16(100 - numericMaxDeadzone.Value);
                pedalCurveControl1.Invalidate();

            }
        }
    }
}
