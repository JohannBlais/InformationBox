namespace InfoBox.Test
{
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Threading;

    public partial class InformationBoxDesigner : Form
    {
        public InformationBoxDesigner()
        {
            InitializeComponent();

            LoadIcons();
        }

        /// <summary>
        /// Loads the icons.
        /// </summary>
        private void LoadIcons()
        {
            foreach (InformationBoxIcon icon in Enum.GetValues(typeof(InformationBoxIcon)))
            {
                ddlIcons.Items.Add(icon);
            }
        }

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <returns></returns>
        private InformationBoxButtons GetButtons()
        {
            if (rdbAbortRetryIgnore.Checked) return InformationBoxButtons.AbortRetryIgnore;
            if (rdbOK.Checked) return InformationBoxButtons.OK;
            if (rdbOKCancel.Checked) return InformationBoxButtons.OKCancel;
            if (rdbRetryCancel.Checked) return InformationBoxButtons.RetryCancel;
            if (rdbYesNo.Checked) return InformationBoxButtons.YesNo;
            if (rdbYesNoCancel.Checked) return InformationBoxButtons.YesNoCancel;
            if (rdbYesNoUser1.Checked) return InformationBoxButtons.YesNoUser1;
            if (rdbOKCancelUser1.Checked) return InformationBoxButtons.OKCancelUser1;
            if (rdbUser1User2.Checked) return InformationBoxButtons.User1User2;
            if (rdbUser1.Checked) return InformationBoxButtons.User1;
            return InformationBoxButtons.OK;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <returns></returns>
        private InformationBoxIcon GetIcon()
        {
            if (null != ddlIcons.SelectedItem)
                return (InformationBoxIcon) ddlIcons.SelectedItem;
            return InformationBoxIcon.None;
        }

        /// <summary>
        /// Gets the default button.
        /// </summary>
        /// <returns></returns>
        private InformationBoxDefaultButton GetDefaultButton()
        {
            if (rdbButton1.Checked) return InformationBoxDefaultButton.Button1;
            if (rdbButton2.Checked) return InformationBoxDefaultButton.Button2;
            if (rdbButton3.Checked) return InformationBoxDefaultButton.Button3;
            return InformationBoxDefaultButton.Button1;
        }

        /// <summary>
        /// Gets the buttons layout.
        /// </summary>
        /// <returns></returns>
        private InformationBoxButtonsLayout GetButtonsLayout()
        {
            if (rdbLayoutGroupLeft.Checked) return InformationBoxButtonsLayout.GroupLeft;
            if (rdbLayoutGroupRight.Checked) return InformationBoxButtonsLayout.GroupRight;
            if (rdbLayoutGroupMiddle.Checked) return InformationBoxButtonsLayout.GroupMiddle;
            if (rdbLayoutSeparate.Checked) return InformationBoxButtonsLayout.Separate;
            return InformationBoxButtonsLayout.GroupMiddle;
        }

        /// <summary>
        /// Gets the auto size mode.
        /// </summary>
        /// <returns></returns>
        private InformationBoxAutoSizeMode GetAutoSize()
        {
            if (rdbAutoSizeMinimumHeight.Checked) return InformationBoxAutoSizeMode.MinimumHeight;
            if (rdbAutoSizeMinimumWidth.Checked) return InformationBoxAutoSizeMode.MinimumWidth;
            return InformationBoxAutoSizeMode.None;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns></returns>
        private InformationBoxPosition GetPosition()
        {
            if (rdbPositionCenterOnParent.Checked) return InformationBoxPosition.CenterOnParent;
            if (rdbPositionCenterOnScreen.Checked) return InformationBoxPosition.CenterOnScreen;
            return InformationBoxPosition.CenterOnParent;
        }

        /// <summary>
        /// Gets the help navigator.
        /// </summary>
        /// <returns></returns>
        private HelpNavigator GetHelpNavigator()
        {
            if (rdbHelpFind.Checked) return HelpNavigator.Find;
            if (rdbHelpIndex.Checked) return HelpNavigator.Index;
            if (rdbHelpTopic.Checked) return HelpNavigator.Topic;
            if (rdbHelpTableOfContents.Checked) return HelpNavigator.TableOfContents;
            return 0;
        }

        /// <summary>
        /// Gets the state of the check box.
        /// </summary>
        /// <returns></returns>
        private InformationBoxCheckBox GetCheckBoxState()
        {
            InformationBoxCheckBox check = 0;
            if (clbCheckBox.GetItemCheckState(0) == CheckState.Checked) check |= InformationBoxCheckBox.Show;
            if (clbCheckBox.GetItemCheckState(1) == CheckState.Checked) check |= InformationBoxCheckBox.Checked;
            if (clbCheckBox.GetItemCheckState(2) == CheckState.Checked) check |= InformationBoxCheckBox.RightAligned;
            return check;
        }

        /// <summary>
        /// Gets the style.
        /// </summary>
        /// <returns></returns>
        private InformationBoxStyle GetStyle()
        {
            if (rdbStyleModern.Checked) return InformationBoxStyle.Modern;
            return InformationBoxStyle.Standard;
        }

        /// <summary>
        /// Generates the code.
        /// </summary>
        private void GenerateCode()
        {
            InformationBoxButtons buttons = GetButtons();
            InformationBoxIcon icon = GetIcon();
            String iconFileName = txbIcon.Text;
            InformationBoxDefaultButton defaultButton = GetDefaultButton();
            InformationBoxButtonsLayout buttonsLayout = GetButtonsLayout();
            InformationBoxAutoSizeMode autoSize = GetAutoSize();
            InformationBoxPosition position = GetPosition();
            HelpNavigator navigator = GetHelpNavigator();
            InformationBoxCheckBox checkState = GetCheckBoxState();
            InformationBoxStyle style = GetStyle();

            StringBuilder codeBuilder = new StringBuilder();
            if (checkState == 0)
            {
                codeBuilder.AppendFormat("InformationBox.Show(\"{0}\", ",
                                         txbText.Text.Replace(Environment.NewLine, "\\n"));
            }
            else
            {
                codeBuilder.Append("CheckState doNotShowState = CheckState.Indeterminate;");
                codeBuilder.Append(Environment.NewLine);
                codeBuilder.AppendFormat("InformationBox.Show(\"{0}\", ref doNotShowState, ",
                                         txbText.Text.Replace(Environment.NewLine, "\\n"));
            }

            if (!String.Empty.Equals(txbHelpFile.Text) || !String.Empty.Equals(txbTitle.Text))
                codeBuilder.AppendFormat("\"{0}\", ", txbText.Text.Replace(Environment.NewLine, "\\n"));

            if (buttons != InformationBoxButtons.OK)
                codeBuilder.AppendFormat("InformationBoxButtons.{0}, ", buttons);

            if (buttons == InformationBoxButtons.OKCancelUser1 ||
                buttons == InformationBoxButtons.User1User2 ||
                buttons == InformationBoxButtons.YesNoUser1)
                codeBuilder.AppendFormat("new string[] {{ \"{0}\", \"{1}\" }}, ", txbUser1.Text, txbUser2.Text);

            if (icon != InformationBoxIcon.None)
                codeBuilder.AppendFormat("InformationBoxIcon.{0}, ", icon);

            if (!String.Empty.Equals(iconFileName))
                codeBuilder.AppendFormat("new System.Drawing.Icon(@\"{0}\"), ", iconFileName);

            if (defaultButton != InformationBoxDefaultButton.Button1)
                codeBuilder.AppendFormat("InformationBoxDefaultButton.{0}, ", defaultButton);

            if (buttonsLayout != InformationBoxButtonsLayout.GroupMiddle)
                codeBuilder.AppendFormat("InformationBoxDefaultButton.{0}, ", buttonsLayout);

            if (autoSize != InformationBoxAutoSizeMode.None)
                codeBuilder.AppendFormat("InformationBoxAutoSizeMode.{0}, ", autoSize);

            if (position != InformationBoxPosition.CenterOnParent)
                codeBuilder.AppendFormat("InformationBoxPosition.{0}, ", position);

            if (chbHelpButton.Checked)
                codeBuilder.AppendFormat("{0}, ", true);

            if (!String.Empty.Equals(txbHelpFile.Text))
                codeBuilder.AppendFormat("\"{0}\", ", txbHelpFile.Text);

            if (navigator != (HelpNavigator) 0)
                codeBuilder.AppendFormat("HelpNavigator.{0}, ", navigator);

            if (!String.Empty.Equals(txbHelpTopic.Text))
                codeBuilder.AppendFormat("\"{0}\", ", txbHelpTopic.Text);

            if (checkState != 0)
            {
                codeBuilder.Append("InformationBoxCheckBox.Show");
                if ((checkState & InformationBoxCheckBox.Checked) == InformationBoxCheckBox.Checked)
                    codeBuilder.Append(" | InformationBoxCheckBox.Checked");
                if ((checkState & InformationBoxCheckBox.RightAligned) == InformationBoxCheckBox.RightAligned)
                    codeBuilder.Append(" | InformationBoxCheckBox.RightAligned");
                codeBuilder.Append(", ");
            }

            if (style != InformationBoxStyle.Standard)
                codeBuilder.AppendFormat("InformationBoxStyle.{0}", style);

            codeBuilder[codeBuilder.Length - 2] = ')';
            codeBuilder[codeBuilder.Length - 1] = ';';

            txbCode.Text = codeBuilder.ToString().Replace("\"\"", "String.Empty");
        }

        /// <summary>
        /// Handles the Click event of the btnShow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (null != ddlLanguage.SelectedItem)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ddlLanguage.SelectedItem.ToString().Substring(0, 2));
            }

            GenerateCode();

            InformationBoxButtons buttons = GetButtons();
            InformationBoxIcon icon = GetIcon();
            String iconFileName = txbIcon.Text;
            InformationBoxDefaultButton defaultButton = GetDefaultButton();
            InformationBoxButtonsLayout buttonsLayout = GetButtonsLayout();
            InformationBoxAutoSizeMode autoSize = GetAutoSize();
            InformationBoxPosition position = GetPosition();
            HelpNavigator navigator = GetHelpNavigator();
            InformationBoxCheckBox checkState = GetCheckBoxState();
            CheckState state = 0;
            InformationBoxStyle style = GetStyle();

            if (String.Empty.Equals(iconFileName))
            {
                InformationBox.Show(txbText.Text, ref state, txbTitle.Text, buttons, new string[] { txbUser1.Text, txbUser2.Text }, icon, defaultButton, buttonsLayout, autoSize, position, chbHelpButton.Checked, txbHelpFile.Text, navigator, txbHelpTopic.Text, checkState, style);
            }
            else
            {
                InformationBox.Show(txbText.Text, ref state, txbTitle.Text, buttons, new string[] { txbUser1.Text, txbUser2.Text }, new Icon(iconFileName), defaultButton, buttonsLayout, autoSize, position, chbHelpButton.Checked, txbHelpFile.Text, navigator, txbHelpTopic.Text, checkState, style);
            }

            if (checkState != 0)
            {
                InformationBox.Show(String.Format("The state of the checkbox was {0}", state), InformationBoxIcon.Information);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateCode();
        }

        /// <summary>
        /// Handles the Click event of the btnIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnIcon_Click(object sender, EventArgs e)
        {
            if (ofdIcon.ShowDialog() != DialogResult.OK)
            {
                txbIcon.Text = String.Empty;
            }

            txbIcon.Text = ofdIcon.FileName;
        }

        /// <summary>
        /// Handles the Click event of the btnHelpFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnHelpFile_Click(object sender, EventArgs e)
        {
            if (ofdHelpFile.ShowDialog() != DialogResult.OK)
            {
                txbHelpFile.Text = String.Empty;
            }

            txbHelpFile.Text = ofdHelpFile.FileName;
        }

        /// <summary>
        /// Handles the HelpRequested event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="hlpevent">The <see cref="System.Windows.Forms.HelpEventArgs"/> instance containing the event data.</param>
        private void Form1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            InformationBox.Show("Help has been requested somewhere", "Help", InformationBoxButtons.OK, InformationBoxIcon.Question);
        }
    }
}