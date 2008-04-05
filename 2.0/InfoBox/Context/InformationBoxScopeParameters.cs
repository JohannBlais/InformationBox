using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace InfoBox
{
    /// <summary>
    /// Contains the parameters used by a specific scope.
    /// </summary>
    [DebuggerStepThrough]
    public struct InformationBoxScopeParameters
    {
        #region Attributes

        private AutoCloseParameters autoClose;
        private InformationBoxAutoSizeMode? autoSizeMode;
        private InformationBoxBehavior? behavior;
        private InformationBoxButtons? buttons;
        private InformationBoxButtonsLayout? layout;
        private InformationBoxCheckBox? checkbox;
        private InformationBoxDefaultButton? defaultButton;
        private InformationBoxIcon? icon;
        private Icon titleIcon;
        private Icon customIcon;
        private InformationBoxOpacity? opacity;
        private InformationBoxPosition? position;
        private InformationBoxStyle? style;
        private InformationBoxTitleIconStyle? titleIconStyle;
        private Boolean? help;
        private HelpNavigator? helpNavigator;
        private DesignParameters design;

        #endregion Attributes

        #region Properties

        /// <summary>
        /// Gets or sets the auto close.
        /// </summary>
        /// <value>The auto close.</value>
        public AutoCloseParameters AutoClose
        {
            get { return autoClose; }
            set { autoClose = value; }
        }

        /// <summary>
        /// Gets or sets the auto size mode.
        /// </summary>
        /// <value>The auto size mode.</value>
        public InformationBoxAutoSizeMode? AutoSizeMode
        {
            get { return autoSizeMode; }
            set { autoSizeMode = value; }
        }

        /// <summary>
        /// Gets or sets the behavior.
        /// </summary>
        /// <value>The behavior.</value>
        public InformationBoxBehavior? Behavior
        {
            get { return behavior; }
            set { behavior = value; }
        }

        /// <summary>
        /// Gets or sets the buttons.
        /// </summary>
        /// <value>The buttons.</value>
        public InformationBoxButtons? Buttons
        {
            get { return buttons; }
            set { buttons = value; }
        }

        /// <summary>
        /// Gets or sets the layout.
        /// </summary>
        /// <value>The layout.</value>
        public InformationBoxButtonsLayout? Layout
        {
            get { return layout; }
            set { layout = value; }
        }

        /// <summary>
        /// Gets or sets the checkbox.
        /// </summary>
        /// <value>The checkbox.</value>
        public InformationBoxCheckBox? Checkbox
        {
            get { return checkbox; }
            set { checkbox = value; }
        }

        /// <summary>
        /// Gets or sets the default button.
        /// </summary>
        /// <value>The default button.</value>
        public InformationBoxDefaultButton? DefaultButton
        {
            get { return defaultButton; }
            set { defaultButton = value; }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public InformationBoxIcon? Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        /// <summary>
        /// Gets or sets the title icon.
        /// </summary>
        /// <value>The icon.</value>
        public Icon TitleIcon
        {
            get { return titleIcon; }
            set { titleIcon = value; }
        }

        /// <summary>
        /// Gets or sets the custom icon.
        /// </summary>
        /// <value>The custom icon.</value>
        public Icon CustomIcon
        {
            get { return customIcon; }
            set { customIcon = value; }
        }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>The opacity.</value>
        public InformationBoxOpacity? Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public InformationBoxPosition? Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public InformationBoxStyle? Style
        {
            get { return style; }
            set { style = value; }
        }

        /// <summary>
        /// Gets or sets the title icon style.
        /// </summary>
        /// <value>The title icon style.</value>
        public InformationBoxTitleIconStyle? TitleIconStyle
        {
            get { return titleIconStyle; }
            set { titleIconStyle = value; }
        }

        /// <summary>
        /// Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        public bool? Help
        {
            get { return help; }
            set { help = value; }
        }

        /// <summary>
        /// Gets or sets the help navigator.
        /// </summary>
        /// <value>The help navigator.</value>
        public HelpNavigator? HelpNavigator
        {
            get { return helpNavigator; }
            set { helpNavigator = value; }
        }

        /// <summary>
        /// Gets or sets the design.
        /// </summary>
        /// <value>The design.</value>
        public DesignParameters Design
        {
            get { return design; }
            set { design = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Merges the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public InformationBoxScopeParameters Merge(InformationBoxScopeParameters parameters)
        {
            if (parameters.Icon.HasValue && !Icon.HasValue)
                icon = parameters.Icon.Value;

            if (parameters.CustomIcon != null && null == CustomIcon)
                customIcon = parameters.CustomIcon;

            if (parameters.Buttons.HasValue && !Buttons.HasValue)
                buttons = parameters.Buttons.Value;

            if (parameters.DefaultButton.HasValue && !DefaultButton.HasValue)
                defaultButton = parameters.DefaultButton.Value;

            if (parameters.Layout.HasValue && !Layout.HasValue)
                layout = parameters.Layout.Value;

            if (parameters.AutoSizeMode.HasValue && !AutoSizeMode.HasValue)
                autoSizeMode = parameters.AutoSizeMode.Value;

            if (parameters.Position.HasValue && !Position.HasValue)
                position = parameters.Position.Value;

            if (parameters.Checkbox.HasValue && !Checkbox.HasValue)
                checkbox = parameters.Checkbox.Value;

            if (parameters.Style.HasValue && !Style.HasValue)
                style = parameters.Style.Value;

            if (parameters.AutoClose != null && null == AutoClose)
                autoClose = parameters.AutoClose;

            if (parameters.Design != null && null == Design)
                design = parameters.Design;

            if (parameters.TitleIconStyle.HasValue && !TitleIconStyle.HasValue)
                titleIconStyle = parameters.TitleIconStyle.Value;

            if (parameters.TitleIcon != null && null == TitleIcon)
                titleIcon = parameters.TitleIcon;

            if (parameters.Behavior.HasValue && !Behavior.HasValue)
                behavior = parameters.Behavior.Value;

            if (parameters.Opacity.HasValue && !Opacity.HasValue)
                opacity = parameters.Opacity.Value;

            if (parameters.Help.HasValue && !Help.HasValue)
                help = parameters.Help.Value;

            if (parameters.HelpNavigator.HasValue && !HelpNavigator.HasValue)
                helpNavigator = parameters.HelpNavigator.Value;

            return this;
        }

        #endregion Methods
    }
}
