using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InfoBox.Controls
{
    /// <summary>
    /// Glass label
    /// </summary>
    [Category("Glass Components")]
    [DefaultProperty("Text")]
    [Description("Label with glass look and feel")]
    [ToolboxBitmap(typeof (System.Windows.Forms.Label))]
    public partial class Label : Panel
    {
        #region Attributes

        private Color disabledForeColor = Color.Gray;
        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        public Label()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the text color when the label is disabled
        /// </summary>
        /// <value>The color of the disabled fore.</value>
        [Category("Appearance"), Description("Defines the text color when the label is disabled")]
        public Color DisabledForeColor
        {
            get { return disabledForeColor; }
            set
            {
                disabledForeColor = value;
                RefreshLabelColor();
            }
        }

        /// <summary>
        /// Gets or sets the alignment of the text
        /// </summary>
        /// <value>The text align.</value>
        [Category("Appearance"), Description("Defines the alignment of the text")]
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;
                labelText.TextAlign = textAlign;
                labelText.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the label text
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.String"></see>.</returns>
        [Category("Appearance"), Description("Defines the text of the label"), Browsable(true)]
        public override string Text
        {
            get { return labelText.Text; }
            set
            {
                labelText.Text = value;
                labelText.Invalidate();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the text forecolor
        /// </summary>
        private void RefreshLabelColor()
        {
            labelText.ForeColor = Enabled ? ForeColor : disabledForeColor;
            labelText.Invalidate();
        }

        #endregion Methods

        #region Event handlers

        /// <summary>
        /// When forecolor is changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnNewForeColor(object sender, EventArgs e)
        {
            RefreshLabelColor();
        }

        /// <summary>
        /// When the 'Enabled' property is changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnEnabledChanged(object sender, EventArgs e)
        {
            RefreshLabelColor();
            Invalidate();
        }

        #endregion Event handlers

        #region Event Copy

        /// <summary>
        /// Handles the MouseDown event of the labelText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void labelText_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        /// <summary>
        /// Handles the MouseMove event of the labelText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void labelText_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        /// <summary>
        /// Handles the MouseUp event of the labelText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void labelText_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        #endregion Event Copy
    }
}