namespace InfoBox.Internals
{
    using InfoBox.Properties;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Media;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal class InformationBoxViewModel
    {
        #region Properties

        public string Text { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string HelpFile { get; set; } = string.Empty;

        public string HelpTopic { get; set; } = string.Empty;

        public string ButtonUser1Text { get; set; } = "User1";

        public string ButtonUser2Text { get; set; } = "User2";

        public string ButtonUser3Text { get; set; } = "User3";

        public string DoNotShowAgainText { get; set; }

        public InformationBoxButtons Buttons { get; set; } = InformationBoxButtons.OK;

        public InformationBoxIcon Icon { get; set; } = InformationBoxIcon.None;

        public Icon CustomIcon { get; set; }

        public IconType IconType { get; set; } = IconType.Internal;

        public InformationBoxDefaultButton DefaultButton { get; set; } = InformationBoxDefaultButton.Button1;

        public InformationBoxButtonsLayout ButtonsLayout { get; set; } = InformationBoxButtonsLayout.GroupMiddle;

        public InformationBoxAutoSizeMode AutoSizeMode { get; set; } = InformationBoxAutoSizeMode.None;

        public InformationBoxPosition Position { get; set; } = InformationBoxPosition.CenterOnParent;

        public InformationBoxCheckBox CheckBox { get; set; } = 0;

        public InformationBoxStyle Style { get; set; } = InformationBoxStyle.Standard;

        public AutoCloseParameters AutoClose { get; set; }

        public DesignParameters Design { get; set; }

        public FontParameters FontParameters { get; set; }

        public InformationBoxTitleIconStyle TitleStyle { get; set; } = InformationBoxTitleIconStyle.None;

        public Icon TitleIcon { get; set; }

        public InformationBoxBehavior Behavior { get; set; } = InformationBoxBehavior.Modal;

        public AsyncResultCallback Callback { get; set; }

        public InformationBoxOpacity Opacity { get; set; } = InformationBoxOpacity.NoFade;

        public InformationBoxOrder Order { get; set; } = InformationBoxOrder.Default;

        public InformationBoxSound Sound { get; set; } = InformationBoxSound.Default;

        public bool ShowHelpButton { get; set; }

        public HelpNavigator HelpNavigator { get; set; } = HelpNavigator.TableOfContents;

        public Form Parent { get; set; }

        #endregion Properties

        #region Scope Loading

        public void LoadFromScope()
        {
            if (InformationBoxScope.Current == null)
            {
                return;
            }

            InformationBoxScopeParameters parameters = InformationBoxScope.Current.Parameters;

            if (parameters.Icon.HasValue)
            {
                this.Icon = parameters.Icon.Value;
            }

            if (parameters.CustomIcon != null)
            {
                this.IconType = IconType.UserDefined;
                this.CustomIcon = parameters.CustomIcon;
            }

            if (parameters.Buttons.HasValue)
            {
                this.Buttons = parameters.Buttons.Value;
            }

            if (parameters.DefaultButton.HasValue)
            {
                this.DefaultButton = parameters.DefaultButton.Value;
            }

            if (parameters.Layout.HasValue)
            {
                this.ButtonsLayout = parameters.Layout.Value;
            }

            if (parameters.AutoSizeMode.HasValue)
            {
                this.AutoSizeMode = parameters.AutoSizeMode.Value;
            }

            if (parameters.Position.HasValue)
            {
                this.Position = parameters.Position.Value;
            }

            if (parameters.CheckBox.HasValue)
            {
                this.CheckBox = parameters.CheckBox.Value;
            }

            if (parameters.Style.HasValue)
            {
                this.Style = parameters.Style.Value;
            }

            if (parameters.AutoClose != null)
            {
                this.AutoClose = parameters.AutoClose;
            }

            if (parameters.Design != null)
            {
                this.Design = parameters.Design;
            }

            if (parameters.Font != null)
            {
                this.FontParameters = parameters.Font;
            }

            if (parameters.TitleIconStyle.HasValue)
            {
                this.TitleStyle = parameters.TitleIconStyle.Value;
            }

            if (parameters.TitleIcon != null)
            {
                this.TitleIcon = parameters.TitleIcon;
            }

            if (parameters.Behavior.HasValue)
            {
                this.Behavior = parameters.Behavior.Value;
            }

            if (parameters.Opacity.HasValue)
            {
                this.Opacity = parameters.Opacity.Value;
            }

            if (parameters.Help.HasValue)
            {
                this.ShowHelpButton = parameters.Help.Value;
            }

            if (parameters.HelpNavigator.HasValue)
            {
                this.HelpNavigator = parameters.HelpNavigator.Value;
            }

            if (parameters.Order.HasValue)
            {
                this.Order = parameters.Order.Value;
            }

            if (parameters.Sound.HasValue)
            {
                this.Sound = parameters.Sound.Value;
            }
        }

        #endregion Scope Loading

        #region Business Logic

        public ButtonDefinition[] GetButtonDefinitions()
        {
            var list = new List<ButtonDefinition>();

            if (this.Buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                list.Add(new ButtonDefinition { Name = "Abort", Text = Resources.LabelAbort });
            }

            if (this.Buttons == InformationBoxButtons.OK ||
                this.Buttons == InformationBoxButtons.OKCancel ||
                this.Buttons == InformationBoxButtons.OKCancelUser1)
            {
                list.Add(new ButtonDefinition { Name = "OK", Text = Resources.LabelOK });
            }

            if (this.Buttons == InformationBoxButtons.YesNo ||
                this.Buttons == InformationBoxButtons.YesNoCancel ||
                this.Buttons == InformationBoxButtons.YesNoUser1)
            {
                list.Add(new ButtonDefinition { Name = "Yes", Text = Resources.LabelYes });
            }

            if (this.Buttons == InformationBoxButtons.AbortRetryIgnore ||
                this.Buttons == InformationBoxButtons.RetryCancel)
            {
                list.Add(new ButtonDefinition { Name = "Retry", Text = Resources.LabelRetry });
            }

            if (this.Buttons == InformationBoxButtons.YesNo ||
                this.Buttons == InformationBoxButtons.YesNoCancel ||
                this.Buttons == InformationBoxButtons.YesNoUser1)
            {
                list.Add(new ButtonDefinition { Name = "No", Text = Resources.LabelNo });
            }

            if (this.Buttons == InformationBoxButtons.OKCancel ||
                this.Buttons == InformationBoxButtons.OKCancelUser1 ||
                this.Buttons == InformationBoxButtons.RetryCancel ||
                this.Buttons == InformationBoxButtons.YesNoCancel)
            {
                list.Add(new ButtonDefinition { Name = "Cancel", Text = Resources.LabelCancel });
            }

            if (this.Buttons == InformationBoxButtons.AbortRetryIgnore)
            {
                list.Add(new ButtonDefinition { Name = "Ignore", Text = Resources.LabelIgnore });
            }

            if (this.Buttons == InformationBoxButtons.OKCancelUser1 ||
                this.Buttons == InformationBoxButtons.User1User2User3 ||
                this.Buttons == InformationBoxButtons.User1User2 ||
                this.Buttons == InformationBoxButtons.YesNoUser1 ||
                this.Buttons == InformationBoxButtons.User1)
            {
                list.Add(new ButtonDefinition { Name = "User1", Text = this.ButtonUser1Text });
            }

            if (this.Buttons == InformationBoxButtons.User1User2 ||
                this.Buttons == InformationBoxButtons.User1User2User3)
            {
                list.Add(new ButtonDefinition { Name = "User2", Text = this.ButtonUser2Text });
            }

            if (this.Buttons == InformationBoxButtons.User1User2User3)
            {
                list.Add(new ButtonDefinition { Name = "User3", Text = this.ButtonUser3Text });
            }

            if (this.ShowHelpButton || !String.IsNullOrEmpty(this.HelpFile))
            {
                list.Add(new ButtonDefinition { Name = "Help", Text = Resources.LabelHelp });
            }

            return list.ToArray();
        }

        public InformationBoxResult MapButtonNameToResult(string buttonName)
        {
            switch (buttonName)
            {
                case "Abort": return InformationBoxResult.Abort;
                case "OK": return InformationBoxResult.OK;
                case "Yes": return InformationBoxResult.Yes;
                case "Retry": return InformationBoxResult.Retry;
                case "No": return InformationBoxResult.No;
                case "Cancel": return InformationBoxResult.Cancel;
                case "Ignore": return InformationBoxResult.Ignore;
                case "User1": return InformationBoxResult.User1;
                case "User2": return InformationBoxResult.User2;
                case "User3": return InformationBoxResult.User3;
                default: return InformationBoxResult.None;
            }
        }

        public bool IsHelpButton(string buttonName)
        {
            return "Help".Equals(buttonName, StringComparison.Ordinal);
        }

        public double GetOpacityValue()
        {
            switch (this.Opacity)
            {
                case InformationBoxOpacity.Faded10: return 0.1;
                case InformationBoxOpacity.Faded20: return 0.2;
                case InformationBoxOpacity.Faded30: return 0.3;
                case InformationBoxOpacity.Faded40: return 0.4;
                case InformationBoxOpacity.Faded50: return 0.5;
                case InformationBoxOpacity.Faded60: return 0.6;
                case InformationBoxOpacity.Faded70: return 0.7;
                case InformationBoxOpacity.Faded80: return 0.8;
                case InformationBoxOpacity.Faded90: return 0.9;
                case InformationBoxOpacity.NoFade: return 1.0;
                default: return 1.0;
            }
        }

        public SystemSound GetSystemSound()
        {
            if (this.Sound == InformationBoxSound.None)
            {
                return null;
            }

            if (this.IconType == IconType.UserDefined)
            {
                return SystemSounds.Beep;
            }

            switch (IconHelper.GetCategory(this.Icon))
            {
                case InformationBoxMessageCategory.Asterisk: return SystemSounds.Asterisk;
                case InformationBoxMessageCategory.Exclamation: return SystemSounds.Exclamation;
                case InformationBoxMessageCategory.Hand: return SystemSounds.Hand;
                case InformationBoxMessageCategory.Question: return SystemSounds.Question;
                default: return SystemSounds.Beep;
            }
        }

        public CheckBoxConfiguration GetCheckBoxConfiguration()
        {
            bool isRightAligned = (this.CheckBox & InformationBoxCheckBox.RightAligned) == InformationBoxCheckBox.RightAligned;

            return new CheckBoxConfiguration
            {
                Text = this.DoNotShowAgainText ?? Resources.LabelDoNotShow,
                Visible = (this.CheckBox & InformationBoxCheckBox.Show) == InformationBoxCheckBox.Show,
                Checked = (this.CheckBox & InformationBoxCheckBox.Checked) == InformationBoxCheckBox.Checked,
                TextAlign = isRightAligned ? ContentAlignment.BottomRight : ContentAlignment.BottomLeft,
                CheckAlign = isRightAligned ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft,
            };
        }

        public WindowStyleConfiguration GetWindowStyleConfiguration()
        {
            if (this.Style == InformationBoxStyle.Modern)
            {
                Color barsBackColor = Color.Black;
                Color formBackColor = Color.Silver;

                if (this.Design != null)
                {
                    barsBackColor = this.Design.BarsBackColor;
                    formBackColor = this.Design.FormBackColor;
                }

                return new WindowStyleConfiguration
                {
                    BarsBackColor = barsBackColor,
                    FormBackColor = formBackColor,
                    BorderStyle = FormBorderStyle.None,
                    TitleLabelVisible = true,
                    AdjustPanelTop = false,
                    RemoveSideBorder = false,
                };
            }
            else
            {
                Color barsBackColor = SystemColors.Control;
                Color formBackColor = SystemColors.Control;

                if (this.Design != null)
                {
                    barsBackColor = this.Design.BarsBackColor;
                    formBackColor = this.Design.FormBackColor;
                }

                return new WindowStyleConfiguration
                {
                    BarsBackColor = barsBackColor,
                    FormBackColor = formBackColor,
                    BorderStyle = FormBorderStyle.FixedDialog,
                    TitleLabelVisible = false,
                    AdjustPanelTop = true,
                    RemoveSideBorder = true,
                };
            }
        }

        public IconConfiguration GetIconConfiguration(int scaledIconSize)
        {
            var config = new IconConfiguration();

            if (this.IconType == IconType.Internal)
            {
                if (this.Icon == InformationBoxIcon.None)
                {
                    config.IconPanelVisible = false;
                    config.IconImage = null;
                }
                else
                {
                    config.IconPanelVisible = true;
                    config.IconImage = IconHelper.FromEnum(this.Icon).ToBitmap();
                }
            }
            else
            {
                config.IconImage = new Icon(this.CustomIcon, scaledIconSize, scaledIconSize).ToBitmap();
                config.IconPanelVisible = true;
            }

            if (this.TitleStyle == InformationBoxTitleIconStyle.None)
            {
                config.ShowFormIcon = false;
                config.FormIcon = Resources.IconBlank;
            }
            else if (this.TitleStyle == InformationBoxTitleIconStyle.SameAsBox)
            {
                config.ShowFormIcon = true;
                config.FormIcon = this.IconType == IconType.Internal
                    ? IconHelper.FromEnum(this.Icon)
                    : this.CustomIcon;
            }
            else if (this.TitleStyle == InformationBoxTitleIconStyle.Custom)
            {
                config.ShowFormIcon = true;
                config.FormIcon = this.TitleIcon;
            }

            return config;
        }

        public bool HasIcon()
        {
            return this.Icon != InformationBoxIcon.None || this.IconType == IconType.UserDefined;
        }

        public bool HasCheckBox()
        {
            return (this.CheckBox & InformationBoxCheckBox.Show) == InformationBoxCheckBox.Show;
        }

        public int GetAutoCloseButtonIndex(int buttonCount)
        {
            if (this.AutoClose == null)
            {
                return -1;
            }

            InformationBoxDefaultButton button;
            if (this.AutoClose.Mode == AutoCloseDefinedParameters.Button)
            {
                button = this.AutoClose.DefaultButton;
            }
            else
            {
                button = this.DefaultButton;
            }

            if (button == InformationBoxDefaultButton.Button1 && buttonCount > 0)
            {
                return 0;
            }
            else if (button == InformationBoxDefaultButton.Button2 && buttonCount > 1)
            {
                return 1;
            }
            else if (button == InformationBoxDefaultButton.Button3 && buttonCount > 2)
            {
                return 2;
            }

            return -1;
        }

        public AutoCloseTickResult ProcessAutoCloseTick(int elapsedTime, int buttonCount)
        {
            var result = new AutoCloseTickResult { ButtonIndex = -1 };

            if (this.AutoClose == null)
            {
                return result;
            }

            if (elapsedTime == this.AutoClose.Seconds)
            {
                result.ShouldStopTimer = true;
                result.ShouldClose = true;

                if (this.AutoClose.Mode == AutoCloseDefinedParameters.Result)
                {
                    result.UseDirectResult = true;
                    result.DirectResult = this.AutoClose.Result;
                }
                else
                {
                    int buttonIndex = this.GetAutoCloseButtonIndex(buttonCount);
                    if (buttonIndex >= 0)
                    {
                        result.ButtonIndex = buttonIndex;
                    }
                }
            }
            else
            {
                int buttonIndex = this.GetAutoCloseButtonIndex(buttonCount);
                result.ButtonIndex = buttonIndex;
                result.ShouldClose = false;
            }

            return result;
        }

        public string FormatAutoCloseButtonText(string currentText, int secondsRemaining)
        {
            Regex extractLabel = new Regex(@".*?\(\d+\)");

            if (extractLabel.IsMatch(currentText))
            {
                return String.Format(CultureInfo.InvariantCulture, "{0} ({1})",
                    currentText.Substring(0, currentText.LastIndexOf(" (", StringComparison.OrdinalIgnoreCase)),
                    secondsRemaining);
            }
            else
            {
                return String.Format(CultureInfo.InvariantCulture, "{0} ({1})", currentText, secondsRemaining);
            }
        }

        public int GetAutoCloseSecondsRemaining(int elapsedTime)
        {
            if (this.AutoClose == null)
            {
                return 0;
            }

            return this.AutoClose.Seconds - elapsedTime;
        }

        #endregion Business Logic
    }
}
