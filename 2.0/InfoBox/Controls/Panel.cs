using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InfoBox.Controls
{
    /// <summary>
    /// Panel is a panel with a glass look and feel
    /// </summary>
    [ToolboxBitmap(typeof (System.Windows.Forms.Panel))]
    public class Panel : System.Windows.Forms.Panel
    {
        #region Attributes

        private SideBorder sideBorder;
        private Color sideBorderBottomColor = Color.Transparent;
        private Color sideBorderTopColor = Color.White;
        private int sideBorderWidth = 1;

        #endregion Attributes

        #region Properties

        /// <summary>
        /// Get or sets if a custom border is shown on the sides of the control
        /// </summary>
        /// <value>The side border.</value>
        [Category("Side Border"), Description("Defines if a special side border should be displayed"),
         DefaultValue("None")]
        public SideBorder SideBorder
        {
            get { return sideBorder; }
            set
            {
                sideBorder = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border width
        /// </summary>
        /// <value>The width of the side border.</value>
        [Category("Side Border"), Description("Defines the width of the side border")]
        public int SideBorderWidth
        {
            get { return sideBorderWidth; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("The border width must be positive");

                sideBorderWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the top border color
        /// </summary>
        /// <value>The top color of the side border.</value>
        [Category("Side Border"), Description("Defines the top color of the side border")]
        public Color SideBorderTopColor
        {
            get { return sideBorderTopColor; }
            set
            {
                sideBorderTopColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the bottom border color
        /// </summary>
        /// <value>The bottom color of the side border.</value>
        [Category("Side Border"), Description("Defines the bottom color of the side border")]
        public Color SideBorderBottomColor
        {
            get { return sideBorderBottomColor; }
            set
            {
                sideBorderBottomColor = value;
                Invalidate();
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;
        }

        #endregion Constructor

        #region Event Handlers

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            PaintingEngine.PaintGlassEffect(pevent.Graphics, BackColor, Width, Height);
            PaintingEngine.PaintGradientBorders(pevent.Graphics, sideBorderTopColor, sideBorderBottomColor, Width,
                                                Height, sideBorderWidth, sideBorder);
        }

        #endregion Event Handlers
    }
}