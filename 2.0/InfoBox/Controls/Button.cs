// <copyright file="Button.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Glass button</summary>

namespace InfoBox.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Glass button
    /// </summary>
    [Category("Glass Components")]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("Button with glass look and feel")]
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    public partial class Button : Panel
    {
        #region Attributes

        #region Button state

        /// <summary>
        /// Flag for the pushed state
        /// </summary>
        private bool pushed;

        /// <summary>
        /// Flag for the hover state
        /// </summary>
        private bool hover;

        /// <summary>
        /// Flag for the pushed persistant mode
        /// </summary>
        private bool persistantMode;

        #endregion Button state

        /// <summary>
        /// Value of the alpha channel
        /// </summary>
        private int alphaChannelCoeff = 0;

        /// <summary>
        /// Text alignment
        /// </summary>
        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

        /// <summary>
        /// Fore color used for the disabled state
        /// </summary>
        private Color disabledForeColor = Color.Gray;

        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
        }

        #endregion Constructor

        #region Events

        /// <summary>
        /// Event raised when button is clicked
        /// </summary>
        public new event EventHandler<EventArgs> Click;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the text color when the button is disabled
        /// </summary>
        [Category("Appearance"), Description("Defines the text color when the button is disabled")]
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
                this.buttonText.TextAlign = this.textAlign;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the button text
        /// </summary>
        [Category("Appearance"), Description("Defines the text of the button"), Browsable(true)]
        public override string Text
        {
            get
            {
                return this.buttonText.Text;
            }

            set
            {
                this.buttonText.Text = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the button remains clicked after mouse button is released.
        /// </summary>
        /// <value><c>true</c> if the button remains clicked after mouse button is released; otherwise, <c>false</c>.</value>
        [Category("Behavior"), Description("Defines if the button remains clicked after mouse button is released"), DefaultValue("false")]
        public bool PersistantMode
        {
            get
            {
                return this.persistantMode;
            }

            set
            {
                this.persistantMode = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Button"/> is pushed.
        /// </summary>
        /// <value><c>true</c> if pushed; otherwise, <c>false</c>.</value>
        [Category("Behavior"), Description("Defines if button appears as pushed"), Browsable(true)]
        public bool Pushed
        {
            get
            {
                return this.pushed;
            }

            set
            {
                // Do nothing if not in persistant mode
                if (!this.persistantMode)
                {
                    return;
                }

                this.pushed = value;
                this.Invalidate();
            }
        }

        #endregion Properties

        #region Event handlers

        /// <summary>
        /// When button is clicked
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        #region Background

        /// <summary>
        /// Paints the background
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            int tailleZoneSuperieure = (Height - 3) / 2;

            if (this.pushed)
            {
                PaintingEngine.PaintPushedEffect(pevent.Graphics, Width, Height);
            }
            else if (Enabled && (this.hover || this.timerFade.Enabled))
            {
                PaintingEngine.PaintHoverEffect(
                    pevent.Graphics,
                    Color.FromArgb(12 * this.alphaChannelCoeff, Color.Gainsboro),
                    Color.FromArgb(12 * this.alphaChannelCoeff, Color.Black),
                    Color.FromArgb(10 * this.alphaChannelCoeff, Color.White),
                    Color.FromArgb(5 * this.alphaChannelCoeff, Color.Beige),
                    this.Width,
                    this.Height);
            }
        }

        #endregion Background

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

        /// <summary>
        /// When mouse enters the button
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnTextEnter(object sender, EventArgs e)
        {
            if (!Enabled)
            {
                return;
            }

            if (this.pushed && this.persistantMode)
            {
                return;
            }

            this.hover = true;
            this.timerFade.Start();
            this.Invalidate();

            this.OnEnter(e);
        }

        /// <summary>
        /// When mouse leaves the button
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnTextLeave(object sender, EventArgs e)
        {
            if (!Enabled)
            {
                return;
            }

            if (this.pushed && this.persistantMode)
            {
                return;
            }

            this.hover = false;
            this.timerFade.Start();
            this.Invalidate();

            this.OnLeave(e);
        }

        /// <summary>
        /// When user clicks on the button
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void OnTextDown(object sender, MouseEventArgs e)
        {
            this.timerFade.Stop();

            if (!Enabled)
            {
                return;
            }

            if (this.pushed && this.persistantMode)
            {
                this.pushed = false;
            }
            else
            {
                this.pushed = true;
            }

            this.Invalidate();
        }

        /// <summary>
        /// When user release the mouse button
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void OnTextUp(object sender, MouseEventArgs e)
        {
            if (!Enabled)
            {
                return;
            }

            if (this.persistantMode)
            {
                return;
            }

            this.pushed = false;
            this.hover = true;
            this.Invalidate();

            // Raises event
            this.OnClick(new EventArgs());
        }

        #region Timer Tick (for fading effect)

        /// <summary>
        /// Called when [fade tick].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnFadeTick(object sender, EventArgs e)
        {
            if (this.hover)
            {
                this.alphaChannelCoeff += 2;

                if (this.alphaChannelCoeff >= 10)
                {
                    this.alphaChannelCoeff = 10;
                    this.timerFade.Stop();
                }
            }
            else if (!this.pushed)
            {
                this.alphaChannelCoeff -= 2;

                if (this.alphaChannelCoeff <= 0)
                {
                    this.alphaChannelCoeff = 0;
                    this.timerFade.Stop();
                }
            }

            this.Invalidate();
        }

        #endregion Timer Tick (for fading effect)

        #endregion Event handlers

        #region Methods

        /// <summary>
        /// Sets the text forecolor
        /// </summary>
        private void RefreshLabelColor()
        {
            this.buttonText.ForeColor = Enabled ? ForeColor : this.disabledForeColor;
            Invalidate();
        }

        #endregion Methods
    }
}