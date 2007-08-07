namespace InfoBox
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Properties;

    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    internal partial class InformationBoxForm : Form
    {
        #region Consts

        private const int ICON_PANEL_WIDTH = 68;
        private const int BORDER_PADDING = 10;
        
        #endregion Consts

        #region Attributes

        private readonly InformationBoxIcon _icon = InformationBoxIcon.None;
        private InformationBoxResult _result = InformationBoxResult.None;
        private readonly InformationBoxButtons _buttons = InformationBoxButtons.OK;
        private readonly InformationBoxDefaultButton _defaultButton = InformationBoxDefaultButton.Button1;
        private readonly InformationBoxButtonsLayout _buttonsLayout = InformationBoxButtonsLayout.GroupMiddle;
        private readonly InformationBoxAutoSizeMode _autoSizeMode = InformationBoxAutoSizeMode.None;
        private readonly InformationBoxPosition _position = InformationBoxPosition.CenterOnParent;
        private readonly InformationBoxCheckBox _checkBox = 0;
        private readonly InformationBoxStyle _style = InformationBoxStyle.Standard;
        private readonly AutoCloseParameters _autoClose = null;
        private readonly DesignParameters _design = null;

        private readonly string _buttonUser1Text = "User1";
        private readonly string _buttonUser2Text = "User2";

        private readonly Control _buttonAbort = null;
        private readonly Control _buttonOk = null;
        private readonly Control _buttonYes = null;
        private readonly Control _buttonRetry = null;
        private readonly Control _buttonNo = null;
        private readonly Control _buttonCancel = null;
        private readonly Control _buttonIgnore = null;
        private readonly Control _buttonUser1 = null;
        private readonly Control _buttonUser2 = null;
        private readonly Control _buttonHelp = null;

        private readonly IconType _iconType = IconType.Internal;

        private readonly Graphics _measureGraphics = null;
        private StringBuilder internalText = null;

        private readonly bool _showHelpButton = false;
        private readonly string _helpFile = String.Empty;
        private readonly string _helpTopic = String.Empty;
        private readonly HelpNavigator _helpNavigator = HelpNavigator.TableOfContents;

        private readonly Form _activeForm = null;
        private bool _mouseDown = false;

        private Point _lastPointerPosition;

        private int _elapsedTime = 0;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxForm"/> class using the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        internal InformationBoxForm(string text)
        {
            InitializeComponent();
            _measureGraphics = CreateGraphics();

            // Apply default font for message boxes
            Font = SystemFonts.MessageBoxFont;
            messageText.Font = SystemFonts.MessageBoxFont;
            lblTitle.Font = SystemFonts.CaptionFont;

            messageText.Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxForm"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="parameters">The parameters.</param>
        internal InformationBoxForm(string text, params object[] parameters) : this(text)
        {
            _activeForm = Form.ActiveForm;

            int stringCount = 0;

            foreach (object parameter in parameters)
            {
                if (null == parameter)
                    continue;

                // Simple string -> caption
                // Or Help file if the string contains a file name
                if (parameter is string)
                {
                    if (stringCount == 0)
                    {
                        Text = (string) parameter;
                        lblTitle.Text = (string) parameter;
                    }
                    else if (stringCount == 1)
                    {
                        _helpFile = (string) parameter;
                    }
                    else if (stringCount == 2)
                    {
                        _helpTopic = (string) parameter;
                    }
                    stringCount++;
                }
                // Buttons
                else if (parameter is InformationBoxButtons)
                {
                    _buttons = (InformationBoxButtons) parameter;
                }
                // Internal icon
                else if (parameter is InformationBoxIcon)
                {
                    _icon = (InformationBoxIcon) parameter;
                }
                // User defined icon
                else if (parameter is Icon)
                {
                    _iconType = IconType.UserDefined;
                    pcbIcon.Image = new Icon((Icon)parameter, 48, 48).ToBitmap();
                }
                // Default button
                else if (parameter is InformationBoxDefaultButton)
                {
                    _defaultButton = (InformationBoxDefaultButton) parameter;
                }
                // Custom buttons
                else if (parameter is string[])
                {
                    string[] labels = (string[]) parameter;
                    if (labels.Length > 0) _buttonUser1Text = labels[0];
                    if (labels.Length > 1) _buttonUser2Text = labels[1];
                }
                // Buttons layout
                else if (parameter is InformationBoxButtonsLayout)
                {
                    _buttonsLayout = (InformationBoxButtonsLayout) parameter;
                }
                // Autosize mode
                else if (parameter is InformationBoxAutoSizeMode)
                {
                    _autoSizeMode = (InformationBoxAutoSizeMode) parameter;
                }
                // Position
                else if (parameter is InformationBoxPosition)
                {
                    _position = (InformationBoxPosition) parameter;
                }
                // Help button
                else if (parameter is bool)
                {
                    _showHelpButton = (bool) parameter;
                }
                // Help navigator
                else if (parameter is HelpNavigator)
                {
                    _helpNavigator = (HelpNavigator) parameter;
                }
                // Do not show this dialog again ?
                else if (parameter is InformationBoxCheckBox)
                {
                    _checkBox = (InformationBoxCheckBox) parameter;
                }
                // Visual style
                else if (parameter is InformationBoxStyle)
                {
                    _style = (InformationBoxStyle) parameter;
                }
                // Auto-close parameters
                else if (parameter is AutoCloseParameters)
                {
                    _autoClose = (AutoCloseParameters) parameter;
                }
                // Design parameters
                else if (parameter is DesignParameters)
                {
                    _design = (DesignParameters) parameter;
                }
            }
        }

        #endregion Constructors

        #region Show

        /// <summary>
        /// Shows this InformationBox.
        /// </summary>
        /// <returns></returns>
        internal new InformationBoxResult Show()
        {
            SetCheckBox();
            SetButtons();
            SetText();
            SetIcon();
            SetLayout();
            SetFocus();
            SetPosition();
            SetWindowStyle();
            SetAutoClose();
            ShowDialog();

            return _result;
        }

        /// <summary>
        /// Shows this InformationBox.
        /// </summary>
        /// <param name="state">The state of the checkbox.</param>
        /// <returns></returns>
        internal InformationBoxResult Show(ref CheckState state)
        {
            InformationBoxResult result = this.Show();
            state = chbDoNotShow.CheckState;
            return result;
        }

        #endregion Show

        #region Box initialization

        #region Auto close

        /// <summary>
        /// Sets the auto close parameters.
        /// </summary>
        private void SetAutoClose()
        {
            if (null == _autoClose)
                return;

            tmrAutoClose.Interval = 1000;
            tmrAutoClose.Tick += tmrAutoClose_Tick;
            tmrAutoClose.Start();
        }

        #endregion Auto close

        #region Style

        /// <summary>
        /// Sets the window style.
        /// </summary>
        private void SetWindowStyle()
        {
            if (_style == InformationBoxStyle.Modern)
            {
                Color barsBackColor = Color.Black;
                Color formBackColor = Color.Silver;

                if (null != _design)
                {
                    barsBackColor = _design.BarsBackColor;
                    formBackColor = _design.FormBackColor;
                }

                pnlForm.BackColor = formBackColor;
                messageText.BackColor = formBackColor;

                pnlButtons.BackColor = barsBackColor;
                lblTitle.BackColor = barsBackColor;

                FormBorderStyle = FormBorderStyle.None;
                lblTitle.Visible = true;

                foreach(GlassComponents.Controls.Button button in pnlButtons.Controls)
                {
                    button.BackColor = barsBackColor;
                }
            }
            else if (_style == InformationBoxStyle.Standard)
            {
                Color barsBackColor = SystemColors.Control;
                Color formBackColor = SystemColors.Control;

                if (null != _design)
                {
                    barsBackColor = _design.BarsBackColor;
                    formBackColor = _design.FormBackColor;
                }

                pnlButtons.BackColor = barsBackColor;
                pnlForm.BackColor = formBackColor;
                messageText.BackColor = formBackColor;

                FormBorderStyle = FormBorderStyle.FixedDialog;
                lblTitle.Visible = false;
                pnlMain.Top -= lblTitle.Height;
                pnlButtons.SideBorder = GlassComponents.Controls.SideBorder.None;
            }
        }

        #endregion Style

        #region CheckBox

        /// <summary>
        /// Sets the check box.
        /// </summary>
        private void SetCheckBox()
        {
            chbDoNotShow.Text = Resources.LabelDoNotShow;

            chbDoNotShow.Visible = ((_checkBox & InformationBoxCheckBox.Show) == InformationBoxCheckBox.Show);
            chbDoNotShow.Checked = ((_checkBox & InformationBoxCheckBox.Checked) == InformationBoxCheckBox.Checked);
            chbDoNotShow.TextAlign = ((_checkBox & InformationBoxCheckBox.RightAligned) ==
                                      InformationBoxCheckBox.RightAligned)
                                             ? ContentAlignment.BottomRight
                                             : ContentAlignment.BottomLeft;
            chbDoNotShow.CheckAlign = ((_checkBox & InformationBoxCheckBox.RightAligned) ==
                                      InformationBoxCheckBox.RightAligned)
                                             ? ContentAlignment.MiddleRight
                                             : ContentAlignment.MiddleLeft;
        }

        #endregion CheckBox

        #region Position

        /// <summary>
        /// Sets the position.
        /// </summary>
        private void SetPosition()
        {
            if (_position == InformationBoxPosition.CenterOnScreen)
                StartPosition = FormStartPosition.CenterScreen;
            else
                StartPosition = FormStartPosition.CenterParent;
        }

        #endregion Position

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
            int captionWidth = Convert.ToInt32(_measureGraphics.MeasureString(Text, SystemFonts.CaptionFont).Width) + 30;

            // "Do not show this dialog again" width
            int checkBoxWidth = ((_checkBox & InformationBoxCheckBox.Show) == InformationBoxCheckBox.Show)
                                        ? (int) _measureGraphics.MeasureString(chbDoNotShow.Text, chbDoNotShow.Font).Width + BORDER_PADDING * 4
                                        : 0;

            // Width of the text and icon.
            int iconAndTextWidth = 0;

            // Minimum width to display all needed buttons.
            int buttonsMinWidth = (pnlButtons.Controls.Count + 4) * BORDER_PADDING;
            foreach (Control ctrl in pnlButtons.Controls)
            {
                buttonsMinWidth += ctrl.Width;
            }

            // Icon width
            if (_icon != InformationBoxIcon.None || _iconType == IconType.UserDefined)
            {
                iconAndTextWidth += ICON_PANEL_WIDTH;
            }

            // Text width
            iconAndTextWidth += messageText.Width + BORDER_PADDING * 2;

            // Gets the maximum size
            totalWidth = Math.Max(Math.Max(Math.Max(buttonsMinWidth, iconAndTextWidth), captionWidth), checkBoxWidth);

            #endregion Width

            #region Height

            if ((_checkBox & InformationBoxCheckBox.Show) != InformationBoxCheckBox.Show)
            {
                chbDoNotShow.Visible = false;
                pnlBas.Height -= chbDoNotShow.Height;
            }

            int iconHeight = 0;
            if (_icon != InformationBoxIcon.None || _iconType == IconType.UserDefined)
                iconHeight = pcbIcon.Height;

            int textHeight = messageText.Height;

            totalHeight = Math.Max(iconHeight, textHeight) + BORDER_PADDING * 2 + pnlBas.Height;
            pnlMain.Size = new Size(totalWidth, Math.Max(iconHeight, textHeight) + BORDER_PADDING * 2);

            if (_style == InformationBoxStyle.Modern)
                totalHeight += lblTitle.Height;

            #endregion Height

            // Sets the size;
            ClientSize = new Size(totalWidth, totalHeight);

            #region Position

            // Set new position for all components
            // Icon
            pcbIcon.Left = BORDER_PADDING;
            pcbIcon.Top = BORDER_PADDING;

            // Text
            messageText.Left = (_icon != InformationBoxIcon.None || _iconType == IconType.UserDefined) ? ICON_PANEL_WIDTH + BORDER_PADDING : BORDER_PADDING;
            messageText.Top = Convert.ToInt32((pnlText.Height - messageText.Height) / 2);

            // Buttons
            SetButtonsLayout();

            #endregion Position
        }

        private void SetButtonsLayout()
        {
            // Do not count the checkbox
            int buttonsCount = pnlButtons.Controls.Count;
            int index = 0;
            int initialPosition = 0;
            int spaceBetween = 0;
            switch (_buttonsLayout)
            {
                case InformationBoxButtonsLayout.GroupLeft:
                    initialPosition = BORDER_PADDING;
                    spaceBetween = BORDER_PADDING;
                    break;
                case InformationBoxButtonsLayout.GroupMiddle:
                    spaceBetween = BORDER_PADDING;
                    
                    // If there is only one button then we must center it
                    if (buttonsCount == 1)
                        initialPosition += Convert.ToInt32((Width - buttonsCount * pnlButtons.Controls[0].Width) / (buttonsCount + 1));
                    else
                        initialPosition = Convert.ToInt32((Width - (buttonsCount * (pnlButtons.Controls[0].Width + BORDER_PADDING))) / 2);
                    break;
                case InformationBoxButtonsLayout.GroupRight:
                    spaceBetween = BORDER_PADDING;
                    initialPosition = ClientSize.Width - (buttonsCount * (pnlButtons.Controls[0].Width + BORDER_PADDING));
                    break;
                case InformationBoxButtonsLayout.Separate:
                    spaceBetween = Convert.ToInt32((ClientSize.Width - buttonsCount * pnlButtons.Controls[0].Width) / (buttonsCount + 1));
                    initialPosition = spaceBetween;
                    break;
                default:
                    break;
            }

            foreach (Control ctrl in pnlButtons.Controls)
            {
                ctrl.Left = initialPosition + spaceBetween * (index) + ctrl.Width * index;
                ++index;
            }
        }

        #endregion Layout

        #region Icon

        /// <summary>
        /// Sets the icon.
        /// </summary>
        private void SetIcon()
        {
            if (_iconType == IconType.Internal)
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
            }
            else
            {
                pnlIcon.Visible = true;
            }

            pnlIcon.Width = ICON_PANEL_WIDTH;
            this.Icon = Resources.IconBlank;
        }

        #endregion Icon

        #region Text

        /// <summary>
        /// Sets the text.
        /// </summary>
        private void SetText()
        {
            if (_autoSizeMode != InformationBoxAutoSizeMode.None)
            {
                internalText = new StringBuilder(messageText.Text);
                
                Screen currentScreen = Screen.FromControl(this);
                int screenWidth = currentScreen.WorkingArea.Width;

                if (_autoSizeMode == InformationBoxAutoSizeMode.MinimumHeight)
                {
                    // Remove line breaks.
                    internalText = internalText.Replace(Environment.NewLine, " ");
                    Regex splitter = new Regex(@"(?<sentence>.+?(\. |$))", RegexOptions.Compiled);
                    MatchCollection sentences = splitter.Matches(internalText.ToString());
                    StringBuilder formattedText = new StringBuilder();
                    int currentWidth = 0;

                    foreach (Match sentence in sentences)
                    {
                        int sentenceLength = (int) _measureGraphics.MeasureString(sentence.Value, messageText.Font).Width;
                        if (currentWidth != 0 && (sentenceLength + currentWidth) > (screenWidth - 50))
                        {
                            formattedText.Append(Environment.NewLine);
                            currentWidth = 0;
                        }

                        currentWidth += sentenceLength;
                        formattedText.Append(sentence.Value);
                    }

                    internalText = formattedText;
                }
                else if (_autoSizeMode == InformationBoxAutoSizeMode.MinimumWidth)
                {
                    internalText.Replace(". ", "." + Environment.NewLine);
                    internalText.Replace("? ", "?" + Environment.NewLine);
                    internalText.Replace("! ", "!" + Environment.NewLine);
                    internalText.Replace(": ", ":" + Environment.NewLine);
                }

                messageText.Text = internalText.ToString();
            }

            messageText.Size = _measureGraphics.MeasureString(messageText.Text, messageText.Font).ToSize();
            messageText.Width += BORDER_PADDING;
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
                AddButton(_buttonAbort, "Abort", Resources.LabelAbort);
            }

            // Ok
            if (_buttons == InformationBoxButtons.OK ||
                _buttons == InformationBoxButtons.OKCancel ||
                _buttons == InformationBoxButtons.OKCancelUser1)
            {
                AddButton(_buttonOk, "OK", Resources.LabelOK);
            }

            // Yes
            if (_buttons == InformationBoxButtons.YesNo ||
                _buttons == InformationBoxButtons.YesNoCancel ||
                _buttons == InformationBoxButtons.YesNoUser1)
            {
                AddButton(_buttonYes, "Yes", Resources.LabelYes);
            }

            // Retry
            if (_buttons == InformationBoxButtons.AbortRetryIgnore ||
                _buttons == InformationBoxButtons.RetryCancel)
            {
                AddButton(_buttonRetry, "Retry", Resources.LabelRetry);
            }

            // No
            if (_buttons == InformationBoxButtons.YesNo ||
                _buttons == InformationBoxButtons.YesNoCancel ||
                _buttons == InformationBoxButtons.YesNoUser1)
            {
                AddButton(_buttonNo, "No", Resources.LabelNo);
            }

            // Cancel
            if (_buttons == InformationBoxButtons.OKCancel ||
                _buttons == InformationBoxButtons.OKCancelUser1 ||
                _buttons == InformationBoxButtons.RetryCancel ||
                _buttons == InformationBoxButtons.YesNoCancel)
            {
                AddButton(_buttonCancel, "Cancel", Resources.LabelCancel);
            }

            // Ignore
            if (_buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                AddButton(_buttonIgnore, "Ignore", Resources.LabelIgnore);
            }
            
            // User1
            if (_buttons == InformationBoxButtons.OKCancelUser1 ||
                _buttons == InformationBoxButtons.User1User2 ||
                _buttons == InformationBoxButtons.YesNoUser1 ||
                _buttons == InformationBoxButtons.User1)
            {
                AddButton(_buttonUser1, "User1", _buttonUser1Text);
            }

            // User2
            if (_buttons == InformationBoxButtons.User1User2)
            {
                AddButton(_buttonUser2, "User2", _buttonUser2Text);
            }

            // Help button is displayed when asked or when a help file name exists
            if (_showHelpButton || !String.Empty.Equals(_helpFile))
            {
                AddButton(_buttonHelp, "Help", Resources.LabelHelp);
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
            foreach (Control ctrl in pnlButtons.Controls)
            {
                maxSize = Math.Max(Convert.ToInt32(_measureGraphics.MeasureString(ctrl.Text, ctrl.Font).Width + 40), maxSize);
            }

            foreach (Control ctrl in pnlButtons.Controls)
            {
                if (_style == InformationBoxStyle.Standard)
                {
                    ctrl.Size = new Size(maxSize, 23);
                    ctrl.Top = 5;
                }
                else if (_style == InformationBoxStyle.Modern)
                {
                    ctrl.Size = new Size(maxSize, pnlButtons.Height);
                    ctrl.Top = 0;
                }
            }
        }

        /// <summary>
        /// Adds the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        private void AddButton(Control button, string name, string text)
        {
            if (_style == InformationBoxStyle.Standard)
            {
                button = new Button();
                (button as Button).FlatStyle = FlatStyle.System;
                (button as Button).UseVisualStyleBackColor = true;
                (button as Button).Click += _button_Click;
            
            }
            else if (_style == InformationBoxStyle.Modern)
            {
                button = new GlassComponents.Controls.Button();
                (button as GlassComponents.Controls.Button).PersistantMode = false;
                (button as GlassComponents.Controls.Button).Click += _button_Click;
            }

            button.Font = SystemFonts.MessageBoxFont;
            button.Name = name;
            button.Text = text;
            pnlButtons.Controls.Add(button);
        }

        #endregion Buttons

        #endregion Box initialization

        #region Button click

        /// <summary>
        /// Handles the buttons.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void HandleButton(Control sender)
        {
            Control senderControl = (Control)sender;
            switch (senderControl.Name)
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

            if (senderControl.Name.Equals("Help"))
            {
                OpenHelp();
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        #endregion Button click

        #region Help

        private void OpenHelp()
        {
            // If there is an active form
            if (null != _activeForm)
            {
                MethodInfo met = _activeForm.GetType().GetMethod("OnHelpRequested", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
                if (null != met)
                {
                    // Call for help on the active form.
                    met.Invoke(_activeForm, new object[] { new HelpEventArgs(MousePosition) });
                }
            }

            // If a help file is specified
            if (!String.Empty.Equals(_helpFile))
            {
                // If no topic is specified
                if (String.Empty.Equals(_helpTopic))
                    Help.ShowHelp(_activeForm, _helpFile, _helpNavigator);
                else
                    Help.ShowHelp(_activeForm, _helpFile, _helpTopic);
            }
        }

        #endregion Help

        #region Event handling

        /// <summary>
        /// Handles the Click event of the buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void _button_Click(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                HandleButton((Control) sender);
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the InformationBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void InformationBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_result == InformationBoxResult.None)
                _result = InformationBoxResult.Cancel;
        }

        /// <summary>
        /// Handles the Paint event of the pnlForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void pnlForm_Paint(object sender, PaintEventArgs e)
        {
            if (_style == InformationBoxStyle.Modern)
                ControlPaint.DrawBorder(e.Graphics, pnlForm.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Handles the MouseDown event of the lblTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _lastPointerPosition = e.Location;
                _mouseDown = true;
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the lblTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown)
                return;

            Point location = DesktopLocation;

            location.Offset(new Point(e.Location.X - _lastPointerPosition.X,
                                      e.Location.Y - _lastPointerPosition.Y));

            DesktopLocation = location;
        }

        /// <summary>
        /// Handles the MouseUp event of the lblTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDown = false;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the InformationBoxForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InformationBoxForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (_style == InformationBoxStyle.Modern)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (_defaultButton == InformationBoxDefaultButton.Button1 && pnlButtons.Controls.Count > 0)
                    {
                        HandleButton(pnlButtons.Controls[0]);
                    }
                    else if (_defaultButton == InformationBoxDefaultButton.Button2 && pnlButtons.Controls.Count > 1)
                    {
                        HandleButton(pnlButtons.Controls[1]);
                    }
                    else if (_defaultButton == InformationBoxDefaultButton.Button3 && pnlButtons.Controls.Count > 2)
                    {
                        HandleButton(pnlButtons.Controls[2]);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the tmrAutoClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void tmrAutoClose_Tick(object sender, EventArgs e)
        {
            if (_elapsedTime == _autoClose.Seconds)
            {
                tmrAutoClose.Stop();

                switch (_autoClose.Mode)
                {
                    case AutoCloseDefinedParameters.Button:
                        if (_autoClose.DefaultButton == InformationBoxDefaultButton.Button1 && pnlButtons.Controls.Count > 0)
                            HandleButton(pnlButtons.Controls[0]);
                        else if (_autoClose.DefaultButton == InformationBoxDefaultButton.Button2 && pnlButtons.Controls.Count > 1)
                            HandleButton(pnlButtons.Controls[1]);
                        else if (_autoClose.DefaultButton == InformationBoxDefaultButton.Button3 && pnlButtons.Controls.Count > 2)
                            HandleButton(pnlButtons.Controls[2]);
                        return;
                    case AutoCloseDefinedParameters.TimeOnly:
                        if (_defaultButton == InformationBoxDefaultButton.Button1 && pnlButtons.Controls.Count > 0)
                            HandleButton(pnlButtons.Controls[0]);
                        else if (_defaultButton == InformationBoxDefaultButton.Button2 && pnlButtons.Controls.Count > 1)
                            HandleButton(pnlButtons.Controls[1]);
                        else if (_defaultButton == InformationBoxDefaultButton.Button3 && pnlButtons.Controls.Count > 2)
                            HandleButton(pnlButtons.Controls[2]);
                        return;
                    case AutoCloseDefinedParameters.Result:
                        _result = _autoClose.Result;
                        DialogResult = DialogResult.OK;
                        return;
                    default:
                        break;
                }
            }
            else
            {
                Control buttonToUpdate = null;
                if (_autoClose.Mode == AutoCloseDefinedParameters.Button)
                {
                    if (_autoClose.DefaultButton == InformationBoxDefaultButton.Button1 && pnlButtons.Controls.Count > 0)
                        buttonToUpdate = pnlButtons.Controls[0];
                    else if (_autoClose.DefaultButton == InformationBoxDefaultButton.Button2 && pnlButtons.Controls.Count > 1)
                        buttonToUpdate = pnlButtons.Controls[1];
                    else if (_autoClose.DefaultButton == InformationBoxDefaultButton.Button3 && pnlButtons.Controls.Count > 2)
                        buttonToUpdate = pnlButtons.Controls[2];
                }
                else
                {
                    if (_defaultButton == InformationBoxDefaultButton.Button1 && pnlButtons.Controls.Count > 0)
                        buttonToUpdate = pnlButtons.Controls[0];
                    else if (_defaultButton == InformationBoxDefaultButton.Button2 && pnlButtons.Controls.Count > 1)
                        buttonToUpdate = pnlButtons.Controls[1];
                    else if (_defaultButton == InformationBoxDefaultButton.Button3 && pnlButtons.Controls.Count > 2)
                        buttonToUpdate = pnlButtons.Controls[2];
                }

                if (null != buttonToUpdate)
                {
                    Regex extractLabel = new Regex(@".*?\(\d+\)");

                    if (buttonToUpdate is Button)
                    {
                        Button button = (Button) buttonToUpdate;
                        if (extractLabel.IsMatch(button.Text))
                            button.Text = String.Format("{0} ({1})", button.Text.Substring(0, button.Text.LastIndexOf(" (")), _autoClose.Seconds - _elapsedTime);
                        else
                            button.Text = String.Format("{0} ({1})", button.Text, _autoClose.Seconds - _elapsedTime);
                    }
                    else if (buttonToUpdate is GlassComponents.Controls.Button)
                    {
                        GlassComponents.Controls.Button button = (GlassComponents.Controls.Button) buttonToUpdate;
                        if (extractLabel.IsMatch(button.Text))
                            button.Text = String.Format("{0} ({1})", button.Text.Substring(0, button.Text.LastIndexOf(" (")), _autoClose.Seconds - _elapsedTime);
                        else
                            button.Text = String.Format("{0} ({1})", button.Text, _autoClose.Seconds - _elapsedTime);
                    }
                }
            }


            _elapsedTime++;
        }

        #endregion Event handling        
    }
}