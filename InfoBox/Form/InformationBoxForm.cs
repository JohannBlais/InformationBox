// <copyright file="InformationBoxForm.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Displays a message box that can contain text, buttons, and symbols that inform and instruct the user</summary>

namespace InfoBox
{
    using InfoBox.Controls;
    using InfoBox.Internals;
    using InfoBox.Properties;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Media;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    internal partial class InformationBoxForm : Form
    {
        #region Consts

        /// <summary>
        /// Width of the icon panel
        /// </summary>
        private const int IconPanelWidth = 68;

        /// <summary>
        /// Padding for the borders
        /// </summary>
        private const int BorderPadding = 10;

        #endregion Consts

        #region Attributes

        /// <summary>
        /// DPI scale factor relative to 96 DPI
        /// </summary>
        private float dpiScale;

        /// <summary>
        /// Contains the callback used to inform the caller of a modeless box
        /// </summary>
        private readonly AsyncResultCallback callback;

        /// <summary>
        /// Text for the first user button
        /// </summary>
        private readonly string buttonUser1Text = "User1";

        /// <summary>
        /// Text for the second user button
        /// </summary>
        private readonly string buttonUser2Text = "User2";

        /// <summary>
        /// Text for the third user button
        /// </summary>
        private readonly string buttonUser3Text = "User3";

        /// <summary>
        /// Help file associated to the help button
        /// </summary>
        private readonly string helpFile;

        /// <summary>
        /// Help topic associated to the help button
        /// </summary>
        private readonly string helpTopic;

        /// <summary>
        /// Text for the "Do not show again" checkbox
        /// </summary>
        private readonly string doNotShowAgainText;

        /// <summary>
        /// Contains a reference to the active form
        /// </summary>
        private readonly Form activeForm;

        /// <summary>
        /// Result corresponding the clicked button
        /// </summary>
        private InformationBoxResult result = InformationBoxResult.None;

        /// <summary>
        /// Main icon of the form
        /// </summary>
        private InformationBoxIcon icon;

        /// <summary>
        /// Custom icon
        /// </summary>
        private Icon customIcon;

        /// <summary>
        /// Buttons displayed on the form
        /// </summary>
        private InformationBoxButtons buttons;

        /// <summary>
        /// Default button
        /// </summary>
        private InformationBoxDefaultButton defaultButton;

        /// <summary>
        /// Buttons layout
        /// </summary>
        private InformationBoxButtonsLayout buttonsLayout = InformationBoxButtonsLayout.GroupMiddle;

        /// <summary>
        /// Contains the autosize mode
        /// </summary>
        private InformationBoxAutoSizeMode autoSizeMode = InformationBoxAutoSizeMode.None;

        /// <summary>
        /// Contains the box initial position
        /// </summary>
        private InformationBoxPosition position = InformationBoxPosition.CenterOnParent;

        /// <summary>
        /// Contains a value defining whether displaying the checkbox or not
        /// </summary>
        private InformationBoxCheckBox checkBox = 0;

        /// <summary>
        /// Contains the style of the box
        /// </summary>
        private InformationBoxStyle style = InformationBoxStyle.Standard;

        /// <summary>
        /// Contains the autoclose parameters
        /// </summary>
        private AutoCloseParameters autoClose;

        /// <summary>
        /// Contains the design parameters
        /// </summary>
        private DesignParameters design;

        /// <summary>
        /// Contains the font parameters
        /// </summary>
        private FontParameters fontParameters;

        /// <summary>
        /// Contains the style of the title
        /// </summary>
        private InformationBoxTitleIconStyle titleStyle = InformationBoxTitleIconStyle.None;

        /// <summary>
        /// Contains the title icon
        /// </summary>
        private Icon titleIcon;

        /// <summary>
        /// Contains if the box is modal or not
        /// </summary>
        private InformationBoxBehavior behavior = InformationBoxBehavior.Modal;

        /// <summary>
        /// Contains the opacity of the form
        /// </summary>
        private InformationBoxOpacity opacity = InformationBoxOpacity.NoFade;

        /// <summary>
        /// Contains the icon type
        /// </summary>
        private IconType iconType = IconType.Internal;

        /// <summary>
        /// Contains if the help button should be displayed
        /// </summary>
        private bool showHelpButton;

        /// <summary>
        /// Help navigator associated to the help button
        /// </summary>
        private HelpNavigator helpNavigator = HelpNavigator.TableOfContents;

        /// <summary>
        /// Contains whether a mouse button is down
        /// </summary>
        private bool mouseDown;

        /// <summary>
        /// Last stored pointer position
        /// </summary>
        private Point lastPointerPosition;

        /// <summary>
        /// Elapsed time for the autoclose
        /// </summary>
        private int elapsedTime;

        /// <summary>
        /// Z-Order of the form
        /// </summary>
        private InformationBoxOrder order = InformationBoxOrder.Default;

        /// <summary>
        /// Sound to play on opening
        /// </summary>
        private InformationBoxSound sound;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxForm"/>.
        /// </summary>
        private InformationBoxForm()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxForm"/> class using the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="title">The title.</param>
        /// <param name="helpFile">The help file.</param>
        /// <param name="helpTopic">The help topic.</param>
        /// <param name="initialization">The initialization.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="customIcon">The custom icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="customButtons">The custom buttons.</param>
        /// <param name="buttonsLayout">The buttons layout.</param>
        /// <param name="autoSizeMode">The auto size mode.</param>
        /// <param name="position">The position.</param>
        /// <param name="showHelpButton">if set to <c>true</c> shows help button.</param>
        /// <param name="helpNavigator">The help navigator.</param>
        /// <param name="showDoNotShowAgainCheckBox">if set to <c>true</c> shows the do not show again check box.</param>
        /// <param name="doNotShowAgainText">If not null, the value will replace the default text for the "Do not show again" checkbox.</param>
        /// <param name="style">The style.</param>
        /// <param name="autoClose">The auto close configuration.</param>
        /// <param name="design">The design.</param>
        /// <param name="fontParameters">The font parameters.</param>
        /// <param name="titleStyle">The title style.</param>
        /// <param name="titleIcon">The title icon.</param>
        /// <param name="legacyButtons">The legacy buttons.</param>
        /// <param name="legacyIcon">The legacy icon.</param>
        /// <param name="legacyDefaultButton">The legacy default button.</param>
        /// <param name="behavior">The behavior.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="opacity">The opacity.</param>
        /// <param name="parent">The parent form.</param>
        /// <param name="order">The z-order</param>
        /// <param name="sound">The sound</param>
        internal InformationBoxForm(string text,
                                    string title = "",
                                    string helpFile = "",
                                    string helpTopic = "",
                                    InformationBoxInitialization initialization = InformationBoxInitialization.FromScopeAndParameters,
                                    InformationBoxButtons buttons = InformationBoxButtons.OK,
                                    InformationBoxIcon icon = InformationBoxIcon.None,
                                    Icon customIcon = null,
                                    InformationBoxDefaultButton defaultButton = InformationBoxDefaultButton.Button1,
                                    string[] customButtons = null,
                                    InformationBoxButtonsLayout buttonsLayout = InformationBoxButtonsLayout.GroupMiddle,
                                    InformationBoxAutoSizeMode autoSizeMode = InformationBoxAutoSizeMode.None,
                                    InformationBoxPosition position = InformationBoxPosition.CenterOnParent,
                                    bool showHelpButton = false,
                                    HelpNavigator helpNavigator = HelpNavigator.TableOfContents,
                                    InformationBoxCheckBox showDoNotShowAgainCheckBox = 0,
                                    string doNotShowAgainText = null,
                                    InformationBoxStyle style = InformationBoxStyle.Standard,
                                    AutoCloseParameters autoClose = null,
                                    DesignParameters design = null,
                                    FontParameters fontParameters = null,
                                    InformationBoxTitleIconStyle titleStyle = InformationBoxTitleIconStyle.None,
                                    InformationBoxTitleIcon titleIcon = null,
                                    MessageBoxButtons? legacyButtons = null,
                                    MessageBoxIcon? legacyIcon = null,
                                    MessageBoxDefaultButton? legacyDefaultButton = null,
                                    InformationBoxBehavior behavior = InformationBoxBehavior.Modal,
                                    AsyncResultCallback callback = null,
                                    InformationBoxOpacity opacity = InformationBoxOpacity.NoFade,
                                    Form parent = null,
                                    InformationBoxOrder order = InformationBoxOrder.Default,
                                    InformationBoxSound sound = InformationBoxSound.Default)
        {
            this.InitializeComponent();
            this.dpiScale = this.DeviceDpi / 96f;

            // Apply default font for message boxes
            this.Font = SystemFonts.MessageBoxFont;
            this.messageText.Font = SystemFonts.MessageBoxFont;
            this.lblTitle.Font = SystemFonts.CaptionFont;

            this.messageText.Text = text;

            if (InformationBoxInitialization.FromParametersOnly == initialization)
            {
                this.LoadCurrentScope();
            }

            this.Text = title;
            this.lblTitle.Text = title;
            this.helpFile = helpFile;
            this.helpTopic = helpTopic;
            this.buttons = buttons;
            this.icon = icon;
            if (customIcon != null)
            {
                this.iconType = IconType.UserDefined;
                this.customIcon = customIcon;
            }
            this.defaultButton = defaultButton;
            if (customButtons != null)
            {
                if (customButtons.Length > 0)
                {
                    this.buttonUser1Text = customButtons[0];
                }

                if (customButtons.Length > 1)
                {
                    this.buttonUser2Text = customButtons[1];
                }

                if (customButtons.Length > 2)
                {
                    this.buttonUser3Text = customButtons[2];
                }
            }
            this.buttonsLayout = buttonsLayout;
            this.autoSizeMode = autoSizeMode;
            this.position = position;
            this.showHelpButton = showHelpButton;
            this.helpNavigator = helpNavigator;
            this.checkBox = showDoNotShowAgainCheckBox;
            this.doNotShowAgainText = doNotShowAgainText;
            this.style = style;
            this.autoClose = autoClose;
            this.design = design;
            this.fontParameters = fontParameters;
            this.titleStyle = titleStyle;
            if (titleIcon != null)
            {
                this.titleIcon = titleIcon.Icon;
            }
            if (!(legacyButtons is null))
            {
                this.buttons = MessageBoxEnumConverter.Parse(legacyButtons.Value);
            }
            if (!(legacyIcon is null))
            {
                this.icon = MessageBoxEnumConverter.Parse(legacyIcon.Value);
            }
            if (!(legacyDefaultButton is null))
            {
                this.defaultButton = MessageBoxEnumConverter.Parse(legacyDefaultButton.Value);
            }
            this.behavior = behavior;
            this.callback = callback;
            this.opacity = opacity;
            this.Parent = parent;
            this.order = order;
            this.sound = sound;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxForm"/> class.
        /// </summary>
        /// <param name="text">The text of the box.</param>
        /// <param name="parameters">The parameters.</param>
        internal InformationBoxForm(string text, params object[] parameters)
            : this(text)
        {
            this.activeForm = ActiveForm;

            // Looks for a parameter of the type InformationBoxInitialization.
            // If found and equal to InformationBoxInitialization.FromParametersOnly,
            // skips the scope parameters.
            bool loadScope = true;
            foreach (object param in parameters)
            {
                if (param is InformationBoxInitialization)
                {
                    InformationBoxInitialization value = (InformationBoxInitialization)param;
                    if (InformationBoxInitialization.FromParametersOnly == value)
                    {
                        loadScope = false;
                    }
                }
            }

            if (loadScope)
            {
                this.LoadCurrentScope();
            }

            int stringCount = 0;

            foreach (object parameter in parameters)
            {
                if (null == parameter)
                {
                    continue;
                }

                // Simple string -> caption
                // Or Help file if the string contains a file name
                if (parameter is string)
                {
                    if (stringCount == 0)
                    {
                        this.Text = (string)parameter;
                        this.lblTitle.Text = (string)parameter;
                    }
                    else if (stringCount == 1)
                    {
                        this.helpFile = (string)parameter;
                    }
                    else if (stringCount == 2)
                    {
                        this.helpTopic = (string)parameter;
                    }
                    else if (stringCount == 3)
                    {
                        this.doNotShowAgainText = (string)parameter;
                    }

                    stringCount++;
                }
                else if (parameter is InformationBoxButtons)
                {
                    // Buttons
                    this.buttons = (InformationBoxButtons)parameter;
                }
                else if (parameter is InformationBoxIcon)
                {
                    // Internal icon
                    this.icon = (InformationBoxIcon)parameter;
                }
                else if (parameter is Icon)
                {
                    // User defined icon
                    this.iconType = IconType.UserDefined;
                    this.customIcon = (Icon)parameter;
                }
                else if (parameter is InformationBoxDefaultButton)
                {
                    // Default button
                    this.defaultButton = (InformationBoxDefaultButton)parameter;
                }
                else if (parameter is string[])
                {
                    // Custom buttons
                    string[] labels = (string[])parameter;
                    if (labels.Length > 0)
                    {
                        this.buttonUser1Text = labels[0];
                    }

                    if (labels.Length > 1)
                    {
                        this.buttonUser2Text = labels[1];
                    }

                    if (labels.Length > 2)
                    {
                        this.buttonUser3Text = labels[2];
                    }
                }
                else if (parameter is InformationBoxButtonsLayout)
                {
                    // Buttons layout
                    this.buttonsLayout = (InformationBoxButtonsLayout)parameter;
                }
                else if (parameter is InformationBoxAutoSizeMode)
                {
                    // Autosize mode
                    this.autoSizeMode = (InformationBoxAutoSizeMode)parameter;
                }
                else if (parameter is InformationBoxPosition)
                {
                    // Position
                    this.position = (InformationBoxPosition)parameter;
                }
                else if (parameter is bool)
                {
                    // Help button
                    this.showHelpButton = (bool)parameter;
                }
                else if (parameter is HelpNavigator)
                {
                    // Help navigator
                    this.helpNavigator = (HelpNavigator)parameter;
                }
                else if (parameter is InformationBoxCheckBox)
                {
                    // Do not show this dialog again ?
                    this.checkBox = (InformationBoxCheckBox)parameter;
                }
                else if (parameter is InformationBoxStyle)
                {
                    // Visual style
                    this.style = (InformationBoxStyle)parameter;
                }
                else if (parameter is AutoCloseParameters)
                {
                    // Auto-close parameters
                    this.autoClose = (AutoCloseParameters)parameter;
                }
                else if (parameter is DesignParameters)
                {
                    // Design parameters
                    this.design = (DesignParameters)parameter;
                }
                else if (parameter is FontParameters)
                {
                    // Font parameters
                    this.fontParameters = (FontParameters)parameter;
                }
                else if (parameter is Font)
                {
                    // Direct font parameter - use for both message and title
                    this.fontParameters = new FontParameters((Font)parameter);
                }
                else if (parameter is InformationBoxTitleIconStyle)
                {
                    // Title style
                    this.titleStyle = (InformationBoxTitleIconStyle)parameter;
                }
                else if (parameter is InformationBoxTitleIcon)
                {
                    // Title icon
                    this.titleIcon = ((InformationBoxTitleIcon)parameter).Icon;
                }
                else if (parameter is MessageBoxButtons?)
                {
                    // MessageBox buttons
                    this.buttons = MessageBoxEnumConverter.Parse((MessageBoxButtons)parameter);
                }
                else if (parameter is MessageBoxIcon?)
                {
                    // MessageBox icon
                    this.icon = MessageBoxEnumConverter.Parse((MessageBoxIcon)parameter);
                }
                else if (parameter is MessageBoxDefaultButton?)
                {
                    // MessageBox default button
                    this.defaultButton = MessageBoxEnumConverter.Parse((MessageBoxDefaultButton)parameter);
                }
                else if (parameter is InformationBoxBehavior)
                {
                    // InformationBox behaviour
                    this.behavior = (InformationBoxBehavior)parameter;
                }
                else if (parameter is AsyncResultCallback)
                {
                    // Callback for the result
                    this.callback = (AsyncResultCallback)parameter;
                }
                else if (parameter is InformationBoxOpacity)
                {
                    // Opacity
                    this.opacity = (InformationBoxOpacity)parameter;
                }
                else if (parameter is Form)
                {
                    // Form parent
                    this.Parent = (Form)Parent;
                }
                else if (parameter is InformationBoxOrder)
                {
                    // z-order
                    this.order = (InformationBoxOrder)parameter;
                }
                else if (parameter is InformationBoxSound)
                {
                    // Sound
                    this.sound = (InformationBoxSound)parameter;
                }
            }
        }

        #endregion Constructors

        #region Show

        /// <summary>
        /// Shows this InformationBox.
        /// </summary>
        /// <returns>The result corresponding to the button clicked</returns>
        internal new InformationBoxResult Show()
        {
            this.SetCheckBox();
            this.SetButtons();
            this.SetFont();
            this.SetText();
            this.SetIcon();
            this.SetLayout();
            this.SetFocus();
            this.SetPosition();
            this.SetWindowStyle();
            this.SetAutoClose();
            this.SetOpacity();
            this.PlaySound();
            this.SetOrder();

            if (this.behavior == InformationBoxBehavior.Modal)
            {
                ShowDialog();
            }
            else
            {
                base.Show();
            }

            return this.result;
        }

        /// <summary>
        /// Shows this InformationBox.
        /// </summary>
        /// <param name="state">The state of the checkbox.</param>
        /// <returns>The result corresponding to the button clicked</returns>
        internal InformationBoxResult Show(out CheckState state)
        {
            this.result = this.Show();
            state = this.chbDoNotShow.CheckState;
            return this.result;
        }

        #endregion Show

        #region Sound

        /// <summary>
        /// Plays the sound associated with the icon type.
        /// </summary>
        private void PlaySound()
        {
            if (sound == InformationBoxSound.None)
            {
                return;
            }

            SystemSound soundToPlay;

            if (this.iconType == IconType.UserDefined)
            {
                soundToPlay = SystemSounds.Beep;
            }
            else
            {
                switch (IconHelper.GetCategory(this.icon))
                {
                    case InformationBoxMessageCategory.Asterisk:
                        soundToPlay = SystemSounds.Asterisk;
                        break;
                    case InformationBoxMessageCategory.Exclamation:
                        soundToPlay = SystemSounds.Exclamation;
                        break;
                    case InformationBoxMessageCategory.Hand:
                        soundToPlay = SystemSounds.Hand;
                        break;
                    case InformationBoxMessageCategory.Question:
                        soundToPlay = SystemSounds.Question;
                        break;
                    default:
                        soundToPlay = SystemSounds.Beep;
                        break;
                }
            }

            if (null != soundToPlay)
            {
                soundToPlay.Play();
            }
        }

        #endregion Sound

        /// <summary>
        /// Scales a 96-DPI pixel value to the current monitor DPI.
        /// </summary>
        private int ScaleDpi(int value) => (int)Math.Round(value * this.dpiScale);

        #region Box initialization

        /// <summary>
        /// Loads the current scope.
        /// </summary>
        private void LoadCurrentScope()
        {
            if (InformationBoxScope.Current == null)
            {
                return;
            }

            InformationBoxScopeParameters parameters = InformationBoxScope.Current.Parameters;

            if (parameters.Icon.HasValue)
            {
                this.icon = parameters.Icon.Value;
            }

            if (parameters.CustomIcon != null)
            {
                this.iconType = IconType.UserDefined;
                this.customIcon = parameters.CustomIcon;
            }

            if (parameters.Buttons.HasValue)
            {
                this.buttons = parameters.Buttons.Value;
            }

            if (parameters.DefaultButton.HasValue)
            {
                this.defaultButton = parameters.DefaultButton.Value;
            }

            if (parameters.Layout.HasValue)
            {
                this.buttonsLayout = parameters.Layout.Value;
            }

            if (parameters.AutoSizeMode.HasValue)
            {
                this.autoSizeMode = parameters.AutoSizeMode.Value;
            }

            if (parameters.Position.HasValue)
            {
                this.position = parameters.Position.Value;
            }

            if (parameters.CheckBox.HasValue)
            {
                this.checkBox = parameters.CheckBox.Value;
            }

            if (parameters.Style.HasValue)
            {
                this.style = parameters.Style.Value;
            }

            if (parameters.AutoClose != null)
            {
                this.autoClose = parameters.AutoClose;
            }

            if (parameters.Design != null)
            {
                this.design = parameters.Design;
            }

            if (parameters.Font != null)
            {
                this.fontParameters = parameters.Font;
            }

            if (parameters.TitleIconStyle.HasValue)
            {
                this.titleStyle = parameters.TitleIconStyle.Value;
            }

            if (parameters.TitleIcon != null)
            {
                this.titleIcon = parameters.TitleIcon;
            }

            if (parameters.Behavior.HasValue)
            {
                this.behavior = parameters.Behavior.Value;
            }

            if (parameters.Opacity.HasValue)
            {
                this.opacity = parameters.Opacity.Value;
            }

            if (parameters.Help.HasValue)
            {
                this.showHelpButton = parameters.Help.Value;
            }

            if (parameters.HelpNavigator.HasValue)
            {
                this.helpNavigator = parameters.HelpNavigator.Value;
            }

            if (parameters.Order.HasValue)
            {
                this.order = parameters.Order.Value;
            }

            if (parameters.Sound.HasValue)
            {
                this.sound = parameters.Sound.Value;
            }
        }

        #region Auto close

        /// <summary>
        /// Sets the auto close parameters.
        /// </summary>
        private void SetAutoClose()
        {
            if (null == this.autoClose)
            {
                return;
            }

            this.tmrAutoClose.Interval = 1000;
            this.tmrAutoClose.Tick += this.TmrAutoClose_Tick;
            this.tmrAutoClose.Start();
        }

        #endregion Auto close

        #region Opacity

        /// <summary>
        /// Sets the opacity.
        /// </summary>
        private void SetOpacity()
        {
            switch (this.opacity)
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
            if (this.style == InformationBoxStyle.Modern)
            {
                Color barsBackColor = Color.Black;
                Color formBackColor = Color.Silver;

                if (null != this.design)
                {
                    barsBackColor = this.design.BarsBackColor;
                    formBackColor = this.design.FormBackColor;
                }

                this.pnlForm.BackColor = formBackColor;
                this.messageText.BackColor = formBackColor;

                this.pnlButtons.BackColor = barsBackColor;
                this.lblTitle.BackColor = barsBackColor;

                FormBorderStyle = FormBorderStyle.None;
                this.lblTitle.Visible = true;

                foreach (Controls.Button button in this.pnlButtons.Controls)
                {
                    button.BackColor = barsBackColor;
                }
            }
            else if (this.style == InformationBoxStyle.Standard)
            {
                Color barsBackColor = SystemColors.Control;
                Color formBackColor = SystemColors.Control;

                if (null != this.design)
                {
                    barsBackColor = this.design.BarsBackColor;
                    formBackColor = this.design.FormBackColor;
                }

                this.pnlButtons.BackColor = barsBackColor;
                this.pnlForm.BackColor = formBackColor;
                this.messageText.BackColor = formBackColor;

                FormBorderStyle = FormBorderStyle.FixedDialog;
                this.lblTitle.Visible = false;
                this.pnlMain.Top -= this.lblTitle.Height;
                this.pnlButtons.SideBorder = SideBorder.None;
            }
        }

        #endregion Style

        #region CheckBox

        /// <summary>
        /// Sets the check box.
        /// </summary>
        private void SetCheckBox()
        {
            this.chbDoNotShow.Text = this.doNotShowAgainText ?? Resources.LabelDoNotShow;

            this.chbDoNotShow.Visible = ((this.checkBox & InformationBoxCheckBox.Show) == InformationBoxCheckBox.Show);
            this.chbDoNotShow.Checked = ((this.checkBox & InformationBoxCheckBox.Checked) == InformationBoxCheckBox.Checked);
            this.chbDoNotShow.TextAlign = ((this.checkBox & InformationBoxCheckBox.RightAligned) == InformationBoxCheckBox.RightAligned)
                                         ? ContentAlignment.BottomRight
                                         : ContentAlignment.BottomLeft;
            this.chbDoNotShow.CheckAlign = ((this.checkBox & InformationBoxCheckBox.RightAligned) == InformationBoxCheckBox.RightAligned)
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
            if (this.position == InformationBoxPosition.CenterOnScreen)
            {
                StartPosition = FormStartPosition.CenterScreen;
                CenterToScreen();
            }
            else
            {
                if (this.Parent != null && ((Form)this.Parent).IsMdiChild)
                {
                    StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    StartPosition = FormStartPosition.CenterParent;
                }
                CenterToParent();
            }
        }

        #endregion Position

        #region Focus

        /// <summary>
        /// Sets the focus.
        /// </summary>
        private void SetFocus()
        {
            if (this.defaultButton == InformationBoxDefaultButton.Button1 && this.pnlButtons.Controls.Count > 0)
            {
                this.pnlButtons.Controls[0].Select();
            }

            if (this.defaultButton == InformationBoxDefaultButton.Button2 && this.pnlButtons.Controls.Count > 1)
            {
                this.pnlButtons.Controls[1].Select();
            }

            if (this.defaultButton == InformationBoxDefaultButton.Button3 && this.pnlButtons.Controls.Count > 2)
            {
                this.pnlButtons.Controls[2].Select();
            }
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
            this.pnlScrollText.AutoScroll = false;

            #region Width

            // Caption width including button
            int captionWidth = TextRenderer.MeasureText(Text, SystemFonts.CaptionFont, Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix).Width + ScaleDpi(30);
            if (this.titleStyle != InformationBoxTitleIconStyle.None)
            {
                captionWidth += ScaleDpi(BorderPadding) * 2;
            }

            // "Do not show this dialog again" width
            int checkBoxWidth = ((this.checkBox & InformationBoxCheckBox.Show) == InformationBoxCheckBox.Show)
                                    ? TextRenderer.MeasureText(this.chbDoNotShow.Text, this.chbDoNotShow.Font, Size.Empty, TextFormatFlags.NoPadding).Width + ScaleDpi(BorderPadding) * 4
                                    : 0;

            // Width of the text and icon.
            int iconAndTextWidth = 0;

            // Minimum width to display all needed buttons.
            int buttonsMinWidth = (this.pnlButtons.Controls.Count + 4) * ScaleDpi(BorderPadding);
            foreach (Control ctrl in this.pnlButtons.Controls)
            {
                buttonsMinWidth += ctrl.Width;
            }

            // Icon width
            if (this.icon != InformationBoxIcon.None || this.iconType == IconType.UserDefined)
            {
                iconAndTextWidth += ScaleDpi(IconPanelWidth);
            }

            // Text width
            iconAndTextWidth += this.messageText.Width + ScaleDpi(BorderPadding) * 2;

            // Gets the maximum size
            totalWidth = Math.Max(Math.Max(Math.Max(buttonsMinWidth, iconAndTextWidth), captionWidth), checkBoxWidth);

            #endregion Width

            #region Height

            if ((this.checkBox & InformationBoxCheckBox.Show) != InformationBoxCheckBox.Show)
            {
                this.chbDoNotShow.Visible = false;
            }

            int iconHeight = 0;
            if (this.icon != InformationBoxIcon.None || this.iconType == IconType.UserDefined)
            {
                iconHeight = this.pcbIcon.Height;
            }

            int textHeight = this.messageText.Height;

            totalHeight = Math.Max(iconHeight, textHeight) + ScaleDpi(BorderPadding) * 2 + this.pnlBas.Height;

            // Add a small space to avoid vertical scrollbar.
            if (iconAndTextWidth > Screen.PrimaryScreen.WorkingArea.Width - ScaleDpi(100))
            {
                totalHeight += ScaleDpi(20);
            }

            bool verticalScroll = false;
            if (totalHeight > Screen.PrimaryScreen.WorkingArea.Height - ScaleDpi(50))
            {
                totalHeight = Screen.PrimaryScreen.WorkingArea.Height - ScaleDpi(50);
                totalWidth += ScaleDpi(20);
                this.messageText.Top = ScaleDpi(BorderPadding);
                verticalScroll = true;
                this.pnlScrollText.AutoScroll = true;
            }

            this.pnlMain.Size = new Size(Math.Min(Screen.PrimaryScreen.WorkingArea.Width - ScaleDpi(20), totalWidth), totalHeight - this.pnlBas.Height);

            if (this.style == InformationBoxStyle.Modern)
            {
                totalHeight += this.lblTitle.Height;
            }

            #endregion Height

            // Sets the size
            ClientSize = new Size(Math.Min(Screen.PrimaryScreen.WorkingArea.Width - ScaleDpi(20), totalWidth), totalHeight);

            #region Position

            // Set new position for all components
            // Icon
            this.pcbIcon.Left = ScaleDpi(BorderPadding);
            this.pcbIcon.Top = ScaleDpi(BorderPadding);

            // Text
            this.pnlScrollText.Width = ClientSize.Width - ((this.icon != InformationBoxIcon.None || this.iconType == IconType.UserDefined)
                                       ? ScaleDpi(IconPanelWidth) + ScaleDpi(BorderPadding) + ScaleDpi(5)
                                       : ScaleDpi(BorderPadding));
            this.messageText.Left = 0;

            if (this.messageText.Width > this.pnlScrollText.ClientSize.Width)
            {
                verticalScroll = true;
                this.pnlScrollText.AutoScroll = true;
            }

            if (!verticalScroll)
            {
                this.messageText.Top = Convert.ToInt32((this.pnlText.Height - this.messageText.Height) / 2);
            }

            // Buttons
            this.SetButtonsLayout();

            #endregion Position
        }

        /// <summary>
        /// Sets the buttons layout.
        /// </summary>
        private void SetButtonsLayout()
        {
            // Do not count the checkbox
            int buttonsCount = this.pnlButtons.Controls.Count;
            int index = 0;
            int initialPosition = 0;
            int spaceBetween = 0;
            switch (this.buttonsLayout)
            {
                case InformationBoxButtonsLayout.GroupLeft:
                    spaceBetween = ScaleDpi(BorderPadding);
                    initialPosition = ScaleDpi(BorderPadding);
                    break;
                case InformationBoxButtonsLayout.GroupMiddle:
                    spaceBetween = ScaleDpi(BorderPadding);

                    // If there is only one button then we must center it
                    if (buttonsCount == 1)
                    {
                        initialPosition += Convert.ToInt32((this.pnlButtons.ClientSize.Width - this.pnlButtons.Controls[0].Width) / 2);
                    }
                    else
                    {
                        initialPosition = Convert.ToInt32((this.pnlButtons.ClientSize.Width - (buttonsCount * (this.pnlButtons.Controls[0].Width + ScaleDpi(BorderPadding)))) / 2);
                    }

                    break;
                case InformationBoxButtonsLayout.GroupRight:
                    spaceBetween = ScaleDpi(BorderPadding);
                    initialPosition = this.pnlButtons.ClientSize.Width - (buttonsCount * (this.pnlButtons.Controls[0].Width + ScaleDpi(BorderPadding)));
                    break;
                case InformationBoxButtonsLayout.Separate:
                    spaceBetween = Convert.ToInt32((this.pnlButtons.ClientSize.Width - buttonsCount * this.pnlButtons.Controls[0].Width) / (buttonsCount + 1));
                    initialPosition = spaceBetween;
                    break;
                default:
                    break;
            }

            foreach (Control ctrl in this.pnlButtons.Controls)
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
            if (this.iconType == IconType.Internal)
            {
                if (this.icon == InformationBoxIcon.None)
                {
                    this.pnlIcon.Visible = false;
                    this.pcbIcon.Image = null;
                }
                else
                {
                    this.pnlIcon.Visible = true;
                    this.pcbIcon.Image = IconHelper.FromEnum(this.icon).ToBitmap();
                }
            }
            else
            {
                this.pcbIcon.Image = new Icon(this.customIcon, ScaleDpi(48), ScaleDpi(48)).ToBitmap();
                this.pnlIcon.Visible = true;
            }

            this.pnlIcon.Width = ScaleDpi(IconPanelWidth);

            if (this.titleStyle == InformationBoxTitleIconStyle.None)
            {
                ShowIcon = false;
                Icon = Resources.IconBlank;
            }
            else if (this.titleStyle == InformationBoxTitleIconStyle.SameAsBox)
            {
                if (this.iconType == IconType.Internal)
                {
                    Icon = IconHelper.FromEnum(this.icon);
                }
                else
                {
                    Icon = this.customIcon;
                }
            }
            else if (this.titleStyle == InformationBoxTitleIconStyle.Custom)
            {
                Icon = this.titleIcon;
            }
        }

        #endregion Icon

        #region Z-Order

        /// <summary>
        /// Sets the order.
        /// </summary>
        private void SetOrder()
        {
            if (this.order == InformationBoxOrder.TopMost)
            {
                this.TopMost = true;
            }
        }

        #endregion Z-Order

        #region Font

        /// <summary>
        /// Sets the font.
        /// </summary>
        private void SetFont()
        {
            if (this.fontParameters != null)
            {
                if (this.fontParameters.HasFont())
                {
                    this.messageText.Font = this.fontParameters.MessageFont;
                }

                if (this.fontParameters.HasColor())
                {
                    this.messageText.ForeColor = this.fontParameters.MessageColor.Value;
                }
            }
        }

        #endregion Font

        #region Text

        /// <summary>
        /// Sets the text.
        /// </summary>
        private void SetText()
        {
            Screen currentScreen = Screen.FromControl(this);
            int screenWidth = currentScreen.WorkingArea.Width;

            var internalText = String.Empty;
            internalText = TextHelper.NormalizeLineBreaks(this.messageText.Text);                       

            if (this.autoSizeMode == InformationBoxAutoSizeMode.FitToText)
            {
                this.messageText.WordWrap = false;
                this.messageText.Text = internalText.ToString();
                this.messageText.Size = TextRenderer.MeasureText(internalText, this.messageText.Font, Size.Empty, TextFormatFlags.NoPadding);
            }
            else
            {
                if (this.autoSizeMode == InformationBoxAutoSizeMode.None)
                {
                    this.messageText.WordWrap = true;
                    this.messageText.Text = internalText.ToString();
                    this.messageText.Size = TextRenderer.MeasureText(internalText, this.messageText.Font, new Size(screenWidth / 2, 0), TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
                }
                else
                {
                    if (this.autoSizeMode == InformationBoxAutoSizeMode.MinimumHeight)
                    {
                        // Remove line breaks.
                        internalText = TextHelper.ReplaceLineBreaksWithSpaces(internalText);
                        var sentences = TextHelper.SplitTextIntoSentences(internalText);

                        StringBuilder formattedText = new StringBuilder();
                        int currentWidth = 0;

                        foreach (Match sentence in sentences)
                        {
                            // FIX: In case an icon is configured, the maximum width of the text should be reduced to accomodate the icon width and avoid the horizontal scrollbar.
                            int sentenceLength = TextRenderer.MeasureText(sentence.Value, this.messageText.Font, Size.Empty, TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding).Width;
                            if (currentWidth != 0 && (sentenceLength + currentWidth) > (screenWidth - ScaleDpi(50)))
                            {
                                formattedText.Append(Environment.NewLine);
                                currentWidth = 0;
                            }

                            currentWidth += sentenceLength;
                            formattedText.Append(sentence.Value);
                        }

                        internalText = formattedText.ToString();
                    }
                    else if (this.autoSizeMode == InformationBoxAutoSizeMode.MinimumWidth)
                    {
                        internalText = TextHelper.AddLineBreaksAfterPunctuation(internalText);
                    }

                    this.messageText.Text = internalText.ToString();
                    this.messageText.Size = TextRenderer.MeasureText(this.messageText.Text, this.messageText.Font, Size.Empty, TextFormatFlags.TextBoxControl);
                }
            }

            this.messageText.Width += ScaleDpi(BorderPadding);
        }

        #endregion Text

        #region Buttons

        /// <summary>
        /// Sets the buttons, order of addition is respected.
        /// </summary>
        private void SetButtons()
        {
            // Abort button
            if (this.buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                this.AddButton("Abort", Resources.LabelAbort);
            }

            // Ok
            if (this.buttons == InformationBoxButtons.OK ||
                this.buttons == InformationBoxButtons.OKCancel ||
                this.buttons == InformationBoxButtons.OKCancelUser1)
            {
                this.AddButton("OK", Resources.LabelOK);
            }

            // Yes
            if (this.buttons == InformationBoxButtons.YesNo ||
                this.buttons == InformationBoxButtons.YesNoCancel ||
                this.buttons == InformationBoxButtons.YesNoUser1)
            {
                this.AddButton("Yes", Resources.LabelYes);
            }

            // Retry
            if (this.buttons == InformationBoxButtons.AbortRetryIgnore ||
                this.buttons == InformationBoxButtons.RetryCancel)
            {
                this.AddButton("Retry", Resources.LabelRetry);
            }

            // No
            if (this.buttons == InformationBoxButtons.YesNo ||
                this.buttons == InformationBoxButtons.YesNoCancel ||
                this.buttons == InformationBoxButtons.YesNoUser1)
            {
                this.AddButton("No", Resources.LabelNo);
            }

            // Cancel
            if (this.buttons == InformationBoxButtons.OKCancel ||
                this.buttons == InformationBoxButtons.OKCancelUser1 ||
                this.buttons == InformationBoxButtons.RetryCancel ||
                this.buttons == InformationBoxButtons.YesNoCancel)
            {
                this.AddButton("Cancel", Resources.LabelCancel);
            }

            // Ignore
            if (this.buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                this.AddButton("Ignore", Resources.LabelIgnore);
            }

            // User1
            if (this.buttons == InformationBoxButtons.OKCancelUser1 ||
                this.buttons == InformationBoxButtons.User1User2User3 ||
                this.buttons == InformationBoxButtons.User1User2 ||
                this.buttons == InformationBoxButtons.YesNoUser1 ||
                this.buttons == InformationBoxButtons.User1)
            {
                this.AddButton("User1", this.buttonUser1Text);
            }

            // User2
            if (this.buttons == InformationBoxButtons.User1User2 ||
                this.buttons == InformationBoxButtons.User1User2User3)
            {
                this.AddButton("User2", this.buttonUser2Text);
            }

            // User3
            if (this.buttons == InformationBoxButtons.User1User2User3)
            {
                this.AddButton("User3", this.buttonUser3Text);
            }

            // Help button is displayed when asked or when a help file name exists
            if (this.showHelpButton || !String.IsNullOrEmpty(this.helpFile))
            {
                this.AddButton("Help", Resources.LabelHelp);
            }

            this.SetButtonsSize();
        }

        /// <summary>
        /// Sets the buttons size.
        /// </summary>
        private void SetButtonsSize()
        {
            // All button will have the same size
            int maxSize = 0;

            // Measures the width of each button
            foreach (Control ctrl in this.pnlButtons.Controls)
            {
                maxSize = Math.Max(TextRenderer.MeasureText(ctrl.Text, ctrl.Font, Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix).Width + ScaleDpi(40), maxSize);
            }

            foreach (Control ctrl in this.pnlButtons.Controls)
            {
                if (this.style == InformationBoxStyle.Standard)
                {
                    ctrl.Size = new Size(maxSize, ScaleDpi(23));
                    ctrl.Top = ScaleDpi(5);
                }
                else if (this.style == InformationBoxStyle.Modern)
                {
                    ctrl.Size = new Size(maxSize, this.pnlButtons.Height);
                    ctrl.Top = 0;
                }
            }
        }

        /// <summary>
        /// Adds the button.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="text">The text of the button.</param>
        private void AddButton(string name, string text)
        {
            Control buttonToAdd;

            if (this.style == InformationBoxStyle.Modern)
            {
                buttonToAdd = new Controls.Button();
                (buttonToAdd as Controls.Button).PersistantMode = false;
                (buttonToAdd as Controls.Button).Click += this.Button_Click;
            }
            else
            {
                buttonToAdd = new System.Windows.Forms.Button();
                (buttonToAdd as System.Windows.Forms.Button).FlatStyle = FlatStyle.System;
                (buttonToAdd as System.Windows.Forms.Button).UseVisualStyleBackColor = true;
                (buttonToAdd as System.Windows.Forms.Button).Click += this.Button_Click;
            }

            buttonToAdd.Font = SystemFonts.MessageBoxFont;
            buttonToAdd.Name = name;
            buttonToAdd.Text = text;
            this.pnlButtons.Controls.Add(buttonToAdd);
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
                    this.result = InformationBoxResult.Abort;
                    break;
                case "OK":
                    this.result = InformationBoxResult.OK;
                    break;
                case "Yes":
                    this.result = InformationBoxResult.Yes;
                    break;
                case "Retry":
                    this.result = InformationBoxResult.Retry;
                    break;
                case "No":
                    this.result = InformationBoxResult.No;
                    break;
                case "Cancel":
                    this.result = InformationBoxResult.Cancel;
                    break;
                case "Ignore":
                    this.result = InformationBoxResult.Ignore;
                    break;
                case "User1":
                    this.result = InformationBoxResult.User1;
                    break;
                case "User2":
                    this.result = InformationBoxResult.User2;
                    break;
                case "User3":
                    this.result = InformationBoxResult.User3;
                    break;
                default:
                    this.result = InformationBoxResult.None;
                    break;
            }

            if (senderControl.Name.Equals("Help"))
            {
                this.OpenHelp();
            }
            else
            {
                DialogResult = DialogResult.OK;
                if (this.behavior == InformationBoxBehavior.Modeless)
                {
                    Close();
                }
            }
        }

        #endregion Button click

        #region Help

        /// <summary>
        /// Opens the help.
        /// </summary>
        private void OpenHelp()
        {
            // If there is an active form
            if (null != this.activeForm)
            {
                MethodInfo met = this.activeForm.GetType().GetMethod("OnHelpRequested", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
                if (null != met)
                {
                    // Call for help on the active form.
                    met.Invoke(this.activeForm, new object[] { new HelpEventArgs(MousePosition) });
                }
            }

            // If a help file is specified
            if (!String.IsNullOrEmpty(this.helpFile))
            {
                // If no topic is specified
                if (String.IsNullOrEmpty(this.helpTopic))
                {
                    Help.ShowHelp(this.activeForm, this.helpFile, this.helpNavigator);
                }
                else
                {
                    Help.ShowHelp(this.activeForm, this.helpFile, this.helpTopic);
                }
            }
        }

        #endregion Help

        #region Event handling

        /// <summary>
        /// Handles DPI changes (e.g. moving the form to a monitor with different scaling).
        /// </summary>
        protected override void OnDpiChanged(DpiChangedEventArgs e)
        {
            base.OnDpiChanged(e);
            this.dpiScale = e.DeviceDpiNew / 96f;
            this.pnlScrollText.AutoScrollPosition = Point.Empty;
            this.SetButtonsSize();
            this.SetText();
            this.SetIcon();
            this.SetLayout();
        }

        /// <summary>
        /// Handles the Click event of the buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                this.HandleButton((Control)sender);
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the InformationBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void InformationBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.result == InformationBoxResult.None)
            {
                this.result = InformationBoxResult.Cancel;
            }

            if (this.behavior == InformationBoxBehavior.Modeless && null != this.callback)
            {
                Invoke(this.callback, this.result);
            }
        }

        /// <summary>
        /// Handles the Paint event of the pnlForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void PnlForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.style == InformationBoxStyle.Modern)
            {
                ControlPaint.DrawBorder(e.Graphics, this.pnlForm.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the lblTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void LblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.lastPointerPosition = e.Location;
                this.mouseDown = true;
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the lblTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void LblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouseDown)
            {
                return;
            }

            Point location = DesktopLocation;

            location.Offset(new Point(e.Location.X - this.lastPointerPosition.X, e.Location.Y - this.lastPointerPosition.Y));

            DesktopLocation = location;
        }

        /// <summary>
        /// Handles the MouseUp event of the lblTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void LblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = false;
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
        }

        /// <summary>
        /// Handles the Tick event of the tmrAutoClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TmrAutoClose_Tick(object sender, EventArgs e)
        {
            if (this.elapsedTime == this.autoClose.Seconds)
            {
                this.tmrAutoClose.Stop();

                switch (this.autoClose.Mode)
                {
                    case AutoCloseDefinedParameters.Button:
                        if (this.autoClose.DefaultButton == InformationBoxDefaultButton.Button1 && this.pnlButtons.Controls.Count > 0)
                        {
                            this.HandleButton(this.pnlButtons.Controls[0]);
                        }
                        else if (this.autoClose.DefaultButton == InformationBoxDefaultButton.Button2 && this.pnlButtons.Controls.Count > 1)
                        {
                            this.HandleButton(this.pnlButtons.Controls[1]);
                        }
                        else if (this.autoClose.DefaultButton == InformationBoxDefaultButton.Button3 && this.pnlButtons.Controls.Count > 2)
                        {
                            this.HandleButton(this.pnlButtons.Controls[2]);
                        }

                        return;
                    case AutoCloseDefinedParameters.TimeOnly:
                        if (this.defaultButton == InformationBoxDefaultButton.Button1 && this.pnlButtons.Controls.Count > 0)
                        {
                            this.HandleButton(this.pnlButtons.Controls[0]);
                        }
                        else if (this.defaultButton == InformationBoxDefaultButton.Button2 && this.pnlButtons.Controls.Count > 1)
                        {
                            this.HandleButton(this.pnlButtons.Controls[1]);
                        }
                        else if (this.defaultButton == InformationBoxDefaultButton.Button3 && this.pnlButtons.Controls.Count > 2)
                        {
                            this.HandleButton(this.pnlButtons.Controls[2]);
                        }

                        return;
                    case AutoCloseDefinedParameters.Result:
                        this.result = this.autoClose.Result;
                        DialogResult = DialogResult.OK;
                        return;
                    default:
                        break;
                }
            }
            else
            {
                Control buttonToUpdate = null;
                if (this.autoClose.Mode == AutoCloseDefinedParameters.Button)
                {
                    if (this.autoClose.DefaultButton == InformationBoxDefaultButton.Button1 && this.pnlButtons.Controls.Count > 0)
                    {
                        buttonToUpdate = this.pnlButtons.Controls[0];
                    }
                    else if (this.autoClose.DefaultButton == InformationBoxDefaultButton.Button2 && this.pnlButtons.Controls.Count > 1)
                    {
                        buttonToUpdate = this.pnlButtons.Controls[1];
                    }
                    else if (this.autoClose.DefaultButton == InformationBoxDefaultButton.Button3 && this.pnlButtons.Controls.Count > 2)
                    {
                        buttonToUpdate = this.pnlButtons.Controls[2];
                    }
                }
                else
                {
                    if (this.defaultButton == InformationBoxDefaultButton.Button1 && this.pnlButtons.Controls.Count > 0)
                    {
                        buttonToUpdate = this.pnlButtons.Controls[0];
                    }
                    else if (this.defaultButton == InformationBoxDefaultButton.Button2 && this.pnlButtons.Controls.Count > 1)
                    {
                        buttonToUpdate = this.pnlButtons.Controls[1];
                    }
                    else if (this.defaultButton == InformationBoxDefaultButton.Button3 && this.pnlButtons.Controls.Count > 2)
                    {
                        buttonToUpdate = this.pnlButtons.Controls[2];
                    }
                }

                if (null != buttonToUpdate)
                {
                    Regex extractLabel = new Regex(@".*?\(\d+\)");

                    if (buttonToUpdate is System.Windows.Forms.Button)
                    {
                        System.Windows.Forms.Button button = (System.Windows.Forms.Button)buttonToUpdate;
                        if (extractLabel.IsMatch(button.Text))
                        {
                            button.Text = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", button.Text.Substring(0, button.Text.LastIndexOf(" (", StringComparison.OrdinalIgnoreCase)), this.autoClose.Seconds - this.elapsedTime);
                        }
                        else
                        {
                            button.Text = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", button.Text, this.autoClose.Seconds - this.elapsedTime);
                        }
                    }
                    else if (buttonToUpdate is Controls.Button)
                    {
                        Controls.Button button = (Controls.Button)buttonToUpdate;
                        if (extractLabel.IsMatch(button.Text))
                        {
                            button.Text = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", button.Text.Substring(0, button.Text.LastIndexOf(" (", StringComparison.OrdinalIgnoreCase)), this.autoClose.Seconds - this.elapsedTime);
                        }
                        else
                        {
                            button.Text = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", button.Text, this.autoClose.Seconds - this.elapsedTime);
                        }
                    }
                }
            }

            this.elapsedTime++;
        }

        #endregion Event handling
    }
}
