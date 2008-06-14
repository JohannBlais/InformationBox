// <copyright file="InformationBoxScopeParameters.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains the parameters used by a specific scope</summary>

namespace InfoBox
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Diagnostics;

    /// <summary>
    /// Contains the parameters used by a specific scope.
    /// </summary>
    [DebuggerStepThrough]
    public struct InformationBoxScopeParameters
    {
        #region Attributes

        /// <summary>
        /// Contains the autoclose parameters
        /// </summary>
        private AutoCloseParameters autoClose;

        /// <summary>
        /// Contains the autosize mode
        /// </summary>
        private InformationBoxAutoSizeMode? autoSizeMode;

        /// <summary>
        /// Contains the behavior
        /// </summary>
        private InformationBoxBehavior? behavior;

        /// <summary>
        /// Contains the buttons
        /// </summary>
        private InformationBoxButtons? buttons;

        /// <summary>
        /// Contains the layout
        /// </summary>
        private InformationBoxButtonsLayout? layout;

        /// <summary>
        /// Contains the check box for the "do not show again" feature
        /// </summary>
        private InformationBoxCheckBox? checkbox;

        /// <summary>
        /// Contains the default button
        /// </summary>
        private InformationBoxDefaultButton? defaultButton;
        
        /// <summary>
        /// Contains the main icon
        /// </summary>
        private InformationBoxIcon? icon;

        /// <summary>
        /// Contains the title icon
        /// </summary>
        private Icon titleIcon;

        /// <summary>
        /// Contains a custom icon
        /// </summary>
        private Icon customIcon;

        /// <summary>
        /// Contains the opacity
        /// </summary>
        private InformationBoxOpacity? opacity;

        /// <summary>
        /// Contains the position
        /// </summary>
        private InformationBoxPosition? position;

        /// <summary>
        /// Contains the global style of the box
        /// </summary>
        private InformationBoxStyle? style;

        /// <summary>
        /// Contains the style of the title icon
        /// </summary>
        private InformationBoxTitleIconStyle? titleIconStyle;

        /// <summary>
        /// Contains a value defining whether or not to show the help button
        /// </summary>
        private bool? help;

        /// <summary>
        /// Contains an optional help navigator
        /// </summary>
        private HelpNavigator? helpNavigator;

        /// <summary>
        /// Contains the design parameters
        /// </summary>
        private DesignParameters design;

        #endregion Attributes

        #region Properties

        /// <summary>
        /// Gets or sets the auto close.
        /// </summary>
        /// <value>The auto close.</value>
        public AutoCloseParameters AutoClose
        {
            get { return this.autoClose; }
            set { this.autoClose = value; }
        }

        /// <summary>
        /// Gets or sets the auto size mode.
        /// </summary>
        /// <value>The auto size mode.</value>
        public InformationBoxAutoSizeMode? AutoSizeMode
        {
            get { return this.autoSizeMode; }
            set { this.autoSizeMode = value; }
        }

        /// <summary>
        /// Gets or sets the behavior.
        /// </summary>
        /// <value>The behavior.</value>
        public InformationBoxBehavior? Behavior
        {
            get { return this.behavior; }
            set { this.behavior = value; }
        }

        /// <summary>
        /// Gets or sets the buttons.
        /// </summary>
        /// <value>The buttons.</value>
        public InformationBoxButtons? Buttons
        {
            get { return this.buttons; }
            set { this.buttons = value; }
        }

        /// <summary>
        /// Gets or sets the layout.
        /// </summary>
        /// <value>The layout.</value>
        public InformationBoxButtonsLayout? Layout
        {
            get { return this.layout; }
            set { this.layout = value; }
        }

        /// <summary>
        /// Gets or sets the checkbox.
        /// </summary>
        /// <value>The checkbox.</value>
        public InformationBoxCheckBox? Checkbox
        {
            get { return this.checkbox; }
            set { this.checkbox = value; }
        }

        /// <summary>
        /// Gets or sets the default button.
        /// </summary>
        /// <value>The default button.</value>
        public InformationBoxDefaultButton? DefaultButton
        {
            get { return this.defaultButton; }
            set { this.defaultButton = value; }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The main icon.</value>
        public InformationBoxIcon? Icon
        {
            get { return this.icon; }
            set { this.icon = value; }
        }

        /// <summary>
        /// Gets or sets the title icon.
        /// </summary>
        /// <value>The title icon.</value>
        public Icon TitleIcon
        {
            get { return this.titleIcon; }
            set { this.titleIcon = value; }
        }

        /// <summary>
        /// Gets or sets the custom icon.
        /// </summary>
        /// <value>The custom icon.</value>
        public Icon CustomIcon
        {
            get { return this.customIcon; }
            set { this.customIcon = value; }
        }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>The opacity.</value>
        public InformationBoxOpacity? Opacity
        {
            get { return this.opacity; }
            set { this.opacity = value; }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public InformationBoxPosition? Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public InformationBoxStyle? Style
        {
            get { return this.style; }
            set { this.style = value; }
        }

        /// <summary>
        /// Gets or sets the title icon style.
        /// </summary>
        /// <value>The title icon style.</value>
        public InformationBoxTitleIconStyle? TitleIconStyle
        {
            get { return this.titleIconStyle; }
            set { this.titleIconStyle = value; }
        }

        /// <summary>
        /// Gets or sets the help.
        /// </summary>
        /// <value>The help value.</value>
        public bool? Help
        {
            get { return this.help; }
            set { this.help = value; }
        }

        /// <summary>
        /// Gets or sets the help navigator.
        /// </summary>
        /// <value>The help navigator.</value>
        public HelpNavigator? HelpNavigator
        {
            get { return this.helpNavigator; }
            set { this.helpNavigator = value; }
        }

        /// <summary>
        /// Gets or sets the design.
        /// </summary>
        /// <value>The design.</value>
        public DesignParameters Design
        {
            get { return this.design; }
            set { this.design = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Merges the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result of the merge operation </returns>
        public InformationBoxScopeParameters Merge(InformationBoxScopeParameters parameters)
        {
            if (parameters.Icon.HasValue && !this.Icon.HasValue)
            {
                this.icon = parameters.Icon.Value;
            }

            if (parameters.CustomIcon != null && null == this.CustomIcon)
            {
                this.customIcon = parameters.CustomIcon;
            }

            if (parameters.Buttons.HasValue && !this.Buttons.HasValue)
            {
                this.buttons = parameters.Buttons.Value;
            }

            if (parameters.DefaultButton.HasValue && !this.DefaultButton.HasValue)
            {
                this.defaultButton = parameters.DefaultButton.Value;
            }

            if (parameters.Layout.HasValue && !this.Layout.HasValue)
            {
                this.layout = parameters.Layout.Value;
            }

            if (parameters.AutoSizeMode.HasValue && !this.AutoSizeMode.HasValue)
            {
                this.autoSizeMode = parameters.AutoSizeMode.Value;
            }

            if (parameters.Position.HasValue && !this.Position.HasValue)
            {
                this.position = parameters.Position.Value;
            }

            if (parameters.Checkbox.HasValue && !this.Checkbox.HasValue)
            {
                this.checkbox = parameters.Checkbox.Value;
            }

            if (parameters.Style.HasValue && !this.Style.HasValue)
            {
                this.style = parameters.Style.Value;
            }

            if (parameters.AutoClose != null && null == this.AutoClose)
            {
                this.autoClose = parameters.AutoClose;
            }

            if (parameters.Design != null && null == this.Design)
            {
                this.design = parameters.Design;
            }

            if (parameters.TitleIconStyle.HasValue && !this.TitleIconStyle.HasValue)
            {
                this.titleIconStyle = parameters.TitleIconStyle.Value;
            }

            if (parameters.TitleIcon != null && null == this.TitleIcon)
            {
                this.titleIcon = parameters.TitleIcon;
            }

            if (parameters.Behavior.HasValue && !this.Behavior.HasValue)
            {
                this.behavior = parameters.Behavior.Value;
            }

            if (parameters.Opacity.HasValue && !this.Opacity.HasValue)
            {
                this.opacity = parameters.Opacity.Value;
            }

            if (parameters.Help.HasValue && !this.Help.HasValue)
            {
                this.help = parameters.Help.Value;
            }

            if (parameters.HelpNavigator.HasValue && !this.HelpNavigator.HasValue)
            {
                this.helpNavigator = parameters.HelpNavigator.Value;
            }

            return this;
        }

        #endregion Methods
    }
}
