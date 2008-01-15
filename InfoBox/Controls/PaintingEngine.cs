using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfoBox.Controls
{
    internal class PaintingEngine
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private PaintingEngine() { }

        /// <summary>
        /// Paints a glass effect on pGraphics using the pWidth width, the pHeight height and the pColor color
        /// </summary>
        /// <param name="pGraphics">Graphics to paint into</param>
        /// <param name="pColor">Base color of the glass effect</param>
        /// <param name="pWidth">Width of the effect</param>
        /// <param name="pHeight">Height of the effect</param>
        internal static void PaintGlassEffect(Graphics pGraphics, Color pColor, int pWidth, int pHeight)
        {
            // Fill the background
            SolidBrush BackBrush = new SolidBrush(Color.Gainsboro);
            pGraphics.FillRectangle(BackBrush, new Rectangle(0, 0, pWidth, pHeight));

            int TopZoneHeight = (pHeight - 3) / 2;

            // Create brushes
            LinearGradientBrush loBrushTop = new LinearGradientBrush(new RectangleF(0, 0, pWidth, TopZoneHeight),
                                                                     Color.FromArgb(50, pColor),
                                                                     Color.FromArgb(160, pColor),
                                                                     LinearGradientMode.Vertical);

            LinearGradientBrush loBrushMiddle = new LinearGradientBrush(new RectangleF(0, 0, pWidth, pHeight - 3 - TopZoneHeight),
                                                                        Color.FromArgb(190, pColor),
                                                                        Color.FromArgb(210, pColor),
                                                                        LinearGradientMode.Vertical);

            LinearGradientBrush loBrushBottom = new LinearGradientBrush(new RectangleF(0, 0, pWidth, 3),
                                                                        Color.FromArgb(210, pColor),
                                                                        Color.FromArgb(50, pColor),
                                                                        LinearGradientMode.Vertical);

            // Fill zones
            pGraphics.FillRectangle(loBrushTop, new Rectangle(0, 0, pWidth, TopZoneHeight));
            pGraphics.FillRectangle(loBrushMiddle, new Rectangle(0, TopZoneHeight, pWidth, pHeight - 3 - TopZoneHeight));
            pGraphics.FillRectangle(loBrushBottom, new Rectangle(0, pHeight - 3, pWidth, 3));

            pGraphics.DrawLine(new Pen(Color.DimGray), new Point(0, pHeight - 1), new Point(pWidth, pHeight - 1));
        }

        /// <summary>
        /// Paints borders on sides on selected sides of the zone delimited by pWidth and pHeight
        /// </summary>
        /// <param name="pGraphics">Graphics to paint into</param>
        /// <param name="pTopColor">Top color of the border</param>
        /// <param name="pBottomColor">Bottom color of the border</param>
        /// <param name="pWidth">Width of the control</param>
        /// <param name="pHeight">Height of the control</param>
        /// <param name="pBorderWidth">Width of the border</param>
        /// <param name="pSides">Represents on which sides are the borders</param>
        internal static void PaintGradientBorders(Graphics pGraphics, Color pTopColor, Color pBottomColor, int pWidth, int pHeight, int pBorderWidth, SideBorder pSides)
        {
            if (pSides != SideBorder.None)
            {
                LinearGradientBrush loBrushSide = new LinearGradientBrush(new Rectangle(0, 0, pBorderWidth, pHeight),
                                                                          pTopColor,
                                                                          pBottomColor,
                                                                          LinearGradientMode.Vertical);

                if (pSides != SideBorder.Right)
                    pGraphics.FillRectangle(loBrushSide, new Rectangle(0, 0, pBorderWidth, pHeight));
                if (pSides != SideBorder.Left)
                    pGraphics.FillRectangle(loBrushSide, new Rectangle(pWidth - pBorderWidth, 0, pBorderWidth, pHeight));
            }

        }

        /// <summary>
        /// Paints an hover effect according the provided position ans style
        /// </summary>
        /// <param name="pGraphics">Graphics to paint into</param>
        /// <param name="pOuterColor">Outer color of the effect</param>
        /// <param name="pMiddleColor">Middle color of the effect</param>
        /// <param name="pInnerColor">Inner color of the effect</param>
        /// <param name="pLightColor">Color of the light effect</param>
        /// <param name="pWidth">Width of the control (for centering purpose)</param>
        /// <param name="pHeight">Height of the control (for centering purpose)</param>
        internal static void PaintHoverEffect(Graphics pGraphics,
                                              Color pOuterColor,
                                              Color pMiddleColor,
                                              Color pInnerColor,
                                              Color pLightColor,
                                              int pWidth,
                                              int pHeight)
        {
            // Light effect
            int TopZoneHeight = (pHeight - 3) / 2;
            Rectangle LightEffectRectangle;

            Pen OuterBorderPen = new Pen(new SolidBrush(pOuterColor));
            Pen MiddleBorderPen = new Pen(new SolidBrush(pMiddleColor));
            Pen InnerBorderPen = new Pen(new SolidBrush(pInnerColor));

            GraphicsPath OuterBorderPath = new GraphicsPath();
            GraphicsPath MiddleBorderPath = new GraphicsPath();
            GraphicsPath InnerBorderPath = new GraphicsPath();

            OuterBorderPath.AddLine(new Point(3, pHeight), new Point(3, 5));
            OuterBorderPath.AddCurve(new Point[] { new Point(3, 5), new Point(5, 3) }, 0.5f);
            OuterBorderPath.AddLine(new Point(5, 3), new Point(pWidth - 6, 3));
            OuterBorderPath.AddCurve(new Point[] { new Point(pWidth - 6, 3), new Point(pWidth - 4, 5) }, 0.5f);
            OuterBorderPath.AddLine(new Point(pWidth - 4, 5), new Point(pWidth - 4, pHeight));

            MiddleBorderPath.AddLine(new Point(4, pHeight), new Point(4, 5));
            MiddleBorderPath.AddLine(new Point(5, 4), new Point(pWidth - 6, 4));
            MiddleBorderPath.AddLine(new Point(pWidth - 5, 5), new Point(pWidth - 5, pHeight));

            InnerBorderPath.AddLines(new Point[] { new Point(5, pHeight), new Point(5, 5), new Point(pWidth - 6, 5), new Point(pWidth - 6, pHeight) });

            LightEffectRectangle = new Rectangle(new Point(6, 6), new Size(pWidth - 12, TopZoneHeight - 6));

            pGraphics.DrawPath(OuterBorderPen, OuterBorderPath);
            pGraphics.DrawPath(MiddleBorderPen, MiddleBorderPath);
            pGraphics.DrawPath(InnerBorderPen, InnerBorderPath);

            SolidBrush HoverEffectButton = new SolidBrush(pLightColor);
            pGraphics.FillRectangle(HoverEffectButton, LightEffectRectangle);
        }

        /// <summary>
        /// Paints an pushed effect according the provided position ans style
        /// </summary>
        /// <param name="pGraphics">Graphics to paint into</param>
        /// <param name="pWidth">Width of the control (for centering purpose)</param>
        /// <param name="pHeight">Height of the control (for centering purpose)</param>
        internal static void PaintPushedEffect(Graphics pGraphics,
                                               int pWidth,
                                               int pHeight)
        {
            Pen OuterBorderPen = new Pen(new SolidBrush(Color.FromArgb(120, Color.Gainsboro)));
            GraphicsPath OuterBorderPath = new GraphicsPath();
            OuterBorderPath.AddLine(new Point(3, pHeight), new Point(3, 5));
            OuterBorderPath.AddCurve(new Point[] { new Point(3, 5), new Point(5, 3) }, 0.5f);
            OuterBorderPath.AddLine(new Point(5, 3), new Point(pWidth - 6, 3));
            OuterBorderPath.AddCurve(new Point[] { new Point(pWidth - 6, 3), new Point(pWidth - 4, 5) }, 0.5f);
            OuterBorderPath.AddLine(new Point(pWidth - 4, 5), new Point(pWidth - 4, pHeight));

            Pen InnerBorderPen = new Pen(new SolidBrush(Color.FromArgb(120, Color.Black)));
            GraphicsPath InnerBorderPath = new GraphicsPath();
            InnerBorderPath.AddLine(new Point(4, pHeight), new Point(4, 5));
            InnerBorderPath.AddLine(new Point(5, 4), new Point(pWidth - 6, 4));
            InnerBorderPath.AddLine(new Point(pWidth - 5, 5), new Point(pWidth - 5, pHeight));

            Pen OuterBorderButtonPen = new Pen(new SolidBrush(Color.FromArgb(70, Color.Black)));
            GraphicsPath OuterButtonBorderPath = new GraphicsPath();
            OuterButtonBorderPath.AddLines(new Point[] { new Point(5, pHeight),
                                                         new Point(5, 5),
                                                         new Point(pWidth - 6, 5),
                                                         new Point(pWidth - 6, pHeight) });

            pGraphics.DrawPath(OuterBorderPen, OuterBorderPath);
            pGraphics.DrawPath(InnerBorderPen, InnerBorderPath);
            pGraphics.DrawPath(OuterBorderButtonPen, OuterButtonBorderPath);
        }
    }
}
