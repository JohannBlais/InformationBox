namespace InfoBox.Internals
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal static class ParameterParser
    {
        internal static InformationBoxViewModel Parse(string text, params object[] parameters)
        {
            var vm = new InformationBoxViewModel { Text = text };

            bool loadScope = true;
            foreach (object param in parameters)
            {
                if (param is InformationBoxInitialization)
                {
                    if (InformationBoxInitialization.FromParametersOnly == (InformationBoxInitialization)param)
                    {
                        loadScope = false;
                    }
                }
            }

            if (loadScope)
            {
                vm.LoadFromScope();
            }

            int stringCount = 0;

            foreach (object parameter in parameters)
            {
                if (null == parameter)
                {
                    continue;
                }

                if (parameter is string)
                {
                    if (stringCount == 0)
                    {
                        vm.Title = (string)parameter;
                    }
                    else if (stringCount == 1)
                    {
                        vm.HelpFile = (string)parameter;
                    }
                    else if (stringCount == 2)
                    {
                        vm.HelpTopic = (string)parameter;
                    }
                    else if (stringCount == 3)
                    {
                        vm.DoNotShowAgainText = (string)parameter;
                    }

                    stringCount++;
                }
                else if (parameter is InformationBoxButtons)
                {
                    vm.Buttons = (InformationBoxButtons)parameter;
                }
                else if (parameter is InformationBoxIcon)
                {
                    vm.Icon = (InformationBoxIcon)parameter;
                }
                else if (parameter is Icon)
                {
                    vm.IconType = IconType.UserDefined;
                    vm.CustomIcon = (Icon)parameter;
                }
                else if (parameter is InformationBoxDefaultButton)
                {
                    vm.DefaultButton = (InformationBoxDefaultButton)parameter;
                }
                else if (parameter is string[])
                {
                    string[] labels = (string[])parameter;
                    if (labels.Length > 0)
                    {
                        vm.ButtonUser1Text = labels[0];
                    }

                    if (labels.Length > 1)
                    {
                        vm.ButtonUser2Text = labels[1];
                    }

                    if (labels.Length > 2)
                    {
                        vm.ButtonUser3Text = labels[2];
                    }
                }
                else if (parameter is InformationBoxButtonsLayout)
                {
                    vm.ButtonsLayout = (InformationBoxButtonsLayout)parameter;
                }
                else if (parameter is InformationBoxAutoSizeMode)
                {
                    vm.AutoSizeMode = (InformationBoxAutoSizeMode)parameter;
                }
                else if (parameter is InformationBoxPosition)
                {
                    vm.Position = (InformationBoxPosition)parameter;
                }
                else if (parameter is bool)
                {
                    vm.ShowHelpButton = (bool)parameter;
                }
                else if (parameter is HelpNavigator)
                {
                    vm.HelpNavigator = (HelpNavigator)parameter;
                }
                else if (parameter is InformationBoxCheckBox)
                {
                    vm.CheckBox = (InformationBoxCheckBox)parameter;
                }
                else if (parameter is InformationBoxStyle)
                {
                    vm.Style = (InformationBoxStyle)parameter;
                }
                else if (parameter is AutoCloseParameters)
                {
                    vm.AutoClose = (AutoCloseParameters)parameter;
                }
                else if (parameter is DesignParameters)
                {
                    vm.Design = (DesignParameters)parameter;
                }
                else if (parameter is FontParameters)
                {
                    vm.FontParameters = (FontParameters)parameter;
                }
                else if (parameter is Font)
                {
                    vm.FontParameters = new FontParameters((Font)parameter);
                }
                else if (parameter is InformationBoxTitleIconStyle)
                {
                    vm.TitleStyle = (InformationBoxTitleIconStyle)parameter;
                }
                else if (parameter is InformationBoxTitleIcon)
                {
                    vm.TitleIcon = ((InformationBoxTitleIcon)parameter).Icon;
                }
                else if (parameter is MessageBoxButtons?)
                {
                    vm.Buttons = MessageBoxEnumConverter.Parse((MessageBoxButtons)parameter);
                }
                else if (parameter is MessageBoxIcon?)
                {
                    vm.Icon = MessageBoxEnumConverter.Parse((MessageBoxIcon)parameter);
                }
                else if (parameter is MessageBoxDefaultButton?)
                {
                    vm.DefaultButton = MessageBoxEnumConverter.Parse((MessageBoxDefaultButton)parameter);
                }
                else if (parameter is InformationBoxBehavior)
                {
                    vm.Behavior = (InformationBoxBehavior)parameter;
                }
                else if (parameter is AsyncResultCallback)
                {
                    vm.Callback = (AsyncResultCallback)parameter;
                }
                else if (parameter is InformationBoxOpacity)
                {
                    vm.Opacity = (InformationBoxOpacity)parameter;
                }
                else if (parameter is Form)
                {
                    vm.Parent = (Form)parameter;
                }
                else if (parameter is InformationBoxOrder)
                {
                    vm.Order = (InformationBoxOrder)parameter;
                }
                else if (parameter is InformationBoxSound)
                {
                    vm.Sound = (InformationBoxSound)parameter;
                }
            }

            return vm;
        }

        internal static InformationBoxViewModel ParseNamed(
            string text,
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
            Font font = null,
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
            var vm = new InformationBoxViewModel { Text = text };

            if (InformationBoxInitialization.FromParametersOnly == initialization)
            {
                vm.LoadFromScope();
            }

            vm.Title = title;
            vm.HelpFile = helpFile;
            vm.HelpTopic = helpTopic;
            vm.Buttons = buttons;
            vm.Icon = icon;

            if (customIcon != null)
            {
                vm.IconType = IconType.UserDefined;
                vm.CustomIcon = customIcon;
            }

            vm.DefaultButton = defaultButton;

            if (customButtons != null)
            {
                if (customButtons.Length > 0)
                {
                    vm.ButtonUser1Text = customButtons[0];
                }

                if (customButtons.Length > 1)
                {
                    vm.ButtonUser2Text = customButtons[1];
                }

                if (customButtons.Length > 2)
                {
                    vm.ButtonUser3Text = customButtons[2];
                }
            }

            vm.ButtonsLayout = buttonsLayout;
            vm.AutoSizeMode = autoSizeMode;
            vm.Position = position;
            vm.ShowHelpButton = showHelpButton;
            vm.HelpNavigator = helpNavigator;
            vm.CheckBox = showDoNotShowAgainCheckBox;
            vm.DoNotShowAgainText = doNotShowAgainText;
            vm.Style = style;
            vm.AutoClose = autoClose;
            vm.Design = design;

            if (font != null)
            {
                vm.FontParameters = new FontParameters(font);
            }
            else
            {
                vm.FontParameters = fontParameters;
            }

            vm.TitleStyle = titleStyle;

            if (titleIcon != null)
            {
                vm.TitleIcon = titleIcon.Icon;
            }

            if (legacyButtons.HasValue)
            {
                vm.Buttons = MessageBoxEnumConverter.Parse(legacyButtons.Value);
            }

            if (legacyIcon.HasValue)
            {
                vm.Icon = MessageBoxEnumConverter.Parse(legacyIcon.Value);
            }

            if (legacyDefaultButton.HasValue)
            {
                vm.DefaultButton = MessageBoxEnumConverter.Parse(legacyDefaultButton.Value);
            }

            vm.Behavior = behavior;
            vm.Callback = callback;
            vm.Opacity = opacity;
            vm.Parent = parent;
            vm.Order = order;
            vm.Sound = sound;

            return vm;
        }
    }
}
