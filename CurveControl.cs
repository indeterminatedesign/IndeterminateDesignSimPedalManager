using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace IndeterminateDesignSimPedalManager
{

    public class PedalCurveControl : Control
    {
        private PedalData _pedalData;
        private long _currentPedalValue;
        private string _curveName;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long CurrentPedalValue
        {
            get => _currentPedalValue;
            set
            {
                _currentPedalValue = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PedalData PedalData
        {
            get => _pedalData;
            set
            {
                _pedalData = value;
                Invalidate();   // Trigger repaint
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurveName
        {
            get => CurveName;
            set
            {
                _curveName = value;
                Invalidate();
            }
        }

        public PedalCurveControl()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!Enabled)
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Background
            g.Clear(Color.Black);

            if (Width < 100 || Height < 100)
                return;

            int margin = 65;

            int graphWidth = Width - margin * 2;
            int graphHeight = Height - margin * 2;


            Rectangle graphArea = new Rectangle(
                margin,
                margin,
                graphWidth,
                graphHeight);

            //Draw Title
            using (Pen gridPen = new Pen(Color.DimGray, 1))
            using (Brush textBrush = new SolidBrush(Color.White))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                g.DrawString(
                _curveName,
                Font,
                textBrush,
                Width / 2,
                graphHeight,
                sf);
            }


            //
            // Draw Grid + Labels
            //
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

                    g.DrawLine(
                        gridPen,
                        x,
                        margin,
                        x,
                        margin + graphHeight);


                    // Horizontal grid line
                    float y = margin + graphHeight - fraction * graphHeight;

                    g.DrawLine(
                        gridPen,
                        margin,
                        y,
                        margin + graphWidth,
                        y);


                    // X-axis labels
                    g.DrawString(
                        percent.ToString(),
                        Font,
                        textBrush,
                        x,
                        margin + graphHeight + 18,
                        sf);


                    // Y-axis labels
                    g.DrawString(
                        percent.ToString(),
                        Font,
                        textBrush,
                        margin - 25,
                        y,
                        sf);
                }
            }


            //
            // Draw border
            //
            using (Pen axisPen = new Pen(Color.White, 2))
            {
                g.DrawRectangle(axisPen, graphArea);
            }


            //
            // Draw 1:1 reference line
            //
            using (Pen referencePen = new Pen(Color.Gray, 1))
            {
                g.DrawLine(
                    referencePen,
                    margin,
                    margin + graphHeight,
                    margin + graphWidth,
                    margin);
            }


            //
            // Validate curve data
            //
            if (_pedalData.inputToOutputRatios == null ||
                _pedalData.inputToOutputRatios.Length < 8)
            {
                DrawAxisTitles(g);
                return;
            }


            //
            // Convert curve points to screen coordinates
            //
            PointF[] points = new PointF[4];

            for (int i = 0; i < 4; i++)
            {
                float input =
                    _pedalData.inputToOutputRatios[i * 2];

                float output =
                    _pedalData.inputToOutputRatios[i * 2 + 1];


                float x =
                    margin +
                    (input / 100.0f) * graphWidth;


                float y =
                    margin +
                    graphHeight -
                    (output / 100.0f) * graphHeight;


                points[i] = new PointF(x, y);
            }


            //
            // Add endpoints
            //
            PointF[] curvePoints = new PointF[6];

            curvePoints[0] = new PointF(
                margin,
                margin + graphHeight);


            Array.Copy(
                points,
                0,
                curvePoints,
                1,
                points.Length);


            curvePoints[5] = new PointF(
                margin + graphWidth,
                margin);



            //
            // Draw curve
            //
            using (Pen curvePen = new Pen(Color.Lime, 3))
            {
                g.DrawLines(
                    curvePen,
                    curvePoints);
            }


            //
            // Draw curve points
            //
            using (Brush pointBrush = new SolidBrush(Color.Red))
            {
                foreach (PointF p in curvePoints)
                {
                    g.FillEllipse(
                        pointBrush,
                        p.X - 5,
                        p.Y - 5,
                        10,
                        10);
                }
            }


            //
            // Draw current pedal position
            //
            if (_pedalData.range > 0 &&
                _currentPedalValue != 0)
            {
                float pedalPosition =
                    (_currentPedalValue - _pedalData.startValue) /
                    (float)_pedalData.range;


                pedalPosition = Math.Clamp(
                    pedalPosition,
                    0,
                    1);


                float currentX =
                    margin +
                    pedalPosition * graphWidth;


                using (Pen currentPen = new Pen(Color.Yellow, 2))
                {
                    g.DrawLine(
                        currentPen,
                        currentX,
                        margin,
                        currentX,
                        margin + graphHeight);
                }
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

                    g.DrawString(
                        "Input %",
                        Font,
                        textBrush,
                        Width / 2,
                        Height - 35,
                        sf);
                }


                // Y-axis title
                g.TranslateTransform(
                    6,
                    Height / 2 + 30);

                g.RotateTransform(-90);


                g.DrawString(
                    "Output %",
                    Font,
                    textBrush,
                    0,
                    0);


                g.ResetTransform();
            }
        }

    }
}