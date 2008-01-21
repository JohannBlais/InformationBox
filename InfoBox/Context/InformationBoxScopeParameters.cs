using System;
using System.Drawing;
using System.Windows.Forms;

namespace InfoBox
{
    /// <summary>
    /// Contains the parameters used by a specific scope.
    /// </summary>
    public class InformationBoxScopeParameters
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
    }
}
