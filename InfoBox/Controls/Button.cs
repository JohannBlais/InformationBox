using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InfoBox.Controls
{
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
        #region Events

        /// <summary>
        /// Event raised when button is clicked
        /// </summary>
        public new event EventHandler<EventArgs> Click;

        #endregion Events

        #region Attributes

        #region Button state

        private bool pushed;
        private bool hover;
        private bool persistantMode;

        #endregion Button state

        private int alphaChannelCoeff = 0;
        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;
        private Color disabledForeColor = Color.Gray;

        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Button()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the text color when the button is disabled
        /// </summary>
        [Category("Appearance"), Description("Defines the text color when the button is disabled")]
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
        [Category("Appearance"), Description("Defines the alignment of the text")]
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;
                buttonText.TextAlign = textAlign;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the button text
        /// </summary>
        [Category("Appearance"), Description("Defines the text of the button"), Browsable(true)]
        public override string Text
        {
            get { return buttonText.Text; }
            set
            {
                buttonText.Text = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets if the button remains clicked after mouse button is released
        /// </summary>
        [Category("Behavior"), Description("Defines if the button remains clicked after mouse button is released"), DefaultValue("false")]
        public bool PersistantMode
        {
            get { return persistantMode; }
            set {
                persistantMode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets if button is pushed (only if persistant mode)
        /// </summary>
        [Category("Behavior"), Description("Defines if button appears as pushed"), Browsable(true)]
        public bool Pushed
        {
            get { return pushed; }
            set
            {
                // Do nothing if not in persistant mode
                if (!persistantMode) return;

                pushed = value;
                Invalidate();
            }
        }

        #endregion Properties

        #region Methods
        /// <summary>
        /// Sets the text forecolor
        /// </summary>
        private void RefreshLabelColor()
        {
            buttonText.ForeColor = Enabled ? ForeColor : disabledForeColor;
            Invalidate();
        }

        #endregion Methods

        #region Event handlers

        #region Background

        /// <summary>
        /// Paints the background
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            int tailleZoneSuperieure = (Height - 3) / 2;

            if (pushed)
            {
                PaintingEngine.PaintPushedEffect(pevent.Graphics, Width, Height);
            }
            else if (Enabled && (hover || timerFade.Enabled))
            {
                PaintingEngine.PaintHoverEffect(pevent.Graphics,
                                                Color.FromArgb(12 * alphaChannelCoeff, Color.Gainsboro),
                                                Color.FromArgb(12 * alphaChannelCoeff, Color.Black),
                                                Color.FromArgb(10 * alphaChannelCoeff, Color.White),
                                                Color.FromArgb(5 * alphaChannelCoeff, Color.Beige),
                                                Width,
                                                Height);
            }
        }

        #endregion Background

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

        /// <summary>
        /// When button is clicked
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        /// <summary>
        /// When mouse enters the button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnTextEnter(object sender, EventArgs e)
        {
            if (!Enabled)
                return;

            if (pushed && persistantMode)
                return;

            hover = true;
            timerFade.Start();
            Invalidate();

            base.OnEnter(e);
        }

        /// <summary>
        /// When mouse leaves the button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnTextLeave(object sender, EventArgs e)
        {
            if (!Enabled)
                return;

            if (pushed && persistantMode)
                return;

            hover = false;
            timerFade.Start();
            Invalidate();

            base.OnLeave(e);
        }

        /// <summary>
        /// When user clicks on the button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnTextDown(object sender, MouseEventArgs e)
        {
            timerFade.Stop();

            if (!Enabled)
                return;

            if (pushed && persistantMode)
            {
                pushed = false;
            }
            else
            {
                pushed = true;
            }

            Invalidate();
        }

        /// <summary>
        /// When user release the mouse button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnTextUp(object sender, MouseEventArgs e)
        {
            if (!Enabled)
                return;

            if (persistantMode)
                return;

            pushed = false;
            hover = true;
            Invalidate();

            // Raises event
            OnClick(new EventArgs());
        }

        #region Timer Tick (for fading effect)

        private void OnFadeTick(object sender, EventArgs e)
        {
            if (hover)
            {
                alphaChannelCoeff += 2;

                if (alphaChannelCoeff >= 10)
                {
                    alphaChannelCoeff = 10;
                    timerFade.Stop();
                }
            }
            else if (!pushed)
            {
                alphaChannelCoeff -= 2;

                if (alphaChannelCoeff <= 0)
                {
                    alphaChannelCoeff = 0;
                    timerFade.Stop();
                }
            }

            Invalidate();
        }

        #endregion Timer Tick (for fading effect)

        #endregion Event handlers
    }
}