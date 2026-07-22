using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace IndeterminateDesignSimPedalManager
{
    public class PedalCurveControl : Control
    {
        private short[] _inputToOutputRatios = new short[12];
        private short _currentPedalPercent;
        private string _curveName = "Default Curve"; // Added default string to prevent null draws

        private short LowerDeadZone
        {
            get => (_inputToOutputRatios != null && _inputToOutputRatios.Length > 1)
                ? _inputToOutputRatios[0]
                : (short)0;
            set
            {
                _inputToOutputRatios[0] = value;
            }
        }

        private short UpperDeadZone
        {
            get => (_inputToOutputRatios != null && _inputToOutputRatios.Length > 1)
                ? _inputToOutputRatios[10]
                : (short)0;
            set
            {

                _inputToOutputRatios[10] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public short CurrentPedalPercent
        {
            get => _currentPedalPercent;
            set
            {
                _currentPedalPercent = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public short[] InputToOutputRatios
        {
            get => _inputToOutputRatios;
            set
            {
                _inputToOutputRatios = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurveName
        {
            get => _curveName;
            set { _curveName = value; Invalidate(); }
        }

        public PedalCurveControl()
        {
            // Control styles must be set for both DesignTime and Runtime to render properly
            DoubleBuffered = true;
            ResizeRedraw = true;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.DesignMode)
            {
                return; // Guard complex runtime logic here if you add any later
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!Enabled) return;
            if (_inputToOutputRatios == null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Background 
            g.Clear(Color.Black);

            if (Width < 100 || Height < 100) return;

            int margin = 65;
            int graphWidth = Width - margin * 2;
            int graphHeight = Height - margin * 2;
            Rectangle graphArea = new Rectangle(margin, margin, graphWidth, graphHeight);

            // Draw Title 
            using (Brush textBrush = new SolidBrush(Color.White))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                g.DrawString(_curveName ?? "Pedal Curve", Font, textBrush, Width / 2, margin / 2, sf);
            }

            if (_inputToOutputRatios == null || _inputToOutputRatios.Length < 12)
            {
                DrawAxisTitles(g);
                return;
            }


            using (System.Drawing.Drawing2D.HatchBrush deadzoneBrush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, Color.FromArgb(180, Color.Red), Color.Transparent))
            {
                // Lower Deadzone: Shaded from 0 to Point 1 X
                int lowerWidth = Convert.ToInt32(graphWidth * (LowerDeadZone / 100.0f));
                Rectangle lowerRect = new Rectangle(margin, margin, lowerWidth, graphHeight);

                // Upper Deadzone: Shaded from Point 4 X up to 100%
                int upperDeadZoneWidth = Convert.ToInt32(graphWidth * ((100 - UpperDeadZone) / 100.0f));
                Rectangle upperRect = new Rectangle((margin + graphWidth) - upperDeadZoneWidth, margin, upperDeadZoneWidth, graphHeight);

                g.FillRectangle(deadzoneBrush, lowerRect);
                g.FillRectangle(deadzoneBrush, upperRect);
            }

            // Draw Grid + Labels 
            using (Pen gridPen = new Pen(Color.DimGray, 1))
            using (Brush textBrush = new SolidBrush(Color.White))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                for (int percent = 0; percent <= 100; percent += 20)
                {
                    float fraction = percent / 100.0f;
                    // Vertical grid line 
                    float x = margin + fraction * graphWidth;
                    g.DrawLine(gridPen, x, margin, x, margin + graphHeight);

                    // Horizontal grid line 
                    float y = margin + graphHeight - fraction * graphHeight;
                    g.DrawLine(gridPen, margin, y, margin + graphWidth, y);

                    // X-axis labels 
                    g.DrawString(percent.ToString(), Font, textBrush, x, margin + graphHeight + 18, sf);

                    // Y-axis labels 
                    g.DrawString(percent.ToString(), Font, textBrush, margin - 25, y, sf);
                }
            }

            // Draw border 
            using (Pen axisPen = new Pen(Color.White, 2))
            {
                g.DrawRectangle(axisPen, graphArea);
            }

            // Draw 1:1 reference line 
            using (Pen referencePen = new Pen(Color.Gray, 1))
            {
                g.DrawLine(referencePen, margin, margin + graphHeight, margin + graphWidth, margin);
            }


            //Draw the actual cruve
            int totalPoints = _inputToOutputRatios.Length / 2; // 12 elements / 2 variables each = 6 points
            PointF[] curvePoints = new PointF[totalPoints + 2];

            //Add a point at 0,0 and 100,100 to show the flat line of the deadzones
            curvePoints[0] = (new PointF(margin, margin + graphHeight));
            curvePoints[totalPoints + 1] = (new PointF(margin + graphWidth, margin));

            for (int i = 0; i < totalPoints; i++)
            {
                float input = _inputToOutputRatios[i * 2];       // X value
                float output = _inputToOutputRatios[i * 2 + 1];  // Y value

                float x = margin + (input / 100.0f) * graphWidth;
                float y = margin + graphHeight - (output / 100.0f) * graphHeight;
                curvePoints[i + 1] = new PointF(x, y);
            }



            // Draw curve line string paths
            using (Pen curvePen = new Pen(Color.Lime, 3))
            {
                g.DrawLines(curvePen, curvePoints);
            }

            // Draw curve structural point handles
            using (Brush pointBrush = new SolidBrush(Color.Red))
            {
                foreach (PointF p in curvePoints)
                {
                    g.FillEllipse(pointBrush, p.X - 5, p.Y - 5, 10, 10);
                }
            }

            if(CurrentPedalPercent > 0)
            {
                int i = 0;
            }
            //Draw the current Pedal Position
            using (Pen currentPedal = new Pen(Color.Yellow, 3))
            {
                float verticalPosition = margin + graphWidth *(CurrentPedalPercent / 100.0f);
                PointF start = new PointF(verticalPosition, margin);
                PointF end = new PointF(verticalPosition, margin + graphHeight);
                g.DrawLine(currentPedal, start, end);
            }

            DrawAxisTitles(g);
        }


        private void DrawAxisTitles(Graphics g)
        {
            using (Brush textBrush = new SolidBrush(Color.White))
            {
                // X-axis title 
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    g.DrawString("Input %", Font, textBrush, Width / 2, Height - 35, sf);
                }

                // Y-axis title 
                g.TranslateTransform(6, Height / 2 + 30);
                g.RotateTransform(-90);
                g.DrawString("Output %", Font, textBrush, 0, 0);
                g.ResetTransform();
            }
        }
    }
}
