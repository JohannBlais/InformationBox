using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace InfoBox.Designer
{
    /// <summary>
    /// Designer for the InformationBoxes.
    /// </summary>
    public partial class InformationBoxDesigner : Form
    {
        #region Attributes

        private Color _barsColor = Color.Empty;
        private Color _formColor = Color.Empty;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxDesigner"/> class.
        /// </summary>
        public InformationBoxDesigner()
        {
            InitializeComponent();

            LoadIcons();
            LoadButtons();
            LoadResults();
            LoadOpacities();

            LoadBindings();
        }

        #endregion Constructors

        #region Loading

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
        /// Loads the opacities.
        /// </summary>
        private void LoadOpacities()
        {
            foreach (InformationBoxOpacity op in Enum.GetValues(typeof(InformationBoxOpacity)))
            {
                ddlOpacities.Items.Add(op);
            }

            ddlOpacities.SelectedItem = InformationBoxOpacity.NoFade;
        }

        /// <summary>
        /// Loads the buttons.
        /// </summary>
        private void LoadButtons()
        {
            foreach (InformationBoxDefaultButton icon in Enum.GetValues(typeof(InformationBoxDefaultButton)))
            {
                ddlAutoCloseButton.Items.Add(icon);
            }
        }

        /// <summary>
        /// Loads the results.
        /// </summary>
        private void LoadResults()
        {
            foreach (InformationBoxResult icon in Enum.GetValues(typeof(InformationBoxResult)))
            {
                ddlAutoCloseResult.Items.Add(icon);
            }
        }

        /// <summary>
        /// Loads the bindings.
        /// </summary>
        private void LoadBindings()
        {
            rdbAutoCloseButton.DataBindings.Add("Enabled", chbActivateAutoClose, "Checked");
            rdbAutoCloseResult.DataBindings.Add("Enabled", chbActivateAutoClose, "Checked");
            nudAutoCloseSeconds.DataBindings.Add("Enabled", chbActivateAutoClose, "Checked");
            lblAutoCloseSeconds.DataBindings.Add("Enabled", chbActivateAutoClose, "Checked");

            lblAutoCloseButton.DataBindings.Add("Enabled", rdbAutoCloseButton, "Checked");
            ddlAutoCloseButton.DataBindings.Add("Enabled", rdbAutoCloseButton, "Checked");

            lblAutoCloseResult.DataBindings.Add("Enabled", rdbAutoCloseResult, "Checked");
            ddlAutoCloseResult.DataBindings.Add("Enabled", rdbAutoCloseResult, "Checked");

            lblColorsBars.DataBindings.Add("Enabled", chbCustomColors, "Checked");
            lblColorsForm.DataBindings.Add("Enabled", chbCustomColors, "Checked");
            txbColorsBars.DataBindings.Add("Enabled", chbCustomColors, "Checked");
            txbColorsForm.DataBindings.Add("Enabled", chbCustomColors, "Checked");
            btnColorsBars.DataBindings.Add("Enabled", chbCustomColors, "Checked");
            btnColorsForm.DataBindings.Add("Enabled", chbCustomColors, "Checked");

            lblTitleIcon.DataBindings.Add("Enabled", rdbTitleIconCustom, "Checked");
            txbTitleIconFile.DataBindings.Add("Enabled", rdbTitleIconCustom, "Checked");
            btnTitleIconFile.DataBindings.Add("Enabled", rdbTitleIconCustom, "Checked");
        }

        #endregion Loading

        #region Values

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
        /// Gets the opacity.
        /// </summary>
        /// <returns></returns>
        private InformationBoxOpacity GetOpacity()
        {
            if (null != ddlOpacities.SelectedItem)
                return (InformationBoxOpacity) ddlOpacities.SelectedItem;

            return InformationBoxOpacity.NoFade;
        }

        /// <summary>
        /// Gets the default button.
        /// </summary>
        /// <returns></returns>
        private InformationBoxDefaultButton GetDefaultButton()
        {
            if (rdbDefaultButton1.Checked) return InformationBoxDefaultButton.Button1;
            if (rdbDefaultButton2.Checked) return InformationBoxDefaultButton.Button2;
            if (rdbDefaultButton3.Checked) return InformationBoxDefaultButton.Button3;
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
        /// Gets the auto close.
        /// </summary>
        /// <returns></returns>
        private AutoCloseParameters GetAutoClose()
        {
            if (!chbActivateAutoClose.Checked)
                return null;

            if (nudAutoCloseSeconds.Value == 30 && (!rdbAutoCloseButton.Checked || ddlAutoCloseButton.SelectedIndex == -1)
                && (!rdbAutoCloseResult.Checked || ddlAutoCloseResult.SelectedIndex == -1))
            {
                return AutoCloseParameters.Default;
            }

            if (rdbAutoCloseButton.Checked && ddlAutoCloseButton.SelectedIndex != -1)
            {
                return new AutoCloseParameters(Convert.ToInt32(nudAutoCloseSeconds.Value),
                                               (InformationBoxDefaultButton) Enum.Parse(typeof(InformationBoxDefaultButton),
                                                                                        ddlAutoCloseButton.SelectedItem.ToString()));
            }

            if (rdbAutoCloseResult.Checked && ddlAutoCloseResult.SelectedIndex != -1)
            {
                return new AutoCloseParameters(Convert.ToInt32(nudAutoCloseSeconds.Value),
                                               (InformationBoxResult) Enum.Parse(typeof(InformationBoxResult),
                                                                                 ddlAutoCloseResult.SelectedItem.ToString()));
            }

            return new AutoCloseParameters(Convert.ToInt32(nudAutoCloseSeconds.Value));
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
        /// Gets the design.
        /// </summary>
        /// <returns></returns>
        private DesignParameters GetDesign()
        {
            if (!chbCustomColors.Checked)
                return null;

            return new DesignParameters(_formColor, _barsColor);
        }

        /// <summary>
        /// Gets the title style.
        /// </summary>
        /// <returns></returns>
        private InformationBoxTitleIconStyle GetTitleStyle()
        {
            if (rdbTitleIconNone.Checked) return InformationBoxTitleIconStyle.None;
            if (rdbTitleIconCustom.Checked) return InformationBoxTitleIconStyle.Custom;
            if (rdbTitleIconSameAsBox.Checked) return InformationBoxTitleIconStyle.SameAsBox;
            return InformationBoxTitleIconStyle.SameAsBox;
        }

        #endregion Values

        #region Code generation

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        private void GenerateCode(InformationBoxBehavior behavior)
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
            AutoCloseParameters autoClose = GetAutoClose();
            DesignParameters design = GetDesign();
            InformationBoxTitleIconStyle titleStyle = GetTitleStyle();
            InformationBoxOpacity opacity = GetOpacity();

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
                codeBuilder.AppendFormat("\"{0}\", ", txbTitle.Text);

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
                codeBuilder.AppendFormat("InformationBoxButtonsLayout.{0}, ", buttonsLayout);

            if (autoSize != InformationBoxAutoSizeMode.None)
                codeBuilder.AppendFormat("InformationBoxAutoSizeMode.{0}, ", autoSize);

            if (position != InformationBoxPosition.CenterOnParent)
                codeBuilder.AppendFormat("InformationBoxPosition.{0}, ", position);

            if (chbHelpButton.Checked)
                codeBuilder.Append("true, ");

            if (!String.Empty.Equals(txbHelpFile.Text))
                codeBuilder.AppendFormat("\"{0}\", ", txbHelpFile.Text);

            if (navigator != 0)
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
                codeBuilder.AppendFormat("InformationBoxStyle.{0}, ", style);

            if (chbActivateAutoClose.Checked)
            {
                if (autoClose.Seconds == AutoCloseParameters.Default.Seconds && autoClose.DefaultButton == InformationBoxDefaultButton.Button1 && autoClose.Result == InformationBoxResult.None)
                {
                    codeBuilder.Append("AutoCloseParameters.Default, ");
                }
                else
                {
                    if (rdbAutoCloseButton.Checked && ddlAutoCloseButton.SelectedIndex != -1)
                    {
                        codeBuilder.AppendFormat("new AutoCloseParameters({0}, InformationBoxDefaultButton.{1}), ", Convert.ToInt32(nudAutoCloseSeconds.Value), (InformationBoxDefaultButton)Enum.Parse(typeof(InformationBoxDefaultButton), ddlAutoCloseButton.SelectedItem.ToString()));
                    }
                    else if (rdbAutoCloseResult.Checked && ddlAutoCloseResult.SelectedIndex != -1)
                    {
                        codeBuilder.AppendFormat("new AutoCloseParameters({0}, InformationBoxResult.{1}), ", Convert.ToInt32(nudAutoCloseSeconds.Value), (InformationBoxResult)Enum.Parse(typeof(InformationBoxResult), ddlAutoCloseResult.SelectedItem.ToString()));
                    }
                    else
                    {
                        codeBuilder.AppendFormat("new AutoCloseParameters({0}), ", Convert.ToInt32(nudAutoCloseSeconds.Value));
                    }
                }
            }

            if (null != design)
            {
                codeBuilder.AppendFormat(
                    "new DesignParameters(Color.FromArgb({0},{1},{2}), Color.FromArgb({3},{4},{5})), ",
                    design.FormBackColor.R, design.FormBackColor.G, design.FormBackColor.B, design.BarsBackColor.R,
                    design.BarsBackColor.G, design.BarsBackColor.B);
            }

            if (titleStyle == InformationBoxTitleIconStyle.Custom)
            {
                codeBuilder.AppendFormat("new InformationBoxTitleIcon(@\"{0}\"), ", txbTitleIconFile.Text);
            }
            else if (titleStyle == InformationBoxTitleIconStyle.None)
            {
                codeBuilder.Append("InformationBoxTitleIconStyle.None, ");
            }

            if (behavior == InformationBoxBehavior.Modeless)
            {
                codeBuilder.Append("InformationBoxBehavior.Modeless, ");
            }
            
            if (opacity != InformationBoxOpacity.NoFade)
                codeBuilder.AppendFormat("InformationBoxOpacity.{0}, ", opacity);

            codeBuilder[codeBuilder.Length - 2] = ')';
            codeBuilder[codeBuilder.Length - 1] = ';';

            txbCode.Text = codeBuilder.ToString().Replace("\"\"", "String.Empty");
        }

        #endregion Code generation

        #region Display

        /// <summary>
        /// Call when a asynchronous InformationBox is closed.
        /// </summary>
        /// <param name="result">The result.</param>
        private static void boxClosed(InformationBoxResult result)
        {
            InformationBox.Show(String.Format("I am the result of a modeless box : " + result));
        }

        /// <summary>
        /// Shows the box.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        private void ShowBox(InformationBoxBehavior behavior)
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
            CheckState state = 0;
            InformationBoxStyle style = GetStyle();
            AutoCloseParameters autoClose = GetAutoClose();
            DesignParameters design = GetDesign();
            InformationBoxTitleIconStyle titleStyle = GetTitleStyle();
            InformationBoxOpacity opacity = GetOpacity();

            InformationBoxTitleIcon titleIcon = null;
            if (titleStyle == InformationBoxTitleIconStyle.Custom)
                titleIcon = new InformationBoxTitleIcon(txbTitleIconFile.Text);

            if (String.Empty.Equals(iconFileName))
                InformationBox.Show(txbText.Text, ref state, txbTitle.Text, buttons,
                                    new string[] {txbUser1.Text, txbUser2.Text}, icon, defaultButton, buttonsLayout,
                                    autoSize, position, chbHelpButton.Checked, txbHelpFile.Text, navigator,
                                    txbHelpTopic.Text, checkState, style, autoClose, design, titleStyle, titleIcon,
                                    behavior, new AsyncResultCallBack(boxClosed), opacity);
            else
                InformationBox.Show(txbText.Text, ref state, txbTitle.Text, buttons,
                                    new string[] {txbUser1.Text, txbUser2.Text}, new Icon(iconFileName),
                                    defaultButton, buttonsLayout, autoSize, position, chbHelpButton.Checked,
                                    txbHelpFile.Text, navigator, txbHelpTopic.Text, checkState, style, autoClose,
                                    design, titleStyle, titleIcon, behavior, new AsyncResultCallBack(boxClosed),
                                    opacity);

            if (checkState != 0)
                InformationBox.Show(String.Format("The state of the checkbox was {0}", state),
                                    InformationBoxIcon.Information);
        }

        #endregion Display

        #region Event handlers

        /// <summary>
        /// Handles the Click event of the btnShowModeless control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnShowModeless_Click(object sender, EventArgs e)
        {
            if (null != ddlLanguage.SelectedItem)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ddlLanguage.SelectedItem.ToString().Substring(0, 2));
            }

            GenerateCode(InformationBoxBehavior.Modeless);
            ShowBox(InformationBoxBehavior.Modeless);
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

            GenerateCode(InformationBoxBehavior.Modal);
            ShowBox(InformationBoxBehavior.Modal);
        }

        /// <summary>
        /// Handles the Click event of the btnGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateCode(InformationBoxBehavior.Modal);
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
        /// Handles the Click event of the btnTitleIconFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnTitleIconFile_Click(object sender, EventArgs e)
        {
            if (ofdIcon.ShowDialog() != DialogResult.OK)
            {
                txbTitleIconFile.Text = String.Empty;
            }

            txbTitleIconFile.Text = ofdIcon.FileName;
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

        #region Colors

        /// <summary>
        /// Handles the Click event of the btnColorsBars control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnColorsBars_Click(object sender, EventArgs e)
        {
            if (dlgColor.ShowDialog() != DialogResult.OK)
                return;

            Color selected = dlgColor.Color;

            txbColorsBars.Text = selected.ToString();
            _barsColor = selected;
        }

        /// <summary>
        /// Handles the Click event of the btnColorsForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnColorsForm_Click(object sender, EventArgs e)
        {
            if (dlgColor.ShowDialog() != DialogResult.OK)
                return;

            Color selected = dlgColor.Color;

            txbColorsForm.Text = selected.ToString();
            _formColor = selected;
        }

        #endregion Colors

        #endregion Event handlers
    }
}