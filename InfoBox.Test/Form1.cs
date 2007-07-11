namespace InfoBox.Test
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Threading;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            return InformationBoxButtons.OK;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <returns></returns>
        private InformationBoxIcon GetIcon()
        {
            if (rdbAsterisk.Checked) return InformationBoxIcon.Asterisk;
            if (rdbError.Checked) return InformationBoxIcon.Error;
            if (rdbExclamation.Checked) return InformationBoxIcon.Exclamation;
            if (rdbHand.Checked) return InformationBoxIcon.Hand;
            if (rdbInformation.Checked) return InformationBoxIcon.Information;
            if (rdbNone.Checked) return InformationBoxIcon.None;
            if (rdbQuestion.Checked) return InformationBoxIcon.Question;
            if (rdbStop.Checked) return InformationBoxIcon.Stop;
            if (rdbWarning.Checked) return InformationBoxIcon.Warning;
            if (rdbSuccess.Checked) return InformationBoxIcon.Success;
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

            if (String.Empty.Equals(iconFileName))
            {
                txbCode.Text = String.Format(
                        "InformationBox.Show(\"{0}\", \"{1}\", InformationBoxButtons.{2}, new string[] {{ \"{3}\", \"{4}\" }}, InformationBoxIcon.{5}, InformationBoxDefaultButton.{6}, InformationBoxButtonsLayout.{7}, InformationBoxAutoSizeMode.{8}, InformationBoxPosition.{9}, {10});",
                        txbText.Text.Replace(Environment.NewLine, "\\n"), txbTitle.Text, buttons, txbUser1.Text,
                        txbUser2.Text, icon, defaultButton, buttonsLayout, autoSize, position, chbHelpButton.Checked).Replace("\"\"", "String.Empty");
            }
            else
            {
                txbCode.Text = String.Format(
                        "InformationBox.Show(\"{0}\", \"{1}\", InformationBoxButtons.{2}, new string[] {{ \"{3}\", \"{4}\" }}, new System.Drawing.Icon(@\"{5}\"), InformationBoxDefaultButton.{6}, InformationBoxButtonsLayout.{7}, InformationBoxAutoSizeMode.{8}, InformationBoxPosition.{9}, {10});",
                        txbText.Text.Replace(Environment.NewLine, "\\n"), txbTitle.Text, buttons, txbUser1.Text,
                        txbUser2.Text, iconFileName, defaultButton, buttonsLayout, autoSize, position, chbHelpButton.Checked).Replace("\"\"", "String.Empty");
            }
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

            InformationBoxButtons buttons = GetButtons();
            InformationBoxIcon icon = GetIcon();
            String iconFileName = txbIcon.Text;
            InformationBoxDefaultButton defaultButton = GetDefaultButton();
            InformationBoxButtonsLayout buttonsLayout = GetButtonsLayout();
            InformationBoxAutoSizeMode autoSize = GetAutoSize();
            InformationBoxPosition position = GetPosition();

            if (String.Empty.Equals(iconFileName))
            {
                InformationBox.Show(txbText.Text, txbTitle.Text, buttons, new string[] { txbUser1.Text, txbUser2.Text }, icon, defaultButton, buttonsLayout, autoSize, position, chbHelpButton.Checked);
            }
            else
            {
                InformationBox.Show(txbText.Text, txbTitle.Text, buttons, new string[] { txbUser1.Text, txbUser2.Text }, new Icon(iconFileName), defaultButton, buttonsLayout, autoSize, position, chbHelpButton.Checked);
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

        private void btnIcon_Click(object sender, EventArgs e)
        {
            if (ofdIcon.ShowDialog() != DialogResult.OK)
            {
                txbIcon.Text = String.Empty;
            }

            txbIcon.Text = ofdIcon.FileName;
        }

        private void Form1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            InformationBox.Show("Help has been requested somewhere", "Help", InformationBoxButtons.OK, InformationBoxIcon.Question);
        }

    }
}