using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace InfoBox.Designer.CodeGeneration
{
    /// <summary>
    /// C# code generator for InformationBox calls
    /// </summary>
    public class CSharpGenerator : ICodeGenerator
    {
        #region ICodeGenerator Members

        /// <summary>
        /// Generates a single method call to display an InfoBox.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        /// <param name="text">The text.</param>
        /// <param name="title">The title.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="button2Text">The button2 text.</param>
        /// <param name="button3Text">The button3 text.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="iconFileName">Name of the icon file.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="buttonsLayout">The buttons layout.</param>
        /// <param name="autoSize">The autosize parameters</param>
        /// <param name="position">The position.</param>
        /// <param name="showHelp">if set to <c>true</c> [show help].</param>
        /// <param name="helpFile">The help file.</param>
        /// <param name="helpTopic">The help topic.</param>
        /// <param name="navigator">The navigator.</param>
        /// <param name="checkState">The state of the checkbox.</param>
        /// <param name="doNotShowAgainText">If not null, the value will replace the default text for the "Do not show again" checkbox.</param>
        /// <param name="style">The style.</param>
        /// <param name="useAutoClose">if set to <c>true</c> [use auto close].</param>
        /// <param name="autoClose">The auto-close parameters.</param>
        /// <param name="design">The design.</param>
        /// <param name="titleStyle">The title style.</param>
        /// <param name="titleIconFileName">Filename of the title icon .</param>
        /// <param name="opacity">The opacity.</param>
        /// <param name="order">The order.</param>
        /// <param name="sound">The sound.</param>
        /// <returns></returns>
        public string GenerateSingleCall(InformationBoxBehavior behavior,
                                         string text,
                                         string title,
                                         InformationBoxButtons buttons,
                                         string button1Text,
                                         string button2Text,
                                         string button3Text,
                                         InformationBoxIcon icon,
                                         string iconFileName,
                                         InformationBoxDefaultButton defaultButton,
                                         InformationBoxButtonsLayout buttonsLayout,
                                         InformationBoxAutoSizeMode autoSize,
                                         InformationBoxPosition position,
                                         bool showHelp,
                                         string helpFile,
                                         string helpTopic,
                                         HelpNavigator navigator,
                                         InformationBoxCheckBox checkState,
                                         string doNotShowAgainText,
                                         InformationBoxStyle style,
                                         bool useAutoClose,
                                         AutoCloseParameters autoClose,
                                         DesignParameters design,
                                         InformationBoxTitleIconStyle titleStyle,
                                         string titleIconFileName,
                                         InformationBoxOpacity opacity,
                                         InformationBoxOrder order,
                                         InformationBoxSound sound)
        {
            StringBuilder codeBuilder = new StringBuilder();
            if (checkState == 0)
            {
                codeBuilder.AppendFormat("InformationBox.Show(\"{0}\", ", text.Replace(Environment.NewLine, "\\n"));
            }
            else
            {
                codeBuilder.Append("CheckState doNotShowState = CheckState.Indeterminate;");
                codeBuilder.Append(Environment.NewLine);
                codeBuilder.AppendFormat("InformationBox.Show(\"{0}\", out doNotShowState, ", text.Replace(Environment.NewLine, "\\n"));
            }

            if (!String.IsNullOrEmpty(title))
            {
                codeBuilder.AppendFormat("title: \"{0}\", ", title);
            }

            if (buttons != InformationBoxButtons.OK)
            {
                codeBuilder.AppendFormat("buttons: InformationBoxButtons.{0}, ", buttons);
            }

            if (buttons == InformationBoxButtons.OKCancelUser1 ||
                buttons == InformationBoxButtons.User1User2 ||
                buttons == InformationBoxButtons.YesNoUser1)
            {
                codeBuilder.AppendFormat("customButtons: new string[] {{ \"{0}\", \"{1}\" }}, ", button1Text, button2Text);
            }
            else if (buttons == InformationBoxButtons.User1User2User3)
            {
                codeBuilder.AppendFormat("customButtons: new string[] {{ \"{0}\", \"{1}\", \"{2}\" }}, ", button1Text, button2Text, button3Text);
            }

            if (icon != InformationBoxIcon.None)
            {
                codeBuilder.AppendFormat("icon: InformationBoxIcon.{0}, ", icon);
            }

            if (!String.IsNullOrEmpty(iconFileName))
            {
                codeBuilder.AppendFormat("customIcon: new System.Drawing.Icon(@\"{0}\"), ", iconFileName);
            }

            if (defaultButton != InformationBoxDefaultButton.Button1)
            {
                codeBuilder.AppendFormat("defaultButton: InformationBoxDefaultButton.{0}, ", defaultButton);
            }

            if (buttonsLayout != InformationBoxButtonsLayout.GroupMiddle)
            {
                codeBuilder.AppendFormat("buttonsLayout: InformationBoxButtonsLayout.{0}, ", buttonsLayout);
            }

            if (autoSize != InformationBoxAutoSizeMode.None)
            {
                codeBuilder.AppendFormat("autoSizeMode: InformationBoxAutoSizeMode.{0}, ", autoSize);
            }

            if (sound != InformationBoxSound.Default)
            {
                codeBuilder.AppendFormat("sound: InformationBoxSound.{0}, ", sound);
            }

            if (position != InformationBoxPosition.CenterOnParent)
            {
                codeBuilder.AppendFormat("position: InformationBoxPosition.{0}, ", position);
            }

            if (showHelp)
            {
                codeBuilder.Append("showHelpButton: true, ");
            }

            if (!String.IsNullOrEmpty(helpFile))
            {
                codeBuilder.AppendFormat("helpFile: @\"{0}\", ", helpFile);
            }

            if (navigator != 0)
            {
                codeBuilder.AppendFormat("helpNavigator: HelpNavigator.{0}, ", navigator);
            }

            if (!String.IsNullOrEmpty(helpTopic))
            {
                codeBuilder.AppendFormat("helpTopic: \"{0}\", ", helpTopic);
            }

            if (checkState != 0)
            {
                codeBuilder.Append("showDoNotShowAgainCheckBox: InformationBoxCheckBox.Show");
                if ((checkState & InformationBoxCheckBox.Checked) == InformationBoxCheckBox.Checked)
                {
                    codeBuilder.Append(" | InformationBoxCheckBox.Checked");
                }

                if ((checkState & InformationBoxCheckBox.RightAligned) == InformationBoxCheckBox.RightAligned)
                {
                    codeBuilder.Append(" | InformationBoxCheckBox.RightAligned");
                }

                codeBuilder.Append(", ");
            }

            if (doNotShowAgainText != null)
            {
                codeBuilder.AppendFormat("doNotShowAgainText: \"{0}\", ", doNotShowAgainText);
            }

            if (style != InformationBoxStyle.Standard)
            {
                codeBuilder.AppendFormat("style: InformationBoxStyle.{0}, ", style);
            }

            if (order != InformationBoxOrder.Default)
            {
                codeBuilder.AppendFormat("order: InformationBoxOrder.{0}, ", order);
            }

            if (useAutoClose)
            {
                if (autoClose.Seconds == AutoCloseParameters.Default.Seconds &&
                    autoClose.DefaultButton == InformationBoxDefaultButton.Button1 &&
                    autoClose.Result == InformationBoxResult.None)
                {
                    codeBuilder.Append("autoClose: AutoCloseParameters.Default, ");
                }
                else
                {
                    if (autoClose.Mode == AutoCloseDefinedParameters.Button)
                    {
                        codeBuilder.AppendFormat("autoClose: new AutoCloseParameters({0}, InformationBoxDefaultButton.{1}), ", autoClose.Seconds, autoClose.DefaultButton);
                    }
                    else if (autoClose.Mode == AutoCloseDefinedParameters.Result)
                    {
                        codeBuilder.AppendFormat("autoClose: new AutoCloseParameters({0}, InformationBoxResult.{1}), ", autoClose.Seconds, autoClose.Result);
                    }
                    else
                    {
                        codeBuilder.AppendFormat("autoClose: new AutoCloseParameters({0}), ", autoClose.Seconds);
                    }
                }
            }

            if (null != design)
            {
                codeBuilder.AppendFormat(CultureInfo.InvariantCulture, "design: new DesignParameters(System.Drawing.Color.FromArgb({0},{1},{2}), System.Drawing.Color.FromArgb({3},{4},{5})), ", design.FormBackColor.R, design.FormBackColor.G, design.FormBackColor.B, design.BarsBackColor.R, design.BarsBackColor.G, design.BarsBackColor.B);
            }

            if (titleStyle == InformationBoxTitleIconStyle.Custom)
            {
                codeBuilder.AppendFormat(CultureInfo.InvariantCulture, "titleIcon: new InformationBoxTitleIcon(@\"{0}\"), ", titleIconFileName);
            }
            else if (titleStyle == InformationBoxTitleIconStyle.SameAsBox)
            {
                codeBuilder.Append("titleStyle: InformationBoxTitleIconStyle.SameAsBox, ");
            }

            if (behavior == InformationBoxBehavior.Modeless)
            {
                codeBuilder.Append("behavior: InformationBoxBehavior.Modeless, ");
            }

            if (opacity != InformationBoxOpacity.NoFade)
            {
                codeBuilder.AppendFormat("opacity: InformationBoxOpacity.{0}, ", opacity);
            }

            codeBuilder[codeBuilder.Length - 2] = ')';
            codeBuilder[codeBuilder.Length - 1] = ';';

            return codeBuilder.ToString().Replace("\"\"", "System.String.Empty");
        }

        #endregion
    }
}
