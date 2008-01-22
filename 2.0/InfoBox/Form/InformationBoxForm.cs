using InfoBox.Internals;

using System;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using InfoBox.Controls;
using InfoBox.Properties;

namespace InfoBox
{
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

        private InformationBoxResult _result = InformationBoxResult.None;

        private InformationBoxIcon _icon = InformationBoxIcon.None;
        private Icon _customIcon;
        private InformationBoxButtons _buttons = InformationBoxButtons.OK;
        private InformationBoxDefaultButton _defaultButton = InformationBoxDefaultButton.Button1;
        private InformationBoxButtonsLayout _buttonsLayout = InformationBoxButtonsLayout.GroupMiddle;
        private InformationBoxAutoSizeMode _autoSizeMode = InformationBoxAutoSizeMode.None;
        private InformationBoxPosition _position = InformationBoxPosition.CenterOnParent;
        private InformationBoxCheckBox _checkBox = 0;
        private InformationBoxStyle _style = InformationBoxStyle.Standard;
        private AutoCloseParameters _autoClose;
        private DesignParameters _design;
        private InformationBoxTitleIconStyle _titleStyle = InformationBoxTitleIconStyle.SameAsBox;
        private Icon _titleIcon;
        private InformationBoxBehavior _behavior = InformationBoxBehavior.Modal;
        private readonly AsyncResultCallBack _callback;
        private InformationBoxOpacity _opacity = InformationBoxOpacity.NoFade;

        private readonly string _buttonUser1Text = "User1";
        private readonly string _buttonUser2Text = "User2";

        private IconType _iconType = IconType.Internal;

        private readonly Graphics _measureGraphics;
        private StringBuilder internalText;

        private bool _showHelpButton;
        private readonly string _helpFile = String.Empty;
        private readonly string _helpTopic = String.Empty;
        private HelpNavigator _helpNavigator = HelpNavigator.TableOfContents;

        private readonly Form _activeForm;
        private bool _mouseDown;

        private Point _lastPointerPosition;

        private int _elapsedTime;

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
            this.Font = SystemFonts.MessageBoxFont;
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
            _activeForm = ActiveForm;

            // Looks for a parameter of the type InformationBoxInitialization.
            // If found and equal to InformationBoxInitialization.FromParametersOnly,
            // skips the scope parameters.
            bool loadScope = true;
            foreach (object param in parameters)
            {
                if (param is InformationBoxInitialization)
                {
                    InformationBoxInitialization value = (InformationBoxInitialization) param;
                    if (InformationBoxInitialization.FromParametersOnly == value)
                    {
                        loadScope = false;
                    }
                }
            }

            if (loadScope)
                LoadCurrentScope();

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
                        this.Text = (string) parameter;
                        lblTitle.Text = (string) parameter;
                    }
                    else if (stringCount == 1)
                        _helpFile = (string) parameter;
                    else if (stringCount == 2)
                        _helpTopic = (string) parameter;
                    stringCount++;
                }
                // Buttons
                else if (parameter is InformationBoxButtons)
                    _buttons = (InformationBoxButtons) parameter;
                // Internal icon
                else if (parameter is InformationBoxIcon)
                    _icon = (InformationBoxIcon) parameter;
                // User defined icon
                else if (parameter is Icon)
                {
                    _iconType = IconType.UserDefined;
                    _customIcon = new Icon((Icon) parameter, 48, 48);
                }
                // Default button
                else if (parameter is InformationBoxDefaultButton)
                    _defaultButton = (InformationBoxDefaultButton) parameter;
                // Custom buttons
                else if (parameter is string[])
                {
                    string[] labels = (string[]) parameter;
                    if (labels.Length > 0) _buttonUser1Text = labels[0];
                    if (labels.Length > 1) _buttonUser2Text = labels[1];
                }
                // Buttons layout
                else if (parameter is InformationBoxButtonsLayout)
                    _buttonsLayout = (InformationBoxButtonsLayout) parameter;
                // Autosize mode
                else if (parameter is InformationBoxAutoSizeMode)
                    _autoSizeMode = (InformationBoxAutoSizeMode) parameter;
                // Position
                else if (parameter is InformationBoxPosition)
                    _position = (InformationBoxPosition) parameter;
                // Help button
                else if (parameter is bool)
                    _showHelpButton = (bool) parameter;
                // Help navigator
                else if (parameter is HelpNavigator)
                    _helpNavigator = (HelpNavigator) parameter;
                // Do not show this dialog again ?
                else if (parameter is InformationBoxCheckBox)
                    _checkBox = (InformationBoxCheckBox) parameter;
                // Visual style
                else if (parameter is InformationBoxStyle)
                    _style = (InformationBoxStyle) parameter;
                // Auto-close parameters
                else if (parameter is AutoCloseParameters)
                    _autoClose = (AutoCloseParameters) parameter;
                // Design parameters
                else if (parameter is DesignParameters)
                    _design = (DesignParameters) parameter;
                // Title style
                else if (parameter is InformationBoxTitleIconStyle)
                    _titleStyle = (InformationBoxTitleIconStyle) parameter;
                // Title icon
                else if (parameter is InformationBoxTitleIcon)
                    _titleIcon = ((InformationBoxTitleIcon) parameter).Icon;
                // MessageBox buttons
                else if (parameter is MessageBoxButtons)
                    _buttons = MessageBoxEnumConverter.Parse((MessageBoxButtons) parameter);
                // MessageBox icon
                else if (parameter is MessageBoxIcon)
                    _icon = MessageBoxEnumConverter.Parse((MessageBoxIcon) parameter);
                // MessageBox default button
                else if (parameter is MessageBoxDefaultButton)
                    _defaultButton = MessageBoxEnumConverter.Parse((MessageBoxDefaultButton) parameter);
                // InformationBox behaviour
                else if (parameter is InformationBoxBehavior)
                    _behavior = (InformationBoxBehavior) parameter;
                // Callback for the result
                else if (parameter is AsyncResultCallBack)
                    _callback = (AsyncResultCallBack) parameter;
                // Opacity
                else if (parameter is InformationBoxOpacity)
                    _opacity = (InformationBoxOpacity) parameter;
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
            SetOpacity();
            PlaySound();
            
            if (_behavior == InformationBoxBehavior.Modal)
                ShowDialog();
            else
                base.Show();
            
            return _result;
        }

        /// <summary>
        /// Shows this InformationBox.
        /// </summary>
        /// <param name="state">The state of the checkbox.</param>
        /// <returns></returns>
        internal InformationBoxResult Show(ref CheckState state)
        {
            InformationBoxResult result = Show();
            state = chbDoNotShow.CheckState;
            return result;
        }

        #endregion Show

        #region Sound

        /// <summary>
        /// Plays the sound associated with the icon type.
        /// </summary>
        private void PlaySound()
        {
            SystemSound sound;

            if (_iconType == IconType.UserDefined)
            {
                sound = SystemSounds.Beep;
            }
            else
            {
                switch (IconHelper.GetCategory(_icon))
                {
                    case InformationBoxMessageCategory.Asterisk:
                        sound = SystemSounds.Asterisk;
                        break;
                    case InformationBoxMessageCategory.Exclamation:
                        sound = SystemSounds.Exclamation;
                        break;
                    case InformationBoxMessageCategory.Hand:
                        sound = SystemSounds.Hand;
                        break;
                    case InformationBoxMessageCategory.Question:
                        sound = SystemSounds.Question;
                        break;
                    default:
                        sound = SystemSounds.Beep;
                        break;
                }
            }

            if (null != sound)
                sound.Play();
        }

        #endregion Sound

        #region Box initialization

        /// <summary>
        /// Loads the current scope.
        /// </summary>
        private void LoadCurrentScope()
        {
            if (InformationBoxScope.Current == null)
                return;

            InformationBoxScopeParameters parameters = InformationBoxScope.Current.Parameters;

            if (parameters.Icon.HasValue)
                _icon = parameters.Icon.Value;
            
            if (parameters.CustomIcon != null)
            {
                _iconType = IconType.UserDefined;
                _customIcon = parameters.CustomIcon;
            }

            if (parameters.Buttons.HasValue)
                _buttons = parameters.Buttons.Value;

            if (parameters.DefaultButton.HasValue)
                _defaultButton = parameters.DefaultButton.Value;

            if (parameters.Layout.HasValue)
                _buttonsLayout = parameters.Layout.Value;

            if (parameters.AutoSizeMode.HasValue)
                _autoSizeMode = parameters.AutoSizeMode.Value;

            if (parameters.Position.HasValue)
                _position = parameters.Position.Value;

            if (parameters.Checkbox.HasValue)
                _checkBox = parameters.Checkbox.Value;

            if (parameters.Style.HasValue)
                _style = parameters.Style.Value;

            if (parameters.AutoClose != null)
                _autoClose = parameters.AutoClose;

            if (parameters.Design != null)
                _design = parameters.Design;

            if (parameters.TitleIconStyle.HasValue)
                _titleStyle = parameters.TitleIconStyle.Value;

            if (parameters.TitleIcon != null)
                _titleIcon = parameters.TitleIcon;

            if (parameters.Behavior.HasValue)
                _behavior = parameters.Behavior.Value;

            if (parameters.Opacity.HasValue)
                _opacity = parameters.Opacity.Value;

            if (parameters.Help.HasValue)
                _showHelpButton = parameters.Help.Value;

            if (parameters.HelpNavigator.HasValue)
                _helpNavigator = parameters.HelpNavigator.Value;
        }

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

        #region Opacity

        /// <summary>
        /// Sets the opacity.
        /// </summary>
        private void SetOpacity()
        {
            switch (_opacity)
            {
                case InformationBoxOpacity.Faded10:
                    Opacity = 0.1;
                    break;
                case InformationBoxOpacity.Faded20:
                    Opacity = 0.2;
                    break;
                case InformationBoxOpacity.Faded30:
                    Opacity = 0.3;
                    break;
                case InformationBoxOpacity.Faded40:
                    Opacity = 0.4;
                    break;
                case InformationBoxOpacity.Faded50:
                    Opacity = 0.5;
                    break;
                case InformationBoxOpacity.Faded60:
                    Opacity = 0.6;
                    break;
                case InformationBoxOpacity.Faded70:
                    Opacity = 0.7;
                    break;
                case InformationBoxOpacity.Faded80:
                    Opacity = 0.8;
                    break;
                case InformationBoxOpacity.Faded90:
                    Opacity = 0.9;
                    break;
                case InformationBoxOpacity.NoFade:
                    Opacity = 1.0;
                    break;
                default:
                    break;
            }
        }

        #endregion Opacity

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

                foreach (Controls.Button button in pnlButtons.Controls)
                    button.BackColor = barsBackColor;
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
                pnlButtons.SideBorder = SideBorder.None;
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
            chbDoNotShow.TextAlign = ((_checkBox & InformationBoxCheckBox.RightAligned) == InformationBoxCheckBox.RightAligned)
                                         ? ContentAlignment.BottomRight
                                         : ContentAlignment.BottomLeft;
            chbDoNotShow.CheckAlign = ((_checkBox & InformationBoxCheckBox.RightAligned) == InformationBoxCheckBox.RightAligned)
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
            {
                StartPosition = FormStartPosition.CenterScreen;
                CenterToScreen();
            }
            else
            {
                StartPosition = FormStartPosition.CenterParent;
                CenterToScreen();
            }
        }

        #endregion Position

        #region Focus

        /// <summary>
        /// Sets the focus.
        /// </summary>
        private void SetFocus()
        {
            if (_defaultButton == InformationBoxDefaultButton.Button1 && pnlButtons.Controls.Count > 0)
                pnlButtons.Controls[0].Select();

            if (_defaultButton == InformationBoxDefaultButton.Button2 && pnlButtons.Controls.Count > 1)
                pnlButtons.Controls[1].Select();

            if (_defaultButton == InformationBoxDefaultButton.Button3 && pnlButtons.Controls.Count > 2)
                pnlButtons.Controls[2].Select();
        }

        #endregion Focus

        #region Layout

        /// <summary>
        /// Sets the layout.
        /// </summary>
        private void SetLayout()
        {
            int totalHeight;
            int totalWidth;

            #region Width

            // Caption width including button
            int captionWidth = Convert.ToInt32(_measureGraphics.MeasureString(Text, SystemFonts.CaptionFont).Width) + 30;
            if (_titleStyle != InformationBoxTitleIconStyle.None)
                captionWidth += BORDER_PADDING * 2;

            // "Do not show this dialog again" width
            int checkBoxWidth = ((_checkBox & InformationBoxCheckBox.Show) == InformationBoxCheckBox.Show)
                                    ? (int) _measureGraphics.MeasureString(chbDoNotShow.Text, chbDoNotShow.Font).Width + BORDER_PADDING * 4
                                    : 0;

            // Width of the text and icon.
            int iconAndTextWidth = 0;

            // Minimum width to display all needed buttons.
            int buttonsMinWidth = (pnlButtons.Controls.Count + 4) * BORDER_PADDING;
            foreach (Control ctrl in pnlButtons.Controls)
                buttonsMinWidth += ctrl.Width;

            // Icon width
            if (_icon != InformationBoxIcon.None || _iconType == IconType.UserDefined)
                iconAndTextWidth += ICON_PANEL_WIDTH;

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

            // Add a small space to avoid vertical scrollbar.
            if (iconAndTextWidth > Screen.PrimaryScreen.WorkingArea.Width - 100)
                totalHeight += 20;

            bool verticalScroll = false;
            if (totalHeight > Screen.PrimaryScreen.WorkingArea.Height - 50)
            {
                totalHeight = Screen.PrimaryScreen.WorkingArea.Height - 50;
                totalWidth += 20;
                messageText.Top = BORDER_PADDING;
                verticalScroll = true;
            }

            pnlMain.Size =
                new Size(Math.Min(Screen.PrimaryScreen.WorkingArea.Width - 20, totalWidth), totalHeight - pnlBas.Height);

            if (_style == InformationBoxStyle.Modern)
                totalHeight += lblTitle.Height;

            #endregion Height

            // Sets the size;
            ClientSize = new Size(Math.Min(Screen.PrimaryScreen.WorkingArea.Width - 20, totalWidth), totalHeight);

            #region Position

            // Set new position for all components
            // Icon
            pcbIcon.Left = BORDER_PADDING;
            pcbIcon.Top = BORDER_PADDING;

            // Text
            pnlScrollText.Width = ClientSize.Width - ((_icon != InformationBoxIcon.None || _iconType == IconType.UserDefined)
                                       ? ICON_PANEL_WIDTH + BORDER_PADDING + 5
                                       : BORDER_PADDING);
            if (!verticalScroll)
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
                    initialPosition = ClientSize.Width -
                                      (buttonsCount * (pnlButtons.Controls[0].Width + BORDER_PADDING));
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
                pcbIcon.Image = _customIcon.ToBitmap();
                pnlIcon.Visible = true;
            }

            pnlIcon.Width = ICON_PANEL_WIDTH;

            if (_titleStyle == InformationBoxTitleIconStyle.None)
            {
                ShowIcon = false;
                Icon = Resources.IconBlank;
            }
            else if (_titleStyle == InformationBoxTitleIconStyle.SameAsBox)
            {
                if (_iconType == IconType.Internal)
                    Icon = IconHelper.FromEnum(_icon);
                else
                    Icon = _customIcon;
            }
            else if (_titleStyle == InformationBoxTitleIconStyle.Custom)
                Icon = _titleIcon;
        }

        #endregion Icon

        #region Text

        /// <summary>
        /// Sets the text.
        /// </summary>
        private void SetText()
        {
            messageText.Text = messageText.Text.Replace("\n\r", "\n");
            messageText.Text = messageText.Text.Replace("\n", Environment.NewLine);

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
                    internalText.Replace(") ", ")" + Environment.NewLine);
                    internalText.Replace(", ", "," + Environment.NewLine);
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
                AddButton("Abort", Resources.LabelAbort);
            }

            // Ok
            if (_buttons == InformationBoxButtons.OK ||
                _buttons == InformationBoxButtons.OKCancel ||
                _buttons == InformationBoxButtons.OKCancelUser1)
            {
                AddButton("OK", Resources.LabelOK);
            }

            // Yes
            if (_buttons == InformationBoxButtons.YesNo ||
                _buttons == InformationBoxButtons.YesNoCancel ||
                _buttons == InformationBoxButtons.YesNoUser1)
            {
                AddButton("Yes", Resources.LabelYes);
            }

            // Retry
            if (_buttons == InformationBoxButtons.AbortRetryIgnore ||
                _buttons == InformationBoxButtons.RetryCancel)
            {
                AddButton("Retry", Resources.LabelRetry);
            }

            // No
            if (_buttons == InformationBoxButtons.YesNo ||
                _buttons == InformationBoxButtons.YesNoCancel ||
                _buttons == InformationBoxButtons.YesNoUser1)
            {
                AddButton("No", Resources.LabelNo);
            }

            // Cancel
            if (_buttons == InformationBoxButtons.OKCancel ||
                _buttons == InformationBoxButtons.OKCancelUser1 ||
                _buttons == InformationBoxButtons.RetryCancel ||
                _buttons == InformationBoxButtons.YesNoCancel)
            {
                AddButton("Cancel", Resources.LabelCancel);
            }

            // Ignore
            if (_buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                AddButton("Ignore", Resources.LabelIgnore);
            }
            
            // User1
            if (_buttons == InformationBoxButtons.OKCancelUser1 ||
                _buttons == InformationBoxButtons.User1User2 ||
                _buttons == InformationBoxButtons.YesNoUser1 ||
                _buttons == InformationBoxButtons.User1)
            {
                AddButton("User1", _buttonUser1Text);
            }

            // User2
            if (_buttons == InformationBoxButtons.User1User2)
            {
                AddButton("User2", _buttonUser2Text);
            }

            // Help button is displayed when asked or when a help file name exists
            if (_showHelpButton || !String.IsNullOrEmpty(_helpFile))
            {
                AddButton("Help", Resources.LabelHelp);
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
                maxSize = Math.Max(Convert.ToInt32(_measureGraphics.MeasureString(ctrl.Text, ctrl.Font).Width + 40), maxSize);

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
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        private void AddButton(string name, string text)
        {
            Control buttonToAdd;

            if (_style == InformationBoxStyle.Modern)
            {
                buttonToAdd = new Controls.Button();
                (buttonToAdd as Controls.Button).PersistantMode = false;
                (buttonToAdd as Controls.Button).Click += _button_Click;
            }
            else
            {
                buttonToAdd = new System.Windows.Forms.Button();
                (buttonToAdd as System.Windows.Forms.Button).FlatStyle = FlatStyle.System;
                (buttonToAdd as System.Windows.Forms.Button).UseVisualStyleBackColor = true;
                (buttonToAdd as System.Windows.Forms.Button).Click += _button_Click;

            }

            buttonToAdd.Font = SystemFonts.MessageBoxFont;
            buttonToAdd.Name = name;
            buttonToAdd.Text = text;
            pnlButtons.Controls.Add(buttonToAdd);
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
            Control senderControl = sender;
            switch (senderControl.Name)
            {
                case "Abort":
                    _result = InformationBoxResult.Abort;
                    break;
                case "OK":
                    _result = InformationBoxResult.OK;
                    break;
                case "Yes":
                    _result = InformationBoxResult.Yes;
                    break;
                case "Retry":
                    _result = InformationBoxResult.Retry;
                    break;
                case "No":
                    _result = InformationBoxResult.No;
                    break;
                case "Cancel":
                    _result = InformationBoxResult.Cancel;
                    break;
                case "Ignore":
                    _result = InformationBoxResult.Ignore;
                    break;
                case "User1":
                    _result = InformationBoxResult.User1;
                    break;
                case "User2":
                    _result = InformationBoxResult.User2;
                    break;
                default:
                    _result = InformationBoxResult.None;
                    break;
            }

            if (senderControl.Name.Equals("Help"))
            {
                OpenHelp();
            }
            else
            {
                DialogResult = DialogResult.OK;
                if (_behavior == InformationBoxBehavior.Modeless)
                    Close();
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
                    met.Invoke(_activeForm, new object[] {new HelpEventArgs(MousePosition)});
                }
            }

            // If a help file is specified
            if (!String.IsNullOrEmpty(_helpFile))
            {
                // If no topic is specified
                if (String.IsNullOrEmpty(_helpTopic))
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
        private void _button_Click(object sender, EventArgs e)
        {
            if (sender is Control)
                HandleButton((Control) sender);
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

            if (_behavior == InformationBoxBehavior.Modeless && null != _callback)
                Invoke(_callback, _result);
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
                _mouseDown = false;
        }

        /// <summary>
        /// Handles the KeyDown event of the InformationBoxForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InformationBoxForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();

            if (_style == InformationBoxStyle.Modern)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (_defaultButton == InformationBoxDefaultButton.Button1 && pnlButtons.Controls.Count > 0)
                        HandleButton(pnlButtons.Controls[0]);
                    else if (_defaultButton == InformationBoxDefaultButton.Button2 && pnlButtons.Controls.Count > 1)
                        HandleButton(pnlButtons.Controls[1]);
                    else if (_defaultButton == InformationBoxDefaultButton.Button3 && pnlButtons.Controls.Count > 2)
                        HandleButton(pnlButtons.Controls[2]);
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the tmrAutoClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tmrAutoClose_Tick(object sender, EventArgs e)
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

                    if (buttonToUpdate is System.Windows.Forms.Button)
                    {
                        System.Windows.Forms.Button button = (System.Windows.Forms.Button) buttonToUpdate;
                        if (extractLabel.IsMatch(button.Text))
                        {
                            button.Text = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", button.Text.Substring(0, button.Text.LastIndexOf(" (")), _autoClose.Seconds - _elapsedTime);
                        }
                        else
                            button.Text = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", button.Text, _autoClose.Seconds - _elapsedTime);
                    }
                    else if (buttonToUpdate is Controls.Button)
                    {
                        Controls.Button button = (Controls.Button) buttonToUpdate;
                        if (extractLabel.IsMatch(button.Text))
                        {
                            button.Text = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", button.Text.Substring(0, button.Text.LastIndexOf(" (")), _autoClose.Seconds - _elapsedTime);
                        }
                        else
                            button.Text = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", button.Text, _autoClose.Seconds - _elapsedTime);
                    }
                }
            }

            _elapsedTime++;
        }

        #endregion Event handling        
    }
}
