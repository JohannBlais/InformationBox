// <copyright file="InformationBoxModel.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Pure data model for InformationBox configuration</summary>

namespace InfoBox.Presentation
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Pure data model containing all configuration for an InformationBox.
    /// </summary>
    /// <remarks>
    /// This model separates data from business logic and UI,
    /// enabling testability without WinForms dependencies.
    /// See TESTABILITY_ROADMAP.md - P0.1
    /// </remarks>
    public class InformationBoxModel
    {
        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the font for message text.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// Gets or sets the buttons to display.
        /// </summary>
        public InformationBoxButtons Buttons { get; set; }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public InformationBoxIcon Icon { get; set; }

        /// <summary>
        /// Gets or sets the custom icon.
        /// </summary>
        public Icon CustomIcon { get; set; }

        /// <summary>
        /// Gets or sets the default button.
        /// </summary>
        public InformationBoxDefaultButton DefaultButton { get; set; }

        /// <summary>
        /// Gets or sets the buttons layout.
        /// </summary>
        public InformationBoxButtonsLayout ButtonsLayout { get; set; }

        /// <summary>
        /// Gets or sets the auto-size mode.
        /// </summary>
        public InformationBoxAutoSizeMode AutoSizeMode { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public InformationBoxPosition Position { get; set; }

        /// <summary>
        /// Gets or sets the checkbox configuration.
        /// </summary>
        public InformationBoxCheckBox CheckBox { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public InformationBoxStyle Style { get; set; }

        /// <summary>
        /// Gets or sets the auto-close parameters.
        /// </summary>
        public AutoCloseParameters AutoClose { get; set; }

        /// <summary>
        /// Gets or sets the design parameters.
        /// </summary>
        public DesignParameters Design { get; set; }

        /// <summary>
        /// Gets or sets the font parameters.
        /// </summary>
        public FontParameters FontParameters { get; set; }

        /// <summary>
        /// Gets or sets the title icon style.
        /// </summary>
        public InformationBoxTitleIconStyle TitleStyle { get; set; }

        /// <summary>
        /// Gets or sets the title icon.
        /// </summary>
        public Icon TitleIcon { get; set; }

        /// <summary>
        /// Gets or sets whether to show the help button.
        /// </summary>
        public bool ShowHelpButton { get; set; }

        /// <summary>
        /// Gets or sets the help navigator.
        /// </summary>
        public HelpNavigator HelpNavigator { get; set; }

        /// <summary>
        /// Gets or sets the custom button texts.
        /// </summary>
        public string[] CustomButtons { get; set; }

        /// <summary>
        /// Gets or sets the working area (screen bounds).
        /// </summary>
        public Rectangle WorkingArea { get; set; }

        /// <summary>
        /// Gets or sets the icon panel width.
        /// </summary>
        public int IconPanelWidth { get; set; }

        /// <summary>
        /// Gets or sets the border padding.
        /// </summary>
        public int BorderPadding { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxModel"/> class.
        /// </summary>
        public InformationBoxModel()
        {
            // Set defaults
            this.Buttons = InformationBoxButtons.OK;
            this.Icon = InformationBoxIcon.None;
            this.DefaultButton = InformationBoxDefaultButton.Button1;
            this.ButtonsLayout = InformationBoxButtonsLayout.GroupMiddle;
            this.AutoSizeMode = InformationBoxAutoSizeMode.None;
            this.Position = InformationBoxPosition.CenterOnParent;
            this.Style = InformationBoxStyle.Standard;
            this.TitleStyle = InformationBoxTitleIconStyle.None;
            this.HelpNavigator = HelpNavigator.TableOfContents;
            this.IconPanelWidth = 68;
            this.BorderPadding = 20;
            this.WorkingArea = new Rectangle(0, 0, 1920, 1080);
        }
    }
}
