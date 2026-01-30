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
        /// Contains the viewmodel holding all configuration and business logic
        /// </summary>
        private readonly InformationBoxViewModel viewModel;

        /// <summary>
        /// Contains a reference to the active form
        /// </summary>
        private readonly Form activeForm;

        /// <summary>
        /// Result corresponding the clicked button
        /// </summary>
        private InformationBoxResult result = InformationBoxResult.None;

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

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxForm"/>.
        /// </summary>
        private InformationBoxForm()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxForm"/> class using a pre-built viewmodel.
        /// </summary>
        /// <param name="viewModel">The viewmodel containing all configuration.</param>
        internal InformationBoxForm(InformationBoxViewModel viewModel)
        {
            this.InitializeComponent();
            this.dpiScale = this.DeviceDpi / 96f;

            this.Font = SystemFonts.MessageBoxFont;
            this.messageText.Font = SystemFonts.MessageBoxFont;
            this.lblTitle.Font = SystemFonts.CaptionFont;

            this.viewModel = viewModel;
            this.activeForm = ActiveForm;

            this.messageText.Text = viewModel.Text;
            this.Text = viewModel.Title;
            this.lblTitle.Text = viewModel.Title;
            this.Parent = viewModel.Parent;
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
            : this(ParameterParser.ParseNamed(text, title, helpFile, helpTopic, initialization, buttons, icon, customIcon,
                defaultButton, customButtons, buttonsLayout, autoSizeMode, position, showHelpButton, helpNavigator,
                showDoNotShowAgainCheckBox, doNotShowAgainText, style, autoClose, design, fontParameters, null, titleStyle,
                titleIcon, legacyButtons, legacyIcon, legacyDefaultButton, behavior, callback, opacity, parent, order, sound))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxForm"/> class.
        /// </summary>
        /// <param name="text">The text of the box.</param>
        /// <param name="parameters">The parameters.</param>
        internal InformationBoxForm(string text, params object[] parameters)
            : this(ParameterParser.Parse(text, parameters))
        {
        }

        #endregion Constructors

        #region Show

        /// <summary>
        /// Shows this InformationBox.
        /// </summary>
        /// <returns>The result corresponding to the button clicked</returns>
        internal new InformationBoxResult Show()
        {
            this.ApplyCheckBox();
            this.ApplyButtons();
            this.ApplyFont();
            this.ApplyText();
            this.ApplyIcon();
            this.ApplyLayout();
            this.ApplyFocus();
            this.ApplyPosition();
            this.ApplyWindowStyle();
            this.ApplyAutoClose();
            this.ApplyOpacity();
            this.PlaySound();
            this.ApplyOrder();

            if (this.viewModel.Behavior == InformationBoxBehavior.Modal)
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
            SystemSound soundToPlay = this.viewModel.GetSystemSound();
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

        #region Auto close

        /// <summary>
        /// Sets the auto close parameters.
        /// </summary>
        private void ApplyAutoClose()
        {
            if (null == this.viewModel.AutoClose)
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
        /// Applies the opacity.
        /// </summary>
        private void ApplyOpacity()
        {
            Opacity = this.viewModel.GetOpacityValue();
        }

        #endregion Opacity

        #region Style

        /// <summary>
        /// Applies the window style.
        /// </summary>
        private void ApplyWindowStyle()
        {
            var config = this.viewModel.GetWindowStyleConfiguration();

            this.pnlForm.BackColor = config.FormBackColor;
            this.messageText.BackColor = config.FormBackColor;
            this.pnlButtons.BackColor = config.BarsBackColor;
            this.lblTitle.BackColor = config.BarsBackColor;

            FormBorderStyle = config.BorderStyle;
            this.lblTitle.Visible = config.TitleLabelVisible;

            if (config.TitleLabelVisible)
            {
                foreach (Controls.Button button in this.pnlButtons.Controls)
                {
                    button.BackColor = config.BarsBackColor;
                }
            }

            if (config.AdjustPanelTop)
            {
                this.pnlMain.Top -= this.lblTitle.Height;
            }

            if (config.RemoveSideBorder)
            {
                this.pnlButtons.SideBorder = SideBorder.None;
            }
        }

        #endregion Style

        #region CheckBox

        /// <summary>
        /// Applies the check box configuration.
        /// </summary>
        private void ApplyCheckBox()
        {
            var config = this.viewModel.GetCheckBoxConfiguration();
            this.chbDoNotShow.Text = config.Text;
            this.chbDoNotShow.Visible = config.Visible;
            this.chbDoNotShow.Checked = config.Checked;
            this.chbDoNotShow.TextAlign = config.TextAlign;
            this.chbDoNotShow.CheckAlign = config.CheckAlign;
        }

        #endregion CheckBox

        #region Position

        /// <summary>
        /// Applies the position.
        /// </summary>
        private void ApplyPosition()
        {
            if (this.viewModel.Position == InformationBoxPosition.CenterOnScreen)
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
        /// Applies the focus.
        /// </summary>
        private void ApplyFocus()
        {
            if (this.viewModel.DefaultButton == InformationBoxDefaultButton.Button1 && this.pnlButtons.Controls.Count > 0)
            {
                this.pnlButtons.Controls[0].Select();
            }

            if (this.viewModel.DefaultButton == InformationBoxDefaultButton.Button2 && this.pnlButtons.Controls.Count > 1)
            {
                this.pnlButtons.Controls[1].Select();
            }

            if (this.viewModel.DefaultButton == InformationBoxDefaultButton.Button3 && this.pnlButtons.Controls.Count > 2)
            {
                this.pnlButtons.Controls[2].Select();
            }
        }

        #endregion Focus

        #region Layout

        /// <summary>
        /// Applies the layout.
        /// </summary>
        private void ApplyLayout()
        {
            int totalHeight;
            int totalWidth;
            this.pnlScrollText.AutoScroll = false;

            #region Width

            // Caption width including button
            int captionWidth = TextRenderer.MeasureText(Text, SystemFonts.CaptionFont, Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix).Width + ScaleDpi(30);
            if (this.viewModel.TitleStyle != InformationBoxTitleIconStyle.None)
            {
                captionWidth += ScaleDpi(BorderPadding) * 2;
            }

            // "Do not show this dialog again" width
            int checkBoxWidth = this.viewModel.HasCheckBox()
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
            if (this.viewModel.HasIcon())
            {
                iconAndTextWidth += ScaleDpi(IconPanelWidth);
            }

            // Text width
            iconAndTextWidth += this.messageText.Width + ScaleDpi(BorderPadding) * 2;

            // Gets the maximum size
            totalWidth = Math.Max(Math.Max(Math.Max(buttonsMinWidth, iconAndTextWidth), captionWidth), checkBoxWidth);

            #endregion Width

            #region Height

            if (!this.viewModel.HasCheckBox())
            {
                this.chbDoNotShow.Visible = false;
            }

            int iconHeight = 0;
            if (this.viewModel.HasIcon())
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

            if (this.viewModel.Style == InformationBoxStyle.Modern)
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
            this.pnlScrollText.Width = ClientSize.Width - (this.viewModel.HasIcon()
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
            this.ApplyButtonsLayout();

            #endregion Position
        }

        /// <summary>
        /// Applies the buttons layout.
        /// </summary>
        private void ApplyButtonsLayout()
        {
            // Do not count the checkbox
            int buttonsCount = this.pnlButtons.Controls.Count;
            int index = 0;
            int initialPosition = 0;
            int spaceBetween = 0;
            switch (this.viewModel.ButtonsLayout)
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
        /// Applies the icon configuration.
        /// </summary>
        private void ApplyIcon()
        {
            var config = this.viewModel.GetIconConfiguration(ScaleDpi(48));

            this.pnlIcon.Visible = config.IconPanelVisible;
            this.pcbIcon.Image = config.IconImage;
            this.pnlIcon.Width = ScaleDpi(IconPanelWidth);

            if (!config.ShowFormIcon)
            {
                ShowIcon = false;
            }

            if (config.FormIcon != null)
            {
                Icon = config.FormIcon;
            }
        }

        #endregion Icon

        #region Z-Order

        /// <summary>
        /// Applies the z-order.
        /// </summary>
        private void ApplyOrder()
        {
            if (this.viewModel.Order == InformationBoxOrder.TopMost)
            {
                this.TopMost = true;
            }
        }

        #endregion Z-Order

        #region Font

        /// <summary>
        /// Applies the font.
        /// </summary>
        private void ApplyFont()
        {
            if (this.viewModel.FontParameters != null)
            {
                if (this.viewModel.FontParameters.HasFont())
                {
                    this.messageText.Font = this.viewModel.FontParameters.MessageFont;
                }

                if (this.viewModel.FontParameters.HasColor())
                {
                    this.messageText.ForeColor = this.viewModel.FontParameters.MessageColor.Value;
                }
            }
        }

        #endregion Font

        #region Text

        /// <summary>
        /// Applies the text layout.
        /// </summary>
        private void ApplyText()
        {
            Screen currentScreen = Screen.FromControl(this);
            int screenWidth = currentScreen.WorkingArea.Width;

            var internalText = String.Empty;
            internalText = TextHelper.NormalizeLineBreaks(this.messageText.Text);

            if (this.viewModel.AutoSizeMode == InformationBoxAutoSizeMode.FitToText)
            {
                this.messageText.WordWrap = false;
                this.messageText.Text = internalText.ToString();
                this.messageText.Size = TextRenderer.MeasureText(internalText, this.messageText.Font, Size.Empty, TextFormatFlags.NoPadding);
            }
            else
            {
                if (this.viewModel.AutoSizeMode == InformationBoxAutoSizeMode.None)
                {
                    this.messageText.WordWrap = true;
                    this.messageText.Text = internalText.ToString();
                    this.messageText.Size = TextRenderer.MeasureText(internalText, this.messageText.Font, new Size(screenWidth / 2, 0), TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
                }
                else
                {
                    if (this.viewModel.AutoSizeMode == InformationBoxAutoSizeMode.MinimumHeight)
                    {
                        // Remove line breaks.
                        internalText = TextHelper.ReplaceLineBreaksWithSpaces(internalText);
                        var sentences = TextHelper.SplitTextIntoSentences(internalText);

                        StringBuilder formattedText = new StringBuilder();
                        int currentWidth = 0;

                        foreach (Match sentence in sentences)
                        {
                            int sentenceLength = TextRenderer.MeasureText(sentence.Value, this.messageText.Font, Size.Empty, TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding).Width;
                            if (currentWidth != 0 && (sentenceLength + currentWidth + this.pnlIcon.Width) > (screenWidth - ScaleDpi(50)))
                            {
                                formattedText.Append(Environment.NewLine);
                                currentWidth = 0;
                            }

                            currentWidth += sentenceLength;
                            formattedText.Append(sentence.Value);
                        }

                        internalText = formattedText.ToString();
                    }
                    else if (this.viewModel.AutoSizeMode == InformationBoxAutoSizeMode.MinimumWidth)
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
        /// Applies the buttons from the viewmodel definitions.
        /// </summary>
        private void ApplyButtons()
        {
            var definitions = this.viewModel.GetButtonDefinitions();
            foreach (var def in definitions)
            {
                this.AddButton(def.Name, def.Text);
            }

            this.ApplyButtonsSize();
        }

        /// <summary>
        /// Applies the buttons size.
        /// </summary>
        private void ApplyButtonsSize()
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
                if (this.viewModel.Style == InformationBoxStyle.Standard)
                {
                    ctrl.Size = new Size(maxSize, ScaleDpi(23));
                    ctrl.Top = ScaleDpi(5);
                }
                else if (this.viewModel.Style == InformationBoxStyle.Modern)
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

            if (this.viewModel.Style == InformationBoxStyle.Modern)
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

            if (this.viewModel.IsHelpButton(senderControl.Name))
            {
                this.OpenHelp();
            }
            else
            {
                this.result = this.viewModel.MapButtonNameToResult(senderControl.Name);
                DialogResult = DialogResult.OK;
                if (this.viewModel.Behavior == InformationBoxBehavior.Modeless)
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
            if (!String.IsNullOrEmpty(this.viewModel.HelpFile))
            {
                // If no topic is specified
                if (String.IsNullOrEmpty(this.viewModel.HelpTopic))
                {
                    Help.ShowHelp(this.activeForm, this.viewModel.HelpFile, this.viewModel.HelpNavigator);
                }
                else
                {
                    Help.ShowHelp(this.activeForm, this.viewModel.HelpFile, this.viewModel.HelpTopic);
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
            this.ApplyButtonsSize();
            this.ApplyText();
            this.ApplyIcon();
            this.ApplyLayout();
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

            if (this.viewModel.Behavior == InformationBoxBehavior.Modeless && null != this.viewModel.Callback)
            {
                Invoke(this.viewModel.Callback, this.result);
            }
        }

        /// <summary>
        /// Handles the Paint event of the pnlForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void PnlForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.viewModel.Style == InformationBoxStyle.Modern)
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
            var tickResult = this.viewModel.ProcessAutoCloseTick(this.elapsedTime, this.pnlButtons.Controls.Count);

            if (tickResult.ShouldStopTimer)
            {
                this.tmrAutoClose.Stop();
            }

            if (tickResult.ShouldClose)
            {
                if (tickResult.UseDirectResult)
                {
                    this.result = tickResult.DirectResult;
                    DialogResult = DialogResult.OK;
                }
                else if (tickResult.ButtonIndex >= 0)
                {
                    this.HandleButton(this.pnlButtons.Controls[tickResult.ButtonIndex]);
                }

                return;
            }

            // Update button text with countdown
            if (tickResult.ButtonIndex >= 0)
            {
                Control buttonToUpdate = this.pnlButtons.Controls[tickResult.ButtonIndex];
                int secondsRemaining = this.viewModel.GetAutoCloseSecondsRemaining(this.elapsedTime);
                buttonToUpdate.Text = this.viewModel.FormatAutoCloseButtonText(buttonToUpdate.Text, secondsRemaining);
            }

            this.elapsedTime++;
        }

        #endregion Event handling
    }
}
