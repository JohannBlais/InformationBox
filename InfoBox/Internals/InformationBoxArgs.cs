// <copyright file="InformationBoxArgs.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Strongly-typed result of parsing the params object[] of InformationBoxForm</summary>

namespace InfoBox.Internals
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Strongly-typed aggregate of the values dispatched by
    /// <see cref="InformationBoxArgsParser.Parse(object[])"/> from a loosely-typed
    /// <c>params object[]</c>.
    /// </summary>
    /// <remarks>
    /// Reference-type properties are <see langword="null"/> when the corresponding
    /// parameter was not present. Value-type properties use nullable wrappers so the
    /// consumer can tell "value not supplied" from "value supplied with the default
    /// enum/bool". The form uses this distinction to keep its own default fields
    /// untouched when the parameter is absent.
    /// </remarks>
    internal sealed class InformationBoxArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether the active scope should be loaded.
        /// Set to <see langword="false"/> when <see cref="InformationBoxInitialization.FromParametersOnly"/>
        /// is present in the input. Defaults to <see langword="true"/>.
        /// </summary>
        public bool LoadScope { get; set; } = true;

        /// <summary>
        /// Gets or sets the dialog title (first positional string).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the help file path (second positional string).
        /// </summary>
        public string HelpFile { get; set; }

        /// <summary>
        /// Gets or sets the help topic id (third positional string).
        /// </summary>
        public string HelpTopic { get; set; }

        /// <summary>
        /// Gets or sets the replacement text for the "do not show again" checkbox
        /// (fourth positional string).
        /// </summary>
        public string DoNotShowAgainText { get; set; }

        /// <summary>
        /// Gets or sets the buttons preset.
        /// </summary>
        public InformationBoxButtons? Buttons { get; set; }

        /// <summary>
        /// Gets or sets the built-in icon preset.
        /// </summary>
        public InformationBoxIcon? Icon { get; set; }

        /// <summary>
        /// Gets or sets the user-supplied custom icon. Non-null implies the form's
        /// <c>iconType</c> should be set to <see cref="IconType.UserDefined"/>.
        /// </summary>
        public Icon CustomIcon { get; set; }

        /// <summary>
        /// Gets or sets the default button selection.
        /// </summary>
        public InformationBoxDefaultButton? DefaultButton { get; set; }

        /// <summary>
        /// Gets or sets the label of the first user-defined button.
        /// </summary>
        public string ButtonUser1Text { get; set; }

        /// <summary>
        /// Gets or sets the label of the second user-defined button.
        /// </summary>
        public string ButtonUser2Text { get; set; }

        /// <summary>
        /// Gets or sets the label of the third user-defined button.
        /// </summary>
        public string ButtonUser3Text { get; set; }

        /// <summary>
        /// Gets or sets the button layout.
        /// </summary>
        public InformationBoxButtonsLayout? ButtonsLayout { get; set; }

        /// <summary>
        /// Gets or sets the auto-size mode.
        /// </summary>
        public InformationBoxAutoSizeMode? AutoSizeMode { get; set; }

        /// <summary>
        /// Gets or sets the on-screen position.
        /// </summary>
        public InformationBoxPosition? Position { get; set; }

        /// <summary>
        /// Gets or sets whether the help button is shown.
        /// </summary>
        public bool? ShowHelpButton { get; set; }

        /// <summary>
        /// Gets or sets the help navigator.
        /// </summary>
        public HelpNavigator? HelpNavigator { get; set; }

        /// <summary>
        /// Gets or sets the "do not show again" checkbox state.
        /// </summary>
        public InformationBoxCheckBox? CheckBox { get; set; }

        /// <summary>
        /// Gets or sets the visual style.
        /// </summary>
        public InformationBoxStyle? Style { get; set; }

        /// <summary>
        /// Gets or sets the auto-close configuration.
        /// </summary>
        public AutoCloseParameters AutoClose { get; set; }

        /// <summary>
        /// Gets or sets the design (colors) configuration.
        /// </summary>
        public DesignParameters Design { get; set; }

        /// <summary>
        /// Gets or sets the font configuration.
        /// </summary>
        public FontParameters FontParameters { get; set; }

        /// <summary>
        /// Gets or sets the title icon style.
        /// </summary>
        public InformationBoxTitleIconStyle? TitleStyle { get; set; }

        /// <summary>
        /// Gets or sets the unwrapped title icon (extracted from
        /// <see cref="InformationBoxTitleIcon"/>).
        /// </summary>
        public Icon TitleIcon { get; set; }

        /// <summary>
        /// Gets or sets the modal/modeless behavior.
        /// </summary>
        public InformationBoxBehavior? Behavior { get; set; }

        /// <summary>
        /// Gets or sets the async result callback for modeless dialogs.
        /// </summary>
        public AsyncResultCallback Callback { get; set; }

        /// <summary>
        /// Gets or sets the opacity preset.
        /// </summary>
        public InformationBoxOpacity? Opacity { get; set; }

        /// <summary>
        /// Gets or sets the parent form.
        /// </summary>
        public Form Parent { get; set; }

        /// <summary>
        /// Gets or sets the z-order preset.
        /// </summary>
        public InformationBoxOrder? Order { get; set; }

        /// <summary>
        /// Gets or sets the sound preset.
        /// </summary>
        public InformationBoxSound? Sound { get; set; }
    }
}
