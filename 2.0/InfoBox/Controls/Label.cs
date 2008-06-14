// <copyright file="Label.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Glass label</summary>

namespace InfoBox.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Glass label
    /// </summary>
    [Category("Glass Components")]
    [DefaultProperty("Text")]
    [Description("Label with glass look and feel")]
    [ToolboxBitmap(typeof(System.Windows.Forms.Label))]
    public partial class Label : Panel
    {
        #region Attributes

        /// <summary>
        /// Fore color used for the disabled state
        /// </summary>
        private Color disabledForeColor = Color.Gray;

        /// <summary>
        /// Text alignment
        /// </summary>
        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        public Label()
        {
            this.InitializeComponent();
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
            get
            {
                return this.disabledForeColor;
            }

            set
            {
                this.disabledForeColor = value;
                this.RefreshLabelColor();
            }
        }

        /// <summary>
        /// Gets or sets the alignment of the text
        /// </summary>
        /// <value>The text align.</value>
        [Category("Appearance"), Description("Defines the alignment of the text")]
        public ContentAlignment TextAlign
        {
            get
            {
                return this.textAlign;
            }

            set
            {
                this.textAlign = value;
                this.labelText.TextAlign = this.textAlign;
                this.labelText.Invalidate();
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
            get
            {
                return this.labelText.Text;
            }

            set
            {
                this.labelText.Text = value;
                this.labelText.Invalidate();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the text forecolor
        /// </summary>
        private void RefreshLabelColor()
        {
            this.labelText.ForeColor = Enabled ? ForeColor : this.disabledForeColor;
            this.labelText.Invalidate();
        }

        #endregion Methods

        #region Event handlers

        /// <summary>
        /// When forecolor is changed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnNewForeColor(object sender, EventArgs e)
        {
            this.RefreshLabelColor();
        }

        /// <summary>
        /// When the 'Enabled' property is changed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnEnabledChanged(object sender, EventArgs e)
        {
            this.RefreshLabelColor();
            this.Invalidate();
        }

        #endregion Event handlers

        #region Event Copy

        /// <summary>
        /// Handles the MouseDown event of the labelText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void LabelText_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        /// <summary>
        /// Handles the MouseMove event of the labelText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void LabelText_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        /// <summary>
        /// Handles the MouseUp event of the labelText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void LabelText_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        #endregion Event Copy
    }
}