using System;
using System.Globalization;
using System.Text;

namespace InfoBox.Designer.CodeGeneration
{
    public class VbNetGenerator : ICodeGenerator
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
        public string GenerateSingleCall(InformationBoxBehavior behavior, string text, string title, InformationBoxButtons buttons, string button1Text, string button2Text, string button3Text, InformationBoxIcon icon, string iconFileName, InformationBoxDefaultButton defaultButton, InformationBoxButtonsLayout buttonsLayout, InformationBoxAutoSizeMode autoSize, InformationBoxPosition position, bool showHelp, string helpFile, string helpTopic, System.Windows.Forms.HelpNavigator navigator, InformationBoxCheckBox checkState, InformationBoxStyle style, bool useAutoClose, AutoCloseParameters autoClose, DesignParameters design, InformationBoxTitleIconStyle titleStyle, string titleIconFileName, InformationBoxOpacity opacity, InformationBoxOrder order, InformationBoxSound sound)
        {
            StringBuilder codeBuilder = new StringBuilder();
            if (checkState == 0)
            {
                codeBuilder.AppendFormat("InformationBox.Show(\"{0}\", ", text.Replace(Environment.NewLine, "\\n"));
            }
            else
            {
                codeBuilder.Append("Dim doNotShowState as CheckState = CheckState.Indeterminate");
                codeBuilder.Append(Environment.NewLine);
                codeBuilder.AppendFormat("InformationBox.Show(\"{0}\", doNotShowState, ", text.Replace(Environment.NewLine, "\\n"));
            }

            if (!String.IsNullOrEmpty(helpFile) || !String.IsNullOrEmpty(title))
            {
                codeBuilder.AppendFormat("\"{0}\", ", title);
            }

            if (buttons != InformationBoxButtons.OK)
            {
                codeBuilder.AppendFormat("InformationBoxButtons.{0}, ", buttons);
            }

            if (buttons == InformationBoxButtons.OKCancelUser1 ||
                buttons == InformationBoxButtons.User1User2 ||
                buttons == InformationBoxButtons.YesNoUser1)
            {
                codeBuilder.AppendFormat("New String() {{ \"{0}\", \"{1}\" }}, ", button1Text, button2Text);
            }
            else if (buttons == InformationBoxButtons.User1User2User3)
            {
                codeBuilder.AppendFormat("New String() {{ \"{0}\", \"{1}\", \"{2}\" }}, ", button1Text, button2Text, button3Text);
            }

            if (icon != InformationBoxIcon.None)
            {
                codeBuilder.AppendFormat("InformationBoxIcon.{0}, ", icon);
            }

            if (!String.IsNullOrEmpty(iconFileName))
            {
                codeBuilder.AppendFormat("New System.Drawing.Icon(@\"{0}\"), ", iconFileName);
            }

            if (defaultButton != InformationBoxDefaultButton.Button1)
            {
                codeBuilder.AppendFormat("InformationBoxDefaultButton.{0}, ", defaultButton);
            }

            if (buttonsLayout != InformationBoxButtonsLayout.GroupMiddle)
            {
                codeBuilder.AppendFormat("InformationBoxButtonsLayout.{0}, ", buttonsLayout);
            }

            if (autoSize != InformationBoxAutoSizeMode.None)
            {
                codeBuilder.AppendFormat("InformationBoxAutoSizeMode.{0}, ", autoSize);
            }

            if (sound != InformationBoxSound.Default)
            {
                codeBuilder.AppendFormat("InformationBoxSound.{0}, ", sound);
            }

            if (position != InformationBoxPosition.CenterOnParent)
            {
                codeBuilder.AppendFormat("InformationBoxPosition.{0}, ", position);
            }

            if (showHelp)
            {
                codeBuilder.Append("True, ");
            }

            if (!String.IsNullOrEmpty(helpFile))
            {
                codeBuilder.AppendFormat("\"{0}\", ", helpFile);
            }

            if (navigator != 0)
            {
                codeBuilder.AppendFormat("HelpNavigator.{0}, ", navigator);
            }

            if (!String.IsNullOrEmpty(helpTopic))
            {
                codeBuilder.AppendFormat("\"{0}\", ", helpTopic);
            }

            if (checkState != 0)
            {
                codeBuilder.Append("InformationBoxCheckBox.Show");
                if ((checkState & InformationBoxCheckBox.Checked) == InformationBoxCheckBox.Checked)
                {
                    codeBuilder.Append(" Or InformationBoxCheckBox.Checked");
                }

                if ((checkState & InformationBoxCheckBox.RightAligned) == InformationBoxCheckBox.RightAligned)
                {
                    codeBuilder.Append(" Or InformationBoxCheckBox.RightAligned");
                }

                codeBuilder.Append(", ");
            }

            if (style != InformationBoxStyle.Standard)
            {
                codeBuilder.AppendFormat("InformationBoxStyle.{0}, ", style);
            }

            if (order != InformationBoxOrder.Default)
            {
                codeBuilder.AppendFormat("InformationBoxOrder.{0}, ", order);
            }

            if (useAutoClose)
            {
                if (autoClose.Seconds == AutoCloseParameters.Default.Seconds &&
                    autoClose.DefaultButton == InformationBoxDefaultButton.Button1 &&
                    autoClose.Result == InformationBoxResult.None)
                {
                    codeBuilder.Append("AutoCloseParameters.Default, ");
                }
                else
                {
                    if (autoClose.Mode == AutoCloseDefinedParameters.Button)
                    {
                        codeBuilder.AppendFormat("New AutoCloseParameters({0}, InformationBoxDefaultButton.{1}), ", autoClose.Seconds, autoClose.DefaultButton);
                    }
                    else if (autoClose.Mode == AutoCloseDefinedParameters.Result)
                    {
                        codeBuilder.AppendFormat("New AutoCloseParameters({0}, InformationBoxResult.{1}), ", autoClose.Seconds, autoClose.Result);
                    }
                    else
                    {
                        codeBuilder.AppendFormat("New AutoCloseParameters({0}), ", autoClose.Seconds);
                    }
                }
            }

            if (null != design)
            {
                codeBuilder.AppendFormat(CultureInfo.InvariantCulture, "New DesignParameters(Color.FromArgb({0},{1},{2}), Color.FromArgb({3},{4},{5})), ", design.FormBackColor.R, design.FormBackColor.G, design.FormBackColor.B, design.BarsBackColor.R, design.BarsBackColor.G, design.BarsBackColor.B);
            }

            if (titleStyle == InformationBoxTitleIconStyle.Custom)
            {
                codeBuilder.AppendFormat(CultureInfo.InvariantCulture, "New InformationBoxTitleIcon(@\"{0}\"), ", titleIconFileName);
            }
            else if (titleStyle == InformationBoxTitleIconStyle.SameAsBox)
            {
                codeBuilder.Append("InformationBoxTitleIconStyle.SameAsBox, ");
            }

            if (behavior == InformationBoxBehavior.Modeless)
            {
                codeBuilder.Append("InformationBoxBehavior.Modeless, ");
            }

            if (opacity != InformationBoxOpacity.NoFade)
            {
                codeBuilder.AppendFormat("InformationBoxOpacity.{0}, ", opacity);
            }

            codeBuilder[codeBuilder.Length - 2] = ')';

            return codeBuilder.ToString().Replace("\"\"", "String.Empty");
        }

        #endregion
    }
}
