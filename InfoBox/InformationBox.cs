using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InfoBox
{
    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    public partial class InformationBox : Form
    {
        #region Consts

        private const int ICON_PANEL_WIDTH = 76;
        private const int BUTTONS_MIN_SPACE = 10;
        private const int BORDER_PADDING = 10;
        private const int WINDOW_DECORATION_HEIGHT = 30;

        #endregion Consts

        #region Attributes

        private InformationBoxIcon _icon = InformationBoxIcon.None;
        private InformationBoxResult _result = InformationBoxResult.None;
        private InformationBoxButtons _buttons = InformationBoxButtons.OK;

        private Button _buttonAbort = null;
        private Button _buttonOk = null;
        private Button _buttonYes = null;
        private Button _buttonRetry = null;
        private Button _buttonNo = null;
        private Button _buttonCancel = null;
        private Button _buttonIgnore = null;
        private Button _buttonUser1 = null;
        private Button _buttonUser2 = null;

        #endregion Attributes

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        private InformationBox(string text)
        {
            InitializeComponent();

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
            Text = caption;
        }

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
        /// <returns></returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons)
        {
            InformationBox box = new InformationBox(text, caption, buttons);
            return Show(box);
        }

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
            box.ShowDialog();

            return box._result;
        }

        /// <summary>
        /// Sets the layout.
        /// </summary>
        private void SetLayout()
        {
            int totalHeight = 200;
            int totalWidth = 0;

            #region Width

            // Caption width including button
            int captionWidth = Convert.ToInt32(CreateGraphics().MeasureString(Text, Font).Width) + 40;

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

            // Set new position for all components
            // Icon
            pcbIcon.Left = Convert.ToInt32((pnlIcon.Width - pcbIcon.Width) / 2);
            pcbIcon.Top = Convert.ToInt32((pnlIcon.Height - pcbIcon.Height) / 2);
            
            // Text
            lblText.Left = 9;
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
        }


        #region Icon

        /// <summary>
        /// Sets the icon.
        /// </summary>
        private void SetIcon()
        {
            if (_icon == InformationBoxIcon.None)
                pnlIcon.Visible = false;

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
                AddButton(_buttonUser1, "User1");
            }

            // User2
            if (_buttons == InformationBoxButtons.User1User2)
            {
                AddButton(_buttonUser2, "User2");
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
            button.Name = name;
            button.Text = name;
            button.Click += new EventHandler(_button_Click);
            pnlButtons.Controls.Add(button);
        }

        #endregion Buttons

        #region Event handling

        /// <summary>
        /// Handles the Click event of the buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void _button_Click(object sender, EventArgs e)
        {
            
        }

        #endregion Event handling
    }
}