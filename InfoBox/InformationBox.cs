namespace InfoBox
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    public partial class InformationBox : Form
    {
        #region Consts

        private const int ICON_PANEL_WIDTH = 68;
        private const int BUTTONS_MIN_SPACE = 10;
        private const int BORDER_PADDING = 10;
        private const int WINDOW_DECORATION_HEIGHT = 30;

        #endregion Consts

        #region Attributes

        private readonly InformationBoxIcon _icon = InformationBoxIcon.None;
        private InformationBoxResult _result = InformationBoxResult.None;
        private readonly InformationBoxButtons _buttons = InformationBoxButtons.OK;
        private readonly InformationBoxDefaultButton _defaultButton = InformationBoxDefaultButton.Button1;

        private readonly string _buttonUser1Text = "User1";
        private readonly string _buttonUser2Text = "User2";

        private readonly Button _buttonAbort = null;
        private readonly Button _buttonOk = null;
        private readonly Button _buttonYes = null;
        private readonly Button _buttonRetry = null;
        private readonly Button _buttonNo = null;
        private readonly Button _buttonCancel = null;
        private readonly Button _buttonIgnore = null;
        private readonly Button _buttonUser1 = null;
        private readonly Button _buttonUser2 = null;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        private InformationBox(string text)
        {
            InitializeComponent();

            // Apply default font for message boxes
            Font = SystemFonts.MessageBoxFont;
            lblText.Font = SystemFonts.MessageBoxFont;

            lblText.Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        private InformationBox(string text, string caption) : this(text)
        {
            Text = caption;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons)
            : this(text, caption)
        {
            _buttons = buttons;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons, string button1Text)
            : this(text, caption)
        {
            _buttons = buttons;
            _buttonUser1Text = button1Text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="button2Text">The button2 text.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text)
            : this(text, caption)
        {
            _buttons = buttons;
            _buttonUser1Text = button1Text;
            _buttonUser2Text = button2Text;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons, InformationBoxIcon icon)
            : this(text, caption, buttons)
        {
            _icon = icon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="icon">The icon.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons, string button1Text, InformationBoxIcon icon)
            : this(text, caption, buttons, button1Text)
        {
            _icon = icon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="button2Text">The button2 text.</param>
        /// <param name="icon">The icon.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, InformationBoxIcon icon)
            : this(text, caption, buttons, button1Text, button2Text)
        {
            _icon = icon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
            : this(text, caption, buttons, icon)
        {
            _defaultButton = defaultButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons, string button1Text, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
            : this(text, caption, buttons, button1Text, icon)
        {
            _defaultButton = defaultButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="button2Text">The button2 text.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        private InformationBox(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
            : this(text, caption, buttons, button1Text, button2Text, icon)
        {
            _defaultButton = defaultButton;
        }

        #endregion Constructors

        #region Show

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text)
        {
            InformationBox box = new InformationBox(text);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption)
        {
            InformationBox box = new InformationBox(text, caption);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons)
        {
            InformationBox box = new InformationBox(text, caption, buttons);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text)
        {
            InformationBox box = new InformationBox(text, caption, buttons, button1Text);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="button2Text">The button2 text.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text)
        {
            InformationBox box = new InformationBox(text, caption, buttons, button1Text, button2Text);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, InformationBoxIcon icon)
        {
            InformationBox box = new InformationBox(text, caption, buttons, icon);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, InformationBoxIcon icon)
        {
            InformationBox box = new InformationBox(text, caption, buttons, button1Text, icon);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="button2Text">The button2 text.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, InformationBoxIcon icon)
        {
            InformationBox box = new InformationBox(text, caption, buttons, button1Text, button2Text, icon);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBox box = new InformationBox(text, caption, buttons, icon, defaultButton);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBox box = new InformationBox(text, caption, buttons, button1Text, icon, defaultButton);
            return Show(box);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="button2Text">The button2 text.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBox box = new InformationBox(text, caption, buttons, button1Text, button2Text, icon, defaultButton);
            return Show(box);
        }

        #endregion Show

        #region Box initialization

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns></returns>
        private static InformationBoxResult Show(InformationBox box)
        {
            box.SetButtons();
            box.SetText();
            box.SetIcon();
            box.SetLayout();
            box.SetFocus();
            box.ShowDialog();

            return box._result;
        }

        #region Focus

        /// <summary>
        /// Sets the focus.
        /// </summary>
        private void SetFocus()
        {
            if (_defaultButton == InformationBoxDefaultButton.Button1 && pnlButtons.Controls.Count > 0)
            {
                pnlButtons.Controls[0].Select();
            }

            if (_defaultButton == InformationBoxDefaultButton.Button2 && pnlButtons.Controls.Count > 1)
            {
                pnlButtons.Controls[1].Select();
            }

            if (_defaultButton == InformationBoxDefaultButton.Button3 && pnlButtons.Controls.Count > 2)
            {
                pnlButtons.Controls[2].Select();
            }
        }

        #endregion Focus

        #region Layout

        /// <summary>
        /// Sets the layout.
        /// </summary>
        private void SetLayout()
        {
            int totalHeight = 200;
            int totalWidth = 0;

            #region Width

            // Caption width including button
            int captionWidth = Convert.ToInt32(CreateGraphics().MeasureString(Text, SystemFonts.CaptionFont).Width) + 30;

            int iconAndTextWidth = 0;
            
            // Minimum width to display all needed buttons.
            int buttonsMinWidth = pnlButtons.Controls.Count * BUTTONS_MIN_SPACE;
            foreach (Control ctrl in pnlButtons.Controls)
            {
                buttonsMinWidth += ctrl.Width;
            }

            // Icon width
            if (_icon != InformationBoxIcon.None)
            {
                iconAndTextWidth += ICON_PANEL_WIDTH;
            }

            // Text width
            iconAndTextWidth += lblText.Width + BORDER_PADDING * 2;

            // Gets the maximum size
            totalWidth = Math.Max(Math.Max(buttonsMinWidth, iconAndTextWidth), captionWidth);

            #endregion Width

            #region Height

            int iconHeight = 0;
            if (_icon != InformationBoxIcon.None)
                iconHeight = pcbIcon.Height;

            int textHeight = lblText.Height;

            totalHeight = Math.Max(iconHeight, textHeight) + BORDER_PADDING * 2 + pnlButtons.Height + WINDOW_DECORATION_HEIGHT;

            #endregion Height

            // Sets the size;
            Width = totalWidth;
            Height = totalHeight;

            #region Position

            // Set new position for all components
            // Icon
            pcbIcon.Left = BORDER_PADDING;
            pcbIcon.Top = BORDER_PADDING;

            // Text
            lblText.Left = (_icon != InformationBoxIcon.None) ? ICON_PANEL_WIDTH : BORDER_PADDING;
            lblText.Top = Convert.ToInt32((pnlText.Height - lblText.Height) / 2);

            // Buttons
            int buttonsCount = pnlButtons.Controls.Count;
            int index = 0;
            int spaceBetween = Convert.ToInt32((ClientSize.Width - buttonsCount * pnlButtons.Controls[0].Width) / (buttonsCount + 1));
            foreach (Control ctrl in pnlButtons.Controls)
            {
                ctrl.Left = spaceBetween * (index + 1) + ctrl.Width * index;
                ++index;
            }

            #endregion Position
        }

        #endregion Layout

        #region Icon

        /// <summary>
        /// Sets the icon.
        /// </summary>
        private void SetIcon()
        {
            if (_icon == InformationBoxIcon.None)
            {
                pnlIcon.Visible = false;
                pcbIcon.Image = null;
            }
            else
            {
                pnlIcon.Visible = true;
                pcbIcon.Image = IconHelper.FromEnum(_icon).ToBitmap();
            }

            pnlIcon.Width = ICON_PANEL_WIDTH;
        }

        #endregion Icon

        #region Text

        /// <summary>
        /// Sets the text.
        /// </summary>
        private void SetText()
        {
            Graphics grph = CreateGraphics();
            SizeF textSize = grph.MeasureString(lblText.Text, lblText.Font);
            lblText.Size = textSize.ToSize();
        }

        #endregion Text

        #region Buttons

        /// <summary>
        /// Sets the buttons, order of addition is respected.
        /// </summary>
        private void SetButtons()
        {
            // Abort button
            if (_buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                AddButton(_buttonAbort, "Abort");
            }

            // Ok
            if (_buttons == InformationBoxButtons.OK ||
                _buttons == InformationBoxButtons.OKCancel ||
                _buttons == InformationBoxButtons.OKCancelUser1)
            {
                AddButton(_buttonOk, "OK");
            }

            // Yes
            if (_buttons == InformationBoxButtons.YesNo ||
                _buttons == InformationBoxButtons.YesNoCancel ||
                _buttons == InformationBoxButtons.YesNoUser1)
            {
                AddButton(_buttonYes, "Yes");
            }

            // Retry
            if (_buttons == InformationBoxButtons.AbortRetryIgnore ||
                _buttons == InformationBoxButtons.RetryCancel)
            {
                AddButton(_buttonRetry, "Retry");
            }

            // No
            if (_buttons == InformationBoxButtons.YesNo ||
                _buttons == InformationBoxButtons.YesNoCancel ||
                _buttons == InformationBoxButtons.YesNoUser1)
            {
                AddButton(_buttonNo, "No");
            }

            // Cancel
            if (_buttons == InformationBoxButtons.OKCancel ||
                _buttons == InformationBoxButtons.OKCancelUser1 ||
                _buttons == InformationBoxButtons.RetryCancel ||
                _buttons == InformationBoxButtons.YesNoCancel)
            {
                AddButton(_buttonCancel, "Cancel");
            }

            // Ignore
            if (_buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                AddButton(_buttonIgnore, "Ignore");
            }
            
            // User1
            if (_buttons == InformationBoxButtons.OKCancelUser1 ||
                _buttons == InformationBoxButtons.User1User2 ||
                _buttons == InformationBoxButtons.YesNoUser1)
            {
                AddButton(_buttonUser1, "User1", _buttonUser1Text);
            }

            // User2
            if (_buttons == InformationBoxButtons.User1User2)
            {
                AddButton(_buttonUser2, "User2", _buttonUser2Text);
            }

            SetButtonsSize();
        }

        /// <summary>
        /// Sets the buttons size.
        /// </summary>
        private void SetButtonsSize()
        {
            // All button will have the same size
            int maxSize = 0;

            // Measures the width of each button
            Graphics grph = CreateGraphics();
            foreach (Control ctrl in pnlButtons.Controls)
            {
                maxSize = Math.Max(Convert.ToInt32(grph.MeasureString(ctrl.Text, ctrl.Font).Width + 40), maxSize);
            }

            foreach (Control ctrl in pnlButtons.Controls)
            {
                ctrl.Size = new Size(maxSize, 23);
                ctrl.Top = 5;
            }
        }

        /// <summary>
        /// Adds the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="name">The name.</param>
        private void AddButton(Button button, string name)
        {
            button = new Button();
            button.FlatStyle = FlatStyle.System;
            button.UseVisualStyleBackColor = true;
            button.Font = SystemFonts.MessageBoxFont;
            button.Name = name;
            button.Text = name;
            button.Click += _button_Click;
            pnlButtons.Controls.Add(button);
        }

        /// <summary>
        /// Adds the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        private void AddButton(Button button, string name, string text)
        {
            button = new Button();
            button.FlatStyle = FlatStyle.System;
            button.UseVisualStyleBackColor = true;
            button.Font = SystemFonts.MessageBoxFont;
            button.Name = name;
            button.Text = text;
            button.Click += _button_Click;
            pnlButtons.Controls.Add(button);
        }

        #endregion Buttons

        #endregion Box initialization

        #region Event handling

        /// <summary>
        /// Handles the Click event of the buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void _button_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button senderButton = (Button)sender;
                switch (senderButton.Name)
                {
                    case "Abort": _result = InformationBoxResult.Abort; break;
                    case "OK": _result = InformationBoxResult.OK; break;
                    case "Yes": _result = InformationBoxResult.Yes; break;
                    case "Retry": _result = InformationBoxResult.Retry; break;
                    case "No": _result = InformationBoxResult.No; break;
                    case "Cancel": _result = InformationBoxResult.Cancel; break;
                    case "Ignore": _result = InformationBoxResult.Ignore; break;
                    case "User1": _result = InformationBoxResult.User1; break;
                    case "User2": _result = InformationBoxResult.User2; break;
                    default: _result = InformationBoxResult.None; break;
                }

                DialogResult = DialogResult.OK;
            }
        }

        private void InformationBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_result == InformationBoxResult.None)
                _result = InformationBoxResult.Cancel;
        }

        #endregion Event handling
    }
}