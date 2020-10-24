// <copyright file="InformationBoxDesigner.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Designer window</summary>

namespace InfoBox.Designer
{
    using InfoBox.Designer.CodeGeneration;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Designer for the InformationBoxes.
    /// </summary>
    public partial class InformationBoxDesigner : Form
    {
        #region Attributes

        /// <summary>
        /// Color of the bars
        /// </summary>
        private Color barsColor = Color.Empty;

        /// <summary>
        /// Color of the form
        /// </summary>
        private Color formColor = Color.Empty;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxDesigner"/> class.
        /// </summary>
        public InformationBoxDesigner()
        {
            this.InitializeComponent();

            this.LoadIcons();
            this.LoadButtons();
            this.LoadResults();
            this.LoadOpacities();

            this.LoadBindings();
        }

        #endregion Constructors

        #region Display

        /// <summary>
        /// Call when a asynchronous InformationBox is closed.
        /// </summary>
        /// <param name="result">The result.</param>
        private static void BoxClosed(InformationBoxResult result)
        {
            InformationBox.Show(String.Format(CultureInfo.InvariantCulture, "I am the result of a modeless box : " + result));
        }

        /// <summary>
        /// Shows the box.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        private void ShowBox(InformationBoxBehavior behavior)
        {
            InformationBoxButtons buttons = this.GetButtons();
            InformationBoxIcon icon = this.GetIcon();
            string iconFileName = this.txbIcon.Text;
            InformationBoxDefaultButton defaultButton = this.GetDefaultButton();
            InformationBoxButtonsLayout buttonsLayout = this.GetButtonsLayout();
            InformationBoxAutoSizeMode autoSize = this.GetAutoSize();
            InformationBoxPosition position = this.GetPosition();
            HelpNavigator navigator = this.GetHelpNavigator();
            InformationBoxCheckBox checkState = this.GetCheckBoxState();
            CheckState state = 0;
            InformationBoxStyle style = this.GetStyle();
            AutoCloseParameters autoClose = this.GetAutoClose();
            DesignParameters design = this.GetDesign();
            InformationBoxTitleIconStyle titleStyle = this.GetTitleStyle();
            InformationBoxOpacity opacity = this.GetOpacity();
            InformationBoxOrder order = this.GetOrder();

            InformationBoxTitleIcon titleIcon = null;
            if (titleStyle == InformationBoxTitleIconStyle.Custom)
            {
                titleIcon = new InformationBoxTitleIcon(this.txbTitleIconFile.Text);
            }

            if (String.IsNullOrEmpty(iconFileName))
            {
                InformationBox.Show(this.txbText.Text, out state, this.txbTitle.Text, buttons, new string[] { this.txbUser1.Text, this.txbUser2.Text }, icon, defaultButton, buttonsLayout, autoSize, position, this.chbHelpButton.Checked, this.txbHelpFile.Text, navigator, this.txbHelpTopic.Text, checkState, style, autoClose, design, titleStyle, titleIcon, behavior, new AsyncResultCallback(BoxClosed), opacity, order);
            }
            else
            {
                InformationBox.Show(this.txbText.Text, out state, this.txbTitle.Text, buttons, new string[] { this.txbUser1.Text, this.txbUser2.Text }, new Icon(iconFileName), defaultButton, buttonsLayout, autoSize, position, this.chbHelpButton.Checked, this.txbHelpFile.Text, navigator, this.txbHelpTopic.Text, checkState, style, autoClose, design, titleStyle, titleIcon, behavior, new AsyncResultCallback(BoxClosed), opacity, order);
            }

            if (checkState != 0)
            {
                InformationBox.Show(
                    String.Format(CultureInfo.InvariantCulture, "The state of the checkbox was {0}", state),
                    InformationBoxIcon.Information);
            }
        }

        #endregion Display

        #region Loading

        /// <summary>
        /// Loads the icons.
        /// </summary>
        private void LoadIcons()
        {
            foreach (InformationBoxIcon icon in Enum.GetValues(typeof(InformationBoxIcon)))
            {
                this.ddlIcons.Items.Add(icon);
            }
        }

        /// <summary>
        /// Loads the opacities.
        /// </summary>
        private void LoadOpacities()
        {
            foreach (InformationBoxOpacity op in Enum.GetValues(typeof(InformationBoxOpacity)))
            {
                this.ddlOpacities.Items.Add(op);
            }

            this.ddlOpacities.SelectedItem = InformationBoxOpacity.NoFade;
        }

        /// <summary>
        /// Loads the buttons.
        /// </summary>
        private void LoadButtons()
        {
            foreach (InformationBoxDefaultButton icon in Enum.GetValues(typeof(InformationBoxDefaultButton)))
            {
                this.ddlAutoCloseButton.Items.Add(icon);
            }
        }

        /// <summary>
        /// Loads the results.
        /// </summary>
        private void LoadResults()
        {
            foreach (InformationBoxResult icon in Enum.GetValues(typeof(InformationBoxResult)))
            {
                this.ddlAutoCloseResult.Items.Add(icon);
            }
        }

        /// <summary>
        /// Loads the bindings.
        /// </summary>
        private void LoadBindings()
        {
            this.rdbAutoCloseButton.DataBindings.Add("Enabled", this.chbActivateAutoClose, "Checked");
            this.rdbAutoCloseResult.DataBindings.Add("Enabled", this.chbActivateAutoClose, "Checked");
            this.nudAutoCloseSeconds.DataBindings.Add("Enabled", this.chbActivateAutoClose, "Checked");
            this.lblAutoCloseSeconds.DataBindings.Add("Enabled", this.chbActivateAutoClose, "Checked");

            this.lblAutoCloseButton.DataBindings.Add("Enabled", this.rdbAutoCloseButton, "Checked");
            this.ddlAutoCloseButton.DataBindings.Add("Enabled", this.rdbAutoCloseButton, "Checked");

            this.lblAutoCloseResult.DataBindings.Add("Enabled", this.rdbAutoCloseResult, "Checked");
            this.ddlAutoCloseResult.DataBindings.Add("Enabled", this.rdbAutoCloseResult, "Checked");

            this.lblColorsBars.DataBindings.Add("Enabled", this.chbCustomColors, "Checked");
            this.lblColorsForm.DataBindings.Add("Enabled", this.chbCustomColors, "Checked");
            this.txbColorsBars.DataBindings.Add("Enabled", this.chbCustomColors, "Checked");
            this.txbColorsForm.DataBindings.Add("Enabled", this.chbCustomColors, "Checked");
            this.btnColorsBars.DataBindings.Add("Enabled", this.chbCustomColors, "Checked");
            this.btnColorsForm.DataBindings.Add("Enabled", this.chbCustomColors, "Checked");

            this.lblTitleIcon.DataBindings.Add("Enabled", this.rdbTitleIconCustom, "Checked");
            this.txbTitleIconFile.DataBindings.Add("Enabled", this.rdbTitleIconCustom, "Checked");
            this.btnTitleIconFile.DataBindings.Add("Enabled", this.rdbTitleIconCustom, "Checked");
        }

        #endregion Loading

        #region Values

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <returns>The value of the buttons</returns>
        private InformationBoxButtons GetButtons()
        {
            if (this.rdbAbortRetryIgnore.Checked)
            {
                return InformationBoxButtons.AbortRetryIgnore;
            }

            if (this.rdbOK.Checked)
            {
                return InformationBoxButtons.OK;
            }

            if (this.rdbOKCancel.Checked)
            {
                return InformationBoxButtons.OKCancel;
            }

            if (this.rdbRetryCancel.Checked)
            {
                return InformationBoxButtons.RetryCancel;
            }

            if (this.rdbYesNo.Checked)
            {
                return InformationBoxButtons.YesNo;
            }

            if (this.rdbYesNoCancel.Checked)
            {
                return InformationBoxButtons.YesNoCancel;
            }

            if (this.rdbYesNoUser1.Checked)
            {
                return InformationBoxButtons.YesNoUser1;
            }

            if (this.rdbOKCancelUser1.Checked)
            {
                return InformationBoxButtons.OKCancelUser1;
            }

            if (this.rdbUser1User2.Checked)
            {
                return InformationBoxButtons.User1User2;
            }

            if (this.rdbUser1.Checked)
            {
                return InformationBoxButtons.User1;
            }

            return InformationBoxButtons.OK;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <returns>The selected icon</returns>
        private InformationBoxIcon GetIcon()
        {
            if (null != this.ddlIcons.SelectedItem)
            {
                return (InformationBoxIcon)this.ddlIcons.SelectedItem;
            }

            return InformationBoxIcon.None;
        }

        /// <summary>
        /// Gets the opacity.
        /// </summary>
        /// <returns>The opacity</returns>
        private InformationBoxOpacity GetOpacity()
        {
            if (null != this.ddlOpacities.SelectedItem)
            {
                return (InformationBoxOpacity)this.ddlOpacities.SelectedItem;
            }

            return InformationBoxOpacity.NoFade;
        }

        /// <summary>
        /// Gets the default button.
        /// </summary>
        /// <returns>The default button</returns>
        private InformationBoxDefaultButton GetDefaultButton()
        {
            if (this.rdbDefaultButton1.Checked)
            {
                return InformationBoxDefaultButton.Button1;
            }

            if (this.rdbDefaultButton2.Checked)
            {
                return InformationBoxDefaultButton.Button2;
            }

            if (this.rdbDefaultButton3.Checked)
            {
                return InformationBoxDefaultButton.Button3;
            }

            return InformationBoxDefaultButton.Button1;
        }

        /// <summary>
        /// Gets the buttons layout.
        /// </summary>
        /// <returns>The buttons layout</returns>
        private InformationBoxButtonsLayout GetButtonsLayout()
        {
            if (this.rdbLayoutGroupLeft.Checked)
            {
                return InformationBoxButtonsLayout.GroupLeft;
            }

            if (this.rdbLayoutGroupRight.Checked)
            {
                return InformationBoxButtonsLayout.GroupRight;
            }

            if (this.rdbLayoutGroupMiddle.Checked)
            {
                return InformationBoxButtonsLayout.GroupMiddle;
            }

            if (this.rdbLayoutSeparate.Checked)
            {
                return InformationBoxButtonsLayout.Separate;
            }

            return InformationBoxButtonsLayout.GroupMiddle;
        }

        /// <summary>
        /// Gets the auto size mode.
        /// </summary>
        /// <returns>The auto size mode</returns>
        private InformationBoxAutoSizeMode GetAutoSize()
        {
            if (this.rdbAutoSizeMinimumHeight.Checked)
            {
                return InformationBoxAutoSizeMode.MinimumHeight;
            }

            if (this.rdbAutoSizeMinimumWidth.Checked)
            {
                return InformationBoxAutoSizeMode.MinimumWidth;
            }

            return InformationBoxAutoSizeMode.None;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns>The position</returns>
        private InformationBoxPosition GetPosition()
        {
            if (this.rdbPositionCenterOnParent.Checked)
            {
                return InformationBoxPosition.CenterOnParent;
            }

            if (this.rdbPositionCenterOnScreen.Checked)
            {
                return InformationBoxPosition.CenterOnScreen;
            }

            return InformationBoxPosition.CenterOnParent;
        }

        /// <summary>
        /// Gets the help navigator.
        /// </summary>
        /// <returns>The help navigator</returns>
        private HelpNavigator GetHelpNavigator()
        {
            if (this.rdbHelpFind.Checked)
            {
                return HelpNavigator.Find;
            }

            if (this.rdbHelpIndex.Checked)
            {
                return HelpNavigator.Index;
            }

            if (this.rdbHelpTopic.Checked)
            {
                return HelpNavigator.Topic;
            }

            if (this.rdbHelpTableOfContents.Checked)
            {
                return HelpNavigator.TableOfContents;
            }

            return 0;
        }

        /// <summary>
        /// Gets the z-order of the form.
        /// </summary>
        /// <returns>The z-order of the form</returns>
        private InformationBoxOrder GetOrder()
        {
            InformationBoxOrder order = InformationBoxOrder.Default;
            if (rdbOrderTopMost.Checked)
            {
                order = InformationBoxOrder.TopMost;
            }

            return order;
        }

        /// <summary>
        /// Gets the state of the check box.
        /// </summary>
        /// <returns>The state of the checkbox</returns>
        private InformationBoxCheckBox GetCheckBoxState()
        {
            InformationBoxCheckBox check = 0;
            if (this.clbCheckBox.GetItemCheckState(0) == CheckState.Checked)
            {
                check |= InformationBoxCheckBox.Show;
            }

            if (this.clbCheckBox.GetItemCheckState(1) == CheckState.Checked)
            {
                check |= InformationBoxCheckBox.Checked;
            }

            if (this.clbCheckBox.GetItemCheckState(2) == CheckState.Checked)
            {
                check |= InformationBoxCheckBox.RightAligned;
            }

            return check;
        }

        /// <summary>
        /// Gets the auto close.
        /// </summary>
        /// <returns>The auto close parameters</returns>
        private AutoCloseParameters GetAutoClose()
        {
            if (!this.chbActivateAutoClose.Checked)
            {
                return null;
            }

            if (this.nudAutoCloseSeconds.Value == 30 &&
                (!this.rdbAutoCloseButton.Checked || this.ddlAutoCloseButton.SelectedIndex == -1) &&
                (!this.rdbAutoCloseResult.Checked || this.ddlAutoCloseResult.SelectedIndex == -1))
            {
                return AutoCloseParameters.Default;
            }

            if (this.rdbAutoCloseButton.Checked && this.ddlAutoCloseButton.SelectedIndex != -1)
            {
                return new AutoCloseParameters(
                    Convert.ToInt32(this.nudAutoCloseSeconds.Value),
                    (InformationBoxDefaultButton)Enum.Parse(typeof(InformationBoxDefaultButton), this.ddlAutoCloseButton.SelectedItem.ToString()));
            }

            if (this.rdbAutoCloseResult.Checked && this.ddlAutoCloseResult.SelectedIndex != -1)
            {
                return new AutoCloseParameters(
                    Convert.ToInt32(this.nudAutoCloseSeconds.Value),
                    (InformationBoxResult)Enum.Parse(typeof(InformationBoxResult), this.ddlAutoCloseResult.SelectedItem.ToString()));
            }

            return new AutoCloseParameters(Convert.ToInt32(this.nudAutoCloseSeconds.Value));
        }

        /// <summary>
        /// Gets the style.
        /// </summary>
        /// <returns>The style.</returns>
        private InformationBoxStyle GetStyle()
        {
            if (this.rdbStyleModern.Checked)
            {
                return InformationBoxStyle.Modern;
            }

            return InformationBoxStyle.Standard;
        }

        /// <summary>
        /// Gets the design.
        /// </summary>
        /// <returns>The design.</returns>
        private DesignParameters GetDesign()
        {
            if (!this.chbCustomColors.Checked)
            {
                return null;
            }

            return new DesignParameters(this.formColor, this.barsColor);
        }

        /// <summary>
        /// Gets the title style.
        /// </summary>
        /// <returns>The style of the title</returns>
        private InformationBoxTitleIconStyle GetTitleStyle()
        {
            if (this.rdbTitleIconNone.Checked)
            {
                return InformationBoxTitleIconStyle.None;
            }

            if (this.rdbTitleIconCustom.Checked)
            {
                return InformationBoxTitleIconStyle.Custom;
            }

            if (this.rdbTitleIconSameAsBox.Checked)
            {
                return InformationBoxTitleIconStyle.SameAsBox;
            }

            return InformationBoxTitleIconStyle.SameAsBox;
        }

        #endregion Values

        #region Code generation

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        private void GenerateCode(InformationBoxBehavior behavior, Language language)
        {
            InformationBoxButtons buttons = this.GetButtons();
            InformationBoxIcon icon = this.GetIcon();
            string iconFileName = this.txbIcon.Text;
            InformationBoxDefaultButton defaultButton = this.GetDefaultButton();
            InformationBoxButtonsLayout buttonsLayout = this.GetButtonsLayout();
            InformationBoxAutoSizeMode autoSize = this.GetAutoSize();
            InformationBoxPosition position = this.GetPosition();
            HelpNavigator navigator = this.GetHelpNavigator();
            InformationBoxCheckBox checkState = this.GetCheckBoxState();
            InformationBoxStyle style = this.GetStyle();
            AutoCloseParameters autoClose = this.GetAutoClose();
            DesignParameters design = this.GetDesign();
            InformationBoxTitleIconStyle titleStyle = this.GetTitleStyle();
            InformationBoxOpacity opacity = this.GetOpacity();
            InformationBoxOrder order = this.GetOrder();

            ICodeGeneratorFactory factory = new CodeGeneratorFactory();
            ICodeGenerator codeGen = factory.CreateGenerator(language);

            String generatedCode = codeGen.GenerateSingleCall(
                    behavior, this.txbText.Text, this.txbTitle.Text, buttons, this.txbUser1.Text, this.txbUser2.Text,
                    icon, iconFileName, defaultButton, buttonsLayout, autoSize, position, this.chbHelpButton.Checked,
                    this.txbHelpFile.Text, this.txbHelpTopic.Text, navigator, checkState, style, this.chbActivateAutoClose.Checked,
                    autoClose, design, titleStyle, this.txbTitleIconFile.Text, opacity, order);

            this.txbCode.Text = generatedCode;
        }

        #endregion Code generation

        #region Event handlers

        /// <summary>
        /// Handles the Click event of the btnShowModeless control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnShowModeless_Click(object sender, EventArgs e)
        {
            if (null != this.ddlLanguage.SelectedItem)
            {
                string culture = this.ddlLanguage.SelectedItem.ToString();
                if (culture[2] == '-')
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(this.ddlLanguage.SelectedItem.ToString().Substring(0, 5));
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(this.ddlLanguage.SelectedItem.ToString().Substring(0, 2));
                }
            }

            this.ShowBox(InformationBoxBehavior.Modeless);
        }

        /// <summary>
        /// Handles the Click event of the btnShow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (null != this.ddlLanguage.SelectedItem)
            {
                string culture = this.ddlLanguage.SelectedItem.ToString();
                if (culture[2] == '-')
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(this.ddlLanguage.SelectedItem.ToString().Substring(0, 5));
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(this.ddlLanguage.SelectedItem.ToString().Substring(0, 2));
                }
            }

            this.ShowBox(InformationBoxBehavior.Modal);
        }

        /// <summary>
        /// Handles the Click event of the btnGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            cmsLanguage.Show(btnGenerate, new Point(0, btnGenerate.Height));
        }

        /// <summary>
        /// Handles the Click event of the tsmCSharp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tsmCSharp_Click(object sender, EventArgs e)
        {
            this.GenerateCode(InformationBoxBehavior.Modal, Language.CSharp);
        }

        /// <summary>
        /// Handles the Click event of the tsmVbNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tsmVbNet_Click(object sender, EventArgs e)
        {
            this.GenerateCode(InformationBoxBehavior.Modal, Language.VBNET);
        }

        /// <summary>
        /// Handles the Click event of the btnIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnIcon_Click(object sender, EventArgs e)
        {
            if (this.ofdIcon.ShowDialog() != DialogResult.OK)
            {
                this.txbIcon.Text = String.Empty;
            }

            this.txbIcon.Text = this.ofdIcon.FileName;
        }

        /// <summary>
        /// Handles the Click event of the btnTitleIconFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnTitleIconFile_Click(object sender, EventArgs e)
        {
            if (this.ofdIcon.ShowDialog() != DialogResult.OK)
            {
                this.txbTitleIconFile.Text = String.Empty;
            }

            this.txbTitleIconFile.Text = this.ofdIcon.FileName;
        }

        /// <summary>
        /// Handles the Click event of the btnHelpFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnHelpFile_Click(object sender, EventArgs e)
        {
            if (this.ofdHelpFile.ShowDialog() != DialogResult.OK)
            {
                this.txbHelpFile.Text = String.Empty;
            }

            this.txbHelpFile.Text = this.ofdHelpFile.FileName;
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

        #region Colors

        /// <summary>
        /// Handles the Click event of the btnColorsBars control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnColorsBars_Click(object sender, EventArgs e)
        {
            if (this.dlgColor.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Color selected = this.dlgColor.Color;

            this.txbColorsBars.Text = selected.ToString();
            this.barsColor = selected;
        }

        /// <summary>
        /// Handles the Click event of the btnColorsForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnColorsForm_Click(object sender, EventArgs e)
        {
            if (this.dlgColor.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Color selected = this.dlgColor.Color;

            this.txbColorsForm.Text = selected.ToString();
            this.formColor = selected;
        }

        #endregion Colors

        #endregion Event handlers
    }
}